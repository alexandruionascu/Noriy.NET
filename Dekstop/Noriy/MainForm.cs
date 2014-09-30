using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Configuration;
using Fiddler;
using System.Threading;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Noriy
{
    public partial class MainForm : Form
    {
        
        private NotifyIcon m_notifyicon;
        private ContextMenu m_menu;
        private RegistryKey Reg = null;
        private HttpProxy Proxy = null;
        private string Username = "null";
        private bool active = false;
        private Login LoginForm = null;
        private Admin AdminForm = null;
        public static string Guid = "";       

        public MainForm()
        {
            InitializeComponent();
            Reg = Registry.CurrentUser.OpenSubKey("Noriy",true);
            if(Reg == null)
            {
                Registry.CurrentUser.CreateSubKey("Noriy");
                Reg = Registry.CurrentUser.OpenSubKey("Noriy", true);
                Reg.SetValue("username","null");
            }
            Username = Reg.GetValue("username").ToString();

            if (Username != "null")
            {
                Proxy = new HttpProxy();
                notifyIcon1.Text = "Logged as " + Username;
            }
            else
            {
                notifyIcon1.Text = "Please log in!";
            }

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
           
            Text = "Tray icons program";
            m_menu = new ContextMenu();

            //LOAD MENU PROPERLY
            LoadMenu(Username == "null");

            notifyIcon1.Visible = true;
            notifyIcon1.ContextMenu = m_menu;
           
        }


        public void LoadMenu(bool UserLogged) //if the user is logged
        {
            m_menu.Dispose();

            if (UserLogged == false)  
            {
                m_menu.MenuItems.Add(0, new MenuItem("Settings", new EventHandler(Settings_Click)));
                m_menu.MenuItems.Add(1, new MenuItem("Logout", new EventHandler(Logout_Click)));
                m_menu.MenuItems.Add(2, new MenuItem("Exit", new EventHandler(Exit_Click)));
            }
            else
            {
                m_menu.MenuItems.Add(0, new MenuItem("Login", new EventHandler(Login_Click)));
                m_menu.MenuItems.Add(1, new MenuItem("Exit", new EventHandler(Exit_Click)));
            }

        }
        protected void Exit_Click(Object sender, System.EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?\nClosing the application being logged in will block the entire web traffic!", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
        protected void Login_Click(Object sender, System.EventArgs e)
        {
            if (LoginForm != null)
            {
                if (LoginForm.IsDisposed == false)
                {
                    LoginForm.Activate();
                    LoginForm.WindowState = FormWindowState.Normal;
                }
                else
                {
                    LoginForm = new Login();
                    LoginForm.FormClosed += new FormClosedEventHandler(LoginForm_FormClosed);
                    LoginForm.Show();
                }
            }
            else
            {
                LoginForm = new Login();
                LoginForm.FormClosed += new FormClosedEventHandler(LoginForm_FormClosed);
                LoginForm.Show();

            }
            
        }
        protected void Settings_Click(Object sender, System.EventArgs e)
        {
            if (AdminForm != null)
            {
                if (AdminForm.IsDisposed == false)
                {
                    AdminForm.Show();
                    AdminForm.Activate();

                }
                else
                {
                    AdminForm = new Admin(Reg.GetValue("username").ToString());
                    AdminForm.Show();
                }
            }
            else
            {
                AdminForm = new Admin(Reg.GetValue("username").ToString());
                AdminForm.Show();
            }
        }
        protected void Logout_Click(Object sender, System.EventArgs e)
        {
            LoadMenu(true);
            if (Proxy != null)
            {
                Proxy.Dispose();
	    }
       
            Logout LogoutForm = new Logout();
            LogoutForm.FormClosed += new FormClosedEventHandler(LogoutForm_FormClosed);
            LogoutForm.Show();

        }

        private void LogoutForm_FormClosed(object sender, EventArgs e)
        {
            LoadMenu(Reg.GetValue("username").ToString() == "null"); // load menu again if needed

            if(Manager.GetUsername() == "null")
            {
                Proxy.Dispose();
                notifyIcon1.Text = "Please log in!";
            }
        }     

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (active == false)
            {
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
               
                active = true;
            }
        }

        private void LoginForm_FormClosed(object sender, EventArgs e)
        {
            LoadMenu(Reg.GetValue("Username").ToString() == "null"); // load menu again if needed

            if (Reg.GetValue("Username").ToString() != "null")
                notifyIcon1.Text = "Logged as " + Reg.GetValue("Username").ToString();

            if (LoginForm != null)
            {
                if (LoginForm.SuccesfulLogin == true)
                {
                    Proxy = new HttpProxy();
                }
            }
        }


        public void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm TheForm = new MainForm();
            TheForm.InitializeComponent();
            Application.Run(TheForm);
        }
       

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Manager.GetUsername() == "null")
            {
                LoginForm = new Login();
                LoginForm.FormClosed += new FormClosedEventHandler(LoginForm_FormClosed);
                LoginForm.Show();

            }
            else
            {
                AdminForm = new Admin(Manager.GetUsername());
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            this.Hide();
        }
    }

   
}
