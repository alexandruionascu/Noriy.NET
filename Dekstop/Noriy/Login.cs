using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using Microsoft.Win32;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Noriy
{
    public partial class Login : Form
    {
        //Global varaibles
        public bool SuccesfulLogin = false;
	private String Host = "http://www.noriy.net/";
        //------------
        public Login()
        {
            InitializeComponent();
            
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (userTextBox.Text.Trim().Length == 0 || passTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Username and password are required!");
            }
            else
            {

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Host + "/Token");
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string httpBody = "grant_type=password&username=" + userTextBox.Text.Trim() + "&password=" + passTextBox.Text.Trim();
                    streamWriter.Write(httpBody);
                    streamWriter.Flush();
                    streamWriter.Close();

                    try
                    {
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            JObject obj = JObject.Parse(result);
                            MessageBox.Show(result.ToString());
                            Manager.SetUsername(userTextBox.Text.Trim());
                            Manager.SetToken(obj["access_token"].ToString());

                        }

                        //successful login
                        MainForm.Guid = Manager.GetGuid();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid username or password!");
                    }
                }
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Host + "/welcome");
        }
    }
}
