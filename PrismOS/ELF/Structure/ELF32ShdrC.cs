namespace PrismOS.ELF.Structure
{
    public unsafe class Elf32ShdrC
    {
        public Elf32ShdrC(Elf32Shdr* st)
        {
            NameOffset = st->sh_name;
            Type = (SectionType)st->sh_type;
            Flag = (SectionAttributes)st->sh_flags;
            Addr = st->sh_addr;
            Offset = st->sh_offset;
            Size = st->sh_size;
            Link = st->sh_link;
            Info = st->sh_info;
            Addralign = st->sh_addralign;
            Entsize = st->sh_entsize;
        }

        public string Name;
        public uint NameOffset;
        public SectionType Type;
        public SectionAttributes Flag;
        public uint Addr;
        public uint Offset;
        public uint Size;
        public uint Link;
        public uint Info;
        public uint Addralign;
        public uint Entsize;
    }
}