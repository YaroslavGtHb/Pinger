using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Ninject;

namespace Pinger
{
    public class UniversalPinger
    {
        private readonly IPingerFactory _pingerFactory;
        private Settings _settings = new Settings();

        [Inject]
        public UniversalPinger(IPingerFactory pingerFactory)
        {
            _pingerFactory = pingerFactory;
        }

        public void Run()
        {
            string logpath = _settings.logpath;
            List<string> rowhosts = new List<string>(File.ReadAllLines(_settings.rowhostspath));


            if (!File.Exists(_settings.settingspath))
            {
                File.WriteAllText(_settings.settingspath, JsonConvert.SerializeObject(_settings));
            }
            else
            {
                _settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(_settings.settingspath));
            }

            if (_settings.protocol == "ICMP")
            {
                var ICMPPinger = _pingerFactory.CreateIcmpPinger(rowhosts, logpath);
                var mainAnswer = ICMPPinger.Ping();


                foreach (var item in mainAnswer)
                {
                    ICMPPinger.Logging(item.Key, item.Value);
                    Console.WriteLine(item.Key + " " + item.Value);
                }

                while (true)
                {
                    var tempAnswer = ICMPPinger.Ping();

                    var ExceptAnswer = tempAnswer.Except(mainAnswer).ToList();

                    if (ExceptAnswer != null)
                    {
                        foreach (var item in ExceptAnswer)
                        {
                            ICMPPinger.Logging(item.Key, item.Value);
                            Console.WriteLine(item.Key + " " + item.Value);
                        }

                        mainAnswer = tempAnswer;
                    }
                }
            }
        }

        //TODO add logic to UniversalPinger.
        //TODO fix all pinger classes to new format, one method for check and write to file, one metod for check one ip.
        //https://habr.com/ru/post/131993/
        //http://80levelelf.com/Post?postId=20
    }
}