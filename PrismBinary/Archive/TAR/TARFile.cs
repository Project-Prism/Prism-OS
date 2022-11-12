using System.Runtime.InteropServices;
using System.Text;

namespace PrismBinary.Archive.TAR
{
    /// <summary>
    /// Class used for loading TAR files
    /// <seealso cref="https://docs.fileformat.com/compression/tar/"/>
    /// </summary>
    public unsafe class TARFile
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TARFile"/> class.
        /// </summary>
        /// <param name="Binary">Raw binary of a tar file.</param>
        public TARFile(byte[] Binary)
        {
            _Files = new();

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
                    Marshal.Copy((IntPtr)(T + 512), Data, 0, Data.Length);

                    _Files.Add(Encoding.UTF8.GetString(H->Name, 100).Trim('\0'), Data);

                    T += SizeToSec((ulong)Data.Length) * 512;
                }
            }
        }

		#region Methods

		#region Reading

		/// <summary>
		/// Reads all text and splits it by \n
		/// </summary>
		/// <param name="File">Name of the file to read.</param>
		/// <returns>Text of 'File' split by \n.</returns>
		public string[] ReadAllLines(string File)
        {
            return ReadAllText(File).Split('\n');
        }
        /// <summary>
        /// Reads all the bytes of the file.
        /// </summary>
        /// <param name="File">Name of the file to read.</param>
        /// <returns>All bytes of 'File'.</returns>
        public byte[] ReadAllBytes(string File)
        {
            File = Format(File);

            return _Files[File];
        }
        /// <summary>
        /// Reads all text from the file.
        /// </summary>
        /// <param name="File">file to read from.</param>
        /// <returns>UTF-8 text read from 'File'.</returns>
        public string ReadAllText(string File)
        {
            File = Format(File);

            return Encoding.UTF8.GetString(_Files[File]);
        }
        /// <summary>
        /// Gets all files in the tar file.
        /// </summary>
        /// <returns>All files bundled in the tar.</returns>
        public Dictionary<string, byte[]> GetFiles()
        {
            return _Files;
        }
        /// <summary>
        /// Lists all file names from the tar bundle.
        /// </summary>
        /// <param name="Path">Path to search in (Recusrive)</param>
        /// <returns>All files in the path specified.</returns>
        public string[] ListFiles()
        {
            return _Files.Keys.ToArray();
        }

        #endregion

        #region Writing

        /// <summary>
        /// Writes all lines in 'Lines' to the file.
        /// </summary>
        /// <param name="File">File to write to.</param>
        /// <param name="Lines">Line to write.</param>
        public void WriteAllLines(string File, string[] Lines)
        {
            File = Format(File);

            string S = "";
            for (int I = 0; I < Lines.Length; I++)
            {
                S += Lines[I] + '\n';
            }
            WriteAllText(File, S[0..(S.Length - 1)]);
        }
        /// <summary>
        /// Writes all bytes om 'Binary' to the file.
        /// </summary>
        /// <param name="File">File to write to.</param>
        /// <param name="Binary">Binary to write.</param>
        public void WriteAllBytes(string File, byte[] Binary)
        {
            File = Format(File);

            if (_Files.ContainsKey(File))
            {
                _Files[File] = Binary;
            }
            else
            {
                _Files.Add(File, Binary);
            }
        }
        /// <summary>
        /// Writes all text in 'Contents' to the file.
        /// </summary>
        /// <param name="File">File to write to.</param>
        /// <param name="Contents">String to write.</param>
        public void WriteAllText(string File, string Contents)
        {
            File = Format(File);

            if (_Files.ContainsKey(File))
            {
                _Files[File] = Encoding.UTF8.GetBytes(Contents);
            }
            else
            {
                _Files.Add(File, Encoding.UTF8.GetBytes(Contents));
            }
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="File">File to delete, must be full path.</param>
        public void Delete(string File)
        {
            File = Format(File);

            _Files.Remove(File);
        }
        /// <summary>
        /// Creates an empty file, mush be full path.
        /// </summary>
        /// <param name="File">Path to new file.</param>
        public void Create(string File)
        {
            File = Format(File);

            _Files.Add(File, Array.Empty<byte>());
        }

        #endregion

        #region Misc

        private static ulong SizeToSec(ulong size)
        {
            return ((size - (size % 512)) / 512) + ((size % 512) != 0 ? 1ul : 0);
        }
        private static string Format(string Base)
        {
            while (Base.Contains('\\'))
            {
                Base = Base.Replace('\\', '/');
            }
            if (Base.StartsWith('/'))
            {
                return Base[1..];
            }

            return Base;
        }
        private static long OCS2L(byte* PTR)
        {
            return Convert.ToInt64(Encoding.UTF8.GetString(PTR, 12).Trim('\0'), 8);
        }

        #endregion

        #endregion

        #region Fields

        private readonly Dictionary<string, byte[]> _Files;

        #endregion
    }
}