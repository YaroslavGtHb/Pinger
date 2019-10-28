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
        private readonly List<string> _rowhosts;
        private readonly IConfigurationRoot _configuration = Startup.Builder.Build();

        public HttpPinger(List<string> rowhosts)
        {
            _rowhosts = rowhosts;
        }

        private string Okanswer { get; } = "OK";
        private string Failedanswer { get; } = "FAILED";

        public async Task<Dictionary<string, string>> Ping()
        {
            var consoleloger = new ConsoleLoger();
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
                    {
                        answer.Add(rowhost, Okanswer);
                        Console.WriteLine(Okanswer);
                    }
                    else
                    {
                        answer.Add(rowhost, Failedanswer);
                        Console.WriteLine(Failedanswer);
                    }
                }
                catch (PingException)
                {
                    answer.Add(rowhost, Failedanswer);
                    Console.WriteLine(Failedanswer);
                }
                catch (ArgumentException)
                {
                    answer.Add(rowhost, Failedanswer);
                    Console.WriteLine(Failedanswer);
                }
                catch (WebException)
                {
                    answer.Add(rowhost, Failedanswer);
                    Console.WriteLine(Failedanswer);
                }
                catch (UriFormatException)
                {
                    answer.Add(rowhost, Failedanswer);
                    Console.WriteLine(Failedanswer);
                }
                catch (FormatException)
                {
                    answer.Add(rowhost, Failedanswer);
                    Console.WriteLine(Failedanswer);
                }
                catch (SocketException)
                {
                    answer.Add(rowhost, Failedanswer);
                    Console.WriteLine(Failedanswer);
                }

                consoleloger.Show(rowhost);
            }

            return answer;
        }
    }
}