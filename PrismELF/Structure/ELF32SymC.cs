namespace PrismELF.Structure
{
    public unsafe class Elf32SymC
    {
        public Elf32SymC(Elf32Sym* st)
        {
            NameOffset = st->st_name;
            Value = st->st_value;
            Size = st->st_size;
            Info = st->st_info;
            Other = st->st_other;
            Shndx = st->st_shndx;

            Binding = (SymbolBinding)(Info >> 0x4);
            Type = (SymbolType)(Info & 0x0F);
        }

        public string Name;
        public uint NameOffset;
        public uint Value;
        public uint Size;
        public byte Info;
        public byte Other;
        public ushort Shndx;
        public SymbolBinding Binding;
        public SymbolType Type;
    }
}