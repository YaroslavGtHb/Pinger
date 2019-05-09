using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace Pinger
{
    class HTTPPinger : IPinger
    {
        private List<string> _rowhosts;
        private Dictionary<string, string> _pingedhosts;
        private string _logpath;

        public HTTPPinger(List<string> rowhosts, Dictionary<string, string> pingedhosts, string logpath)
        {
            _rowhosts = rowhosts;
            _pingedhosts = pingedhosts;
            _logpath = logpath;
        }

        public Dictionary<string, string> Ping()
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();
            foreach (var rowhost in _rowhosts)
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

        public void PingAndLogging()
        {
            foreach (var pingedhost in _pingedhosts)
            {
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest
                        .Create(pingedhost.Key);
                    webRequest.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();

                    using (var writer = new StreamWriter(_logpath, true))
                    {
                        writer.WriteLine(DateTime.Now + " " + pingedhost.Key + " " + "OK (" + response.StatusCode + ")");
                    }
                }
                catch (ArgumentException)
                {
                    using (var writer = new StreamWriter(_logpath, true))
                    {
                        writer.WriteLine(DateTime.Now + " " + pingedhost.Key + " " + "FAILED");
                    }
                }
            }
        }





    }
}
