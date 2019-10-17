using System;
using System.Collections.Generic;
using System.IO;
using Pinger.Properties;

namespace Pinger.Intefaces
{
    public abstract class Loger
    {
        public static List<string> Advancedlogpaths = new List<string>()
        {
            Settings.MainLogpath,
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
