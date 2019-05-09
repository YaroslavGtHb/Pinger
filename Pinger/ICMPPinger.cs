using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;

namespace Pinger
{
    public class ICMPPinger : IPinger
    {
        private List<string> _rowhosts;
        private Dictionary<string, string> _pingedhosts;
        private string _logpath;

        public ICMPPinger(List<string> rowhosts, Dictionary<string, string> pingedhosts, string logpath)
        {
            _rowhosts = rowhosts;
            _pingedhosts = pingedhosts;
            _logpath = logpath;
        }

        public Dictionary<string, string> Ping()
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();
            Ping ping = new Ping();
            foreach (var rowhost in _rowhosts)
            {
                try
                {
                    PingReply pingReply = ping.Send(rowhost);
                    if (pingReply != null) answer.Add(pingReply.Address.ToString(), "OK (" + pingReply.Status + ")");
                }
                catch (PingException)
                {
                    answer.Add(rowhost, "FAILED");
                }            
            }
            return answer;
        }

        public void PingAndLogging ()
        {
            Ping ping = new Ping();

            foreach (var pingedhost in _pingedhosts)
            {
                try
                {
                    PingReply pingReply = ping.Send(pingedhost.Key);
                    if (pingReply != null && pingReply.Status.ToString() != pingedhost.Value)
                    {
                        using (var writer = new StreamWriter(_logpath, true))
                        {
                             writer.WriteLine(DateTime.Now + " " + pingReply.Address + " " + "OK (" + pingReply.Status + ")");
                        }
                    }
                }
                catch (ArgumentException)
                {
                    using (var writer = new StreamWriter(_logpath, true))
                    {
                        writer.WriteLine(DateTime.Now + " " + pingedhost.Key + " " + "FAILED");
                    }
                }

            }
            
        }
    }
}
