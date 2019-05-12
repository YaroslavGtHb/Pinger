using System.Collections.Generic;

namespace Pinger
{
    public interface IPingerFactory
    {
        IIcmpPinger CreateIcmpPinger(List<string> rowhosts, string logpath);
        IHttpPinger CreateHttpPinger(List<string> rowhosts, string logpath);
        ITcpPinger CreateTcpPinger(List<string> rowhosts, string logpath);
    }
}