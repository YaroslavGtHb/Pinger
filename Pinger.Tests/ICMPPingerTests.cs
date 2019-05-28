using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Pinger.Tests
{
    public class ICMPPingerTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void Ping_AviableHost_And_AviableLogpath_Expects_Aviablehost_And_OK()
        {
            List<string> hosts = new List<string>();
            string host = "https://google.com/";
            hosts.Add(host);

            string logpath = "./logstest.txt";

            ICMPPinger icmppinger = new ICMPPinger(hosts, logpath);

            Dictionary<string, string> actual = new Dictionary<string, string>();
            actual.Add(host, "FAILED");

            var expected = icmppinger.Ping();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PingTest()
        {
            string logpath = "./LogsTest.txt";
            List<string> rowhosts = new List<string>(File.ReadAllLines("./HostsTest.txt"));

            ICMPPinger icmppinger = new ICMPPinger(rowhosts, logpath);

            Dictionary<string, string> actual = new Dictionary<string, string>();

            Dictionary<string, string> expected = icmppinger.Ping();

            actual.Add("https://www.google.com/", "OK");
            actual.Add("https://www.google1234455435435.com/", "FAILED");
            actual.Add("92.51.57.80", "FAILED");
            actual.Add("34.22.1.23", "FAILED");

            Assert.AreEqual(expected, actual);
        }
    }
}