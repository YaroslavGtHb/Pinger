using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Pinger.Intefaces;
using Pinger.Properties;

namespace Pinger.Realizations
{
    public class HttpPinger : IHttpPinger
    {
        private string okanswer { get;} = "OK";
        private string failedanswer { get;} = "FAILED";
        private List<string> _rowhosts;
        private string _logpath;
        public HttpPinger(List<string> rowhosts, string logpath)
        {
            _rowhosts = rowhosts;
            _logpath = logpath;
        }
        public Dictionary<string, string> Ping()
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();
            foreach (var rowhost in _rowhosts)
            {
                Console.WriteLine("Host: " + rowhost);
                Console.WriteLine("Period: " + Settings.Period);
                Console.WriteLine("Protocol: " + Settings.Protocol);
                Console.WriteLine();
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest) WebRequest
                        .Create(rowhost);
                    webRequest.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse) webRequest.GetResponse();
                    if (response.StatusCode.ToString() != null && (int) response.StatusCode == Int32.Parse(Settings.Httpvalidcode))
                        answer.Add(rowhost, okanswer);
                    else
                    {
                        answer.Add(rowhost, failedanswer);
                    }
                }
                catch (PingException)
                {
                    answer.Add(rowhost, failedanswer);
                }
                catch (ArgumentException)
                {
                    answer.Add(rowhost, failedanswer);
                }
                catch (WebException)
                {
                    answer.Add(rowhost, failedanswer);
                }
                catch (UriFormatException)
                {
                    answer.Add(rowhost, failedanswer);
                }
                catch (FormatException)
                {
                    answer.Add(rowhost, failedanswer);
                }
                catch (SocketException)
                {
                    answer.Add(rowhost, failedanswer);
                }
            }
            return answer;
        }
        public void Logging(string host, string responce)
        {
            try
            {
                using (var writer = new StreamWriter("./Logs.txt", true))
                {
                    writer.WriteLine(DateTime.Now + " " + host + " " + responce);
                }
            }
            catch (DirectoryNotFoundException)
            {
                using (var writer = new StreamWriter(_logpath, true))
                {
                    writer.WriteLine(DateTime.Now + " " + host + " " + responce);
                }
            }
        }
    }
}