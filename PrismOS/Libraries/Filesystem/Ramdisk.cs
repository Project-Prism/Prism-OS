using System.Collections.Generic;
using Cosmos.HAL.BlockDevice;
using System.IO;

namespace PrismOS.Libraries.Filesystem
{
    public class Ramdisk
    {
        public Ramdisk()
        {
            foreach (BlockDevice Drive in BlockDevice.Devices)
            {
                char DiskID = 'A';
                byte[] Binary = new byte[Drive.BlockCount * Drive.BlockSize];
                Drive.ReadBlock(0, Drive.BlockCount, ref Binary);
                BinaryReader Reader = new(new MemoryStream(Binary));
                Drives.Add(new(DiskID));
                int FolderCount = Reader.ReadInt32();
                int FileCount = Reader.ReadInt32();
                for (int F = 0; F < FolderCount; F++)
                {
                    Drives[DiskID].Root.Folders.Add(new(Reader.ReadString()));
                }
                for (int F = 0; F < FileCount; F++)
                {
                    Drives[DiskID].Root.Files.Add(new(Reader.ReadString(), Reader.ReadBytes((int)Reader.ReadUInt64())));
                }
            }
        }

        public class Drive
        {
            public Drive(char DiskID)
            {
                this.DiskID = DiskID;
                Root = new("/");
            }

            public char DiskID;
            public Folder Root;
        }
        public class File
        {
            public File(string Name, byte[] Binary)
            {
                this.Name = Name;
                this.Binary = Binary;
            }

            public string Name;
            public byte[] Binary;
        }
        public class Folder
        {
            public Folder(string Path)
            {
                this.Path = Path;
            }

            public string Path;
            public List<File> Files = new();
            public List<Folder> Folders = new();
        }
        public List<Drive> Drives;
    }
}