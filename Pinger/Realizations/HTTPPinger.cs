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
        private IConfigurationRoot Configuration = Startup.builder.Build();

        private string Okanswer { get;} = "OK";
        private string Failedanswer { get;} = "FAILED";
        private readonly List<string> _rowhosts;
        public HttpPinger(List<string> rowhosts)
        {
            _rowhosts = rowhosts;
        }
        public async Task<Dictionary<string, string>> Ping()
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();
            foreach (var rowhost in _rowhosts)
            {
                Console.WriteLine(DateTime.Now);
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest) WebRequest
                        .Create(rowhost);
                    webRequest.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse) webRequest.GetResponse();
                    if (response.StatusCode.ToString() != null &&
                        (int) response.StatusCode == Int32.Parse(Configuration["Httpvalidcode"]))
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
                Console.WriteLine("Host: " + rowhost);
                Console.WriteLine("Period: " + Configuration["Period"]);
                Console.WriteLine("Protocol: " + Configuration["Protocol"]);
                Console.WriteLine();
            }
            return answer;
        }
    }
}