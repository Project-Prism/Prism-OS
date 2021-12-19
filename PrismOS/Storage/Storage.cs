using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace PrismOS.Storage
{
    public static class Storage
    {
        public static void InitVFS()
        {
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize(true);
        }

        public static readonly CosmosVFS VFS = new();
        public struct Drives
        {
            public static string CD_Drive { get => VFS.GetVolumes()[^1].mFullPath; }
        }
    }
}
