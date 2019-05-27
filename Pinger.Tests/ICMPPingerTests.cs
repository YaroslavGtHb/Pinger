using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using NUnit.Framework;
using Pinger;

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

            Dictionary<string, string> answer = new Dictionary<string, string>();
            answer.Add(host, "OK");

            ICMPPinger icmppinger = new ICMPPinger(hosts, logpath);

            Assert.That(icmppinger.Ping());

        }

    }
}
