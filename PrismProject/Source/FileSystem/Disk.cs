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

        /// <summary> Start the VFS service. </summary>
        public static void Start() { VFSManager.RegisterVFS(fs); }
        /// <summary> Format a disk </summary>
        public static void Format(string DiskID) { fs.Format(DiskID, "FAT32", true); }
        /// <summary> Get free disk space in bytes </summary>
        public static int GetSpace(string DiskID) { return (int)fs.GetAvailableFreeSpace(DiskID + @":"); }
        /// <summary> Get a disk's file system type </summary>
        public static string GetFs(string DiskID) { return fs.GetFileSystemType(DiskID + @":"); }
        /// <summary> Get a file list in a directory </summary>
        public static List<DirectoryEntry> GetFileList(string DiskID, string Path) { return fs.GetDirectoryListing(Getcd()); }
        /// <summary> Create a new file in a path </summary>
        public static void CreateFile(string DiskID, string Path) { try { fs.CreateFile(DiskID + @":" + Path); }
            catch (Exception e) { e.ToString(); }}
        /// <summary> Write to a file in a path </summary>
        public static void WriteFile(string DiskID, string Path, string Contents) { try { File.WriteAllText(DiskID + @":" + Path, Contents); }
            catch (Exception e) { e.ToString(); }}
        /// <summary> Read a File </summary>
        public static string ReadFile(string DiskID, string Path) { return File.ReadAllText(DiskID + @":" + Path); }
        /// <summary> List all detected disks </summary>
        public static List<DirectoryEntry> GetDisks() { return fs.GetVolumes(); }
        /// <summary> Get current directory </summary>
        public static string Getcd() { return Directory.GetCurrentDirectory(); }
    }
}
