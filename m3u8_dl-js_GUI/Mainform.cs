using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
/// <summary>
/// 2017年6月9日
///     1.开始项目。
/// 2017年6月10日 0.1 - 0.1.3
///     1.初步实现基本功能。
///     2.修复无法拖入文件的bug。
///     3.增加转换为其他格式。
///     4.修复命令写入bug。
///     5.修复线程数最高100的bug。
/// 2017年6月12日 0.1.4
///     1.退出时杀死进程。
///     2.判断用户输入信息是否有效。
///     3.调整逻辑。
///     4.更友好的提示。
///     5.日志更加清晰。
/// 2017年6月13日 0.1.5 - 0.1.6
///     1.m3u8_dl-js常规更新（修复headers的bug）。
///     2.可接受命令行参数。
///     3.修复获取运行目录bug。
/// 2017年6月14日 0.1.7
///     1.添加Curl下载命令。
/// 2017年6月18日 0.1.7
///     1.添加--remove-part-files命令，重试前移除分片文件。
/// 2017年7月8日 0.1.7
///     1.修复%不能正确传入的bug。
/// 2017年8月6日 0.1.7
///     1.界面更改。
///     2.合并方式统一。
///     3.新的合并逻辑。
/// 2017年8月21日 0.1.7
///     1.删除逻辑后置。
/// 2017年8月21日 0.1.8
///     1.延迟执行命令更换。
///     2.队列下载添加提示框（暂未开发）。
/// 2017年9月1日 0.1.8
///     1.解决下载到根目录下出错的问题。
/// 2017年9月7日 0.1.9
///     1.解决误删文件的错误。
///     2.命令括号匹配。
/// 2017年10月14日 0.2.0
///     1.下载按钮只能点击一次。
///     2.首次启动时、双击地址栏时从剪贴板读取有效的url。
///     3.删除分片移至进程结束时判断。
///     4.设置页可打开下载文件夹。
/// 2018年2月27日 0.2.1
///     1.修复一些显著的Bug。
///     2.改用taskkill来结束进程，将不会影响多开软件。
///     3.修改m3u8_dl-js源码中的Curl部分，更好的支持https协议的m3u8下载。
///     4.改进逻辑。
/// 2018年3月16日 0.2.2
///     1.更新curl-7.59.0（下载更稳定）。
///     2.修复bug（结束进程存在逻辑漏洞）。
///     3.默认使用curl。
/// 2018年3月19日 0.2.3
///     1.修复Bug。
/// 2018年3月19日 0.2.4
///     1.增加显示分片个数和时长。
/// 2018年3月20日 0.2.5
///     1.修正配置读取问题。
///     2.修复一些bug。
///     3.新增唤醒idm进行后续下载功能。
///     4.调整ui。
///     5.增加按钮的停留提示。
/// 2018年3月20日 0.2.6
///     1.加入64位curl-7.59.0。
///     2.更新ffmpeg3.4.2。
/// 2018年4月4日 0.2.7
///     1.每次生成不同的批处理以应对多任务（其实没必要）。
///     2.node.exe更新至v8.11.1。
///     3.正式区分x86与x64平台，提高性能。
/// 2018年5月5日 0.3 beta
///     1.使用ffmpeg的concat进行合并，节省一半空间。
/// 2018年5月6日 0.3.0
///     1.修复过多分片无法合并的bug（利用ffmerge.exe实现）。
/// </summary>
namespace M3U8_DL_GUI
{
    public partial class Form1 : Form
    {
        //拖动窗口
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;


        int PID_1 = 0;  //主进程
        int PID_2 = 0;  //下载速度进程
        string downpath = "";  //用于删除文件夹

        //不影响点击任务栏图标最大最小化
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;  // Winuser.h中定义
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;   // 允许最小化操作
                return cp;
            }
        }

        //生成一个时间戳
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        string[] args = null;

        public Form1()
        {
            InitializeComponent();
            Init();  //  RealAction.cs
            Control.CheckForIllegalCrossThreadCalls = false;  //禁止编译器对跨线程访问做检查
        }
        public Form1(string[] args)
        {
            InitializeComponent();
            Init();  //  RealAction.cs
            Control.CheckForIllegalCrossThreadCalls = false;  //禁止编译器对跨线程访问做检查
            this.args = args;
        }

        /// <summary>
        /// 使用taskkill简单的杀死进程树
        /// </summary>
        /// <param name="pid"></param>
        private static void EndProcessTree(int pid)
        {
            if (pid == 0)
                return;

            Process.Start(new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = $"/PID {pid} /f /t",
                CreateNoWindow = true,
                UseShellExecute = false
            }).WaitForExit();
        }

        /// <summary>
        /// 传入参数：父进程id
        /// 功能：根据父进程id，杀死与之相关的进程树
        /// </summary>
        /// <param name="pid"></param>
        public static void KillProcessAndChildren(int pid)
        {
            if (pid == 0)
                return;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                /* process already exited */
            }
        }

        /// <summary>
        /// 用于保存设置项
        /// </summary>
        private void SaveSettings()
        {
            string ProxyType = "";
            string MergeType = "";
            bool auto_remove = checkBox_auto_remove.Checked;  //自动删除
            bool auto_merge = checkBox_auto_merge.Checked;  //自动合并
            bool del_after_merge = checkBox_del_afterMerge.Checked;  //合并后删除
            bool add_base_url = checkBox_add_base_url.Checked;  //url前缀
            bool Headers = checkBox_Headers.Checked;  //请求头
            bool proxy = checkBox_proxy.Checked;  //代理
            bool curl = checkBox_down_with_curl.Checked;  //使用curl下载
            bool newMerge = checkBox_IsNewMerge.Checked;  //使用新的合并方式
            if (radioButton_http.Checked == true) { ProxyType = "HTTP"; }
            if (radioButton_socks5.Checked == true) { ProxyType = "SOCKS5"; }
            if (radioButton_mergeBinary.Checked == true) { MergeType = "Binary"; }
            if (radioButton_mergeFFmpeg.Checked == true) { MergeType = "FFmpeg"; }

            XmlTextWriter xml = new XmlTextWriter(System.Windows.Forms.Application.StartupPath + "\\m3u8_dl_GUI_Settings.xml", Encoding.UTF8);
            xml.Formatting = Formatting.Indented;
            xml.WriteStartDocument();
            xml.WriteStartElement("Settings");

            xml.WriteStartElement("ConvertFormat"); xml.WriteCData(comboBox_convertFormat.SelectedIndex.ToString()); xml.WriteEndElement();
            xml.WriteStartElement("DownPath"); xml.WriteCData(label_outPutPath.Text); xml.WriteEndElement();
            xml.WriteStartElement("MergeType"); xml.WriteCData(MergeType); xml.WriteEndElement();
            xml.WriteStartElement("ProxyType"); xml.WriteCData(ProxyType); xml.WriteEndElement();
            xml.WriteStartElement("ProxyHost"); xml.WriteCData(textBox_proxy.Text); xml.WriteEndElement();
            xml.WriteStartElement("Headers"); xml.WriteCData(textBox_Headers.Text); xml.WriteEndElement();
            xml.WriteStartElement("BaseUrl"); xml.WriteCData(textBox_Base_url.Text); xml.WriteEndElement();
            xml.WriteStartElement("RetryTimes"); xml.WriteCData(numericUpDown_retry.Text); xml.WriteEndElement();
            xml.WriteStartElement("Sleep"); xml.WriteCData(numericUpDown_sleep.Text); xml.WriteEndElement();
            xml.WriteStartElement("Threads"); xml.WriteCData(numericUpDown_threads.Text); xml.WriteEndElement();
            if (auto_remove == true) { xml.WriteStartElement("AutoRemove"); xml.WriteCData("1"); xml.WriteEndElement(); } else { xml.WriteStartElement("AutoRemove"); xml.WriteCData("0"); xml.WriteEndElement(); }
            if (auto_merge == true) { xml.WriteStartElement("AutoMerge"); xml.WriteCData("1"); xml.WriteEndElement(); } else { xml.WriteStartElement("AutoMerge"); xml.WriteCData("0"); xml.WriteEndElement(); }
            if (del_after_merge == true) { xml.WriteStartElement("RemoveAfterMerge"); xml.WriteCData("1"); xml.WriteEndElement(); } else { xml.WriteStartElement("RemoveAfterMerge"); xml.WriteCData("0"); xml.WriteEndElement(); }
            if (add_base_url == true) { xml.WriteStartElement("AddBaseUrl"); xml.WriteCData("1"); xml.WriteEndElement(); } else { xml.WriteStartElement("AddBaseUrl"); xml.WriteCData("0"); xml.WriteEndElement(); }
            if (Headers == true) { xml.WriteStartElement("Header"); xml.WriteCData("1"); xml.WriteEndElement(); } else { xml.WriteStartElement("Header"); xml.WriteCData("0"); xml.WriteEndElement(); }
            if (proxy == true) { xml.WriteStartElement("Proxy"); xml.WriteCData("1"); xml.WriteEndElement(); } else { xml.WriteStartElement("Proxy"); xml.WriteCData("0"); xml.WriteEndElement(); }
            if (curl == true) { xml.WriteStartElement("Curl"); xml.WriteCData("1"); xml.WriteEndElement(); } else { xml.WriteStartElement("Curl"); xml.WriteCData("0"); xml.WriteEndElement(); }
            if (newMerge == true) { xml.WriteStartElement("newMerge"); xml.WriteCData("1"); xml.WriteEndElement(); } else { xml.WriteStartElement("newMerge"); xml.WriteCData("0"); xml.WriteEndElement(); }

            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();
        }

        /// <summary>
        /// 用于读取设置项
        /// </summary>
        private void LoadSettings()
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\m3u8_dl_GUI_Settings.xml"))  //判断程序目录有无配置文件，并读取文件
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(System.Windows.Forms.Application.StartupPath + "\\m3u8_dl_GUI_Settings.xml");    //加载Xml文件  
                XmlNodeList topM = doc.SelectNodes("Settings");
                try
                {
                    foreach (XmlElement element in topM)
                    {
                        label_outPutPath.Text = element.GetElementsByTagName("DownPath")[0].InnerText;
                        textBox_proxy.Text = element.GetElementsByTagName("ProxyHost")[0].InnerText;
                        textBox_Headers.Text = element.GetElementsByTagName("Headers")[0].InnerText;
                        textBox_Base_url.Text = element.GetElementsByTagName("BaseUrl")[0].InnerText;
                        numericUpDown_retry.Text = element.GetElementsByTagName("RetryTimes")[0].InnerText;
                        numericUpDown_sleep.Text = element.GetElementsByTagName("Sleep")[0].InnerText;
                        numericUpDown_threads.Text = element.GetElementsByTagName("Threads")[0].InnerText;
                        comboBox_convertFormat.SelectedIndex= Convert.ToInt32(element.GetElementsByTagName("ConvertFormat")[0].InnerText);

                        if (element.GetElementsByTagName("AutoRemove")[0].InnerText == "1") { checkBox_auto_remove.Checked = true; } else { checkBox_auto_remove.Checked = false; }
                        if (element.GetElementsByTagName("RemoveAfterMerge")[0].InnerText == "1") { checkBox_del_afterMerge.Checked = true; } else { checkBox_del_afterMerge.Checked = false; }
                        if (element.GetElementsByTagName("AddBaseUrl")[0].InnerText == "1") { checkBox_add_base_url.Checked = true; } else { checkBox_add_base_url.Checked = false; }
                        if (element.GetElementsByTagName("Header")[0].InnerText == "1") { checkBox_Headers.Checked = true; } else { checkBox_Headers.Checked = false; }
                        if (element.GetElementsByTagName("Curl")[0].InnerText == "1") { checkBox_down_with_curl.Checked = true; } else { checkBox_down_with_curl.Checked = false; }
                        try
                        {
                            if (element.GetElementsByTagName("newMerge")[0].InnerText == "1") { checkBox_IsNewMerge.Checked = true; } else { checkBox_IsNewMerge.Checked = false; }
                        }
                        catch (Exception)
                        {

                        }

                        if (element.GetElementsByTagName("Proxy")[0].InnerText == "1")
                        {
                            checkBox_proxy.Checked = true;
                            if (element.GetElementsByTagName("ProxyType")[0].InnerText == "HTTP") { radioButton_http.Checked = true; }
                            if (element.GetElementsByTagName("ProxyType")[0].InnerText == "SOCKS5") { radioButton_socks5.Checked = true; }
                        }
                        if (element.GetElementsByTagName("AutoMerge")[0].InnerText == "1")
                        {
                            checkBox_auto_merge.Checked = true;
                            if (element.GetElementsByTagName("MergeType")[0].InnerText == "Binary") { radioButton_mergeBinary.Checked = true; }
                            if (element.GetElementsByTagName("MergeType")[0].InnerText == "FFmpeg") { radioButton_mergeFFmpeg.Checked = true; }
                        }
                    }
                }
                catch(Exception)
                {

                }
            }
            else  //若无配置文件，获取当前程序运行路径，即为默认下载路径
            {
                string lujing = System.Windows.Forms.Application.StartupPath;
                label_outPutPath.Text = lujing;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveSettings();  //保存配置

            if (PID_1 != 0 && PID_2 != 0)
            {
                EndProcessTree(PID_2);
                EndProcessTree(PID_1);
            }

            Dispose();
            Application.Exit();
        }

        private void label_Title_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Convert_Click(object sender, EventArgs e)
        {

        }

        private void filelist_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void filelist_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void filelist_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox_convertFormat.SelectedIndex = 0;
            LoadSettings();  //读取设置

            //从剪切板读取url
            Regex url = new Regex(@"[a-zA-z]+://[^\s]*", RegexOptions.Compiled | RegexOptions.Singleline);//取已下载大小
            string str = url.Match(Clipboard.GetText()).Value;
            textBox_Adress.Text = str;

            //接收命令行参数
            if (args != null)
            {
                if (args.Length == 1)
                {
                    textBox_Adress.Text = args[0];
                }
                else if (args.Length == 2 && args[1] != "--auto-down")
                {
                    textBox_Adress.Text = args[0];
                    textBox_filename.Text = args[1];
                }
                else if (args.Length == 2 && args[1] == "--auto-down")
                {
                    textBox_Adress.Text = args[0];
                }
                else if (args.Length == 3 && args[1] == "--header")
                {
                    textBox_Adress.Text = args[0];
                    checkBox_Headers.Checked = true;
                    textBox_Headers.Text = args[2];
                }
                else if (args.Length == 3 && args[2] == "--auto-down")
                {
                    textBox_Adress.Text = args[0];
                    textBox_filename.Text = args[1];
                }
                else if ((args.Length == 4 || args.Length == 5) && args[1] == "--header")
                {
                    textBox_Adress.Text = args[0];
                    checkBox_Headers.Checked = true;
                    textBox_Headers.Text = args[2];
                }
                else if ((args.Length == 4 || args.Length == 5 || args.Length == 6) && args[2] == "--header")
                {
                    textBox_Adress.Text = args[0];
                    textBox_filename.Text = args[1];
                    checkBox_Headers.Checked = true;
                    textBox_Headers.Text = args[3];
                }
                else
                {
                    MessageBox.Show("现在支持的参数如下：\r\n\r\nm3u8_dl_GUI.exe [url] [name] [OPTIONS]"
                        + "\r\n\r\n　　　　--header [HEADER]  用'|'隔开可添加多个值"
                        + "\r\n　　　　--auto-down  自动开始下载", "未能读取命令行参数！");
                    Dispose();
                    Application.Exit();
                }

                if (args[args.Length - 1] == "--auto-down")
                {
                    button_Download.PerformClick();  //点击下载按钮
                }
            }

            //实例化ToolTip控件
            ToolTip tooltip1 = new ToolTip();
            //设置提示框显示时间，默认5000，最大为32767，超过此数，将以默认5000显示           
            tooltip1.AutoPopDelay = 10000;
            //是否以球状显示提示框            
            tooltip1.IsBalloon = false;
            //设置鼠标需要停留的时间
            tooltip1.InitialDelay = 800;
            //设置要显示提示框的控件 button1按钮
            tooltip1.SetToolTip(button_idmWake, "请用前先在IDM的选项-常规选项卡中\r\n点击添加浏览器按钮，将本exe加入。\r\n\r\n若可被idm捕获，通常会在程序左下角显示浮动框。");
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void 删除全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "图片文件|*.jpg;*.png";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_Adress.Text = fileDialog.FileName;
            }
        }
        
        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                //获取拖拽的文件地址
                var filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
                var hz = filenames[0].LastIndexOf('.') + 1;
                var houzhui = filenames[0].Substring(hz);//文件后缀名
                if (houzhui == "m3u8" || houzhui == "M3U8") //只允许拖入部分文件
                {
                    e.Effect = DragDropEffects.All;
                    string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                    textBox_Adress.Text = path; //将获取到的完整路径赋值到textBox1
                }

            }
        }

        private void textBox_Adress_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void textBox_Adress_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_auto_remove.CheckState == CheckState.Checked)
            {
                checkBox_auto_remove.ForeColor = Color.FromArgb(46, 204, 113);
            }
            if (checkBox_auto_remove.CheckState == CheckState.Unchecked)
            {
                checkBox_auto_remove.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void linkLabel_ChangeOutPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                label_outPutPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void linkLabel_OpenOutPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(label_outPutPath.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/sceext2/m3u8_dl-js");
        }

        private void checkBox_Headers_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Headers.CheckState == CheckState.Checked)
            {
                checkBox_Headers.ForeColor = Color.FromArgb(46, 204, 113);
            }
            if (checkBox_Headers.CheckState == CheckState.Unchecked)
            {
                checkBox_Headers.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void checkBox_auto_merge_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_auto_merge.CheckState == CheckState.Checked)
            {
                checkBox_auto_merge.ForeColor = Color.FromArgb(46, 204, 113);
            }
            if (checkBox_auto_merge.CheckState == CheckState.Unchecked)
            {
                checkBox_auto_merge.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void checkBox_add_base_url_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_add_base_url.CheckState == CheckState.Checked)
            {
                checkBox_add_base_url.ForeColor = Color.FromArgb(46, 204, 113);
            }
            if (checkBox_add_base_url.CheckState == CheckState.Unchecked)
            {
                checkBox_add_base_url.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void checkBox_proxy_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_proxy.CheckState == CheckState.Checked)
            {
                checkBox_proxy.ForeColor = Color.FromArgb(46, 204, 113);
            }
            if (checkBox_proxy.CheckState == CheckState.Unchecked)
            {
                checkBox_proxy.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void label8_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void button_Download_Click(object sender, EventArgs e)
        {
            if (textBox_Adress.Text == "")
            {
                MessageBox.Show("请输入地址！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox_filename.Text == "")
            {
                MessageBox.Show("请输入文件名！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            button_Download.Enabled = false;  //禁用下载按钮
            label_dur_clip_out.Text = "m3u8信息：暂未获取";
            downpath = label_outPutPath.Text + "\\" + textBox_filename.Text;

            if (Directory.Exists(label_outPutPath.Text + "\\" + textBox_filename.Text)
                && (Directory.GetDirectories(label_outPutPath.Text + "\\" + textBox_filename.Text).Length > 0 || Directory.GetFiles(label_outPutPath.Text + "\\" + textBox_filename.Text).Length > 0))  //若存在非空文件夹
            {
                if (MessageBox.Show("注意：发现了以相同文件名命名的非空文件夹，请更改文件名后重试。\r\n但若您想继续之前的下载，请点击是。", "请确认您的操作", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!Directory.Exists(label_outPutPath.Text + "\\" + textBox_filename.Text))//若文件夹不存在则新建文件夹   
                    {
                        Directory.CreateDirectory(label_outPutPath.Text + "\\" + textBox_filename.Text); //新建文件夹   
                    }
                    if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Logs"))//若文件夹不存在则新建文件夹   
                    {
                        Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Logs"); //新建文件夹   
                    }
                    if (System.IO.File.Exists(Path.GetFullPath(label_outPutPath.Text + "\\" + textBox_filename.Text + "\\m3u8_dl.lock")))  //存在则删除
                    {
                        File.Delete(Path.GetFullPath(label_outPutPath.Text + "\\" + textBox_filename.Text + "\\m3u8_dl.lock"));
                    }
                    textBox_writeInfo.Text = "";
                    textBox_writeInfo.Text = GetCommand();
                    string batPath = Path.GetTempPath() + "\\m3u8_dl_GUI_Download_" + GetTimeStamp() + ".bat";
                    StreamWriter writer = new StreamWriter(batPath, false, Encoding.Default);  //false代表替换而不是追加
                    writer.WriteLine("@echo off");
                    writer.Write(textBox_writeInfo.Text);
                    writer.Close();

                    string log_name = System.Windows.Forms.Application.StartupPath + "\\Logs\\Log-" + System.DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss") + ".txt";  //日志文件位置
                    string logger_path = System.Windows.Forms.Application.StartupPath + "\\Tools\\logger.exe";  //logger路径
                    string loggerPath = batPath.Replace("m3u8_dl_GUI_Download", "m3u8_dl_GUI_Logger");
                    StreamWriter logger = new StreamWriter(loggerPath, false, Encoding.Default);  //false代表替换而不是追加
                    logger.WriteLine("@echo off");
                    logger.Write("\"" + batPath + "\"" + " | " + "\"" + logger_path + "\" -format time -log \"" + log_name + "\"  : -");
                    //logger.Write("\"" + logger_path + "\" -format raw -inputcp ANSI -log \"" + log_name + "\" : \"" + batPath + "\"");
                    logger.Close();

                    RealAction(loggerPath);

                    //显示下载速度
                    RealAction2(GetSpeedCommand(batPath));
                }
                else
                {
                    button_Download.Enabled = true;  //启用下载按钮
                }
            }
            else
            {
                if (!Directory.Exists(label_outPutPath.Text + "\\" + textBox_filename.Text))//若文件夹不存在则新建文件夹   
                {
                    Directory.CreateDirectory(label_outPutPath.Text + "\\" + textBox_filename.Text); //新建文件夹   
                }
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Logs"))//若文件夹不存在则新建文件夹   
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Logs"); //新建文件夹   
                }
                if (System.IO.File.Exists(Path.GetFullPath(label_outPutPath.Text + "\\" + textBox_filename.Text + "\\m3u8_dl.lock")))  //存在则删除
                {
                    File.Delete(Path.GetFullPath(label_outPutPath.Text + "\\" + textBox_filename.Text + "\\m3u8_dl.lock"));
                }
                textBox_writeInfo.Text = "";
                textBox_writeInfo.Text = GetCommand();
                string batPath = Path.GetTempPath() + "\\m3u8_dl_GUI_Download_" + GetTimeStamp() + ".bat";
                StreamWriter writer = new StreamWriter(batPath, false, Encoding.Default);  //false代表替换而不是追加
                writer.WriteLine("@echo off");
                writer.Write(textBox_writeInfo.Text);
                writer.Close();

                string log_name = System.Windows.Forms.Application.StartupPath + "\\Logs\\Log-" + System.DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss") + ".txt";  //日志文件位置
                string logger_path = System.Windows.Forms.Application.StartupPath + "\\Tools\\logger.exe";  //logger路径
                string loggerPath = batPath.Replace("m3u8_dl_GUI_Download", "m3u8_dl_GUI_Logger");
                StreamWriter logger = new StreamWriter(loggerPath, false, Encoding.Default);  //false代表替换而不是追加
                logger.WriteLine("@echo off");
                logger.Write("\"" + batPath + "\"" + " | " + "\"" + logger_path + "\" -format time -inputcp ANSI -log \"" + log_name + "\"  : -");
                //logger.Write("\"" + logger_path + "\" -format raw -inputcp ANSI -log \"" + log_name + "\" : \"" + batPath + "\"");
                logger.Close();

                RealAction(loggerPath);

                //显示下载速度
                RealAction2(GetSpeedCommand(batPath));
            }
        }

        private void button_idmWake_Click_1(object sender, EventArgs e)
        {
            if (textBox_Adress.Text == "")
            {
                MessageBox.Show("请输入地址！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            m3u8IDM_wake(textBox_Adress.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();  //保存配置
            /*
            try
            {
                foreach (var pTempProcess in System.Diagnostics.Process.GetProcesses())
                {
                    string sProcessName = pTempProcess.ProcessName; //获取进程名称
                    if (sProcessName == "node")
                    {
                        int sProcessID = pTempProcess.Id; //获取进程ID
                        System.Diagnostics.Process pProcessTemp = Process.GetProcessById(sProcessID);
                        pProcessTemp.Kill(); //杀死它
                        pProcessTemp.Close();
                    }
                    if (sProcessName == "cmd" && pTempProcess.Id == PID_1)
                    {
                        int sProcessID = pTempProcess.Id; //获取进程ID
                        System.Diagnostics.Process pProcessTemp = Process.GetProcessById(sProcessID);
                        pProcessTemp.Kill(); //杀死它
                        pProcessTemp.Close();
                    }
                }

                textBox_Info.Text = "";
            }
            catch
            {

            }
            */
            if (PID_1 != 0 && PID_2 != 0)
            {
                EndProcessTree(PID_2);
                EndProcessTree(PID_1);
            }
        }

        private void m3u8IDM_wake(string url)
        {
            if (url.Contains("iqiyi.com"))   //针对爱奇艺m3u8的处理
            {
                url = url.Replace("/mus/text/", "/mus/");
            }
            if(url.Contains(".ts.m3u8?ver=4"))   //针对腾讯的处理(去除https协议)
            {
                url = url.Replace("https://", "http://");
            }

            WebClient down = new WebClient();
            down.Encoding = Encoding.UTF8;

            if (checkBox_Headers.Checked == true && textBox_Headers.Text != "")
            {
                string[] headers = GetHeader(textBox_Headers.Text.Replace("%", "%%"));
                for (int i = 0; i < headers.Length; i++)
                {
                    down.Headers.Add(headers[i]);
                }
            }

            string filename = "m3u8_dl-js_GUI_temp.m3u8";
            try
            {
                string m3u8_temp = down.DownloadString(url);
                String outpath = "";
                outpath = Environment.GetEnvironmentVariable("TMP") +
                    "\\" + filename;
                StreamWriter writer = new StreamWriter(outpath, false);  //false代表替换而不是追加
                writer.Write(m3u8_temp);
                writer.Close();
            }
            catch (Exception)
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            label4.Visible = true;
            label5.Visible = false;
            label11.Visible = false;
            tabControl1.SelectedIndex = 0;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            label4.Visible = false;
            label5.Visible = true;
            label11.Visible = false;
            tabControl1.SelectedIndex = 1;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            label4.Visible = false;
            label5.Visible = false;
            label11.Visible = true;
            tabControl1.SelectedIndex = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(PID_1!=0&& PID_2 != 0)
            {
                EndProcessTree(PID_2);
                EndProcessTree(PID_1);
                PID_1 = 0; PID_2 = 0;
            }
        }

        private void tabPage_list_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void tabPage_settings_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void linkLabel_ChangeOutPath_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                label_outPutPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBox_Info_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox == null)
                return;

            if (e.KeyChar == (char)1)       // Ctrl-A 相当于输入了AscII=1的控制字符
            {
                textBox.SelectAll();
                e.Handled = true;      // 不再发出“噔”的声音
            }
        }

        private void textBox_Info_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Info.GetLineFromCharIndex(textBox_Info.Text.Length) > 999)  //超过1000行清空
            {
                textBox_Info.Text = "";
            }
        }

        private void checkBox_del_afterMerge_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_del_afterMerge.CheckState == CheckState.Checked)
            {
                checkBox_del_afterMerge.ForeColor = Color.FromArgb(46, 204, 113);
            }
            if (checkBox_del_afterMerge.CheckState == CheckState.Unchecked)
            {
                checkBox_del_afterMerge.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void checkBox_del_afterMerge_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox_del_afterMerge.CheckState == CheckState.Checked)
            {
                checkBox_del_afterMerge.ForeColor = Color.FromArgb(46, 204, 113);
            }
            if (checkBox_del_afterMerge.CheckState == CheckState.Unchecked)
            {
                checkBox_del_afterMerge.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkLabel_githubUrl.Text);
        }

        private void tabPage_PreHandle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void checkBox_down_with_curl_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_down_with_curl.CheckState == CheckState.Checked)
            {
                checkBox_down_with_curl.ForeColor = Color.FromArgb(46, 204, 113);
            }
            if (checkBox_down_with_curl.CheckState == CheckState.Unchecked)
            {
                checkBox_down_with_curl.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void checkBox_IsNewMerge_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_IsNewMerge.CheckState == CheckState.Checked)
            {
                checkBox_IsNewMerge.ForeColor = Color.FromArgb(46, 204, 113);
            }
            if (checkBox_IsNewMerge.CheckState == CheckState.Unchecked)
            {
                checkBox_IsNewMerge.ForeColor = Color.FromArgb(52, 152, 219);
            }
        }

        private void textBox_Adress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x0d')  //回车键 Ascii码值
            {
                e.Handled = true;  //消除 “噔” 的声音
                button_Download.PerformClick();  //点击下载按钮
            }
        }

        private void textBox_Adress_DoubleClick(object sender, EventArgs e)
        {
            //从剪切板读取url
            Regex url = new Regex(@"[a-zA-z]+://[^\s]*", RegexOptions.Compiled | RegexOptions.Singleline);//取已下载大小
            string str = url.Match(Clipboard.GetText()).Value;
            textBox_Adress.Text = str;
        }
    }
}
