using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Pinger.Tests
{
    public class ICMPPingerTests
    {
        string logpath = "./LogsTest.txt";
        List<string> rowhosts = new List<string>(File.ReadAllLines("./HostsTest.txt"));

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PingTest()
        {
            ICMPPinger icmppinger = new ICMPPinger(rowhosts, logpath);

            Dictionary<string, string> actual = new Dictionary<string, string>();

            Dictionary<string, string> expected = icmppinger.Ping();

            actual.Add("https://www.google.com/", "FAILED");
            actual.Add("https://www.google1234455435435.com/", "FAILED");
            actual.Add("92.51.57.80", "OK");
            actual.Add("34.22.1.23", "FAILED");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoggingTest()
        {
            ICMPPinger icmppinger = new ICMPPinger(rowhosts, logpath);
            foreach (var item in rowhosts)
            {
                icmppinger.Logging(item, "OK");
            }

            File.Delete(logpath);
        }
    }
}