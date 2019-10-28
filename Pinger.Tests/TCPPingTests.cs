using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Pinger.Realizations;

namespace Pinger.Tests
{
    public class TcpPingTests
    {
        private readonly string logpath = "./LogsTest.txt";
        private readonly List<string> rowhosts = new List<string>(File.ReadAllLines("./Hosts.txt"));

        [Test]
        public void PingTest()
        {
            var tcppinger = new TcpPinger(rowhosts);
            var actual = new Dictionary<string, string>();
            var expected = tcppinger.Ping().Result;
            actual.Add("https://www.google.com/", "FAILED");
            actual.Add("https://www.google1234455435435.com/", "FAILED");
            actual.Add("216.58.207.78", "OK");
            actual.Add("34.22.1.23", "FAILED");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoggingTest()
        {
            var tcppinger = new TcpPinger(rowhosts);
            var answer = tcppinger.Ping();
            foreach (var item in answer.Result) tcppinger.Logging(item.Key, item.Value);

            File.Delete(logpath);
        }
    }
}