using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.IO;

namespace PrismProject.System2
{
    /// <summary>
    /// VFS file system
    /// </summary>
    internal class VFS
    {
        private static readonly CosmosVFS fs = new CosmosVFS();
        public static string D0 = "0";

        /// <summary>
        /// Initilise VFS, must be done on boot.
        /// </summary>
        public static void StartService()
        {
            VFSManager.RegisterVFS(fs);
        }

        /// <summary>Get a drives assigned letter, if it has one.</summary>
        /// <param name="Drive_ID"></param>
        /// <returns>string</returns>
        public static string GDL(string Drive_ID)
        {
            return new DriveInfo(Drive_ID).Name;
        }

        /// <summary>Assigns a mounted drive a letter.</summary>
        /// <param name="Drive_ID"></param>
        /// <param name="Drive_name"></param>
        public static void SDL(string Drive_ID, string Drive_name)
        {
            new DriveInfo(Drive_ID).VolumeLabel = Drive_name;
        }

        /// <summary>Get a drive's type.</summary>
        /// <param name="Drive_ID"></param>
        /// <returns>string</returns>
        public static string GDT(string Drive_ID)
        {
            return new DriveInfo(Drive_ID).DriveType.ToString();
        }

        /// <summary>Format a specified drive.</summary>
        /// <param name="driveID"></param>
        /// <param name="quickformat"></param>
        /// <returns>Nothing</returns>
        public static int Format(string driveID, bool quickformat)
        {
            try
            {
                fs.Format(driveID, "FAT32", quickformat);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>List the contents of a specified drive.</summary>
        /// <param name="path"></param>
        /// <returns>String array</returns>
        public static string[] LD(string path)
        {
            return Directory.GetFiles(path);
        }

        /// <summary>Get available free space of a drive (in bytes)</summary>
        /// <param name="args"></param>
        /// <returns>long</returns>
        public static long GFS(string args)
        {
            return fs.GetAvailableFreeSpace(args);
        }

        /// <summary>Make a directory on a drive.</summary>
        /// <param name="path"></param>
        public static void MD(string path)
        {
            fs.CreateDirectory(Convert.ToString(path));
        }

        /// <summary>Make a file (not folder) on a drive.</summary>
        /// <param name="path"></param>
        public static void MF(string path)
        {
            File.Create(path);
        }

        /// <summary>Read a file's contents</summary>
        /// <param name="path"></param>
        /// <returns>File's contents.</returns>
        public static string Read(string path)
        {
            return File.ReadAllText(path).ToString();
        }

        /// <summary>Write to a file.</summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        public static void Write(string path, string contents)
        {
            if (!CE(path))
                MF(path);
            File.WriteAllText(path, contents);
        }

        /// <summary>Check if a file exists.</summary>
        /// <param name="path"></param>
        /// <returns>bool</returns>
        public static bool CE(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
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