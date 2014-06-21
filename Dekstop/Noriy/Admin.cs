using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;

namespace Noriy
{
    public partial class Admin : Form
    {
        //Global Variables
        private string StartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        private RegistryKey RKey = Registry.CurrentUser.OpenSubKey("Noriy", true);

        //------------

        public Admin(string username)
        {
            InitializeComponent();
            label1.Text = "Logged as " + username;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            if(RKey.GetValue("RegisterTraffic") == null)
            {
                RKey.SetValue("RegisterTraffic", "false");
            }

            if (RKey.GetValue("RegisterTraffic").ToString() != "false")
            {
                checkBox1.CheckState = CheckState.Checked;
            }
        }

        //run the program at startup
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                WshShell Shell = new WshShell();
                string ShortcutAddress = StartupFolder + @"\TrafficControlProject.lnk";
                IWshShortcut shortcut = (IWshShortcut)Shell.CreateShortcut(ShortcutAddress);
                shortcut.Description = "If deleted, then LaunchOnStartup.exe will not launch on Windows Startup";
                shortcut.WorkingDirectory = Application.StartupPath;
                shortcut.TargetPath = Application.ExecutablePath;
                shortcut.Save();
                MessageBox.Show("Done!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:59873/Basic_Registration/Home.aspx");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                RKey.SetValue("RegisterTraffic", "true");
            }
            else
            {
                RKey.SetValue("RegisterTraffic", "false");
            }
        }
    }
}
