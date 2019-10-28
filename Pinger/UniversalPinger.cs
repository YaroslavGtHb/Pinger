using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Ninject;
using Pinger.IoC;

namespace Pinger
{
    public class UniversalPinger
    {
        private IConfigurationRoot Configuration = Startup.Builder.Build();

        private readonly IPingerFactory _pingerFactory;

        public string Wrongsettingsmessage =
            "Wrong parameter in settings file. Program will be using default settings. Press any key to start.";

        private readonly string wronghostmessage = "Wrong row hosts path in settings file.";

        private readonly string wrongprotocolmessage =
            "Wrong protocol value in settings file. \n Any key to start default ICMP Ping.";

        [Inject]
        public UniversalPinger(IPingerFactory pingerFactory, string wrongsettingsmessage)
        {
            _pingerFactory = pingerFactory;
            Wrongsettingsmessage = wrongsettingsmessage;
        }

        public void Run()
        {
            if (Configuration["Protocol"] == "ICMP")
            {
                IcmpPing();
            }
            else if (Configuration["Protocol"] == "HTTP")
            {
                HttpPing();
            }
            else if (Configuration["Protocol"] == "TCP")
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

        private async void IcmpPing()
        {
            string logpath = Configuration["MainLogpath"];
            List<string> rowhosts;
            try
            {
                rowhosts = new List<string>(File.ReadAllLines(Configuration["Rowhostspath"]));
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(wronghostmessage);
                Console.ReadKey();
                return;
            }

            var icmpPinger = _pingerFactory.CreateIcmpPinger(rowhosts);
            var mainAnswer = await icmpPinger.Ping();
            foreach (var item in mainAnswer)
            {
                icmpPinger.Logging(item.Key, item.Value);
            }

            while (true)
            {
                Thread.Sleep(Int32.Parse(Configuration["Period"]));
                var tempAnswerTask = icmpPinger.Ping();
                var tempAnswer = tempAnswerTask.Result;
                var exceptAnswer = tempAnswer.Except(mainAnswer).ToList();
                foreach (var item in exceptAnswer)
                {
                    icmpPinger.Logging(item.Key, item.Value);
                }

                mainAnswer = tempAnswer;
            }
        }

        private async void HttpPing()
        {
            string logpath = Configuration["MainLogpath"];
            List<string> rowhosts;
            try
            {
                rowhosts = new List<string>(File.ReadAllLines(Configuration["Rowhostspath"]));
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(wronghostmessage);
                Console.ReadKey();
                return;
            }

            var httpPinger = _pingerFactory.CreateHttpPinger(rowhosts);
            var mainAnswer = await httpPinger.Ping();
            foreach (var item in mainAnswer)
            {
                httpPinger.Logging(item.Key, item.Value);
            }

            while (true)
            {
                Thread.Sleep(Int32.Parse(Configuration["Period"]));
                var tempAnswer = httpPinger.Ping().Result;
                var exceptAnswer = tempAnswer.Except(mainAnswer).ToList();
                foreach (var item in exceptAnswer)
                {
                    httpPinger.Logging(item.Key, item.Value);
                }

                mainAnswer = tempAnswer;
            }
        }

        private async void TcpPinger()
        {
            string logpath = Configuration["MainLogpath"];
            List<string> rowhosts;
            try
            {
                rowhosts = new List<string>(File.ReadAllLines(Configuration["Rowhostspath"]));
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(wronghostmessage);
                Console.ReadKey();
                return;
            }

            var tcpPinger = _pingerFactory.CreateTcpPinger(rowhosts);
            var mainAnswer = await tcpPinger.Ping();
            foreach (var item in mainAnswer)
            {
                tcpPinger.Logging(item.Key, item.Value);
            }

            while (true)
            {
                Thread.Sleep(Int32.Parse(Configuration["Period"]));
                var tempAnswer = await tcpPinger.Ping();
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