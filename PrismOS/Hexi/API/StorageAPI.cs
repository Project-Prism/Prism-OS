using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using PrismOS.Hexi.Misc;
using System.Text;

namespace PrismOS.Hexi.API
{
    public static class StorageAPI
    {
        public static void InitVFS(Executable exe, byte[] args)
        {
            CosmosVFS VFS = new();
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize(true);
        }

        public static void WriteText(Executable exe, byte[] args)
        {
            string Path = Encoding.ASCII.GetString(exe.Memory, args[0], args[1]);
            string Data = Encoding.ASCII.GetString(exe.Memory, args[2], args[3]);
            System.IO.File.WriteAllText(Path, Data);
        }
    }
}
