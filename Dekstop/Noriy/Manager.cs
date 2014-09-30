using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;

namespace Noriy
{
    public static class Manager
    {
	private String Host = "www.noriy.net/api/";

        #region RegistryManage
        //Creates registry key
        private static void CreateRegistry()
        {
            RegistryKey RKey = Registry.CurrentUser.CreateSubKey("Noriy");
            RKey = Registry.CurrentUser.OpenSubKey("Noriy", true);
            RKey.SetValue("Username", "null");
            RKey.SetValue("RegisterTraffic", "true");
            RKey.SetValue("Token", "null");
        }

        //Gets the stored username
        public static string GetUsername()
        {
            RegistryKey RKey = Registry.CurrentUser.OpenSubKey("Noriy", true);
            if (RKey == null)
            {
                CreateRegistry();
                return "null";
            }
            else
            {
                return RKey.GetValue("Username").ToString(); 
            }

        }

        //returns true if the user have set to register the traffic
        public static bool GetRegisterTraffic()
        {
            RegistryKey RKey = Registry.CurrentUser.OpenSubKey("Noriy", true);
            if (RKey == null)
            {
                CreateRegistry();
                return true;
            }
            else
            {
                if (RKey.GetValue("RegisterTraffic").ToString() == "true")
                    return true;
                else return false;
            }
        }

        public static string GetToken()
        {
            RegistryKey RKey = Registry.CurrentUser.OpenSubKey("Noriy", true);
            if (RKey == null)
            {
                CreateRegistry();
                return "null";
            }
            else
            {
                return RKey.GetValue("Token").ToString();
            }

        }

        public static void SetUsername(string username)
        {
            RegistryKey RKey = Registry.CurrentUser.OpenSubKey("Noriy", true);
            //if its null, then create the registry first
            if (RKey == null)
                CreateRegistry();

            RKey.SetValue("Username", username);
           

        }

        public static void SetRegistration(bool register)
        {
            RegistryKey RKey = Registry.CurrentUser.OpenSubKey("Noriy", true);
            if (RKey == null)
                CreateRegistry();
            if (register == true)
            {
                RKey.SetValue("RegisterTraffic", "true");
            }
            else
            {
                RKey.SetValue("RegisterTraffic", "false");
            }

        }

        public static void SetToken(string token)
        {
            RegistryKey RKey = Registry.CurrentUser.OpenSubKey("Noriy", true);
            if (RKey == null)
                CreateRegistry();
            RKey.SetValue("Token", token);

        }

        #endregion

        #region WebApi

        //Get the Guid
        public static string GetGuid()
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Host + "account/userinfo");
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers.Add("Authorization", "bearer " + Manager.GetToken());


                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject obj = JObject.Parse(result);
                    return obj["UserId"].ToString();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static int Check(string url)
        {
            string domain = "";
            int index = url.IndexOf('.');
            if(index > 0)
                domain = url.Substring(0, index);
            else
                domain = url;

            if (domain.Trim() == "www")
                domain = url;

            


            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Host + "/api/Check");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"Id\":\"" + Guid.NewGuid().ToString() + "\",\"UserId\":\"" + GetGuid() + "\"," +
                "\"Url\":\"" + domain + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        if(result.ToString() != "0")
                            MessageBox.Show(result.ToString());
                      
                        return Convert.ToInt32(result.ToString());
                    }

                    return 0;


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return 1;
                }
            }


        }


        #endregion
    }
}
