using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace Pinger
{
    public class TCPPinger
    {
        Dictionary<string, string> Ping(List<string> rowhosts)
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();


            foreach (var rowhost in rowhosts)
            {
                try
                {
                    IPAddress[] ip = Dns.GetHostAddresses(rowhost);

                    EndPoint endPoint = new IPEndPoint(ip[0], 80);
                    var times = new List<double>();
                    for (int i = 0; i < 4; i++)
                    {
                        var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        sock.Blocking = true;

                        var stopwatch = new Stopwatch();

                        stopwatch.Start();
                        sock.Connect(endPoint);
                        stopwatch.Stop();

                        double t = stopwatch.Elapsed.TotalMilliseconds;
                        times.Add(t);

                        sock.Close();

                        Thread.Sleep(1000);
                    }

                    answer.Add(rowhost, "OK (" + "MinTime: " + times.Min() + "MaxTime: " + times.Max() + "Average: " + times.Average() + " " + ")");
                }
                catch (SocketException)
                {
                    answer.Add(rowhost, "FAILED");
                }
            }
            return answer;
        }

        public void PingAndLogging(Dictionary<string, string> pingedhosts, string logpath)
        {
            Ping ping = new Ping();

            foreach (var pingedhost in pingedhosts)
            {
                try
                {
                    IPAddress[] ip = Dns.GetHostAddresses(pingedhost.Key);

                    EndPoint endPoint = new IPEndPoint(ip[0], 80);
                    var times = new List<double>();
                    for (int i = 0; i < 4; i++)
                    {
                        var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        sock.Blocking = true;

                        var stopwatch = new Stopwatch();

                        stopwatch.Start();
                        sock.Connect(endPoint);
                        stopwatch.Stop();

                        double t = stopwatch.Elapsed.TotalMilliseconds;
                        times.Add(t);

                        sock.Close();

                        Thread.Sleep(1000);
                    }
                        using (var writer = new StreamWriter(logpath, true))
                        {
                            writer.WriteLine(DateTime.Now + "OK (" + "MinTime: " + times.Min() + "MaxTime: " + times.Max() + "Average: " + times.Average() + " " + ")");
                        }
                }
                catch (SocketException)
                {
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.WriteLine(DateTime.Now + " " + pingedhost.Key + " " + "FAILED");
                    }
                }

            }
        }
    }
}
