using System.Runtime.InteropServices;

namespace PrismAPI.Filesystem.Formats.ELF.ELFSymbolTable;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct ELFSymbolTable64
{
    public ELFSymbolTable64(ELFSymbolTable64* Original)
    {
        Name = Original->Name;
        Value = Original->Value;
        Size = Original->Size;
        Info = Original->Info;
        Other = Original->Other;
        SHIndex = Original->SHIndex;
    }

    #region Fields

    public uint Name;
    public ulong Value;
    public uint Size;
    public char Info;
    public char Other;
    public ushort SHIndex;

    #endregion
}