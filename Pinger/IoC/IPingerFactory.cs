using System.Collections.Generic;
using Pinger.Intefaces;

namespace Pinger.IoC
{
    public interface IPingerFactory
    {
        IIcmpPinger CreateIcmpPinger(List<string> rowhosts);
        IHttpPinger CreateHttpPinger(List<string> rowhosts);
        ITcpPinger CreateTcpPinger(List<string> rowhosts);
    }
}