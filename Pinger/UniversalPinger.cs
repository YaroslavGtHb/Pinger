using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Json;
using Castle.Components.DictionaryAdapter;
using Newtonsoft.Json;
using Ninject;

namespace Pinger
{
    public class UniversalPinger
    {
        private readonly IPingerFactory _pingerFactory;
        private List<string> _rowhosts = new List<string>(File.ReadAllLines("./Hosts.txt"));
        private string _logpath = "./Logs.txt";
        Settings settings = new Settings();

        [Inject]
        public UniversalPinger(IPingerFactory pingerFactory)
        {
            _pingerFactory = pingerFactory;
        }

        public void Run()
        {

            if (File.Exists("./Settings.json"))
            {
                File.WriteAllText("./Settings.json", JsonConvert.SerializeObject(settings));
            }
            else
            {
                settings = JsonConvert.DeserializeObject<Settings>("./Settings.json");
            }


            var HPinger =_pingerFactory.CreateHttpPinger(_rowhosts, _logpath);
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
