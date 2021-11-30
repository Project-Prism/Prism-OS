using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;

namespace PrismOS.Storage
{
    public static class Extras
    {
        public static int Boot_Drive { get; } = 0;
        public static int CD_Drive { get => VFS.GetVolumes().Count - 1; }

        public static void Initiate()
        {
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize(true);
        }

        public static readonly CosmosVFS VFS = new();
    }
}
