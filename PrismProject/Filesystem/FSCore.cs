using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.IO;

namespace PrismProject.Filesystem
{
    class FSCore
    {

        public static readonly CosmosVFS fs = new CosmosVFS();

        public static void CreateFolder(string FullPath)
        {
            try
            {
                fs.CreateDirectory(ParseFullPath(FullPath));
            }
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }

        public static void DeleteEntry(string FullPath)
        {
            try
            {
                File.Delete(ParseFullPath(FullPath));
            }
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }

        public static bool FileExists(string FullPath)
        {
            try
            {
                return File.Exists(ParseFullPath(FullPath));
            }
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }

        public static void Format(int DiskID)
        {
            try
            {
                fs.Format(DiskID.ToString(), "FAT32", true);
            }
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }

        public static List<DirectoryEntry> GetDisks()
        {
            try
            {
                return fs.GetVolumes();
            }
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }

        public static List<DirectoryEntry> GetFolderList(string FullPath)
        {
            try
            {
                return fs.GetDirectoryListing(ParseFullPath(FullPath));
            }
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }

        public static string GetFSType(int DiskID)
        {
            try
            {
                return fs.GetFileSystemType(DiskID.ToString());
            }
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }

        public static int GetSpace(int DiskID)
        {
            try
            {
                return (int)fs.GetAvailableFreeSpace(DiskID + ":");
            }
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }

        public static string ReadFile(string FullPath)
        {
            try
            {
                return File.ReadAllText(ParseFullPath(FullPath));
            }
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }

        /// <summary> Start the VFS service. </summary>
        public static void StartDisk()
        {
            try
            {
                VFSManager.RegisterVFS(fs);
                fs.Initialize();
            }
            catch (Exception aException)
            {
                throw new Exception("An error occured while starting the filesystem. (" + aException.Message + ")");
            }
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
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }

        private static string ParseFullPath(string FullPath)
        {
            try
            {
                return FullPath.Replace("/", @"\");
            }
            catch (Exception aException)
            {
                throw new Exception(aException.Message);
            }
        }
    }
}