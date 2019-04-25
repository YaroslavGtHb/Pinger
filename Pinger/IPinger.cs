using System.Collections.Generic;

namespace Pinger
{
    interface IPinger
    {
        Dictionary<string, string> Ping(List<string> rowhosts);
        void PingAndLogging(Dictionary<string, string> pingedhosts, string logpath);
    }
}
