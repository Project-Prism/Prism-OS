using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace PrismOS.Storage
{
    public static class VFS
    {
        public static void InitVFS()
        {
            CosmosVFS VFS = new();
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize(true);
        }
    }
}
