namespace Pinger
{
    class UniversalPinger
    {
        private IPinger _IcmpPinger;
        private IPinger _HttpPinger;
        private IPinger _TcpPinger;

        public UniversalPinger(IPinger IcmpPinger, IPinger HttpPinger, IPinger TcpPinger)
        {
            _IcmpPinger = IcmpPinger;
            _HttpPinger = HttpPinger;
            _TcpPinger = TcpPinger;
        }


    }
}
