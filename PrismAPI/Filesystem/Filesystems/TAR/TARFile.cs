using System.Text;

namespace PrismAPI.Filesystem.Filesystems.TAR;

/// <summary>
/// Class used for loading TAR files
/// <seealso href="https://docs.fileformat.com/compression/tar/"/>
/// </summary>
public unsafe class TARFile
{
    /// <summary>
    /// Creates a new instance of the <see cref="TARFile"/> class.
    /// </summary>
    /// <param name="Buffer">Raw binary of a tar file.</param>
    public TARFile(byte[] Buffer)
    {
        Reader = new(new MemoryStream(Buffer));
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
        // Properly format the path of the input.
        File = Format(File);

        // Reset the reader position to be safe.
        Reader.BaseStream.Position = 0;

        // Loop over all data.
        for (int I = 0; I < Reader.BaseStream.Length; I++)
        {
            // Get the header for the current file.
            Header H = new(Reader.ReadBytes(512));

            // Check if the current file is the target specified.
            if (File.StartsWith(H.Name))
            {
                return Reader.ReadBytes((int)H.Size);
            }

            // Increment the reader to skip the data section.
            Reader.BaseStream.Position += H.Size;
        }

        // Inform user that file does not exist.
        throw new FileNotFoundException();
    }

    /// <summary>
    /// Reads all text from the file.
    /// </summary>
    /// <param name="File">file to read from.</param>
    /// <returns>UTF-8 text read from 'File'.</returns>
    public string ReadAllText(string File)
    {
        return Encoding.UTF8.GetString(ReadAllBytes(File));
    }

    /// <summary>
    /// Lists all file names from the tar bundle.
    /// </summary>
    /// <param name="Path">Path to search in (Recusrive)</param>
    /// <returns>All files in the path specified.</returns>
    public IEnumerable<string> List(string Path)
    {
        // Reset the reader position to be safe.
        Reader.BaseStream.Position = 0;

        // Properly format the path of the input.
        Path = Format(Path);

        // Loop over all data.
        for (int I = 0; I < Reader.BaseStream.Length; I++)
        {
            // Get the header for the current file.
            Header H = new(Reader.ReadBytes(512));

            // Check if the current file is the target specified.
            if (Path.StartsWith(H.Name))
            {
                yield return H.Name;
            }

            // Increment the reader to skip the data section.
            Reader.BaseStream.Position += H.Size;
        }
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
        // Create stringbuilder instance,
        StringBuilder Builder = new();

        for (int I = 0; I < Lines.Length; I++)
        {
            Builder.AppendLine(Lines[I]);
        }

        WriteAllText(File, Builder.ToString());
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

    #endregion

    #endregion

    #region Fields

    public BinaryReader Reader;

    #endregion
}