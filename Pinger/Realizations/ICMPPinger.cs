using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pinger.Intefaces;

namespace Pinger.Realizations
{
    public class IcmpPinger : Loger, IIcmpPinger
    {
        private IConfigurationRoot Configuration = Startup.builder.Build();

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
                
                try
                {
                    PingReply pingReply = ping.Send(rowhost);

                    if (pingReply != null && pingReply.Status.ToString() == "Success")
                    {
                        answer.Add(rowhost, Okanswer);
                        Console.WriteLine(Okanswer);
                    }
                    else
                    {
                        answer.Add(rowhost, Failedanswer);
                        Console.WriteLine(Failedanswer);
                    }
                }
                catch (PingException)
                {
                    answer.Add(rowhost, Failedanswer);
                    Console.WriteLine(Failedanswer);
                }
                Console.WriteLine("Host: " + rowhost);
                Console.WriteLine("Period: " + Configuration["Period"]);
                Console.WriteLine("Protocol: " + Configuration["Protocol"]);
                Console.WriteLine();
            }
            return answer;
        }
    }
}