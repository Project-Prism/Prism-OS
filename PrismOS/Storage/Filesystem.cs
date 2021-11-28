using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace PrismOS.Storage
{
    public static class Filesystem
    {
        public static void Initiate()
        {
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize(true);
        }

        public static readonly CosmosVFS VFS = new();
    }
}
