using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CrossGateHelper
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AutoRegCom("regsvr32 dm.dll /s");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            Application.Run(new MainForm());
            
        }

        static string AutoRegCom(string strCmd)
        {
            strCmd = "regsvr32 dm.dll /s";
            string rInfo;
            try
            {
                Process myProcess = new Process();
                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo("cmd.exe");
                myProcessStartInfo.UseShellExecute = false;
                myProcessStartInfo.CreateNoWindow = true;
                myProcessStartInfo.RedirectStandardOutput = true;
                myProcess.StartInfo = myProcessStartInfo;
                myProcessStartInfo.Arguments = "/c " + strCmd;
                myProcess.Start();
                StreamReader myStreamReader = myProcess.StandardOutput;
                rInfo = myStreamReader.ReadToEnd();
                myProcess.Close();
                rInfo = strCmd + "\r\n" + rInfo;
                return rInfo;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        static void OnApplicationExit(object sender, EventArgs e)
        {
            
            MainForm.CG.OnExit();
        }
    }
}
