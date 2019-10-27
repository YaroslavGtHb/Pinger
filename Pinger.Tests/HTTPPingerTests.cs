using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Pinger.Realizations;

namespace Pinger.Tests
{
    class HttpPingerTests
    {
        string logpath = "./LogsTest.txt";
        List<string> rowhosts = new List<string>(File.ReadAllLines("./Hosts.txt"));
        [Test]
        public void PingTest()
        {
            HttpPinger httppinger = new HttpPinger(rowhosts);
            Dictionary<string, string> actual = new Dictionary<string, string>();
            var expectedTask = httppinger.Ping();
            var expected = expectedTask.Result;
            actual.Add("https://www.google.com/", "OK");
            actual.Add("https://www.google1234455435435.com/", "FAILED");
            actual.Add("216.58.207.78", "FAILED");
            actual.Add("34.22.1.23", "FAILED");
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void LoggingTest()
        {
            HttpPinger httppinger = new HttpPinger(rowhosts);
            foreach (var item in rowhosts)
            {
                httppinger.Logging(item, "OK");
            }
            File.Delete(logpath);
        }
    }
}