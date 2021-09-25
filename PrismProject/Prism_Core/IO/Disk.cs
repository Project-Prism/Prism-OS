using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.IO;
using static PrismProject.Prism_Core.Internal.Files;

namespace PrismProject.Prism_Core.IO
{
    internal class Disk
    {
        private static readonly CosmosVFS fs = new CosmosVFS();

        public static void CreateFolder(string FullPath)
        {
            try
            { fs.CreateDirectory(ParseFullPath(FullPath)); }
            catch (Exception e)
            { WriteFile(@"0:\System2\Crash.log", "\n[Error] " + e, true); }
        }

        public static void DeleteEntry(string FullPath)
        {
            File.Delete(ParseFullPath(FullPath));
        }

        public static bool FileExists(string FullPath)
        {
            return File.Exists(ParseFullPath(FullPath));
        }

        public static void Format(int DiskID)
        { fs.Format(DiskID.ToString(), "FAT32", true); }

        public static List<DirectoryEntry> GetDisks()
        { return fs.GetVolumes(); }

        public static List<DirectoryEntry> GetFolderList(string FullPath)
        { return fs.GetDirectoryListing(ParseFullPath(FullPath)); }

        public static string GetFSType(int DiskID)
        { return fs.GetFileSystemType(DiskID.ToString()); }

        public static int GetSpace(int DiskID)
        { return (int)fs.GetAvailableFreeSpace(DiskID + ":"); }

        public static string ReadFile(string FullPath)
        { return File.ReadAllText(ParseFullPath(FullPath)); }

        /// <summary> Start the VFS service. </summary>
        public static void Start()
        {
            VFSManager.RegisterVFS(fs);
            fs.Initialize();
            Console.WriteLine("Starting disk service...  [done]");
            if (!FileExists(System_Folder))
            { CreateFolder(System_Folder); WriteFile(System_log, "", false); }
            WriteFile(System_log, "Started disk service sucessfully [ " + Cosmos.HAL.RTC.Hour + ":" + Cosmos.HAL.RTC.Minute + " ]", true);
        }

        public static void WriteFile(string FullPath, string Contents, bool Append)
        {
            try
            {
                switch (Append)
                {
                    case true: File.WriteAllText(ParseFullPath(FullPath), File.ReadAllText(ParseFullPath(FullPath)) + Contents); break;
                    case false: File.WriteAllText(ParseFullPath(FullPath), Contents); break;
                }
            }
            catch (Exception e)
            { WriteFile(@"0:\System2\Crash.log", "\n[Error] Failed to write to " + ParseFullPath(FullPath) + "\n" + e.ToString(), true); }
        }

        private static string ParseFullPath(string FullPath)
        {
            return FullPath.Replace("/", @"\");
        }
    }
}