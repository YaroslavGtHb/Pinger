using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Pinger
{
    class Startup
    {
        public static IConfigurationBuilder builder = new ConfigurationBuilder()
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
