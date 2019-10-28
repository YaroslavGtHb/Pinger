using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Pinger.Realizations;

namespace Pinger.Tests
{
    public class TcpPingTests
    {
        string logpath = "./LogsTest.txt";
        List<string> rowhosts = new List<string>(File.ReadAllLines("./Hosts.txt"));

        [Test]
        public void PingTest()
        {
            TcpPinger tcppinger = new TcpPinger(rowhosts);
            Dictionary<string, string> actual = new Dictionary<string, string>();
            Dictionary<string, string> expected = tcppinger.Ping().Result;
            actual.Add("https://www.google.com/", "FAILED");
            actual.Add("https://www.google1234455435435.com/", "FAILED");
            actual.Add("216.58.207.78", "OK");
            actual.Add("34.22.1.23", "FAILED");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoggingTest()
        {
            TcpPinger tcppinger = new TcpPinger(rowhosts);
            foreach (var item in rowhosts)
            {
                tcppinger.Logging(item, "OK");
            }

            File.Delete(logpath);
        }
    }
}