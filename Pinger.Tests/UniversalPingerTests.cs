using System.IO;
using Newtonsoft.Json;
using Ninject;
using Ninject.Extensions.Factory;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Pinger.IoC;

namespace Pinger.Tests
{
    class UniversalPingerTests
    {
        private string logspath = "./Logs.txt";
        private string wrongvaluepath = "./WrongValue.json";
        private string wronghost = "./WrongHost.json";
        [Test]
        public void WrongValueTest()
        {

            TestDelegate wrongvalue = WrongValue;
            Assert.Throws(typeof(JsonReaderException), wrongvalue);
            File.Delete(logspath);
        }
        private void WrongValue()
        {
            UniversalTesting(wrongvaluepath);
        }
        [Test]
        public void WrongHostTest()
        {
            TestDelegate wronghost = WrongHost;
            Assert.Throws(typeof(FileNotFoundException), wronghost);
            File.Delete(logspath);
        }
        private void WrongHost()
        {
            UniversalTesting(wronghost);
        }
        private void UniversalTesting(string testsettingspath)
        {
            IKernel kernel = new StandardKernel(new NinjectConfig());
            if (!kernel.HasModule("Ninject.Extensions.Factory.FuncModule"))
            {
                kernel.Load(new FuncModule());
            }
            UniversalPinger pinger = kernel.Get<UniversalPinger>();
            pinger.Run();
        }
    }
}
