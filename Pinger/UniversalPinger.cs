using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Json;
using System.Threading;
using Castle.Components.DictionaryAdapter;
using Newtonsoft.Json;
using Ninject;

namespace Pinger
{
    public class UniversalPinger
    {
        private readonly IPingerFactory _pingerFactory;
        private Settings settings = new Settings();

        [Inject]
        public UniversalPinger(IPingerFactory pingerFactory)
        {
            _pingerFactory = pingerFactory;
        }

        public void Run()
        {
            
            string logpath = settings.logpath;
            List<string> rowhosts = new List<string>(File.ReadAllLines(settings.rowhostspath));
            

            if (File.Exists("./Settings.json"))
            {
                File.WriteAllText("./Settings.json", JsonConvert.SerializeObject(settings));
            }
            else
            {
                settings = JsonConvert.DeserializeObject<Settings>("./Settings.json");
            }

            if (settings.protocol == "ICMP")
            {
                var ICMPPinger = _pingerFactory.CreateTcpPinger(rowhosts, logpath);
                
                foreach (var item in ICMPPinger.Ping())
                {
                    ICMPPinger.Logging(item.Value, item.Key);
                }

                var MainAnswer = ICMPPinger.Ping();
                
                while (true)
                {  
                    var TempAnswer = ICMPPinger.Ping();

                    foreach (var main in MainAnswer)
                    {
                        foreach (var temp in TempAnswer)
                        {
                            if (main.Key != temp)
                            {
                                
                            }
                        }
                    }
                    
                }
            }

            var HPinger =_pingerFactory.CreateHttpPinger(rowhosts, logpath);
            var hAnswers = HPinger.Ping();
            foreach (var answer in hAnswers)
            {
                HPinger.Logging(answer.Value, answer.Key);
            }
        }
        //TODO add logic to UniversalPinger.
        //TODO fix all pinger classes to new format, one method for check and write to file, one metod for check one ip.
        //https://habr.com/ru/post/131993/
        //http://80levelelf.com/Post?postId=20

    }
}
