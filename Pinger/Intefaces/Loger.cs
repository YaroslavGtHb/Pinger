using System;
using System.IO;
using Pinger.Properties;

namespace Pinger.Intefaces
{
    public abstract class Loger
    {
        public virtual void Logging(string host, string responce)
        {
            string LogString = DateTime.Now + " " + host + " " + responce;
            try
            {
                using (var writer = new StreamWriter(Settings.MainLogpath, true))
                {
                    writer.WriteLine(LogString);
                }
            }
            catch (DirectoryNotFoundException)
            {
                using (var writer = new StreamWriter(Settings.MainLogpath, true))
                {
                    writer.WriteLine(LogString);
                }
            }
        }
    }
}
