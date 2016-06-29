using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TestLauncher
{
    public partial class App : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public App()
        {
            InitializeComponent();
            buttonGo.Visible = VerificationIntegrite();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.ActiveControl = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            this.ActiveControl = null;
        }

        private void App_Load(object sender, EventArgs e)
        {
            buttonGo.Enabled = false;// Verification();
        }

        private void StartDragWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            //bool test = Util.IsApplicationInstalled("Arma3");
            this.ActiveControl = null;
            Process firstProc = new Process();
            firstProc.StartInfo.FileName = "notepad.exe";
            firstProc.EnableRaisingEvents = true;
        }

        private string GetArma3Path()
        {
            
            return null;
        }

        public void Log(string shortMessage)
        {
            if(shortMessage != null)
                labelLog.Text = shortMessage;
        }

        public void Log(string shortMessage, Color color)
        {
            labelLog.ForeColor = color;
            Log(shortMessage);
        }

        private bool VerificationIntegrite()
        {
            var steamPath = Util.GetSteamPath();
            if(steamPath == null) { Log("Steam est introuvable");return false;}
            var arma3Path = Util.GetArma3Path(steamPath);
            if (arma3Path == null) { Log("Arma 3 est introuvable"); return false; }
            var teamSpeakPath = Util.GetTeamSpeakPath();
            if(teamSpeakPath == null) { Log("TeamSpeak 3 Client est introuvable"); return false; }
            Log("Verification OK");
            return true;
        }
    }
}
