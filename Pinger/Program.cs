using System;
using System.Collections.Generic;
using System.IO;

namespace Pinger
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");
            var hosts = File.ReadAllLines("./hosts.txt");
            List<string> hostslist = new List<string>(hosts);
            Pinger pinger = new Pinger();
            var hostcollection = pinger.Ping(hostslist);
            foreach (var host in hostcollection)
            {
                Console.WriteLine("Host: " + host.Key);
                Console.WriteLine("Status: " + host.Value);
            }


            pinger.PingAndLogging(hostcollection, "./log.txt");

            Console.ReadKey();
        }
    }
}
