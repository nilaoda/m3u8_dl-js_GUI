using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3U8_DL_GUI
{
    public partial class Form1 : Form
    {
        private void RealAction2(string StartFileName)
        {
            Process p = new Process();
            p.StartInfo.FileName = StartFileName;
            p.StartInfo.UseShellExecute = false;    //必须为false才能重定向输出
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.OutputDataReceived += new DataReceivedEventHandler(P_OutputDataReceived);
            p.Start();
            PID_2 = p.Id; //PID
            p.BeginOutputReadLine();
        }

        private delegate void AddMessageHandler(string msg);

        private void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            AddMessageHandler handler = delegate (string msg)
            {
                this.label_speed_out.Text = msg;
            };
            if (this.label_speed_out.InvokeRequired)
                this.label_speed_out.Invoke(handler, e.Data);
        }
    }
}
