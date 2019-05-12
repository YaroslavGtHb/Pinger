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
            
            List<string> hosts = new List<string>(File.ReadAllLines("./hosts.txt"));


            ICMPPinger ipinger = new ICMPPinger(hosts, "./ICMPPinger.txt");
            var Ianswers = ipinger.Ping();
            foreach (var answer in Ianswers)
            {
                ipinger.Logging(answer.Value, answer.Key);
            }

            HTTPPinger HPinger = new HTTPPinger(hosts, "./HTTPPinger.txt");
            var hAnswers = HPinger.Ping();
            foreach (var answer in hAnswers)
            {
                HPinger.Logging(answer.Value, answer.Key);
            }

            TCPPinger TPinger = new TCPPinger(hosts, "./TCPPinger.txt");
            var TAnswers = TPinger.Ping();
            foreach (var answer in TAnswers)
            {
                TPinger.Logging(answer.Value, answer.Key);
            }

            
        }
    }
}
