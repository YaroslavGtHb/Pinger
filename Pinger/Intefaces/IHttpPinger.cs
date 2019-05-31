using System.Collections.Generic;

namespace Pinger.Intefaces
{
    public interface IHttpPinger
    {
        Dictionary<string, string> Ping();
        void Logging(string host, string responce);
    }
}