using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.Data;
using Fiddler;
using System.Configuration;
using System.Threading;
using System.IO;
using System.Net;

namespace Noriy
{
    public class HttpProxy  : IDisposable
    {
        public void Dispose()
        {
            Fiddler.FiddlerApplication.Shutdown();
        }

        public static string RedirectUrl = "www.google.ro";
        //Constructor
        public HttpProxy()
        {
            //Add the events
            Fiddler.FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;
            Fiddler.FiddlerApplication.AfterSessionComplete += FiddlerApplication_AfterSessionComplete;

             // Initial: Fiddler.FiddlerApplication.Startup(8888, true, true);
             Fiddler.FiddlerApplication.Startup(8888, FiddlerCoreStartupFlags.Default);
        }

        //Before Request
        void FiddlerApplication_BeforeRequest(Fiddler.Session oSession)
        {
            //if (Manager.Check(oSession.url) > 0)
            //{
            //    oSession.url = RedirectUrl;
            //}

            MessageBox.Show(oSession.url);
        }

        //AfterSessionComplete
        void FiddlerApplication_AfterSessionComplete(Fiddler.Session oSession)
        {
            Thread thread = new Thread(() => PostUrl(oSession));
            thread.Start();
        }

        void PostUrl(Fiddler.Session oSession)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:6649/api/Traffic");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.Headers.Add("Authorization", "bearer " + Manager.GetToken());

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"Id\":\"" + Guid.NewGuid().ToString() + "\",\"UserId\":\"" + Manager.GetGuid() + "\"," +
                    "\"Url\":\"" + oSession.url + "\"," + "\"Time\":\"" + DateTime.Now.ToString() + "\"}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();

                    try
                    {
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Server not responding");
            }


        }

        //bool CheckUrl(Fiddler.Session oSession)
        //{
        //    string url = oSession.url;

        //    //Check if it is in custom table
        //    MySqlConnection Conn = new MySqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);
        //    try
        //    {

        //        RegistryKey reg = Registry.CurrentUser.OpenSubKey("Noriy");
        //        string username = reg.GetValue("username").ToString();

        //        int size1 = url.IndexOf('/');

        //        string domain1 = "";

        //        string domain2 = url;

        //        if (url.Substring(0, 4) == "www.")
        //        {
        //            domain2 = url.Substring(4, domain2.Length - 4);
        //        }

        //        int size2 = domain2.IndexOf('/');

        //        if (size1 < 1)
        //        {
        //            domain1 = url;
        //        }
        //        else
        //        {
        //            domain1 = url.Substring(0, size1);
        //        }

        //        if (size2 > 1)
        //        {
        //            domain2 = domain2.Substring(0, size2);
        //        }

        //        if (domain1 != domain2)
        //        {

        //            Conn.Open();

        //            string searchQuery1 = "select count(*) from " + username + "_custom where Url like '%" + domain1 + "%'";
        //            string searchQuery2 = "select count(*) from " + username + "_custom where Url like '%" + domain2 + "%'";

        //            MySqlCommand Command1 = new MySqlCommand(searchQuery1, Conn);
        //            MySqlCommand Command2 = new MySqlCommand(searchQuery2, Conn);

        //            int count1 = Convert.ToInt32(Command1.ExecuteScalar().ToString());
        //            int count2 = Convert.ToInt32(Command2.ExecuteScalar().ToString());

        //            if (count1 > 0 || count2 > 0)
        //            {
        //                return false;
        //            }

        //            Conn.Close();
        //        }
        //        else
        //        {
        //            Conn.Open();
        //            string searchQuery2 = "select count(*) from " + username + "_custom where Url like '%" + domain2 + "%'";
        //            MySqlCommand Command2 = new MySqlCommand(searchQuery2, Conn);
        //            int count2 = Convert.ToInt32(Command2.ExecuteScalar().ToString());



        //            if (count2 > 0)
        //            {
        //                return false;
        //            }

        //            Conn.Close();


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }

        //    return true;

        //}

        //void InsertUrl(string url, bool Accepted)
        //{
        //    RegistryKey reg = Registry.CurrentUser.OpenSubKey("Noriy");

        //    if (reg.GetValue("username").ToString() != "null")
        //    {
        //        try
        //        {
        //            if (url.Length < 32) //The maximum length of the url
        //            {
        //                using (MySqlConnection Connection = new MySqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]))
        //                {
        //                    Connection.Open();
        //                    string InsertTraffic = "";


        //                    if(Accepted == true) //Checks if the url has been accepted or rejected
        //                        InsertTraffic = "INSERT INTO " + reg.GetValue("username").ToString() + "_traffic (Url,Time,Accepted) VALUES('" + url + "','" + DateTime.Now.ToString() + "','True');";
        //                    else InsertTraffic = "INSERT INTO " + reg.GetValue("username").ToString() + "_traffic (Url,Time,Accepted) VALUES('" + url + "','" + DateTime.Now.ToString() + "','False');";


        //                    using (MySqlCommand Command = new MySqlCommand(InsertTraffic, Connection))
        //                    {
        //                        Command.ExecuteNonQuery();
        //                    }

        //                }

        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }

        //    }
         
        //}



        
    }
}
