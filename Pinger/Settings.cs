namespace Pinger
{
    public class Settings
    {
        public string Rowhostspath { get; set; } = "./Hosts.txt";
        public string Logpath { get; set; } = "./Logs.txt";
        public string Protocol { get; set; } = "ICMP";
        public string Settingspath { get; set; } = "./Settings.json";
        public int Period { get; set; } = 1000;
        public int Httpvalidcode { get; set; } = 200;
    }
}