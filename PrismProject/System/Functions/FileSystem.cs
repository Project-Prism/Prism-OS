using System.Collections.Generic;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.IO;

namespace PrismProject
{
    public class Filesystem
    {
        public static CosmosVFS fs = new CosmosVFS();
        public static string Drive_0 = @"0:\";

        public static void Init()
        { //initalise the filesystem, best to only trigger this on boot.
            VFSManager.RegisterVFS(fs);
        }
        public static void format(string driveID, bool quickformat)
        {
            fs.Format(driveID, "FAT32", quickformat);
        }
        public static List<DirectoryEntry> List_Directory(string path)
        {
            return fs.GetDirectoryListing(Convert.ToString(path));
        }
        public static long free_space(string args)
        {
            return fs.GetAvailableFreeSpace(args);
        }
        public static void Create_driectory(string path)
        {
            fs.CreateDirectory(Convert.ToString(path));
        }
        public static void Create_file(string path)
        {
            File.Create(path);
        }
        public static string Read_file(string path)
        {
            return File.ReadAllText(path);
        }
        public static bool Check_exists(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
        }
        public static bool Write(string path, string contents)
        {
            if (Check_exists(path))
                return false;
            else
                File.WriteAllText(path, contents);
                return true;
        }
        public static string Format_type(string DriveID)
        {
            return fs.GetFileSystemType(DriveID);
        }
    }
}
