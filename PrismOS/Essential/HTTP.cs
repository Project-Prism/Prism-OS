using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismOS.Essential
{
    public static class HTTP
    {
        public static string GenHttp(string URL)
        {
            string Path = "/" + URL.Split(".com/")[1];
            return "GET " + Path + "HTTP/1.1\n\r" +
                   "Host: " + URL.Replace(Path, "") + "\n\r" +
                   "User-Agent: Wget (GNU/Linux)" +
                   "Accept: */*" +
                   "Keep-Alive: 300" +
                   "Connection: keep-alive" +
                   "Pragma: no-cache" +
                   "Cache-Control: no-cache";
        }
    }
}
