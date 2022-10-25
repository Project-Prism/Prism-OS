using System.Runtime.InteropServices;

namespace PrismOS.ELF.Structure
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct Elf32Rel
    {
        [FieldOffset(0)] public uint r_offset;
        [FieldOffset(4)] public uint r_info;
    }

}