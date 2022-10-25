using System.Runtime.InteropServices;

namespace PrismELF.Structure
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Elf32Shdr
    {
        [FieldOffset(0)] public uint sh_name;
        [FieldOffset(4)] public uint sh_type;
        [FieldOffset(8)] public uint sh_flags;
        [FieldOffset(12)] public uint sh_addr;
        [FieldOffset(16)] public uint sh_offset;
        [FieldOffset(20)] public uint sh_size;
        [FieldOffset(24)] public uint sh_link;
        [FieldOffset(28)] public uint sh_info;
        [FieldOffset(32)] public uint sh_addralign;
        [FieldOffset(36)] public uint sh_entsize;
    }
}