using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Ninject;
using Ninject.Extensions.Factory;
using Pinger.IoC;
using Pinger.Properties;

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

            UniversalPinger pinger = kernel.Get<UniversalPinger>();
            pinger.Run();
        }
    }
}