using Ninject;
using Ninject.Extensions.Factory;
using Pinger.IoC;

namespace Pinger
{
    class Program
    {
        static void Main()
        {
            IKernel kernel = new StandardKernel(new NinjectConfig());
            if (!kernel.HasModule("Ninject.Extensions.Factory.FuncModule"))
            {
                kernel.Load(new FuncModule());
            }
            Settings settings = new Settings();
            UniversalPinger pinger = kernel.Get<UniversalPinger>();
            pinger.Run(settings);
        }
    }
}