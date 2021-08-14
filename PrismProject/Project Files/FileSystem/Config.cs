using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System.IO;

namespace PrismProject.System2.FileSystem
{
    /// <summary>VFS file system</summary>
    internal class Config
    {
        public static readonly CosmosVFS fs = new CosmosVFS();

        /// <summary>Initilise VFS, must be done on boot.</summary>
        public static void StartService()
        {
            VFSManager.RegisterVFS(fs);
        }
        /// <summary>Get a drive's type.</summary>
        /// <param name="Drive_ID"></param>
        /// <returns>string</returns>
        public static string DriveType(string Drive_ID)
        {
            return new DriveInfo(Drive_ID).DriveType.ToString();
        }
        /// <summary>Get available free space of a drive (in bytes)</summary>
        /// <param name="args"></param>
        /// <returns>long</returns>
        public static long GetFreeBytes(string args)
        {
            return fs.GetAvailableFreeSpace(args);
        }
        /// <summary>Returns the drive's format type.</summary>
        /// <param name="DriveID"></param>
        /// <returns>format type</returns>
        public static string FT(string DriveID)
        {
            return fs.GetFileSystemType(DriveID);
        }
    }
}