using PrismELF.Structure;
using Cosmos.Core.Memory;

namespace PrismELF
{
    public unsafe class UnmanagedExecutible
    {
        public UnmanagedExecutible(byte* Binary)
        {
            // Use max value as we will not read past the size
            Stream = new UnmanagedMemoryStream(Binary, long.MaxValue);
        }

        private readonly UnmanagedMemoryStream Stream;
        private byte* FinalExecutible;
        private ElfFile ELF;

        public void Load()
        {
            ELF = new ElfFile(Stream);


            /*
             * 1. determin the total size of the final loaded sections
             * 2. maloc some space for them and allocate them
             * 3. update headers location information
             */

            //calcualte bss secstion size
            for (var i = 0; i < ELF.SectionHeaders.Count; i++)
            {
                var header = ELF.SectionHeaders[i];
                if (header.Type == SectionType.NotPresentInFile)
                {
                    uint bssbase = 0;
                    for (var index = 0; index < ELF.Symbols.Count; index++)
                    {
                        var sym = ELF.Symbols[index];
                        if (sym.Shndx == 0xFFF2)
                        {
                            var size = sym.Value;
                            sym.Value = bssbase;
                            bssbase += size;
                            sym.Shndx = (ushort)i;
                        }
                    }

                    header.Size = bssbase;
                    break;
                }
            }

            //calcualte final size
            uint totalSize = 0;
            foreach (var header in ELF.SectionHeaders)
            {
                if ((header.Flag & SectionAttributes.Alloc) == SectionAttributes.Alloc)
                {
                    totalSize += header.Size;
                }
            }

            //alocate final size
            FinalExecutible = Heap.Alloc(totalSize);

            UnmanagedMemoryStream stream = new(FinalExecutible, long.MaxValue);
            var writer = new BinaryWriter(stream);

            foreach (var header in ELF.SectionHeaders)
            {
                if ((header.Flag & SectionAttributes.Alloc) != SectionAttributes.Alloc) continue;

                if (header.Type == SectionType.NotPresentInFile)
                {
                    //update the meta data
                    header.Offset = (uint)writer.BaseStream.Position;

                    for (int i = 0; i < header.Size; i++)
                    {
                        writer.Write((byte)0);
                    }
                }
                else
                {
                    //read the data from the orginal file
                    var reader = new BinaryReader(Stream);
                    reader.BaseStream.Position = header.Offset;

                    //update the meta data
                    header.Offset = (uint)writer.BaseStream.Position;

                    //write the data from the old file into the loaded executible
                    for (int i = 0; i < header.Size; i++)
                    {
                        writer.Write(reader.ReadByte());
                    }
                }
            }
        }


        public void Link()
        {
            foreach (var rel in ELF.RelocationInformation)
            {
                var symval = ELF.Symbols[(int)rel.Symbol].Value;

                var addr = (uint)FinalExecutible +
                           ELF.SectionHeaders[(int)ELF.SectionHeaders[rel.Section].Info].Offset;
                var refr = (uint*)(addr + rel.Offset);

                var memOffset = (uint)FinalExecutible +
                                ELF.SectionHeaders[ELF.Symbols[(int)rel.Symbol].Shndx].Offset;

                switch (rel.Type)
                {
                    case RelocationType.R38632:
                        *refr = (symval + *refr) + memOffset; // Symbol + Offset
                        break;
                    case RelocationType.R386Pc32:
                        *refr = (symval + *refr - (uint)refr) + memOffset; // Symbol + Offset - Section Offset
                        break;
                    case RelocationType.R386None:
                        //nop
                        break;
                    default:
                        Console.WriteLine($"Error RelocationType({(int)rel.Type}) not implmented");
                        break;
                }
            }
        }

        public uint Invoke(string function)
        {
            for (int i = 0; i < ELF.Symbols.Count; i++)
            {
                if (function == ELF.Symbols[i].Name)
                {
                    Invoker.Offset =
                        (uint)FinalExecutible + ELF.Symbols[i].Value +
                        ELF.SectionHeaders[ELF.Symbols[i].Shndx].Offset;

                    Invoker.StoreState();
                    Invoker.CallCode();
                    Invoker.LoadState();

                    break;
                }
            }

            return Invoker.Stack[0];
        }
    }
}