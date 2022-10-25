using PrismELF.Structure;

namespace PrismELF
{
	public unsafe class ElfFile
	{
		public Elf32EhdrC ElfHeader { get; set; }
		public List<Elf32ShdrC> SectionHeaders { get; set; } = new();
		public List<Elf32RelC> RelocationInformation { get; set; } = new();
		public List<Elf32SymC> Symbols { get; set; } = new();
		private readonly List<uint> StringTables = new();

		public string ResolveName(Elf32ShdrC section, uint offset, UnmanagedMemoryStream stream)
		{
			var old = stream.Position;
			if (section.Type != SectionType.SymbolTable)
			{
				stream.Position = StringTables[0] + offset;
			}
			else
			{
				stream.Position = StringTables[1] + offset;
			}
			var reader = new BinaryReader(stream);
			var s = reader.ReadString();
			stream.Position = old;
			return s;
		}

		public ElfFile(UnmanagedMemoryStream stream)
		{
			//load main file header
			ElfHeader = new Elf32EhdrC((Elf32Ehdr*)stream.PositionPointer);

			//load section headers
			var header = (Elf32Shdr*)(stream.PositionPointer + ElfHeader.Shoff);

			for (int i = 0; i < ElfHeader.Shnum; i++)
			{
				var x = new Elf32ShdrC(&header[i]);
				if (x.Type == SectionType.StringTable) StringTables.Add(x.Offset);
				SectionHeaders.Add(x);
			}

			//now we can load names into symbols and process sub data
			for (var index = 0; index < SectionHeaders.Count; index++)
			{
				var sectionHeader = SectionHeaders[index];
				sectionHeader.Name = ResolveName(sectionHeader, sectionHeader.NameOffset, stream);

				switch (sectionHeader.Type)
				{
					case SectionType.Relocation:
						for (int i = 0; i < sectionHeader.Size / sectionHeader.Entsize; i++)
						{
							RelocationInformation.Add(new Elf32RelC(
									(Elf32Rel*)(stream.PositionPointer + sectionHeader.Offset + i * sectionHeader.Entsize))
							{ Section = index });
						}

						break;
					case SectionType.SymbolTable:
						for (int i = 0; i < sectionHeader.Size / sectionHeader.Entsize; i++)
						{
							var x = new Elf32SymC(
								(Elf32Sym*)(stream.PositionPointer + sectionHeader.Offset + i * sectionHeader.Entsize));
							x.Name = ResolveName(sectionHeader, x.NameOffset, stream);
							Symbols.Add(x);
						}

						break;
				}
			}
		}
	}
}