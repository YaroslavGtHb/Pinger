using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Pinger.Intefaces
{
    public abstract class Loger
    {
        private static readonly IConfigurationRoot Configuration = Startup.Builder.Build();

        public static List<string> Advancedlogpaths = new List<string>
        {
            Configuration["MainLogpath"],
            "./AdvancedLogs.txt"
        };

        public virtual void Logging(string host, string responce)
        {
            var logString = DateTime.Now + " " + host + " " + responce;

            try
            {
                foreach (var logpath in Advancedlogpaths)
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.WriteLine(logString);
                    }
            }
            catch (DirectoryNotFoundException)
            {
                foreach (var logpath in Advancedlogpaths)
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.WriteLine(logString);
                    }
            }
        }
    }
}