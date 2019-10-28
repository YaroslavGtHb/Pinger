using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Pinger
{
    class Startup
    {
        public static IConfigurationBuilder Builder = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"Httpvalidcode", "200"},
                {"MainLogpath", "./Logs.txt"},
                {"Period", "1000"},
                {"Protocol", "ICMP"},
                {"Rowhostspath", "./Hosts.txt"},
            });
    }
}