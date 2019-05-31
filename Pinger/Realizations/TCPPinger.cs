using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Pinger.Intefaces;

namespace Pinger.Realizations
{
    public class TcpPinger : ITcpPinger
    {
        private List<string> _rowhosts;
        private string _logpath;
        private Settings _settings = new Settings();

        public TcpPinger(List<string> rowhosts, string logpath)
        {
            _rowhosts = rowhosts;
            _logpath = logpath;
        }

        public Dictionary<string, string> Ping()
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();


            foreach (var rowhost in _rowhosts)
            {
                Console.WriteLine("Host: " + rowhost);
                Console.WriteLine("Period: " + _settings.Period);
                Console.WriteLine("Protocol: " + _settings.Protocol);
                Console.WriteLine();

                try
                {
                    IPAddress[] ip = Dns.GetHostAddresses(rowhost);

                    EndPoint endPoint = new IPEndPoint(ip[0], 80);
                    var times = new List<double>();
                    var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sock.Blocking = true;

                    var stopwatch = new Stopwatch();

                    stopwatch.Start();
                    sock.Connect(endPoint);
                    stopwatch.Stop();

                    double t = stopwatch.Elapsed.TotalMilliseconds;
                    times.Add(t);

                    sock.Close();

                    answer.Add(rowhost, "OK");
                }
                catch (SocketException)
                {
                    answer.Add(rowhost, "FAILED");
                }
            }

            return answer;
        }

        public void Logging(string responce, string host)
        {
            using (var writer = new StreamWriter(_logpath, true))
            {
                writer.WriteLine(DateTime.Now + " " + host + " " + responce);
            }
        }
    }
}