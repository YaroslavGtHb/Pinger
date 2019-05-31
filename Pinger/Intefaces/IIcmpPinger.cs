using System.Collections.Generic;

namespace Pinger.Intefaces
{
    public interface IIcmpPinger
    {
        Dictionary<string, string> Ping();
        void Logging(string host, string responce);
    }
}