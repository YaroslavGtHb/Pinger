using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Pinger
{
    class Program
    {
        static void Main()
        {
            //Console.WriteLine("Hello World!");
            //var hosts = File.ReadAllLines("./hosts.txt");
            //List<string> hostslist = new List<string>(hosts);
            //ICMPPinger pinger = new ICMPPinger();
            //var hostcollection = pinger.Ping(hostslist);
            //foreach (var host in hostcollection)
            //{
            //    Console.WriteLine("Host: " + host.Key);
            //    Console.WriteLine("Status: " + host.Value);
            //}


            //pinger.PingAndLogging(hostcollection, "./log.txt");

            //Console.ReadKey();
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest
                .Create("https://www.google.com/");
            webRequest.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            //Returns "MovedPermanently", not 301 which is what I want.

            Console.Write(response.StatusCode.ToString());

        }
    }
}
