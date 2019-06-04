using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Ninject;
using Pinger.IoC;

namespace Pinger
{
    public class UniversalPinger
    {
        private readonly IPingerFactory _pingerFactory;
        private Settings _settings = new Settings();
        private string wronghostmessage = "Wrong row hosts path in setiings file.";

        private string wringsettingsmessage =
            "Wrong parameter in settings file. Program will be using default settings. Press any key to start.";

        [Inject]
        public UniversalPinger(IPingerFactory pingerFactory)
        {
            _pingerFactory = pingerFactory;
        }

        public void Run()
        {
            if (!File.Exists(_settings.Settingspath))
            {
                File.WriteAllText(_settings.Settingspath, JsonConvert.SerializeObject(_settings));
            }
            else
            {
                try
                {
                    _settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(_settings.Settingspath));
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine(wringsettingsmessage);
                    Console.ReadKey();
                }
            }

            if (_settings.Protocol == "ICMP")
            {
                IcmpPing();
            }
            else if (_settings.Protocol == "HTTP")
            {
                HttpPing();
            }
            else if (_settings.Protocol == "TCP")
            {
                TcpPinger();
            }
            else
            {
                Console.WriteLine("Wrong protocol value in settings file.");
                Console.WriteLine("Any key to start default ICMP Ping.");
                Console.ReadKey();
                IcmpPing();
            }
        }

        private void IcmpPing()
        {
            string logpath = _settings.Logpath;

            List<string> rowhosts;

            try
            {
                rowhosts = new List<string>(File.ReadAllLines(_settings.Rowhostspath));
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
                Thread.Sleep(_settings.Period);

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
            string logpath = _settings.Logpath;

            List<string> rowhosts;

            try
            {
                rowhosts = new List<string>(File.ReadAllLines(_settings.Rowhostspath));
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(wronghostmessage);
                Console.ReadKey();
                return;
            }

            var httpPinger = _pingerFactory.CreateHttpPinger(rowhosts, logpath);
            var mainAnswer = httpPinger.Ping();

            foreach (var item in mainAnswer)
            {
                httpPinger.Logging(item.Key, item.Value);
            }

            while (true)
            {
                Thread.Sleep(_settings.Period);

                var tempAnswer = httpPinger.Ping();

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
            string logpath = _settings.Logpath;

            List<string> rowhosts;

            try
            {
                rowhosts = new List<string>(File.ReadAllLines(_settings.Rowhostspath));
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
                Thread.Sleep(_settings.Period);

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