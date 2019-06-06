using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Parameters;
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

        //https://habr.com/ru/post/235995/
        //http://codeclimber.net.nz/archive/2009/02/05/how-to-use-ninject-with-aspnet-mvc/
        //https://steemit.com/utopian-io/@rufu/get-started-with-ninject-in-c-programming
        //https://stackoverflow.com/questions/19000466/why-am-i-getting-interceptor-attempted-to-proceed-for-a-method-without-a-targe
    }
}