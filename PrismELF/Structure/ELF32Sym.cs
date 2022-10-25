using System.Runtime.InteropServices;

namespace PrismELF.Structure
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Elf32Sym
    {
        [FieldOffset(0)] public uint st_name;
        [FieldOffset(4)] public uint st_value;
        [FieldOffset(8)] public uint st_size;
        [FieldOffset(12)] public byte st_info;
        [FieldOffset(13)] public byte st_other;
        [FieldOffset(14)] public ushort st_shndx;
    }
}