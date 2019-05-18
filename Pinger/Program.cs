using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Parameters;

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
        //https://habr.com/ru/post/235995/
        //http://codeclimber.net.nz/archive/2009/02/05/how-to-use-ninject-with-aspnet-mvc/
        //https://steemit.com/utopian-io/@rufu/get-started-with-ninject-in-c-programming
        //https://stackoverflow.com/questions/19000466/why-am-i-getting-interceptor-attempted-to-proceed-for-a-method-without-a-targe
    }
}
