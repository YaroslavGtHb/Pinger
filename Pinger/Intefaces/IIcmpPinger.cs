using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pinger.Intefaces
{
    public interface IIcmpPinger
    {
        Task<Dictionary<string, string>> Ping(Dictionary<string, string> rowhosts);
        void Logging(string host, string responce);
    }
}