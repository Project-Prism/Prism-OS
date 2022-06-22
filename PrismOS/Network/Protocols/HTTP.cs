using System;

namespace PrismOS.Network.Protocols
{
    public static class HTTP
    {
        public static string GetUserAgent()
        {
            return "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
        }

        public static string GenerateHTTPResponse(string Contents)
        {
            return
                "HTTP / 1.1 200 OK" +
                $"Date: {DateTime.Now:ddd, d MMM yyyy h:mm:ss}\n" +
                "Server: Cosmos Devkit / .Net 5(Prism OS)\n" +
                "Content - Type: text / html\n" +
                "Connection: Closed;\n" + Contents;
        }
        public static string GenerateHTTPGetRequest(URL URL)
        {
            return
                $"GET {URL.Path} HTTP / 1.1\n" +
                $"Host: {URL.Domain}\n" +
                "Connection: Closed;\n" +
                "Accept: text / html, application / xhtml + xml, application / xml; q = 0.9, image / webp, image / apng, * / *; q = 0.8\n" +
                $"User-Agent: {GetUserAgent()}\n" +
                "Accept-Language: en - US, en; q = 0.5\n" +
                "Upgrade-Insecure-Requests: 1\n" +
                "Cache-Control: max - age = 0\n" +
                "If-Modified-Since: Thu, 01 Jan 1970 00:00:00 GMT\n" +
                "If-None-Match: W/\"0\"\n" +
                "Accept-Encoding: gzip, deflate, br\n" +
                "Accept-Language: en - US, en; q = 0.5\n" +
                "Pragma: no - cache\n" +
                "Cache-Control: no - cache\n" +
                "Connection: Closed;\n";
        }
    }
}