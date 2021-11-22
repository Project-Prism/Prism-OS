using Cosmos.System.FileSystem.Listing;
using System;
using System.Collections.Generic;
using System.IO;

namespace Prism.Libraries.Storage
{
    public static class VFS
    {
        public static void CreateFolder(string FullPath)
        {
            try
            {
                Filesystem.VFS.CreateDirectory(ParseFullPath(FullPath));
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
                Filesystem.VFS.Format(DiskID.ToString(), "FAT32", true);
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
                return Filesystem.VFS.GetVolumes();
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
                return Filesystem.VFS.GetDirectoryListing(ParseFullPath(FullPath));
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
                return Filesystem.VFS.GetFileSystemType(DiskID.ToString());
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
                return (int)Filesystem.VFS.GetAvailableFreeSpace(DiskID + ":");
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