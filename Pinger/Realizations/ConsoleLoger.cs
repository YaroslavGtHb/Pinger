using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pinger.Intefaces;

namespace Pinger.Realizations
{
    class ConsoleLoger
    {
        private IConfigurationRoot Configuration = Startup.builder.Build();

        public void Show(string rowhost)
        {
            Console.WriteLine("Host: " + rowhost);
            Console.WriteLine("Period: " + Configuration["Period"]);
            Console.WriteLine("Protocol: " + Configuration["Protocol"]);
            Console.WriteLine();
        }
    }
}
