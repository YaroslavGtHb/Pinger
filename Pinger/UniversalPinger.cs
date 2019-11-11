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
        private readonly IConfigurationRoot _configuration = Startup.Builder.Build();
        private readonly IPingerFactory _pingerFactory;

        private readonly string wronghostmessage = "Wrong row hosts path in settings file.";

        private readonly string wrongprotocolmessage =
            "Wrong protocol value in settings file. \n Any key to start default ICMP Ping.";

        [Inject]
        public UniversalPinger(IPingerFactory pingerFactory)
        {
            _pingerFactory = pingerFactory;
        }

        public void Run()
        {
            if (_configuration["Protocol"] == "ICMP")
            {
                IcmpPing();
            }
            else if (_configuration["Protocol"] == "HTTP")
            {
                HttpPing();
            }
            else if (_configuration["Protocol"] == "TCP")
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
            Dictionary<string, string> rowhosts = new Dictionary<string, string>();
            try
            {
                var rowhostskeys = new List<string>(File.ReadAllLines(_configuration["Rowhostspath"]));
                foreach (var item in rowhostskeys)
                {
                    rowhosts.Add(item, item);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(wronghostmessage);
                Console.ReadKey();
                return;
            }

            var icmpPinger = _pingerFactory.CreateIcmpPinger();
            var mainAnswer = await icmpPinger.Ping(rowhosts);
            foreach (var item in mainAnswer) icmpPinger.Logging(item.Key, item.Value);

            while (true)
            {
                Thread.Sleep(int.Parse(_configuration["Period"]));
                var tempAnswerTask = icmpPinger.Ping(rowhosts);
                var tempAnswer = tempAnswerTask.Result;
                var exceptAnswer = tempAnswer.Except(mainAnswer).ToList();
                foreach (var item in exceptAnswer) icmpPinger.Logging(item.Key, item.Value);

                mainAnswer = tempAnswer;
            }
        }

        private async void HttpPing()
        {
            Dictionary<string, string> rowhosts = new Dictionary<string, string>();
            try
            {
                var rowhostskeys = new List<string>(File.ReadAllLines(_configuration["Rowhostspath"]));
                foreach (var item in rowhostskeys)
                {
                    rowhosts.Add(item, item);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(wronghostmessage);
                Console.ReadKey();
                return;
            }

            var httpPinger = _pingerFactory.CreateHttpPinger();
            var mainAnswer = await httpPinger.Ping(rowhosts);
            foreach (var item in mainAnswer) httpPinger.Logging(item.Key, item.Value);

            while (true)
            {
                Thread.Sleep(int.Parse(_configuration["Period"]));
                var tempAnswer = httpPinger.Ping(rowhosts).Result;
                var exceptAnswer = tempAnswer.Except(mainAnswer).ToList();
                foreach (var item in exceptAnswer) httpPinger.Logging(item.Key, item.Value);

                mainAnswer = tempAnswer;
            }
        }

        private async void TcpPinger()
        {
            var logpath = _configuration["MainLogpath"];
            Dictionary<string, string> rowhosts = new Dictionary<string, string>();
            try
            {
                var rowhostskeys = new List<string>(File.ReadAllLines(_configuration["Rowhostspath"]));
                foreach (var item in rowhostskeys)
                {
                    rowhosts.Add(item, item);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine(wronghostmessage);
                Console.ReadKey();
                return;
            }

            var tcpPinger = _pingerFactory.CreateTcpPinger();
            var mainAnswer = await tcpPinger.Ping(rowhosts);
            foreach (var item in mainAnswer) tcpPinger.Logging(item.Key, item.Value);

            while (true)
            {
                Thread.Sleep(int.Parse(_configuration["Period"]));
                var tempAnswer = await tcpPinger.Ping(rowhosts);
                var exceptAnswer = tempAnswer.Except(mainAnswer).ToList();
                foreach (var item in exceptAnswer) tcpPinger.Logging(item.Key, item.Value);

                mainAnswer = tempAnswer;
            }
        }
    }
}