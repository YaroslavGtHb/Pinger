namespace Pinger
{
    public class Settings
    {
        public string rowhostspath { get; set; } = "./Hosts.txt";
        public string logpath { get; set; } = "./Logs.txt";
        public string protocol { get; set; } = "ICMP";
        public string settingspath { get; set; } = "./Settings.json";
        public int period { get; set; } = 1000;
        public int httpvalidcode { get; set; } = 200;
    }
}
