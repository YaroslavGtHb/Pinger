using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace Pinger
{
    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IIcmpPinger>().To<ICMPPinger>();
            Bind<IHttpPinger>().To<HTTPPinger>();
            Bind<ITcpPinger>().To<TCPPinger>();

            Bind<IPingerFactory>().ToFactory();
        }
    }
}