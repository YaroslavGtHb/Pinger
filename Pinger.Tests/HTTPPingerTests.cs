using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Pinger.Realizations;

namespace Pinger.Tests
{
    class HttpPingerTests
    {
        string logpath = "./LogsTest.txt";
        List<string> rowhosts = new List<string>(File.ReadAllLines("./HostsTest.txt"));


        [Test]
        public void PingTest()
        {
            HttpPinger httppinger = new HttpPinger(rowhosts, logpath);

            Dictionary<string, string> actual = new Dictionary<string, string>();

            Dictionary<string, string> expected = httppinger.Ping();

            actual.Add("https://www.google.com/", "OK");
            actual.Add("https://www.google1234455435435.com/", "FAILED");
            actual.Add("92.51.57.80", "FAILED");
            actual.Add("34.22.1.23", "FAILED");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoggingTest()
        {
            HttpPinger httppinger = new HttpPinger(rowhosts, logpath);
            foreach (var item in rowhosts)
            {
                httppinger.Logging(item, "OK");
            }

            File.Delete(logpath);
        }

    }
}
