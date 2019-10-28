﻿using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pinger.Intefaces;

namespace Pinger.Realizations
{
    public class IcmpPinger : Loger, IIcmpPinger
    {
        private string Okanswer { get; } = "OK";
        private string Failedanswer { get; } = "FAILED";
        private readonly List<string> _rowhosts;
        public IcmpPinger(List<string> rowhosts)
        {
            _rowhosts = rowhosts;
        }
        public async Task<Dictionary<string, string>> Ping()
        {
            var consoleloger = new ConsoleLoger();
            Dictionary<string, string> answer = new Dictionary<string, string>();
            Ping ping = new Ping();
            foreach (var rowhost in _rowhosts)
            {
                Console.WriteLine(DateTime.Now);
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
                
                consoleloger.Show(rowhost);
            }
            return answer;
        }
    }
}