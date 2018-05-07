using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3U8_DL_GUI
{
    public partial class Form1 : Form
    {
        //建立动态数组存放文件名数据
        ArrayList fileName = new ArrayList();

        //x64
        string curl_path = System.Windows.Forms.Application.StartupPath + @"\Tools\curl_64.exe";
        string node_path = System.Windows.Forms.Application.StartupPath + @"\Tools\node_64.exe";

        //x86
        //string curl_path = System.Windows.Forms.Application.StartupPath + @"\Tools\curl.exe";
        //string node_path = System.Windows.Forms.Application.StartupPath + @"\Tools\node.exe";

        public string GetCommand()
        {
            string ffmerge_path = System.Windows.Forms.Application.StartupPath + @"\Tools\ffmerge.exe";  //ffmerge路径
            string ffmpeg_path = System.Windows.Forms.Application.StartupPath + @"\Tools\ffmpeg.exe";  //ffmpeg路径
            //string logger_path = System.Windows.Forms.Application.StartupPath + "\\Tools\\logger.exe";  //logger路径
            string m3u8_dl_path = System.Windows.Forms.Application.StartupPath + @"\Tools\m3u8_dl-js-master\dist\m3u8_dl.js";  //主程序路径
            string is_task_done_path = System.Windows.Forms.Application.StartupPath + @"\Tools\m3u8_dl-js-master\dist\is_task_done.js";
            string auto_retry_path = System.Windows.Forms.Application.StartupPath + @"\Tools\m3u8_dl-js-master\dist\auto_retry.js";
            string m3u8_path = textBox_Adress.Text.Trim().Replace("%","%%");
            //string log_name = System.Windows.Forms.Application.StartupPath + "\\Logs\\Log-" + System.DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss") + ".txt";  //日志文件位置
            string command = "";
            string file_path = label_outPutPath.Text + "\\" + textBox_filename.Text;
            //string command = "\"" + logger_path + "\" -format raw -log \"" + log_name + "\" : ";  //最终命令
            command += "(echo =======================-m3u8_dl-js 版本信息-=======================)";
            command += " && (\"" + node_path + "\"" + " \"" + m3u8_dl_path + "\" --version)";
            command += " && (echo =======================-m3u8_dl-js 命令开始-=======================)";
            command += " && (" + "\"" + node_path + "\"" + " \"" + auto_retry_path + "\" -o \"" + label_outPutPath.Text + "\\" + textBox_filename.Text.Replace("%", "%%") + "\""
                + " --retry " + numericUpDown_retry.Text + " --sleep " + numericUpDown_sleep.Text + " --remove-part-files --use-raw-m3u8 \"" + m3u8_path + "\""
                + " -- "  //下面是传递给m3u8_dl的参数
                + "--thread " + numericUpDown_threads.Text + " ";
            if (checkBox_auto_remove.Checked == true) { command += "--auto-remove "; }
            if (checkBox_add_base_url.Checked == true && textBox_Base_url.Text != "" && !textBox_Base_url.Text.Contains(" "))
            {
                command += "--m3u8-base-url " + textBox_Base_url.Text.Trim().Replace("%", "%%") + " "; 
            }
            if (checkBox_proxy.Checked == true && textBox_proxy.Text != "") 
            {
                if (radioButton_http.Checked == true) { command += "--proxy-http \"" + textBox_proxy.Text.Trim() + "\" "; }
                if (radioButton_socks5.Checked == true) { command += "--proxy-socks5 \"" + textBox_proxy.Text.Trim() + "\" "; }
            }
            if (checkBox_Headers.Checked == true && textBox_Headers.Text != "") 
            {
                string[] headers = GetHeader(textBox_Headers.Text.Replace("%", "%%"));
                for(int i = 0; i < headers.Length; i++)
                {
                    command += "--header \"" + headers[i] + "\" ";
                }
            }
            if (checkBox_down_with_curl.Checked == true)
            {
                command += "--dl-with-curl \"" + curl_path + "\" ";
            }

            command += "--exit-on-flag)";
            command += " && (" + "echo =======================-检测是否完成下载-=======================)";
            command += " && (" + "\"" + node_path + "\"" + " \"" + is_task_done_path + "\" \"" + label_outPutPath.Text + "\\" + textBox_filename.Text + "\")";  //检测是否完成下载

            //合并选项
            if (checkBox_auto_merge.Checked == true)
            {
                command += " && (cd /d \"" + label_outPutPath.Text + "\")";
                command += " && (" + "echo =======================-合并命令开始-=======================)";

                /*
                if (radioButton_mergeBinary.Checked == true)
                {
                    command += " && " //+ "\"" + logger_path + "\" -format raw -append -log \"" + log_name + "\" : "
                        + "copy /b \"" + label_outPutPath.Text + "\\" + textBox_filename.Text + "\\*.ts\" \""
                        + label_outPutPath.Text + "\\" + textBox_filename.Text + ".ts\"";
                }
                if (radioButton_mergeFFmpeg.Checked == true)
                {
                    string file_path = label_outPutPath.Text + "\\" + textBox_filename.Text;
                    command += " && cd /d \"" + file_path + "\" "
                        + " && \"" + ffmpeg_path + "\" -f concat -safe 0 -i \""
                        + label_outPutPath.Text + "\\" + textBox_filename.Text
                        + "\\ffmpeg_merge.list\" -threads 0 -c copy -f mpegts \""
                        + label_outPutPath.Text + "\\" + textBox_filename.Text + ".ts\"";
                    command += " && cd /d ..";  //调回上级目录
                }
                */

                //通过 ffmpeg_merge.list 读取全部文件，写入数组，生成转换文件
                //修改了 do_dl.js 的 114 行 o.push(`${c.name.ts}`);

                if (checkBox_IsNewMerge.Checked == true)
                {
                    command += " && (echo [转换分片中...])";
                    command += " && (for /f \"usebackq tokens=*\" %%i in (\"" + file_path + "\\ffmpeg_merge.list\") do (";  //usebackq参数，防止引号中的内容被当成字符串
                    command += " \"" + ffmpeg_path + "\" -y -i \"" + file_path + "\\%%i\" -loglevel quiet -map 0 -c copy -f mpegts -bsf:v h264_mp4toannexb \"" + file_path + "\\[TS]%%i\"";
                    command += " && del \"" + file_path + "\\%%i\"";
                    command += " && move \"" + file_path + "\\[TS]%%i\" \"" + file_path + "\\%%i\" >nul))";
                }

                /*
                command += " && (echo [合并分片中...])"
                        + " && (copy /b \"" + file_path + "\\*.ts\" \""
                        + file_path + ".ts\")";
                */

                command += " && pushd \"" + file_path  + "\"";  //进入此目录，为合并做准备

                //转换格式部分
                if (comboBox_convertFormat.SelectedIndex != 0)
                {
                    if (comboBox_convertFormat.SelectedIndex == 1) //MP4
                    {
                        /*
                        command += " && (" + "\"" + ffmpeg_path + "\"" + " -threads 0 -i \"" + file_path + ".ts\""
                            + " -c copy -y -bsf:a aac_adtstoasc \"" + file_path + ".mp4\")";
                       */
                        command += " && (" + "\"" + ffmerge_path + "\" \"" 
                            + ffmpeg_path + "\" \"" + file_path 
                            + "\\ffmpeg_merge.list\" \"" + file_path + "\" MP4)";
                    }
                    if (comboBox_convertFormat.SelectedIndex == 2) //MKV
                    {
                        command += " && (" + "\"" + ffmerge_path + "\" \""
                            + ffmpeg_path + "\" \"" + file_path
                            + "\\ffmpeg_merge.list\" \"" + file_path + "\" MKV)";
                    }
                    if (comboBox_convertFormat.SelectedIndex == 3) //FLV
                    {
                        command += " && (" + "\"" + ffmerge_path + "\" \""
                            + ffmpeg_path + "\" \"" + file_path
                            + "\\ffmpeg_merge.list\" \"" + file_path + "\" FLV)";
                    }
                    if (comboBox_convertFormat.SelectedIndex == 4) //TS
                    {
                        command += " && (" + "\"" + ffmerge_path + "\" \""
                            + ffmpeg_path + "\" \"" + file_path
                            + "\\ffmpeg_merge.list\" \"" + file_path + "\" TS)";
                    }
                }
                else  //不转换格式，采用旧版二进制合并方案
                {
                    command += " && (" + "echo =======================-合并命令开始-=======================)";
                    command += " && (copy /b \"" + file_path + "\\*.ts\" \""
                        + file_path + ".ts\")";
                }

                /*
                if (checkBox_del_afterMerge.Checked == true)
                {
                    command += " && (" + "echo =======================-删除命令开始-=======================)";
                    command += " && (ping 127.0.0.1 -n 3 >nul)";  //延迟3秒执行
                    command += " && (" //+ "\"" + logger_path + "\" -format raw -append -log \"" + log_name + "\" : "
                        + "rd /s /Q \"" + file_path + "\")";
                }
                */
            }

            command += " && (type nul >\"" + file_path + "\\isNormalExited\")";  //所有命令执行完毕后生成空白文件用于判断是否为正常退出进程

            return command.Replace(":\\\\", ":\\");  //简单粗暴解决根目录问题
        }

        public string GetSpeedCommand(string batPath)
        {
            string show_dl_speed_path = System.Windows.Forms.Application.StartupPath + "\\Tools\\m3u8_dl-js-master\\dist\\show_dl_speed.js";
            string command = "";
            command = "\"" + node_path + "\" \"" + show_dl_speed_path + "\" --retry-after 30 --retry-hide 5 --put-exit-flag \"" 
                + label_outPutPath.Text + "\\" + textBox_filename.Text + "\"";
            string file_path = batPath.Replace("m3u8_dl_GUI_Download", "m3u8_dl_GUI_Show_Speed");
            StreamWriter writer = new StreamWriter(file_path, false, Encoding.Default);  //false代表替换而不是追加
            writer.WriteLine("@echo off");
            writer.Write(command);
            writer.Close();

            return file_path;
        }

        private static string[] GetHeader(string ArrayStr)
        {
            string StrJson = ArrayStr;
            return StrJson.Split('|');
        }
    }
}
