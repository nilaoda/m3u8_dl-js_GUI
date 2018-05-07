namespace M3U8_DL_GUI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_EXIT = new System.Windows.Forms.Button();
            this.label_Title = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.彻底删除任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OutPath = new System.Windows.Forms.Label();
            this.Command = new System.Windows.Forms.Label();
            this.OutName = new System.Windows.Forms.Label();
            this.button_MIN = new System.Windows.Forms.Button();
            this.textBox_Adress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox_auto_remove = new System.Windows.Forms.CheckBox();
            this.button_Download = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBox_Headers = new System.Windows.Forms.CheckBox();
            this.textBox_Headers = new System.Windows.Forms.TextBox();
            this.numericUpDown_retry = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown_sleep = new System.Windows.Forms.NumericUpDown();
            this.textBox_Base_url = new System.Windows.Forms.TextBox();
            this.checkBox_add_base_url = new System.Windows.Forms.CheckBox();
            this.label_Name = new System.Windows.Forms.Label();
            this.textBox_filename = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_threads = new System.Windows.Forms.NumericUpDown();
            this.checkBox_auto_merge = new System.Windows.Forms.CheckBox();
            this.checkBox_proxy = new System.Windows.Forms.CheckBox();
            this.radioButton_http = new System.Windows.Forms.RadioButton();
            this.radioButton_socks5 = new System.Windows.Forms.RadioButton();
            this.textBox_proxy = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_LIST = new System.Windows.Forms.TabPage();
            this.label_dur_clip_out = new System.Windows.Forms.Label();
            this.textBox_writeInfo = new System.Windows.Forms.TextBox();
            this.button_Stop = new System.Windows.Forms.Button();
            this.label_speed_out = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_Info = new System.Windows.Forms.TextBox();
            this.button_idmWake = new System.Windows.Forms.Button();
            this.tabPage_SETTINGS = new System.Windows.Forms.TabPage();
            this.linkLabel_OpenOutPath = new System.Windows.Forms.LinkLabel();
            this.checkBox_IsNewMerge = new System.Windows.Forms.CheckBox();
            this.checkBox_down_with_curl = new System.Windows.Forms.CheckBox();
            this.linkLabel_githubUrl = new System.Windows.Forms.LinkLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_convertFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_del_afterMerge = new System.Windows.Forms.CheckBox();
            this.linkLabel_ChangeOutPath = new System.Windows.Forms.LinkLabel();
            this.label_OutPath = new System.Windows.Forms.Label();
            this.label_outPutPath = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton_mergeFFmpeg = new System.Windows.Forms.RadioButton();
            this.radioButton_mergeBinary = new System.Windows.Forms.RadioButton();
            this.tabPage_PREHANDLE = new System.Windows.Forms.TabPage();
            this.button_UnloadPreHandle = new System.Windows.Forms.Button();
            this.button_LoadPreHandle = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.linkLabel_pMain = new System.Windows.Forms.LinkLabel();
            this.linkLabel_pSets = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.linkLabel_pPreH = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_retry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sleep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_threads)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage_LIST.SuspendLayout();
            this.tabPage_SETTINGS.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage_PREHANDLE.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_EXIT
            // 
            this.button_EXIT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_EXIT.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_EXIT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.button_EXIT.FlatAppearance.BorderSize = 0;
            this.button_EXIT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_EXIT.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_EXIT.ForeColor = System.Drawing.Color.White;
            this.button_EXIT.Location = new System.Drawing.Point(1017, 0);
            this.button_EXIT.Margin = new System.Windows.Forms.Padding(0);
            this.button_EXIT.Name = "button_EXIT";
            this.button_EXIT.Size = new System.Drawing.Size(124, 32);
            this.button_EXIT.TabIndex = 104;
            this.button_EXIT.Text = "EXIT";
            this.button_EXIT.UseVisualStyleBackColor = false;
            this.button_EXIT.Click += new System.EventHandler(this.button3_Click);
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.label_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.label_Title.Location = new System.Drawing.Point(9, 11);
            this.label_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(427, 31);
            this.label_Title.TabIndex = 106;
            this.label_Title.Text = "m3u8_dl-js_GUI 0.3.0  by：nilaoda";
            this.label_Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_Title_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.删除全部ToolStripMenuItem,
            this.彻底删除任务ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 76);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.删除ToolStripMenuItem.Text = "开始下载";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 删除全部ToolStripMenuItem
            // 
            this.删除全部ToolStripMenuItem.Name = "删除全部ToolStripMenuItem";
            this.删除全部ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.删除全部ToolStripMenuItem.Text = "删除任务";
            this.删除全部ToolStripMenuItem.Click += new System.EventHandler(this.删除全部ToolStripMenuItem_Click);
            // 
            // 彻底删除任务ToolStripMenuItem
            // 
            this.彻底删除任务ToolStripMenuItem.Name = "彻底删除任务ToolStripMenuItem";
            this.彻底删除任务ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.彻底删除任务ToolStripMenuItem.Text = "彻底删除任务";
            // 
            // OutPath
            // 
            this.OutPath.AutoSize = true;
            this.OutPath.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.OutPath.Location = new System.Drawing.Point(281, 35);
            this.OutPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.OutPath.Name = "OutPath";
            this.OutPath.Size = new System.Drawing.Size(63, 15);
            this.OutPath.TabIndex = 110;
            this.OutPath.Text = "OutPath";
            this.OutPath.Visible = false;
            // 
            // Command
            // 
            this.Command.AutoSize = true;
            this.Command.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Command.Location = new System.Drawing.Point(281, 50);
            this.Command.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Command.Name = "Command";
            this.Command.Size = new System.Drawing.Size(63, 15);
            this.Command.TabIndex = 117;
            this.Command.Text = "Command";
            this.Command.Visible = false;
            // 
            // OutName
            // 
            this.OutName.AutoSize = true;
            this.OutName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.OutName.Location = new System.Drawing.Point(281, 18);
            this.OutName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.OutName.Name = "OutName";
            this.OutName.Size = new System.Drawing.Size(63, 15);
            this.OutName.TabIndex = 118;
            this.OutName.Text = "OutName";
            this.OutName.Visible = false;
            // 
            // button_MIN
            // 
            this.button_MIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_MIN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_MIN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.button_MIN.FlatAppearance.BorderSize = 0;
            this.button_MIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_MIN.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_MIN.ForeColor = System.Drawing.Color.White;
            this.button_MIN.Location = new System.Drawing.Point(892, 0);
            this.button_MIN.Margin = new System.Windows.Forms.Padding(0);
            this.button_MIN.Name = "button_MIN";
            this.button_MIN.Size = new System.Drawing.Size(124, 32);
            this.button_MIN.TabIndex = 128;
            this.button_MIN.Text = "MIN";
            this.button_MIN.UseVisualStyleBackColor = false;
            this.button_MIN.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_Adress
            // 
            this.textBox_Adress.AllowDrop = true;
            this.textBox_Adress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.textBox_Adress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Adress.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox_Adress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.textBox_Adress.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.textBox_Adress.Location = new System.Drawing.Point(17, 65);
            this.textBox_Adress.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Adress.Name = "textBox_Adress";
            this.textBox_Adress.Size = new System.Drawing.Size(1102, 34);
            this.textBox_Adress.TabIndex = 0;
            this.textBox_Adress.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.textBox_Adress.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_Adress_DragEnter);
            this.textBox_Adress.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_Adress_DragOver);
            this.textBox_Adress.DoubleClick += new System.EventHandler(this.textBox_Adress_DoubleClick);
            this.textBox_Adress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Adress_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Location = new System.Drawing.Point(13, 26);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(561, 27);
            this.label7.TabIndex = 140;
            this.label7.Text = "m3u8链接或文件(可拖入本地文件，双击贴入剪贴板中的地址)";
            // 
            // checkBox_auto_remove
            // 
            this.checkBox_auto_remove.AutoSize = true;
            this.checkBox_auto_remove.Checked = true;
            this.checkBox_auto_remove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_auto_remove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_auto_remove.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.checkBox_auto_remove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.checkBox_auto_remove.Location = new System.Drawing.Point(123, 129);
            this.checkBox_auto_remove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_auto_remove.Name = "checkBox_auto_remove";
            this.checkBox_auto_remove.Size = new System.Drawing.Size(194, 31);
            this.checkBox_auto_remove.TabIndex = 141;
            this.checkBox_auto_remove.Text = "自动删除加密分片";
            this.checkBox_auto_remove.UseVisualStyleBackColor = true;
            this.checkBox_auto_remove.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button_Download
            // 
            this.button_Download.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.button_Download.FlatAppearance.BorderSize = 0;
            this.button_Download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Download.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button_Download.ForeColor = System.Drawing.Color.White;
            this.button_Download.Location = new System.Drawing.Point(818, 110);
            this.button_Download.Margin = new System.Windows.Forms.Padding(4);
            this.button_Download.Name = "button_Download";
            this.button_Download.Size = new System.Drawing.Size(147, 36);
            this.button_Download.TabIndex = 4;
            this.button_Download.Text = "开始下载";
            this.button_Download.UseVisualStyleBackColor = false;
            this.button_Download.Click += new System.EventHandler(this.button_Download_Click);
            // 
            // checkBox_Headers
            // 
            this.checkBox_Headers.AutoSize = true;
            this.checkBox_Headers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_Headers.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.checkBox_Headers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.checkBox_Headers.Location = new System.Drawing.Point(123, 322);
            this.checkBox_Headers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_Headers.Name = "checkBox_Headers";
            this.checkBox_Headers.Size = new System.Drawing.Size(379, 31);
            this.checkBox_Headers.TabIndex = 146;
            this.checkBox_Headers.Text = "添加Headers (使用 \'|\' 隔开以添加多个)";
            this.checkBox_Headers.UseVisualStyleBackColor = true;
            this.checkBox_Headers.CheckedChanged += new System.EventHandler(this.checkBox_Headers_CheckedChanged);
            // 
            // textBox_Headers
            // 
            this.textBox_Headers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.textBox_Headers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Headers.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox_Headers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.textBox_Headers.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.textBox_Headers.Location = new System.Drawing.Point(123, 371);
            this.textBox_Headers.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Headers.Name = "textBox_Headers";
            this.textBox_Headers.Size = new System.Drawing.Size(839, 34);
            this.textBox_Headers.TabIndex = 147;
            this.textBox_Headers.Text = "User-Agent:Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like G" +
    "ecko) Chrome/50.0.2661.102 Safari/537.36";
            // 
            // numericUpDown_retry
            // 
            this.numericUpDown_retry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.numericUpDown_retry.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.numericUpDown_retry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.numericUpDown_retry.Location = new System.Drawing.Point(288, 436);
            this.numericUpDown_retry.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown_retry.Name = "numericUpDown_retry";
            this.numericUpDown_retry.Size = new System.Drawing.Size(68, 34);
            this.numericUpDown_retry.TabIndex = 149;
            this.numericUpDown_retry.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(117, 439);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 27);
            this.label2.TabIndex = 150;
            this.label2.Text = "自动重试次数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(436, 439);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 27);
            this.label3.TabIndex = 152;
            this.label3.Text = "重试间隔时间(s)：";
            // 
            // numericUpDown_sleep
            // 
            this.numericUpDown_sleep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.numericUpDown_sleep.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.numericUpDown_sleep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.numericUpDown_sleep.Location = new System.Drawing.Point(629, 436);
            this.numericUpDown_sleep.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown_sleep.Name = "numericUpDown_sleep";
            this.numericUpDown_sleep.Size = new System.Drawing.Size(68, 34);
            this.numericUpDown_sleep.TabIndex = 151;
            this.numericUpDown_sleep.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // textBox_Base_url
            // 
            this.textBox_Base_url.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.textBox_Base_url.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Base_url.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox_Base_url.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.textBox_Base_url.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.textBox_Base_url.Location = new System.Drawing.Point(123, 269);
            this.textBox_Base_url.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Base_url.Name = "textBox_Base_url";
            this.textBox_Base_url.Size = new System.Drawing.Size(839, 34);
            this.textBox_Base_url.TabIndex = 154;
            // 
            // checkBox_add_base_url
            // 
            this.checkBox_add_base_url.AutoSize = true;
            this.checkBox_add_base_url.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_add_base_url.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.checkBox_add_base_url.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.checkBox_add_base_url.Location = new System.Drawing.Point(123, 231);
            this.checkBox_add_base_url.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_add_base_url.Name = "checkBox_add_base_url";
            this.checkBox_add_base_url.Size = new System.Drawing.Size(453, 31);
            this.checkBox_add_base_url.TabIndex = 153;
            this.checkBox_add_base_url.Text = "添加 Base Url (下载本地m3u8时考虑开启此项)";
            this.checkBox_add_base_url.UseVisualStyleBackColor = true;
            this.checkBox_add_base_url.CheckedChanged += new System.EventHandler(this.checkBox_add_base_url_CheckedChanged);
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label_Name.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_Name.Location = new System.Drawing.Point(13, 114);
            this.label_Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(92, 27);
            this.label_Name.TabIndex = 155;
            this.label_Name.Text = "文件名：";
            // 
            // textBox_filename
            // 
            this.textBox_filename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.textBox_filename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_filename.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox_filename.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.textBox_filename.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.textBox_filename.Location = new System.Drawing.Point(120, 111);
            this.textBox_filename.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_filename.Name = "textBox_filename";
            this.textBox_filename.Size = new System.Drawing.Size(532, 34);
            this.textBox_filename.TabIndex = 2;
            this.textBox_filename.Text = "未知视频";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Location = new System.Drawing.Point(788, 439);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 27);
            this.label6.TabIndex = 157;
            this.label6.Text = "线程数：";
            // 
            // numericUpDown_threads
            // 
            this.numericUpDown_threads.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.numericUpDown_threads.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.numericUpDown_threads.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.numericUpDown_threads.Location = new System.Drawing.Point(895, 436);
            this.numericUpDown_threads.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown_threads.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_threads.Name = "numericUpDown_threads";
            this.numericUpDown_threads.Size = new System.Drawing.Size(68, 34);
            this.numericUpDown_threads.TabIndex = 158;
            this.numericUpDown_threads.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // checkBox_auto_merge
            // 
            this.checkBox_auto_merge.AutoSize = true;
            this.checkBox_auto_merge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_auto_merge.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.checkBox_auto_merge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.checkBox_auto_merge.Location = new System.Drawing.Point(373, 129);
            this.checkBox_auto_merge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_auto_merge.Name = "checkBox_auto_merge";
            this.checkBox_auto_merge.Size = new System.Drawing.Size(154, 31);
            this.checkBox_auto_merge.TabIndex = 159;
            this.checkBox_auto_merge.Text = "自动合并分片";
            this.checkBox_auto_merge.UseVisualStyleBackColor = true;
            this.checkBox_auto_merge.CheckedChanged += new System.EventHandler(this.checkBox_auto_merge_CheckedChanged);
            // 
            // checkBox_proxy
            // 
            this.checkBox_proxy.AutoSize = true;
            this.checkBox_proxy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_proxy.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.checkBox_proxy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.checkBox_proxy.Location = new System.Drawing.Point(288, 496);
            this.checkBox_proxy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_proxy.Name = "checkBox_proxy";
            this.checkBox_proxy.Size = new System.Drawing.Size(114, 31);
            this.checkBox_proxy.TabIndex = 160;
            this.checkBox_proxy.Text = "开启代理";
            this.checkBox_proxy.UseVisualStyleBackColor = true;
            this.checkBox_proxy.CheckedChanged += new System.EventHandler(this.checkBox_proxy_CheckedChanged);
            // 
            // radioButton_http
            // 
            this.radioButton_http.AutoSize = true;
            this.radioButton_http.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.radioButton_http.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.radioButton_http.Location = new System.Drawing.Point(413, 498);
            this.radioButton_http.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton_http.Name = "radioButton_http";
            this.radioButton_http.Size = new System.Drawing.Size(82, 31);
            this.radioButton_http.TabIndex = 161;
            this.radioButton_http.Text = "HTTP";
            this.radioButton_http.UseVisualStyleBackColor = true;
            // 
            // radioButton_socks5
            // 
            this.radioButton_socks5.AutoSize = true;
            this.radioButton_socks5.Checked = true;
            this.radioButton_socks5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.radioButton_socks5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.radioButton_socks5.Location = new System.Drawing.Point(504, 498);
            this.radioButton_socks5.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton_socks5.Name = "radioButton_socks5";
            this.radioButton_socks5.Size = new System.Drawing.Size(111, 31);
            this.radioButton_socks5.TabIndex = 162;
            this.radioButton_socks5.TabStop = true;
            this.radioButton_socks5.Text = "SOCKS5";
            this.radioButton_socks5.UseVisualStyleBackColor = true;
            // 
            // textBox_proxy
            // 
            this.textBox_proxy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.textBox_proxy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_proxy.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox_proxy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.textBox_proxy.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.textBox_proxy.Location = new System.Drawing.Point(629, 496);
            this.textBox_proxy.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_proxy.Name = "textBox_proxy";
            this.textBox_proxy.Size = new System.Drawing.Size(333, 34);
            this.textBox_proxy.TabIndex = 163;
            this.textBox_proxy.Text = "127.0.0.1:1080";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_LIST);
            this.tabControl1.Controls.Add(this.tabPage_SETTINGS);
            this.tabControl1.Controls.Add(this.tabPage_PREHANDLE);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabControl1.Location = new System.Drawing.Point(-12, -36);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1144, 680);
            this.tabControl1.TabIndex = 165;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPage_LIST
            // 
            this.tabPage_LIST.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.tabPage_LIST.Controls.Add(this.label_dur_clip_out);
            this.tabPage_LIST.Controls.Add(this.textBox_writeInfo);
            this.tabPage_LIST.Controls.Add(this.button_Stop);
            this.tabPage_LIST.Controls.Add(this.label_speed_out);
            this.tabPage_LIST.Controls.Add(this.label8);
            this.tabPage_LIST.Controls.Add(this.textBox_Info);
            this.tabPage_LIST.Controls.Add(this.button_idmWake);
            this.tabPage_LIST.Controls.Add(this.textBox_Adress);
            this.tabPage_LIST.Controls.Add(this.textBox_filename);
            this.tabPage_LIST.Controls.Add(this.label7);
            this.tabPage_LIST.Controls.Add(this.label_Name);
            this.tabPage_LIST.Controls.Add(this.button_Download);
            this.tabPage_LIST.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabPage_LIST.Location = new System.Drawing.Point(4, 29);
            this.tabPage_LIST.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_LIST.Name = "tabPage_LIST";
            this.tabPage_LIST.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_LIST.Size = new System.Drawing.Size(1136, 647);
            this.tabPage_LIST.TabIndex = 0;
            this.tabPage_LIST.Text = "任务列表";
            this.tabPage_LIST.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabPage_list_MouseDown);
            // 
            // label_dur_clip_out
            // 
            this.label_dur_clip_out.AutoSize = true;
            this.label_dur_clip_out.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label_dur_clip_out.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_dur_clip_out.Location = new System.Drawing.Point(658, 26);
            this.label_dur_clip_out.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_dur_clip_out.Name = "label_dur_clip_out";
            this.label_dur_clip_out.Size = new System.Drawing.Size(207, 27);
            this.label_dur_clip_out.TabIndex = 172;
            this.label_dur_clip_out.Text = "m3u8信息：暂未获取";
            this.label_dur_clip_out.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBox_writeInfo
            // 
            this.textBox_writeInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.textBox_writeInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_writeInfo.Font = new System.Drawing.Font("Consolas", 9F);
            this.textBox_writeInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.textBox_writeInfo.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.textBox_writeInfo.Location = new System.Drawing.Point(501, 271);
            this.textBox_writeInfo.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_writeInfo.Multiline = true;
            this.textBox_writeInfo.Name = "textBox_writeInfo";
            this.textBox_writeInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_writeInfo.Size = new System.Drawing.Size(442, 173);
            this.textBox_writeInfo.TabIndex = 171;
            this.textBox_writeInfo.Visible = false;
            // 
            // button_Stop
            // 
            this.button_Stop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.button_Stop.FlatAppearance.BorderSize = 0;
            this.button_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Stop.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button_Stop.ForeColor = System.Drawing.Color.White;
            this.button_Stop.Location = new System.Drawing.Point(972, 110);
            this.button_Stop.Margin = new System.Windows.Forms.Padding(4);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(147, 36);
            this.button_Stop.TabIndex = 5;
            this.button_Stop.Text = "停止下载";
            this.button_Stop.UseVisualStyleBackColor = false;
            this.button_Stop.Click += new System.EventHandler(this.button4_Click);
            // 
            // label_speed_out
            // 
            this.label_speed_out.AutoSize = true;
            this.label_speed_out.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label_speed_out.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_speed_out.Location = new System.Drawing.Point(171, 164);
            this.label_speed_out.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_speed_out.Name = "label_speed_out";
            this.label_speed_out.Size = new System.Drawing.Size(48, 27);
            this.label_speed_out.TabIndex = 170;
            this.label_speed_out.Text = "      ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label8.Location = new System.Drawing.Point(13, 164);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 27);
            this.label8.TabIndex = 169;
            this.label8.Text = "任务详情：";
            // 
            // textBox_Info
            // 
            this.textBox_Info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.textBox_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Info.Font = new System.Drawing.Font("Consolas", 9F);
            this.textBox_Info.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.textBox_Info.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.textBox_Info.Location = new System.Drawing.Point(17, 211);
            this.textBox_Info.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Info.Multiline = true;
            this.textBox_Info.Name = "textBox_Info";
            this.textBox_Info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Info.Size = new System.Drawing.Size(1102, 417);
            this.textBox_Info.TabIndex = 6;
            this.textBox_Info.TextChanged += new System.EventHandler(this.textBox_Info_TextChanged);
            this.textBox_Info.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Info_KeyPress);
            // 
            // button_idmWake
            // 
            this.button_idmWake.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.button_idmWake.FlatAppearance.BorderSize = 0;
            this.button_idmWake.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_idmWake.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button_idmWake.ForeColor = System.Drawing.Color.White;
            this.button_idmWake.Location = new System.Drawing.Point(663, 110);
            this.button_idmWake.Margin = new System.Windows.Forms.Padding(4);
            this.button_idmWake.Name = "button_idmWake";
            this.button_idmWake.Size = new System.Drawing.Size(147, 36);
            this.button_idmWake.TabIndex = 3;
            this.button_idmWake.Text = "尝试使用idm";
            this.button_idmWake.UseVisualStyleBackColor = false;
            this.button_idmWake.Click += new System.EventHandler(this.button_idmWake_Click_1);
            // 
            // tabPage_SETTINGS
            // 
            this.tabPage_SETTINGS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.tabPage_SETTINGS.Controls.Add(this.linkLabel_OpenOutPath);
            this.tabPage_SETTINGS.Controls.Add(this.checkBox_IsNewMerge);
            this.tabPage_SETTINGS.Controls.Add(this.checkBox_down_with_curl);
            this.tabPage_SETTINGS.Controls.Add(this.linkLabel_githubUrl);
            this.tabPage_SETTINGS.Controls.Add(this.label10);
            this.tabPage_SETTINGS.Controls.Add(this.label9);
            this.tabPage_SETTINGS.Controls.Add(this.comboBox_convertFormat);
            this.tabPage_SETTINGS.Controls.Add(this.label1);
            this.tabPage_SETTINGS.Controls.Add(this.checkBox_del_afterMerge);
            this.tabPage_SETTINGS.Controls.Add(this.linkLabel_ChangeOutPath);
            this.tabPage_SETTINGS.Controls.Add(this.label_OutPath);
            this.tabPage_SETTINGS.Controls.Add(this.label_outPutPath);
            this.tabPage_SETTINGS.Controls.Add(this.panel1);
            this.tabPage_SETTINGS.Controls.Add(this.checkBox_auto_remove);
            this.tabPage_SETTINGS.Controls.Add(this.checkBox_Headers);
            this.tabPage_SETTINGS.Controls.Add(this.textBox_Headers);
            this.tabPage_SETTINGS.Controls.Add(this.numericUpDown_retry);
            this.tabPage_SETTINGS.Controls.Add(this.textBox_proxy);
            this.tabPage_SETTINGS.Controls.Add(this.label2);
            this.tabPage_SETTINGS.Controls.Add(this.radioButton_socks5);
            this.tabPage_SETTINGS.Controls.Add(this.numericUpDown_sleep);
            this.tabPage_SETTINGS.Controls.Add(this.radioButton_http);
            this.tabPage_SETTINGS.Controls.Add(this.label3);
            this.tabPage_SETTINGS.Controls.Add(this.checkBox_proxy);
            this.tabPage_SETTINGS.Controls.Add(this.checkBox_add_base_url);
            this.tabPage_SETTINGS.Controls.Add(this.checkBox_auto_merge);
            this.tabPage_SETTINGS.Controls.Add(this.textBox_Base_url);
            this.tabPage_SETTINGS.Controls.Add(this.numericUpDown_threads);
            this.tabPage_SETTINGS.Controls.Add(this.label6);
            this.tabPage_SETTINGS.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabPage_SETTINGS.Location = new System.Drawing.Point(4, 29);
            this.tabPage_SETTINGS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_SETTINGS.Name = "tabPage_SETTINGS";
            this.tabPage_SETTINGS.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_SETTINGS.Size = new System.Drawing.Size(1136, 647);
            this.tabPage_SETTINGS.TabIndex = 1;
            this.tabPage_SETTINGS.Text = "设置";
            this.tabPage_SETTINGS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabPage_settings_MouseDown);
            // 
            // linkLabel_OpenOutPath
            // 
            this.linkLabel_OpenOutPath.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_OpenOutPath.AutoSize = true;
            this.linkLabel_OpenOutPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel_OpenOutPath.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.linkLabel_OpenOutPath.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_OpenOutPath.Location = new System.Drawing.Point(278, 76);
            this.linkLabel_OpenOutPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_OpenOutPath.Name = "linkLabel_OpenOutPath";
            this.linkLabel_OpenOutPath.Size = new System.Drawing.Size(39, 20);
            this.linkLabel_OpenOutPath.TabIndex = 179;
            this.linkLabel_OpenOutPath.TabStop = true;
            this.linkLabel_OpenOutPath.Text = "打开";
            this.linkLabel_OpenOutPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_OpenOutPath_LinkClicked);
            // 
            // checkBox_IsNewMerge
            // 
            this.checkBox_IsNewMerge.AutoSize = true;
            this.checkBox_IsNewMerge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_IsNewMerge.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.checkBox_IsNewMerge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.checkBox_IsNewMerge.Location = new System.Drawing.Point(123, 178);
            this.checkBox_IsNewMerge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_IsNewMerge.Name = "checkBox_IsNewMerge";
            this.checkBox_IsNewMerge.Size = new System.Drawing.Size(453, 31);
            this.checkBox_IsNewMerge.TabIndex = 178;
            this.checkBox_IsNewMerge.Text = "我想使用更慢的合并方式(少分片使用;后果自负)";
            this.checkBox_IsNewMerge.UseVisualStyleBackColor = true;
            this.checkBox_IsNewMerge.CheckedChanged += new System.EventHandler(this.checkBox_IsNewMerge_CheckedChanged);
            // 
            // checkBox_down_with_curl
            // 
            this.checkBox_down_with_curl.AutoSize = true;
            this.checkBox_down_with_curl.Checked = true;
            this.checkBox_down_with_curl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_down_with_curl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_down_with_curl.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.checkBox_down_with_curl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.checkBox_down_with_curl.Location = new System.Drawing.Point(123, 496);
            this.checkBox_down_with_curl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_down_with_curl.Name = "checkBox_down_with_curl";
            this.checkBox_down_with_curl.Size = new System.Drawing.Size(149, 31);
            this.checkBox_down_with_curl.TabIndex = 177;
            this.checkBox_down_with_curl.Text = "使用curl下载";
            this.checkBox_down_with_curl.UseVisualStyleBackColor = true;
            this.checkBox_down_with_curl.CheckedChanged += new System.EventHandler(this.checkBox_down_with_curl_CheckedChanged);
            // 
            // linkLabel_githubUrl
            // 
            this.linkLabel_githubUrl.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_githubUrl.AutoSize = true;
            this.linkLabel_githubUrl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel_githubUrl.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.linkLabel_githubUrl.LinkColor = System.Drawing.SystemColors.ActiveBorder;
            this.linkLabel_githubUrl.Location = new System.Drawing.Point(255, 565);
            this.linkLabel_githubUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_githubUrl.Name = "linkLabel_githubUrl";
            this.linkLabel_githubUrl.Size = new System.Drawing.Size(294, 20);
            this.linkLabel_githubUrl.TabIndex = 176;
            this.linkLabel_githubUrl.TabStop = true;
            this.linkLabel_githubUrl.Text = "https://github.com/sceext2/m3u8_dl-js";
            this.linkLabel_githubUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.label10.Location = new System.Drawing.Point(119, 566);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 20);
            this.label10.TabIndex = 175;
            this.label10.Text = "了解更多，请访问";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label9.Location = new System.Drawing.Point(649, 28);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 27);
            this.label9.TabIndex = 174;
            this.label9.Text = "合并方式：";
            this.label9.Visible = false;
            // 
            // comboBox_convertFormat
            // 
            this.comboBox_convertFormat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.comboBox_convertFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_convertFormat.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.comboBox_convertFormat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.comboBox_convertFormat.FormattingEnabled = true;
            this.comboBox_convertFormat.Items.AddRange(new object[] {
            "不转换格式",
            "转换为MP4",
            "转换为MKV",
            "转换为FLV",
            "转换为TS"});
            this.comboBox_convertFormat.Location = new System.Drawing.Point(793, 174);
            this.comboBox_convertFormat.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_convertFormat.Name = "comboBox_convertFormat";
            this.comboBox_convertFormat.Size = new System.Drawing.Size(168, 31);
            this.comboBox_convertFormat.TabIndex = 173;
            this.comboBox_convertFormat.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(601, 179);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 27);
            this.label1.TabIndex = 171;
            this.label1.Text = "合并任务完成后：";
            // 
            // checkBox_del_afterMerge
            // 
            this.checkBox_del_afterMerge.AutoSize = true;
            this.checkBox_del_afterMerge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_del_afterMerge.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.checkBox_del_afterMerge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.checkBox_del_afterMerge.Location = new System.Drawing.Point(581, 129);
            this.checkBox_del_afterMerge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox_del_afterMerge.Name = "checkBox_del_afterMerge";
            this.checkBox_del_afterMerge.Size = new System.Drawing.Size(174, 31);
            this.checkBox_del_afterMerge.TabIndex = 170;
            this.checkBox_del_afterMerge.Text = "合并后删除分片";
            this.checkBox_del_afterMerge.UseVisualStyleBackColor = true;
            this.checkBox_del_afterMerge.CheckedChanged += new System.EventHandler(this.checkBox_del_afterMerge_CheckedChanged_1);
            // 
            // linkLabel_ChangeOutPath
            // 
            this.linkLabel_ChangeOutPath.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_ChangeOutPath.AutoSize = true;
            this.linkLabel_ChangeOutPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel_ChangeOutPath.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.linkLabel_ChangeOutPath.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_ChangeOutPath.Location = new System.Drawing.Point(237, 76);
            this.linkLabel_ChangeOutPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_ChangeOutPath.Name = "linkLabel_ChangeOutPath";
            this.linkLabel_ChangeOutPath.Size = new System.Drawing.Size(39, 20);
            this.linkLabel_ChangeOutPath.TabIndex = 169;
            this.linkLabel_ChangeOutPath.TabStop = true;
            this.linkLabel_ChangeOutPath.Text = "更改";
            this.linkLabel_ChangeOutPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_ChangeOutPath_LinkClicked_1);
            // 
            // label_OutPath
            // 
            this.label_OutPath.AutoSize = true;
            this.label_OutPath.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label_OutPath.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_OutPath.Location = new System.Drawing.Point(117, 71);
            this.label_OutPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_OutPath.Name = "label_OutPath";
            this.label_OutPath.Size = new System.Drawing.Size(112, 27);
            this.label_OutPath.TabIndex = 167;
            this.label_OutPath.Text = "输出目录：";
            // 
            // label_outPutPath
            // 
            this.label_outPutPath.AutoSize = true;
            this.label_outPutPath.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label_outPutPath.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_outPutPath.Location = new System.Drawing.Point(325, 71);
            this.label_outPutPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_outPutPath.Name = "label_outPutPath";
            this.label_outPutPath.Size = new System.Drawing.Size(38, 27);
            this.label_outPutPath.TabIndex = 168;
            this.label_outPutPath.Text = "dir";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_mergeFFmpeg);
            this.panel1.Controls.Add(this.radioButton_mergeBinary);
            this.panel1.Location = new System.Drawing.Point(808, 8);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(279, 65);
            this.panel1.TabIndex = 166;
            this.panel1.Visible = false;
            // 
            // radioButton_mergeFFmpeg
            // 
            this.radioButton_mergeFFmpeg.AutoSize = true;
            this.radioButton_mergeFFmpeg.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.radioButton_mergeFFmpeg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.radioButton_mergeFFmpeg.Location = new System.Drawing.Point(135, 14);
            this.radioButton_mergeFFmpeg.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton_mergeFFmpeg.Name = "radioButton_mergeFFmpeg";
            this.radioButton_mergeFFmpeg.Size = new System.Drawing.Size(111, 31);
            this.radioButton_mergeFFmpeg.TabIndex = 165;
            this.radioButton_mergeFFmpeg.Text = "FFmpeg";
            this.radioButton_mergeFFmpeg.UseVisualStyleBackColor = true;
            // 
            // radioButton_mergeBinary
            // 
            this.radioButton_mergeBinary.AutoSize = true;
            this.radioButton_mergeBinary.Checked = true;
            this.radioButton_mergeBinary.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.radioButton_mergeBinary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.radioButton_mergeBinary.Location = new System.Drawing.Point(15, 15);
            this.radioButton_mergeBinary.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton_mergeBinary.Name = "radioButton_mergeBinary";
            this.radioButton_mergeBinary.Size = new System.Drawing.Size(93, 31);
            this.radioButton_mergeBinary.TabIndex = 164;
            this.radioButton_mergeBinary.TabStop = true;
            this.radioButton_mergeBinary.Text = "Binary";
            this.radioButton_mergeBinary.UseVisualStyleBackColor = true;
            // 
            // tabPage_PREHANDLE
            // 
            this.tabPage_PREHANDLE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.tabPage_PREHANDLE.Controls.Add(this.button_UnloadPreHandle);
            this.tabPage_PREHANDLE.Controls.Add(this.button_LoadPreHandle);
            this.tabPage_PREHANDLE.Controls.Add(this.label12);
            this.tabPage_PREHANDLE.Location = new System.Drawing.Point(4, 29);
            this.tabPage_PREHANDLE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_PREHANDLE.Name = "tabPage_PREHANDLE";
            this.tabPage_PREHANDLE.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage_PREHANDLE.Size = new System.Drawing.Size(1136, 647);
            this.tabPage_PREHANDLE.TabIndex = 2;
            this.tabPage_PREHANDLE.Text = "tabPage1";
            this.tabPage_PREHANDLE.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabPage_PreHandle_MouseDown);
            // 
            // button_UnloadPreHandle
            // 
            this.button_UnloadPreHandle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.button_UnloadPreHandle.FlatAppearance.BorderSize = 0;
            this.button_UnloadPreHandle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_UnloadPreHandle.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button_UnloadPreHandle.ForeColor = System.Drawing.Color.White;
            this.button_UnloadPreHandle.Location = new System.Drawing.Point(918, 51);
            this.button_UnloadPreHandle.Margin = new System.Windows.Forms.Padding(4);
            this.button_UnloadPreHandle.Name = "button_UnloadPreHandle";
            this.button_UnloadPreHandle.Size = new System.Drawing.Size(183, 36);
            this.button_UnloadPreHandle.TabIndex = 174;
            this.button_UnloadPreHandle.Text = "移除选定预处理";
            this.button_UnloadPreHandle.UseVisualStyleBackColor = false;
            // 
            // button_LoadPreHandle
            // 
            this.button_LoadPreHandle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.button_LoadPreHandle.FlatAppearance.BorderSize = 0;
            this.button_LoadPreHandle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_LoadPreHandle.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button_LoadPreHandle.ForeColor = System.Drawing.Color.White;
            this.button_LoadPreHandle.Location = new System.Drawing.Point(720, 51);
            this.button_LoadPreHandle.Margin = new System.Windows.Forms.Padding(4);
            this.button_LoadPreHandle.Name = "button_LoadPreHandle";
            this.button_LoadPreHandle.Size = new System.Drawing.Size(183, 36);
            this.button_LoadPreHandle.TabIndex = 173;
            this.button_LoadPreHandle.Text = "导入新的预处理";
            this.button_LoadPreHandle.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label12.ForeColor = System.Drawing.Color.LightGray;
            this.label12.Location = new System.Drawing.Point(395, 308);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(326, 31);
            this.label12.TabIndex = 172;
            this.label12.Text = "很抱歉，这里依旧空空如也。";
            // 
            // linkLabel_pMain
            // 
            this.linkLabel_pMain.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_pMain.AutoSize = true;
            this.linkLabel_pMain.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold);
            this.linkLabel_pMain.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.linkLabel_pMain.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_pMain.Location = new System.Drawing.Point(497, 11);
            this.linkLabel_pMain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_pMain.Name = "linkLabel_pMain";
            this.linkLabel_pMain.Size = new System.Drawing.Size(86, 31);
            this.linkLabel_pMain.TabIndex = 166;
            this.linkLabel_pMain.TabStop = true;
            this.linkLabel_pMain.Text = "主界面";
            this.linkLabel_pMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_pMain.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel_pSets
            // 
            this.linkLabel_pSets.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_pSets.AutoSize = true;
            this.linkLabel_pSets.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold);
            this.linkLabel_pSets.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.linkLabel_pSets.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_pSets.Location = new System.Drawing.Point(616, 11);
            this.linkLabel_pSets.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_pSets.Name = "linkLabel_pSets";
            this.linkLabel_pSets.Size = new System.Drawing.Size(62, 31);
            this.linkLabel_pSets.TabIndex = 167;
            this.linkLabel_pSets.TabStop = true;
            this.linkLabel_pSets.Text = "设置";
            this.linkLabel_pSets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_pSets.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.label4.Location = new System.Drawing.Point(472, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 27);
            this.label4.TabIndex = 167;
            this.label4.Text = "▶";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.label5.Location = new System.Drawing.Point(588, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 27);
            this.label5.TabIndex = 168;
            this.label5.Text = "▶";
            this.label5.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Location = new System.Drawing.Point(9, 71);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1115, 630);
            this.panel2.TabIndex = 169;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.label11.Location = new System.Drawing.Point(685, 15);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 27);
            this.label11.TabIndex = 171;
            this.label11.Text = "▶";
            this.label11.Visible = false;
            // 
            // linkLabel_pPreH
            // 
            this.linkLabel_pPreH.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_pPreH.AutoSize = true;
            this.linkLabel_pPreH.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold);
            this.linkLabel_pPreH.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.linkLabel_pPreH.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.linkLabel_pPreH.Location = new System.Drawing.Point(715, 11);
            this.linkLabel_pPreH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_pPreH.Name = "linkLabel_pPreH";
            this.linkLabel_pPreH.Size = new System.Drawing.Size(134, 31);
            this.linkLabel_pPreH.TabIndex = 170;
            this.linkLabel_pPreH.TabStop = true;
            this.linkLabel_pPreH.Text = "预处理程序";
            this.linkLabel_pPreH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_pPreH.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1143, 711);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.linkLabel_pPreH);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel_pSets);
            this.Controls.Add(this.linkLabel_pMain);
            this.Controls.Add(this.button_MIN);
            this.Controls.Add(this.OutName);
            this.Controls.Add(this.Command);
            this.Controls.Add(this.OutPath);
            this.Controls.Add(this.label_Title);
            this.Controls.Add(this.button_EXIT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "m3u8_dl-js_GUI 0.3.0  by：nilaoda [20180506]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_retry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sleep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_threads)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_LIST.ResumeLayout(false);
            this.tabPage_LIST.PerformLayout();
            this.tabPage_SETTINGS.ResumeLayout(false);
            this.tabPage_SETTINGS.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage_PREHANDLE.ResumeLayout(false);
            this.tabPage_PREHANDLE.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_EXIT;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label OutPath;
        private System.Windows.Forms.Label Command;
        private System.Windows.Forms.Label OutName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除全部ToolStripMenuItem;
        private System.Windows.Forms.Button button_MIN;
        private System.Windows.Forms.TextBox textBox_Adress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox_auto_remove;
        private System.Windows.Forms.Button button_Download;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox checkBox_Headers;
        private System.Windows.Forms.TextBox textBox_Headers;
        private System.Windows.Forms.NumericUpDown numericUpDown_retry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_sleep;
        private System.Windows.Forms.TextBox textBox_Base_url;
        private System.Windows.Forms.CheckBox checkBox_add_base_url;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox textBox_filename;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_threads;
        private System.Windows.Forms.CheckBox checkBox_auto_merge;
        private System.Windows.Forms.CheckBox checkBox_proxy;
        private System.Windows.Forms.RadioButton radioButton_http;
        private System.Windows.Forms.RadioButton radioButton_socks5;
        private System.Windows.Forms.TextBox textBox_proxy;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_LIST;
        private System.Windows.Forms.TabPage tabPage_SETTINGS;
        private System.Windows.Forms.Button button_idmWake;
        private System.Windows.Forms.ToolStripMenuItem 彻底删除任务ToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButton_mergeBinary;
        private System.Windows.Forms.RadioButton radioButton_mergeFFmpeg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel_pMain;
        private System.Windows.Forms.LinkLabel linkLabel_pSets;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_Info;
        private System.Windows.Forms.LinkLabel linkLabel_ChangeOutPath;
        private System.Windows.Forms.Label label_OutPath;
        private System.Windows.Forms.Label label_outPutPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_speed_out;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox_writeInfo;
        private System.Windows.Forms.CheckBox checkBox_del_afterMerge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_convertFormat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel linkLabel_githubUrl;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.LinkLabel linkLabel_pPreH;
        private System.Windows.Forms.TabPage tabPage_PREHANDLE;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBox_down_with_curl;
        private System.Windows.Forms.CheckBox checkBox_IsNewMerge;
        private System.Windows.Forms.LinkLabel linkLabel_OpenOutPath;
        private System.Windows.Forms.Button button_LoadPreHandle;
        private System.Windows.Forms.Button button_UnloadPreHandle;
        private System.Windows.Forms.Label label_dur_clip_out;
    }
}

