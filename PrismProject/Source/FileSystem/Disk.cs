using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using System;
using System.IO;
using System.Collections.Generic;
using Cosmos.System.FileSystem.Listing;

namespace PrismProject.Source.FileSystem
{
    class Disk
    {
        private static readonly CosmosVFS fs = new CosmosVFS();
        private static readonly FolderStructure PFS = new FolderStructure();

        /// <summary> Start the VFS service. </summary>
        public static void Start()
        {
            VFSManager.RegisterVFS(fs);
            fs.Initialize();
            Console.WriteLine("Starting disk service...  [done]");
            if (!FileExists(PFS.System))
            { CreateFolder(PFS.System); WriteFile(PFS.SystemLog, "", false); }
            WriteFile(PFS.SystemLog, "Started disk service sucessfully [ " + Cosmos.HAL.RTC.Hour + ":" + Cosmos.HAL.RTC.Minute + " ]", true);
        }
        private static string ParseFullPath(string FullPath)
        {
            return FullPath.Replace("/", @"\");
        }
        public static void Format(int DiskID)
        { fs.Format(DiskID.ToString(), "FAT32", true); }
        public static int GetSpace(int DiskID)
        { return (int)fs.GetAvailableFreeSpace(DiskID + ":"); }
        public static string GetFSType(int DiskID)
        { return fs.GetFileSystemType(DiskID.ToString()); }
        public static List<DirectoryEntry> GetFolderList(string FullPath)
        { return fs.GetDirectoryListing(ParseFullPath(FullPath)); }
        public static void CreateFolder(string FullPath)
        {
            try
            { fs.CreateDirectory(ParseFullPath(FullPath)); }
            catch (Exception e)
            { WriteFile(@"0:\System2\Crash.log", "\n[Error] "+e, true); }
        }
        public static void WriteFile(string FullPath, string Contents, bool Append)
        {
            try
            { switch (Append)
                {
                    case true: File.WriteAllText(ParseFullPath(FullPath), File.ReadAllText(ParseFullPath(FullPath))+Contents); break;
                    case false: File.WriteAllText(ParseFullPath(FullPath), Contents); break;
                } }
            catch (Exception e)
            { WriteFile(@"0:\System2\Crash.log", "\n[Error] Failed to write to "+ParseFullPath(FullPath)+"\n"+e.ToString(), true); }
        }
        public static string ReadFile(string FullPath)
        { return File.ReadAllText(ParseFullPath(FullPath)); }
        public static List<DirectoryEntry> GetDisks()
        { return fs.GetVolumes(); }
        public static void DeleteEntry(string FullPath)
        {
            File.Delete(ParseFullPath(FullPath));
        }
        public static bool FileExists(string FullPath)
        {
            return File.Exists(ParseFullPath(FullPath));
        }
    }
}
