using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Pinger
{
    class HTTPPinger : IPinger
    {
        private List<string> _rowhosts;
        private string _logpath;
        private int _statuscode;

        public HTTPPinger(List<string> rowhosts, string logpath, int statuscode)
        {
            _rowhosts = rowhosts;
            _logpath = logpath;
            _statuscode = statuscode;
        }

        public Dictionary<string, string> Ping()
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();
            foreach (var rowhost in _rowhosts)
            {
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest) WebRequest
                        .Create(rowhost);
                    webRequest.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse) webRequest.GetResponse();

                    if (response.StatusCode.ToString() != null && (int)response.StatusCode == _statuscode) answer.Add(rowhost, response.StatusCode.ToString());
                }
                catch (PingException)
                {
                    answer.Add(rowhost, "FAILED");
                }
                catch (ArgumentException)
                {
                    answer.Add(rowhost, "FAILED");
                }
                catch (WebException)
                {
                    answer.Add(rowhost, "FAILED");
                }
                catch (UriFormatException)
                {
                    answer.Add(rowhost, "FAILED");
                }
                catch (FormatException)
                {
                    answer.Add(rowhost, "FAILED");
                }
                catch (SocketException)
                {
                    answer.Add(rowhost, "FAILED");
                }
                
            }
            return answer;          
        }

        public void Logging(string host, string responce)
        {
            if (responce == _statuscode.ToString())
            {
                responce = "OK";
            }

            else
            {
                responce = "FAILED";
            }

            using (var writer = new StreamWriter(_logpath, true))
            {
                writer.WriteLine(DateTime.Now + " " + host + " " + responce);
            }

        }
    }
}
