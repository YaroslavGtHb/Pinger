using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Pinger.Realizations;

namespace Pinger.Tests
{
    public class IcmpPingerTests
    {
        string logpath = "./LogsTest.txt";
        List<string> rowhosts = new List<string>(File.ReadAllLines("./Hosts.txt"));
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void PingTest()
        {
            IcmpPinger icmppinger = new IcmpPinger(rowhosts, logpath);
            Dictionary<string, string> actual = new Dictionary<string, string>();
            var expected = icmppinger.Ping().Result;
            actual.Add("https://www.google.com/", "FAILED");
            actual.Add("https://www.google1234455435435.com/", "FAILED");
            actual.Add("216.58.207.78", "OK");
            actual.Add("34.22.1.23", "FAILED");
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void LoggingTest()
        {
            IcmpPinger icmppinger = new IcmpPinger(rowhosts, logpath);
            foreach (var item in rowhosts)
            {
                icmppinger.Logging(item, "OK");
            }
            File.Delete(logpath);
        }
    }
}