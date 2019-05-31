using System.Collections.Generic;
using Pinger.Intefaces;

namespace Pinger.IoC
{
    public interface IPingerFactory
    {
        IIcmpPinger CreateIcmpPinger(List<string> rowhosts, string logpath);
        IHttpPinger CreateHttpPinger(List<string> rowhosts, string logpath);
        ITcpPinger CreateTcpPinger(List<string> rowhosts, string logpath);
    }
}