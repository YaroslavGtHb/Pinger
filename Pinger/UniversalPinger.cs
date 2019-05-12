namespace Pinger
{
    class UniversalPinger
    {
        private IPinger _IcmpPinger;
        private IPinger _HttpPinger;
        private IPinger _TcpPinger;
        private string _logpath;

        public UniversalPinger(IPinger IcmpPinger, IPinger HttpPinger, IPinger TcpPinger, string logpath)
        {
            _IcmpPinger = IcmpPinger;
            _HttpPinger = HttpPinger;
            _TcpPinger = TcpPinger;
            _logpath = logpath;
        }

        //TODO add logic to UniversalPinger.
        //TODO fix all pinger classes to new format, one method for check and write to file, one metod for check one ip.
        //https://habr.com/ru/post/131993/
        //http://80levelelf.com/Post?postId=20

    }
}
