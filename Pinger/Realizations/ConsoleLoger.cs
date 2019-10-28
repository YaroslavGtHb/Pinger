using System;
using Microsoft.Extensions.Configuration;

namespace Pinger.Realizations
{
    class ConsoleLoger
    {
        private IConfigurationRoot Configuration = Startup.Builder.Build();

        public void Show(string rowhost)
        {
            Console.WriteLine("Host: " + rowhost);
            Console.WriteLine("Period: " + Configuration["Period"]);
            Console.WriteLine("Protocol: " + Configuration["Protocol"]);
            Console.WriteLine();
        }
    }
}