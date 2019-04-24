using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;

namespace Pinger
{
    public class Pinger
    {
        public Dictionary<string, string> Ping(List<string> rowhosts)
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();
            Ping ping = new Ping();
            foreach (var rowhost in rowhosts)
            {
                try
                {
                    PingReply pingReply = ping.Send(rowhost);
                    if (pingReply != null) answer.Add(pingReply.Address.ToString(), pingReply.Status.ToString());
                }
                catch (PingException e)
                {
                    Console.Write(DateTime.Now + " " + e.Message);
                }            
            }
            return answer;
        }

        public void PingAndLogging (Dictionary<string, string> pingedhosts, string logpath)
        {
            Ping ping = new Ping();

            foreach (var pingedhost in pingedhosts)
            {
                try
                {
                    PingReply pingReply = ping.Send(pingedhost.Key);
                    if (pingReply != null && pingReply.Status.ToString() != pingedhost.Value)
                    {
                        using (var writer = new StreamWriter(logpath, true))
                        {
                             writer.WriteLine(DateTime.Now + " " + pingReply.Address + " " + pingReply.Status);
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        Console.WriteLine(DateTime.Now + " " + e.Message);
                    }
                }

            }
            
        }
    }
}
