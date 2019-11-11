using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Pinger.Realizations;

namespace Pinger.Tests
{
    internal class HttpPingerTests
    {
        private readonly string logpath = "./LogsTest.txt";
        private readonly Dictionary<string, string> rowhosts = new Dictionary<string, string>();

        [Test]
        public void PingTest()
        {
            var rowhostskeys = new List<string>(File.ReadAllLines("./Hosts.txt"));
            foreach (var item in rowhostskeys) rowhosts.Add(item, item);

            var httppinger = new HttpPinger();
            var actual = new Dictionary<string, string>();
            var expectedTask = httppinger.Ping(rowhosts);
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
            var httppinger = new HttpPinger();
            var answer = httppinger.Ping(rowhosts);
            foreach (var item in answer.Result) httppinger.Logging(item.Key, item.Value);

            File.Delete(logpath);
        }
    }
}