using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Pinger.Properties;
using Pinger;

namespace Pinger.Intefaces
{

    public abstract class Loger
    {
        private static IConfigurationRoot Configuration = Startup.builder.Build();

        public static List<string> Advancedlogpaths = new List<string>()
        {
            Configuration["MainLogpath"],
            "./AdvancedLogs.txt"
        };

        public virtual void Logging(string host, string responce)
        {

            var LogString = DateTime.Now + " " + host + " " + responce;

            try
            {
                foreach (var logpath in Advancedlogpaths)
                {
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.WriteLine(LogString);
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                foreach (var logpath in Advancedlogpaths)
                {
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.WriteLine(LogString);
                    }
                }
            }
        }
    }
}
