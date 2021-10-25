using Cosmos.System.Network.IPv4.TCP.FTP;
using Prism.FileSystem;

namespace Prism.Services
{
    internal static class FTPService
    {
        public static void Start()
        {
            using FtpServer xServer = new(FSCore.fs, "0:\\");
            xServer.Listen();
            while (true)
            {

            }
        }
    }
}
