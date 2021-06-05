using System.Collections.Generic;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace PrismProject
{
    public class Filesystem
    {
        public static CosmosVFS fs = new CosmosVFS();
        public const string Root = "0:/";
        public static void Init()
        {
            VFSManager.RegisterVFS(fs);
        }
        public static List<DirectoryEntry> List_Directory(string args)
        {
            return fs.GetDirectoryListing(Root + args);
        }
        public static void Create_driectory(string args)
        {
            fs.CreateDirectory(Root + args);
        }
    }
}
