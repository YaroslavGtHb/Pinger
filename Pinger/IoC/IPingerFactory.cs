using System.Collections.Generic;
using Pinger.Intefaces;

namespace Pinger.IoC
{
    public interface IPingerFactory
    {
        IIcmpPinger CreateIcmpPinger();
        IHttpPinger CreateHttpPinger();
        ITcpPinger CreateTcpPinger();
    }
}