namespace PrismOS.ELF.Structure
{
    public unsafe class Elf32EhdrC
    {
        public Elf32EhdrC(Elf32Ehdr* st)
        {
            Type = st->e_type;
            Machine = st->e_machine;
            Version = st->e_version;
            Entry = st->e_entry;
            Phoff = st->e_phoff;
            Shoff = st->e_shoff;
            Flags = st->e_flags;
            Ehsize = st->e_ehsize;
            Phentsize = st->e_type;
            Phnum = st->e_phnum;
            Shentsize = st->e_shentsize;
            Shnum = st->e_shnum;
            Shstrndx = st->e_shstrndx;
        }

        public ushort Type;
        public ushort Machine;
        public uint Version;
        public uint Entry;
        public uint Phoff;
        public uint Shoff;
        public uint Flags;
        public ushort Ehsize;
        public ushort Phentsize;
        public ushort Phnum;
        public ushort Shentsize;
        public ushort Shnum;
        public ushort Shstrndx;
    }
}