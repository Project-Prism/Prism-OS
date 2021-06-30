using System.Collections.Generic;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;

namespace PrismProject
{
    public class Filesystem
    {
        public static CosmosVFS fs = new CosmosVFS();
        public const string drive0 = "0:\\";

        public static void Init()
        { //initalise the filesystem, best to only trigger this on boot.
            VFSManager.RegisterVFS(fs);
        }
        public static List<DirectoryEntry> List_Directory(string args)
        {
            return fs.GetDirectoryListing(Convert.ToString(args));
        }
        public static void Create_driectory(string args)
        {
            fs.CreateDirectory(drive0 + Convert.ToString(args));
        }
    }
}
