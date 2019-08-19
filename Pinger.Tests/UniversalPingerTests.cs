using System.IO;
using Ninject;
using Ninject.Extensions.Factory;
using NUnit.Framework;
using Pinger.IoC;

namespace Pinger.Tests
{
    class UniversalPingerTests
    {
        private readonly string logspath = "./Logs.txt";
        [Test]
        public void WrongHostTest()
        {
            TestDelegate wronghost = WrongHost;
            Assert.Throws(typeof(FileNotFoundException), wronghost);
            File.Delete(logspath);
        }
        private void WrongHost()
        {
            UniversalTesting();
        }
        private void UniversalTesting()
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
