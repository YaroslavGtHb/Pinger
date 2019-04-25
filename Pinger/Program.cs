using System;
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
                .Create("");
            webRequest.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            //http://qaru.site/questions/30019/getting-http-status-code-number-200-301-404-etc-from-httpwebrequest-and-httpwebresponse

            Console.Write(response.StatusCode.ToString());

        }
    }
}
