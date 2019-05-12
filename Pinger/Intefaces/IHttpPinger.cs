using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger
{
    public interface IHttpPinger
    {
        Dictionary<string, string> Ping();
        void Logging(string Host, string Responce);
    }
}