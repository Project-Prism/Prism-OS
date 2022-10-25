namespace PrismOS.ELF.Structure
{
    public unsafe class Elf32RelC
    {
        public uint Offset;
        public uint Info;
        public int Section;
        public uint Symbol;
        public RelocationType Type;

        public Elf32RelC(Elf32Rel* st)
        {
            Offset = st->r_offset;
            Info = st->r_info;

            Symbol = Info >> 8;
            Type = (RelocationType)(byte)Info;
        }
    }
}