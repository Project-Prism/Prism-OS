using System;

namespace PrismOS.Libraries.Network
{
    public static class HTTP
    {
        public static string GenerateHTTP(string Contents)
        {
            return
                "HTTP / 1.1 200 OK" +
                $"Date: {DateTime.Now:ddd, d MMM yyyy h:mm:ss}\n" +
                "Server: Cosmos Devkit / .Net 5(Prism OS)\n" +
                "Content - Type: text / html\n" +
                "Connection: Closed;\n" + Contents;
        }
    }
}