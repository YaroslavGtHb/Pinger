using System;
using System.IO;
using Pinger.Properties;

namespace Pinger.Intefaces
{
    public abstract class Loger
    {
        public virtual void Logging(string host, string responce)
        {
            try
            {
                using (var writer = new StreamWriter(Settings.Logpath, true))
                {
                    writer.WriteLine(DateTime.Now + " " + host + " " + responce);
                }
            }
            catch (DirectoryNotFoundException)
            {
                using (var writer = new StreamWriter(Settings.Logpath, true))
                {
                    writer.WriteLine(DateTime.Now + " " + host + " " + responce);
                }
            }
        }
    }
}
