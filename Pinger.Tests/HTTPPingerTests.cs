using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Pinger.Realizations;

namespace Pinger.Tests
{
    internal class HttpPingerTests
    {
        private readonly string logpath = "./LogsTest.txt";
        private readonly List<string> rowhosts = new List<string>(File.ReadAllLines("./Hosts.txt"));

        [Test]
        public void PingTest()
        {
            var httppinger = new HttpPinger(rowhosts);
            var actual = new Dictionary<string, string>();
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
            var httppinger = new HttpPinger(rowhosts);
            var answer = httppinger.Ping();
            foreach (var item in answer.Result) httppinger.Logging(item.Key, item.Value);

            File.Delete(logpath);
        }
    }
}