using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Pinger.Intefaces;


namespace Pinger.Realizations
{
    public class TcpPinger : Loger, ITcpPinger
    {
        private string Okanswer { get; } = "OK";
        private string Failedanswer { get; } = "FAILED";
        private List<string> _rowhosts;

        public TcpPinger(List<string> rowhosts)
        {
            _rowhosts = rowhosts;
        }

        public async Task<Dictionary<string, string>> Ping()
        {
            var consoleloger = new ConsoleLoger();
            Dictionary<string, string> answer = new Dictionary<string, string>();
            foreach (var rowhost in _rowhosts)
            {
                Console.WriteLine(DateTime.Now);
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
                    ShowStatusConsole(ref answer, rowhost, true);
                }
                catch (SocketException)
                {
                    ShowStatusConsole(ref answer, rowhost, false);
                }

                consoleloger.Show(rowhost);
            }

            return answer;
        }
    }
}