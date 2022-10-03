using System.Text;

namespace PrismBinary.Archive.TAR
{
    // https://docs.fileformat.com/compression/tar/
    public unsafe class TARFile
    {
        public TARFile(byte[] Binary)
        {
            Files = new();

            fixed (byte* PTR = Binary)
            {
                for (byte* T = PTR; T < T + Binary.Length; T += 512)
                {
                    Header* H = (Header*)T;
                    if (H->Name[0] == 0)
                    {
                        break;
                    }

                    byte[] Data = new byte[OCS2L(H->Size)];
                    fixed (byte* P = Data)
                    {
                        Cosmos.Core.MemoryOperations.Copy(P, (byte*)((uint)T + 512), Data.Length);
                    }

                    Files.Add(
                        Encoding.UTF8.GetString(H->Name, 100).Trim('\0'),
                        Data);

                    T += SizeToSec((ulong)Data.Length) * 512;
                }
            }
        }

        #region TAR Data

        public Dictionary<string, byte[]> GetFiles()
        {
            return Files;
        }
        internal Dictionary<string, byte[]> Files;

        #endregion

        #region Reading

        public byte[] ReadAllBytes(string File)
        {
            File = Format(File);

            return Files[File];
        }
        public string ReadAllText(string File)
        {
            File = Format(File);

            return Encoding.UTF8.GetString(Files[File]);
        }

        public string[] ListFiles(string Path)
        {
            Path = Format(Path);

            List<string> TFiles = new();
            foreach (string N in Files.Keys)
            {
                if (N.StartsWith(Path))
                {
                    TFiles.Add(N);
                }
            }
            return TFiles.ToArray();
        }

        #endregion

        #region Writing

        public void WriteAllBytes(string File, byte[] Binary)
        {
            File = Format(File);

            if (Files.ContainsKey(File))
            {
                Files[File] = Binary;
            }
            else
            {
                Files.Add(File, Binary);
            }
        }
        public void WriteAllText(string File, string Contents)
        {
            File = Format(File);

            if (Files.ContainsKey(File))
            {
                Files[File] = Encoding.UTF8.GetBytes(Contents);
            }
            else
            {
                Files.Add(File, Encoding.UTF8.GetBytes(Contents));
            }
        }

        public void Delete(string File)
        {
            File = Format(File);

            Files.Remove(File);
        }
        public void Create(string File)
        {
            File = Format(File);

            Files.Add(File, Array.Empty<byte>());
        }

        #endregion

        #region Misc

        public static ulong SizeToSec(ulong size)
        {
            return ((size - (size % 512)) / 512) + ((size % 512) != 0 ? 1ul : 0);
        }
        public static string Format(string Base)
        {
            while (Base.Contains('\\'))
            {
                Base = Base.Replace('\\', '/');
            }
            if (Base.StartsWith('/'))
            {
                return Base[1..];
            }

            Console.WriteLine("Format " + Base + "...");

            return Base;
        }
        public static long OCS2L(byte* PTR)
        {
            return Convert.ToInt64(Encoding.UTF8.GetString(PTR, 12).Trim('\0'), 8);
        }

        #endregion
    }
}