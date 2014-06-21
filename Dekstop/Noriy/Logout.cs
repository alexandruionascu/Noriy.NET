using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Noriy
{
    public partial class Logout : Form
    {
        private RegistryKey Reg = Registry.CurrentUser.OpenSubKey("Noriy", true);

        public Logout()
        {
            InitializeComponent();
            userLabel.Text = Manager.GetUsername();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (passTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter the password!");
            }
            else
            {

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:6649/Token");
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string httpBody = "grant_type=password&username=" + Manager.GetUsername() + "&password=" + passTextBox.Text.Trim();
                    streamWriter.Write(httpBody);
                    streamWriter.Flush();
                    streamWriter.Close();

                    try
                    {
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        MessageBox.Show(httpResponse.StatusCode.ToString());
                        Manager.SetUsername("null");
                        Manager.SetToken("null");

                        //successful logout
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid password!");
                    }
                }
            }
        }

    

       
    }
}
