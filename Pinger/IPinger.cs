using System.Collections.Generic;

namespace Pinger
{
    interface IPinger
    {
        Dictionary<string, string> Ping();
        void Logging(string Host, string Responce);
    }
}
