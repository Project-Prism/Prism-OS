using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismOS.Essential
{
    public static class HTTP
    {
        public static string GenHttp(string URL, string Path)
        {
            return "GET " + Path + " HTTP/1.1\r\n" +
                    "User-Agent: Wget/1.12.3 (linux-gnu)\r\n" +
                    "Accept: */*\r\n" +
                    "Host: " + URL + "\r\n" +
                    "Connection: Keep-Alive\r\n\r\n";
        }
    }
}
