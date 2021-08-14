using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System.IO;

namespace PrismProject.System2.FileSystem
{
    class DriveLetter
    {
        public static string Get(string ID)
        {
            return new DriveInfo(ID).Name;
        }
    }
}
