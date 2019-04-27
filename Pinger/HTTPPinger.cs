using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace Pinger
{
    class HTTPPinger : IPinger
    {
        public Dictionary<string, string> Ping(List<string> rowhosts)
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();
            foreach (var rowhost in rowhosts)
            {
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest
                        .Create(rowhost);
                    webRequest.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                    
                    if (response.StatusCode.ToString() != null) answer.Add(rowhost, "OK (" + response.StatusCode + ")");
                }
                catch (PingException)
                {
                    answer.Add(rowhost, "FAILED");
                }            
            }
            return answer;          
        }

        public void PingAndLogging(Dictionary<string, string> pingedhosts, string logpath)
        {
            foreach (var pingedhost in pingedhosts)
            {
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest
                        .Create(pingedhost.Key);
                    webRequest.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();

                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.WriteLine(DateTime.Now + " " + pingedhost.Key + " " + "OK (" + response.StatusCode + ")");
                    }
                }
                catch (ArgumentException)
                {
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.WriteLine(DateTime.Now + " " + pingedhost.Key + " " + "FAILED");
                    }
                }
            }
        }





    }
}
