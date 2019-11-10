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
        private readonly IConfigurationRoot _configuration = Startup.Builder.Build();

        public async Task<Dictionary<string, string>> Ping(Dictionary<string, string> rowhosts)
        {
            var answer = new Dictionary<string, string>();
            var ping = new Ping();
            foreach (var rowhost in rowhosts.Keys)
            {
                Console.WriteLine(DateTime.Now);
                try
                {
                    var pingReply = ping.Send(rowhost);

                    if (pingReply != null && pingReply.Status.ToString() == "Success")
                        ShowStatusConsole(ref answer, rowhost, true);
                    else
                        ShowStatusConsole(ref answer, rowhost, false);
                }
                catch (PingException)
                {
                    ShowStatusConsole(ref answer, rowhost, false);
                }

                ConsoleLogging(rowhost);
            }

            return answer;
        }
    }
}