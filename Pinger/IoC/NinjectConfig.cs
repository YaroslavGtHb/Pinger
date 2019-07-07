using Ninject.Extensions.Factory;
using Ninject.Modules;
using Pinger.Intefaces;
using Pinger.Realizations;

namespace Pinger.IoC
{
    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IIcmpPinger>().To<IcmpPinger>();
            Bind<IHttpPinger>().To<HttpPinger>();
            Bind<ITcpPinger>().To<TcpPinger>();
            Bind<IPingerFactory>().ToFactory();
        }
    }
}