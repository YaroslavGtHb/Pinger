using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Ninject;
using Pinger.IoC;
using Pinger.Properties;

namespace Pinger
{
    public class UniversalPinger
    {
        private readonly IPingerFactory _pingerFactory;
        private string wrongsettingsmessage =
            "Wrong parameter in settings file. Program will be using default settings. Press any key to start.";
        private string wronghostmessage = "Wrong row hosts path in settings file.";

        private string wrongprotocolmessage =
            "Wrong protocol value in settings file. \n Any key to start default ICMP Ping.";
        [Inject]
        public UniversalPinger(IPingerFactory pingerFactory)
        {
            _pingerFactory = pingerFactory;
        }
        public void Run()
        {

            if (Settings.Protocol == "ICMP")
            {
                IcmpPing();
            }
            else if (Settings.Protocol == "HTTP")
            {
                HttpPing();
            }
            else if (Settings.Protocol == "TCP")
            {
                TcpPinger();
            }
            else
            {
                Console.WriteLine(wrongprotocolmessage);
                Console.ReadKey();
                IcmpPing();
            }
        }
        private void IcmpPing()
        {
            string logpath = Settings.Logpath;
            List<string> rowhosts;
            try
            {
                rowhosts = new List<string>(File.ReadAllLines(Settings.Rowhostspath));
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(wronghostmessage);
                Console.ReadKey();
                return;
            }
            var icmpPinger = _pingerFactory.CreateIcmpPinger(rowhosts, logpath);
            var mainAnswer = icmpPinger.Ping();

            foreach (var item in mainAnswer)
            {
                icmpPinger.Logging(item.Key, item.Value);
            }
            while (true)
            {
                Thread.Sleep(Int32.Parse(Settings.Period));
                var tempAnswer = icmpPinger.Ping();
                var exceptAnswer = tempAnswer.Except(mainAnswer).ToList();
                foreach (var item in exceptAnswer)
                {
                    icmpPinger.Logging(item.Key, item.Value);
                }
                mainAnswer = tempAnswer;
            }
        }
        private void HttpPing()
        {
            string logpath = Settings.Logpath;
            List<string> rowhosts;
            try
            {
                rowhosts = new List<string>(File.ReadAllLines(Settings.Rowhostspath));
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(wronghostmessage);
                Console.ReadKey();
                return;
            }
            var httpPinger = _pingerFactory.CreateHttpPinger(rowhosts, logpath);
            var mainAnswerTask = httpPinger.Ping();
            var mainAnswer = mainAnswerTask.Result;
            foreach (var item in mainAnswer)
            {
                httpPinger.Logging(item.Key, item.Value);
            }
            while (true)
            {
                Thread.Sleep(Int32.Parse(Settings.Period));
                var tempAnswerTask = httpPinger.Ping();
                var tempAnswer = tempAnswerTask.Result;
                var exceptAnswer = tempAnswer.Except(mainAnswer).ToList();
                foreach (var item in exceptAnswer)
                {
                    httpPinger.Logging(item.Key, item.Value);
                }
                mainAnswer = tempAnswer;
            }
        }
        private void TcpPinger()
        {
            string logpath = Settings.Logpath;
            List<string> rowhosts;
            try
            {
                rowhosts = new List<string>(File.ReadAllLines(Settings.Rowhostspath));
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(wronghostmessage);
                Console.ReadKey();
                return;
            }
            var tcpPinger = _pingerFactory.CreateTcpPinger(rowhosts, logpath);
            var mainAnswer = tcpPinger.Ping();
            foreach (var item in mainAnswer)
            {
                tcpPinger.Logging(item.Key, item.Value);
            }
            while (true)
            {
                Thread.Sleep(Int32.Parse(Settings.Period));
                var tempAnswer = tcpPinger.Ping();
                var exceptAnswer = tempAnswer.Except(mainAnswer).ToList();
                foreach (var item in exceptAnswer)
                {
                    tcpPinger.Logging(item.Key, item.Value);
                }
                mainAnswer = tempAnswer;
            }
        }
    }
}