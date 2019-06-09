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
        public void WrongValueTest()
        {

            TestDelegate wrongvalue = WrongValue;
            Assert.Throws(typeof(JsonReaderException), wrongvalue);
        }

        private void WrongValue()
        {
            UniversalTesting("./WrongValue.json");
        }

        [Test]
        public void WrongHostTest()
        {
            TestDelegate wronghost = WrongHost;
            Assert.Throws(typeof(FileNotFoundException), wronghost);
        }

        private void WrongHost()

        {
            UniversalTesting("./WrongHost.json");
        }



        private void UniversalTesting(string testsettingspath)
        {
            IKernel kernel = new StandardKernel(new NinjectConfig());

            if (!kernel.HasModule("Ninject.Extensions.Factory.FuncModule"))
            {
                kernel.Load(new FuncModule());
            }

            var settings = new Settings();

            settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(testsettingspath));

            UniversalPinger pinger = kernel.Get<UniversalPinger>();
            pinger.Run(settings);
        }
    }
}
