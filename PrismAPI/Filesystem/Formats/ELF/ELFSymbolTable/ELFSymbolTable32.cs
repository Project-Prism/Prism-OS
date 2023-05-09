using System.Runtime.InteropServices;

namespace PrismAPI.Filesystem.Formats.ELF.ELFSymbolTable;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct ELFSymbolTable32
{
    public ELFSymbolTable32(ELFSymbolTable32* Original)
    {
        Name = Original->Name;
        Value = Original->Value;
        Size = Original->Size;
        Info = Original->Info;
        Other = Original->Other;
        SHIndex = Original->SHIndex;
    }

    #region Flags

    public uint Name;
    public uint Value;
    public uint Size;
    public char Info;
    public char Other;
    public ushort SHIndex;

    #endregion
}