using System.Collections.Generic;

namespace Pinger
{
    public interface IHttpPinger
    {
        Dictionary<string, string> Ping();
        void Logging(string Host, string Responce);
    }
}