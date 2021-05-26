using System.Collections.Generic;
using Cosmos.System.FileSystem.Listing;

namespace PrismProject
{
    public class Filesystem
    {
        public static List<DirectoryEntry> lsdir(string args)
        {
            return Kernel.fs.GetDirectoryListing("0:/" + args);
        }

        public static void cdir(string args)
        {
            Kernel.fs.CreateDirectory("0:/" + args);
        }
    }
}
