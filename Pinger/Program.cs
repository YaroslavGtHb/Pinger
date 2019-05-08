using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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


            //HttpWebRequest webRequest = (HttpWebRequest)WebRequest
            //    .Create("http://google.com");
            //webRequest.AllowAutoRedirect = false;
            //HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            ////http://qaru.site/questions/30019/getting-http-status-code-number-200-301-404-etc-from-httpwebrequest-and-httpwebresponse
            ////https://stackoverflow.com/questions/26067342/how-to-implement-psping-tcp-ping-in-c-sharp


            //Console.Write(response.StatusCode.ToString());

             
            IPAddress[] ip = Dns.GetHostAddresses("google123.com");
            //System.Net.Sockets.SocketException: "Этот хост неизвестен"
            EndPoint endPoint = new IPEndPoint(ip[0], 80);
            var times = new List<double>();
            for (int i = 0; i < 4; i++)
            {
                var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sock.Blocking = true;

                var stopwatch = new Stopwatch();

                // Measure the Connect call only
                stopwatch.Start();
                sock.Connect(endPoint);
                stopwatch.Stop();

                double t = stopwatch.Elapsed.TotalMilliseconds;
                Console.WriteLine("{0:0.00}ms", t);
                times.Add(t);

                sock.Close();

                Thread.Sleep(1000);
            }
            Console.WriteLine("{0:0.00} {1:0.00} {2:0.00}", times.Min(), times.Max(), times.Average());

        }
    }
}
