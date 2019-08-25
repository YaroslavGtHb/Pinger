using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
