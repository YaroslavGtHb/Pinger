using Ninject;
using Ninject.Extensions.Factory;
using Pinger.IoC;

namespace Pinger
{
    internal class Program
    {
        private static void Main()
        {
            IKernel kernel = new StandardKernel(new NinjectConfig());
            if (!kernel.HasModule("Ninject.Extensions.Factory.FuncModule")) kernel.Load(new FuncModule());

            var pinger = kernel.Get<UniversalPinger>();
            pinger.Run();
        }
    }
}