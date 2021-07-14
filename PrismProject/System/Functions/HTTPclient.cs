using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using System;
using System.Collections.Generic;
using System.Text;


namespace PrismProject
{
    class HTTPClient
    {
        private static string request = string.Empty;
        private static TcpClient tcpc = new TcpClient(80);
        private static Address dns = new Address(8, 8, 8, 8);
        private static EndPoint endPoint = new EndPoint(dns,80);
        public static bool ParseHeader()
        {
            return false;
        }
        public static string Get(string url)
        {
            request +=
                "GET " + GetResource(url) + " HTTP/1.1\n" +
                "Host: " + GetHost(url) + "\n" +
                "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:88.0) Gecko/20100101 Firefox/88.0\n" +
                "";
            using (var xClient = new DnsClient())
            {
                xClient.Connect(dns);

                //Send DNS ask for a single domain name
                xClient.SendAsk(GetHost(url));

                //Receive DNS Response
                Address destination = xClient.Receive(); //can set a timeout value

                xClient.Close();
                tcpc.Connect(destination,80);
                tcpc.Send(Encoding.ASCII.GetBytes(request));
                endPoint.address = destination;
                endPoint.port = 80;
                return Encoding.ASCII.GetString(tcpc.Receive(ref endPoint));
            }
            
        }
        public static string GetHost(string url)
        {
            string newurl = url;
            if (newurl.StartsWith("http://"))
            {
                newurl = newurl.Remove(0, 7);
            }
            else if (newurl.StartsWith("https://"))
            {
                throw new Exception("HTTPS not supported!");
            }
            string[] spliturl = newurl.Split("/");
            return spliturl[0];
        }
        public static string GetResource(string url)
        {
            string newurl = url;
            if (newurl.StartsWith("http://"))
            {
                newurl = newurl.Remove(0, 7);
            }
            else if (newurl.StartsWith("https://"))
            {
                throw new Exception("HTTPS not supported!");
            }
            string[] spliturl = newurl.Split("/");
            for (int i = 1; i < spliturl.Length - 1; i++)
            {
                newurl += spliturl[i];
            }
            return newurl;
        }
    }
    enum HTTPRequest
    { 
        GET = 0,
        POST = 1,
    }
}