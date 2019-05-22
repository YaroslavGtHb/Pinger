using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;

namespace Pinger
{
    public class ICMPPinger : IIcmpPinger
    {
        private List<string> _rowhosts;
        private string _logpath;

        public ICMPPinger(List<string> rowhosts, string logpath)
        {
            _rowhosts = rowhosts;
            _logpath = logpath;
        }

        public Dictionary<string, string> Ping()
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();
            Ping ping = new Ping();
            foreach (var rowhost in _rowhosts)
            {
                try
                {
                    PingReply pingReply = ping.Send(rowhost);

                    if (pingReply.Status.ToString() == "Success")
                    {
                        answer.Add(rowhost, "OK");
                    }
                    else
                    {
                        answer.Add(rowhost, "FAILED");
                    }
                }
                catch (PingException)
                {
                    answer.Add(rowhost, "FAILED");
                }
            }

            return answer;
        }

        public void Logging(string host, string responce)
        {
            using (var writer = new StreamWriter(_logpath, true))
            {
                writer.WriteLine(DateTime.Now + " " + host + " " + responce);
            }
        }
    }
}