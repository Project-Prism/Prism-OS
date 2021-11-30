using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.Listing;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.IO;

namespace PrismOS.Storage
{
    public class Framework
    {
        public Framework()
        {
            VFSManager.RegisterVFS(VFS);
            VFS.Initialize(true);
        }

        public static readonly CosmosVFS VFS = new();

        public struct Drives
        {
            public static int CD_Drive { get => VFS.GetVolumes().Count - 1; }
        }
        public enum DataTypes
        {
            Text,
            Lines,
            Bytes
        }

        public static dynamic Read(string Path, DataTypes Type)
        {
            return Type switch
            {
                DataTypes.Text => File.ReadAllText(Path),
                DataTypes.Lines => File.ReadAllLines(Path),
                DataTypes.Bytes => File.ReadAllBytes(Path),
                _ => null,
            };
        }

        public static void Write(string Path, dynamic Contents, DataTypes Type)
        {
            switch (Type)
            {
                case DataTypes.Text: File.WriteAllText(Path, Contents); break;
                case DataTypes.Lines: File.WriteAllLines(Path, Contents); break;
                case DataTypes.Bytes:
                    File.WriteAllBytes(Path, Contents); break;

                    // Throw an exception if the data type is not correct
                    throw new Exception("This data type has either not been added yet or does not exist.");
            }
        }

        public static List<DirectoryEntry> DirList(string Path)
        {
            if (Path.EndsWith("\\"))
            {
                return Extras.VFS.GetDirectoryListing(Path);
            }
            else
            {
                throw new Exception("Specified file was not a folder.");
            }
        }

        public static void Create(string Path)
        {
            if (Path.EndsWith("\\"))
            {
                Directory.CreateDirectory(Path);
            }
            else
            {
                File.Create(Path);
            }
        }

        public static void Delete(string Path)
        {
            if (Path.EndsWith("\\"))
            {
                Directory.Delete(Path);
            }
            else
            {
                File.Delete(Path);
            }
        }

        public static void Move(string Path, string NewPath)
        {
            if (Path.EndsWith("\\"))
            {
                Directory.Move(Path, NewPath);
            }
            else
            {
                File.Move(Path, NewPath);
            }
        }
    }
}