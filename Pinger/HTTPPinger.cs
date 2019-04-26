using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace Pinger
{
    class HTTPPinger : IPinger
    {
        public Dictionary<string, string> Ping(List<string> rowhosts)
        {
            Dictionary<string, string> answer = new Dictionary<string, string>();
            Ping ping = new Ping();
            foreach (var rowhost in rowhosts)
            {
                try
                {
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest
                        .Create(rowhost);
                    webRequest.AllowAutoRedirect = false;
                    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                    
                    if (response.StatusCode.ToString() != null) answer.Add(rowhost, "OK (" + response.StatusCode.ToString() + ")");
                }
                catch (PingException)
                {
                    answer.Add(rowhost, "FAILED");
                }            
            }
            return answer;          
        }
        
        
        
        
    }
}
