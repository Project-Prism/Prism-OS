using System.Runtime.InteropServices;

namespace PrismOS.ELF.Structure
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public unsafe struct Elf32Ehdr
    {
        [FieldOffset(0)] public fixed byte e_ident[16];
        [FieldOffset(16)] public ushort e_type;
        [FieldOffset(18)] public ushort e_machine;
        [FieldOffset(20)] public uint e_version;
        [FieldOffset(24)] public uint e_entry;
        [FieldOffset(28)] public uint e_phoff;
        [FieldOffset(32)] public uint e_shoff;
        [FieldOffset(36)] public uint e_flags;
        [FieldOffset(40)] public ushort e_ehsize;
        [FieldOffset(42)] public ushort e_phentsize;
        [FieldOffset(44)] public ushort e_phnum;
        [FieldOffset(46)] public ushort e_shentsize;
        [FieldOffset(48)] public ushort e_shnum;
        [FieldOffset(50)] public ushort e_shstrndx;
    }
}