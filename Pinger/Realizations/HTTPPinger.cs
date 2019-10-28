using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Pinger.Intefaces;

namespace Pinger.Realizations
{
    public class HttpPinger : Loger, IHttpPinger
    {
        private readonly IConfigurationRoot _configuration = Startup.Builder.Build();
        private readonly List<string> _rowhosts;

        public HttpPinger(List<string> rowhosts)
        {
            _rowhosts = rowhosts;
        }

        public async Task<Dictionary<string, string>> Ping()
        {
            var answer = new Dictionary<string, string>();
            foreach (var rowhost in _rowhosts)
            {
                Console.WriteLine(DateTime.Now);
                try
                {
                    var webRequest = (HttpWebRequest) WebRequest
                        .Create(rowhost);
                    webRequest.AllowAutoRedirect = false;
                    var response = (HttpWebResponse) webRequest.GetResponse();
                    if ((int) response.StatusCode == int.Parse(_configuration["Httpvalidcode"]))
                        ShowStatusConsole(ref answer, rowhost, true);
                    else
                        ShowStatusConsole(ref answer, rowhost, false);
                }
                catch (Exception ex)
                {
                    if (ex is PingException ||
                        ex is ArgumentException ||
                        ex is WebException ||
                        ex is UriFormatException ||
                        ex is FormatException ||
                        ex is SocketException)
                        ShowStatusConsole(ref answer, rowhost, false);
                }

                ConsoleLogging(rowhost);
            }

            return answer;
        }
    }
}