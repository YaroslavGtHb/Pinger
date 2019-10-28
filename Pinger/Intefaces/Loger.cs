using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Pinger.Intefaces
{
    public abstract class Loger
    {
        private static IConfigurationRoot Configuration = Startup.Builder.Build();

        public static List<string> Advancedlogpaths = new List<string>()
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
                {
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.WriteLine(logString);
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                foreach (var logpath in Advancedlogpaths)
                {
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.WriteLine(logString);
                    }
                }
            }
        }
<<<<<<< HEAD
        public void ConsoleLogging(string rowhost)
        {
            Console.WriteLine("Host: " + rowhost);
            Console.WriteLine("Period: " + Configuration["Period"]);
            Console.WriteLine("Protocol: " + Configuration["Protocol"]);
            Console.WriteLine();
        }
<<<<<<< HEAD

        public void ShowStatusConsole(ref Dictionary<string, string> answer, string rowhost, bool answerstatus)
        {
            if (answerstatus)
            {
                answer.Add(rowhost, "OK");
                Console.WriteLine("OK");
            }
            else
            {
                answer.Add(rowhost, "FAILED");
                Console.WriteLine("FAILED");
            }
        }
=======
>>>>>>> parent of 17b93d0... ConsoleLogging optimization.
=======
>>>>>>> parent of 3829356... TEST.
    }
}