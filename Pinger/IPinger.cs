using System.Collections.Generic;

namespace Pinger
{
    interface IPinger
    {
        Dictionary<string, string> Ping();
        void PingAndLogging();
    }
}
