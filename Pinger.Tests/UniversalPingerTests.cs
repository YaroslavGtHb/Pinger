using System.IO;
using Newtonsoft.Json;
using Ninject;
using Ninject.Extensions.Factory;
using NUnit.Framework;
using Pinger.IoC;

namespace Pinger.Tests
{
    class UniversalPingerTests
    {

        [Test]
        public void WrongHostTest()
        {
            
            TestDelegate calculate = new TestDelegate(WrongHost);;
            Assert.Throws(typeof(JsonReaderException), calculate);
        }

        private void WrongHost()
        {
            IKernel kernel = new StandardKernel(new NinjectConfig());

            if (!kernel.HasModule("Ninject.Extensions.Factory.FuncModule"))
            {
                kernel.Load(new FuncModule());
            }

            Settings settings = new Settings();

            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("./SettingsWrongHost.json"));

            UniversalPinger pinger = kernel.Get<UniversalPinger>();
            pinger.Run(settings);
        }
    }
}
