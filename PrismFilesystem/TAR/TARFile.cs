using System.Runtime.InteropServices;
using System.Text;

namespace PrismFilesystem.TAR
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
        /// <param name="Buffer">Raw binary of a tar file.</param>
        public TARFile(byte[] Buffer)
        {
            this.Buffer = Buffer;
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

            for (int I = 0; I < Buffer.Length; I++)
			{
                fixed (byte* P = Buffer)
				{
                    Header* H = (Header*)P + I;
                    
                    if (H->Name == File)
					{
                        byte[] Data = new byte[OCS2L(H->Size)];
                        Marshal.Copy((IntPtr)(P + 512), Data, 0, Data.Length);
                        return Data;
                    }
                }
			}

            throw new FileNotFoundException();
        }

        /// <summary>
        /// Reads all text from the file.
        /// </summary>
        /// <param name="File">file to read from.</param>
        /// <returns>UTF-8 text read from 'File'.</returns>
        public string ReadAllText(string File)
        {
            File = Format(File);

            return Encoding.UTF8.GetString(ReadAllBytes(File));
        }

        /// <summary>
        /// Lists all file names from the tar bundle.
        /// </summary>
        /// <param name="Path">Path to search in (Recusrive)</param>
        /// <returns>All files in the path specified.</returns>
        public string[] List(string Path)
        {
            List<string> VS = new();

            for (int I = 0; I < Buffer.Length; I++)
            {
                fixed (byte* P = Buffer)
                {
                    Header* H = (Header*)P + I;

                    if (Path.StartsWith(H->Name))
                    {
                        VS.Add(H->Name);
                    }
                }
            }

            return VS.ToArray();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes all text in 'Contents' to the file.
        /// </summary>
        /// <param name="File">File to write to.</param>
        /// <param name="Contents">String to write.</param>
        public void WriteAllText(string File, string Contents)
        {
            File = Format(File);

            WriteAllBytes(File, Encoding.UTF8.GetBytes(Contents));
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="File">File to delete, must be full path.</param>
        public void Delete(string File)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an empty file, mush be full path.
        /// </summary>
        /// <param name="File">Path to new file.</param>
        public void Create(string File)
        {
            throw new NotImplementedException();
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

        public byte[] Buffer;

        #endregion
    }
}