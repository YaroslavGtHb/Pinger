using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Pinger.Intefaces;
using Pinger.Properties;

namespace Pinger.Realizations
{
    public class IcmpPinger : Loger, IIcmpPinger
    {
        private string Okanswer { get; } = "OK";
        private string Failedanswer { get; } = "FAILED";
        private readonly List<string> _rowhosts;
        private string _logpath;
        public IcmpPinger(List<string> rowhosts, string logpath)
        {
            _rowhosts = rowhosts;
            _logpath = logpath;
        }
        public async Task<Dictionary<string, string>> Ping()
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
                        answer.Add(rowhost, Okanswer);
                    }
                    else
                    {
                        answer.Add(rowhost, Failedanswer);
                    }
                }
                catch (PingException)
                {
                    answer.Add(rowhost, Failedanswer);
                }
            }
            return answer;
        }
    }
}