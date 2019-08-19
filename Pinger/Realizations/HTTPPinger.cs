using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Pinger.Intefaces;
using Pinger.Properties;

namespace Pinger.Realizations
{
    public class HttpPinger : IHttpPinger
    {
        private string Okanswer { get;} = "OK";
        private string Failedanswer { get;} = "FAILED";
        private readonly List<string> _rowhosts;
        private string _logpath;
        public HttpPinger(List<string> rowhosts, string logpath)
        {
            _rowhosts = rowhosts;
            _logpath = logpath;
        }
        public async Task<Dictionary<string, string>> Ping()
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
                        answer.Add(rowhost, Okanswer);
                    else
                    {
                        answer.Add(rowhost, Failedanswer);
                    }

                }
                catch (PingException)
                {
                    answer.Add(rowhost, Failedanswer);
                }
                catch (ArgumentException)
                {
                    answer.Add(rowhost, Failedanswer);
                }
                catch (WebException)
                {
                    answer.Add(rowhost, Failedanswer);
                }
                catch (UriFormatException)
                {
                    answer.Add(rowhost, Failedanswer);
                }
                catch (FormatException)
                {
                    answer.Add(rowhost, Failedanswer);
                }
                catch (SocketException)
                {
                    answer.Add(rowhost, Failedanswer);
                }
            }
            return answer;
        }
        public void Logging(string host, string responce)
        {
            string answerline = DateTime.Now + " " + host + " " + responce;
            try
            {
                using (var writer = new StreamWriter(Settings.Logpath, true))
                {
                    writer.WriteLine(answerline);
                }
                Console.WriteLine(answerline);
            }
            catch (DirectoryNotFoundException)
            {
                using (var writer = new StreamWriter(_logpath, true))
                {
                    writer.WriteLine(answerline);
                }
                Console.WriteLine(answerline);
            }
        }
    }
}