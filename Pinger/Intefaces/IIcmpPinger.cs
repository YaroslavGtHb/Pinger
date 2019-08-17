using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pinger.Intefaces
{
    public interface IIcmpPinger
    {
        Task<Dictionary<string, string>> Ping();
        void Logging(string host, string responce);
    }
}