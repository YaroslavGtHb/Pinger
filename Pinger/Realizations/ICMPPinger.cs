using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Pinger.Intefaces;
using Pinger.Properties;

namespace Pinger.Realizations
{
    public class IcmpPinger : IIcmpPinger
    {
        private string okanswer { get; } = "OK";
        private string failedanswer { get; } = "FAILED";
        private List<string> _rowhosts;
        private string _logpath;
        public IcmpPinger(List<string> rowhosts, string logpath)
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
                Console.WriteLine("Host: " + rowhost);
                Console.WriteLine("Period: " + Settings.Period);
                Console.WriteLine("Protocol: " + Settings.Protocol);
                Console.WriteLine();
                try
                {
                    PingReply pingReply = ping.Send(rowhost);

                    if (pingReply != null && pingReply.Status.ToString() == "Success")
                    {
                        answer.Add(rowhost, okanswer);
                    }
                    else
                    {
                        answer.Add(rowhost, failedanswer);
                    }
                }
                catch (PingException)
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