using System.Text;

namespace PrismAPI.Filesystem.Filesystems.TAR;

/// <summary>
/// The header file for posix tar files.
/// </summary>
public struct Header
{
    /// <summary>
    /// Creates a new instance of the <see cref="Header"/>  class.
    /// </summary>
    /// <param name="Binary">The TAR header.</param>
    public Header(byte[] Binary)
    {
        // Create new reader instance.
        BinaryReader Reader = new(new MemoryStream(Binary));

        // Assign all values.
        Name = Encoding.ASCII.GetString(Reader.ReadBytes(100)).Trim('\0');
        Mode = Reader.ReadBytes(8);
        OwnerUID = Reader.ReadBytes(8);
        GroupUID = Reader.ReadBytes(8);
        Size = Convert.ToInt64(Encoding.ASCII.GetString(Reader.ReadBytes(12)).Trim('\0'), 8);
        LastModifyTime = Reader.ReadBytes(12);
        HRChecksum = Reader.ReadBytes(8);
        TypeFlag = (FileTypes)Reader.ReadByte();

        LinkName = Reader.ReadBytes(100);
        Magic = Reader.ReadBytes(6);
        Version = Reader.ReadBytes(2);
        UName = Reader.ReadBytes(32);
        GName = Reader.ReadBytes(32);
        DevMajor = Reader.ReadBytes(8);
        DevMinor = Reader.ReadBytes(8);
        Prefix = Reader.ReadBytes(155);
    }

    #region Fields

    // GNU Compatible
    public string Name;
    public byte[] Mode;
    public byte[] OwnerUID;
    public byte[] GroupUID;
    public long Size;
    public byte[] LastModifyTime;
    public byte[] HRChecksum;
    public FileTypes TypeFlag;

    // Posix Extention
    public byte[] LinkName;
    public byte[] Magic;
    public byte[] Version;
    public byte[] UName;
    public byte[] GName;
    public byte[] DevMajor;
    public byte[] DevMinor;
    public byte[] Prefix;

    #endregion
}