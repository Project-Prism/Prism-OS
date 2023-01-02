using PrismRuntime.ELF.Structure.ELFSectionHeader;
using PrismRuntime.ELF.Structure.ELFProgramHeader;
using PrismRuntime.ELF.Structure.ELFHeader;
using PrismRuntime.ELF.Structure;
using Cosmos.Core;
using PrismTools;

namespace PrismRuntime.ELF
{
    public unsafe class ELFFile32
	{
        /// <summary>
        /// Creates a new instance of the <see cref="ELFFile32"/> class.
        /// </summary>
        /// <param name="Buffer"></param>
		public ELFFile32(byte[] Buffer)
		{
            fixed (byte* P = Buffer)
            {
                // Load main file header.
                Header = new((ELFHeader32*)P);
                SectionHeaders = new();
                StringTable = new();
                Debugger = new("ELF");

                // Load the first section header.
                ELFSectionHeader32 SHHeader = new((ELFSectionHeader32*)(P + Header.SHOffset));

                // Load all section headers.
                for (int I = 0; I < Header.SHCount; I++)
                {
                    ELFSectionHeader32 X = new(&SHHeader + I);
                    if (X.Type == ELFSectionType.StringTable)
                    {
                        StringTable.Add(X.Offset);
                    }
                    SectionHeaders.Add(X);
                }

                // Load the first program header.
                ELFProgramHeader32* PHeader = (ELFProgramHeader32*)(P + Header.PHOffset);

                // Load all program headers.
                for (int I = 0; I < Header.PHCount; I++, PHeader++)
                {
                    switch (PHeader->Type)
                    {
                        case ELFProgramType.Null:
                            break;
                        case ELFProgramType.Load:
                            MemoryOperations.Copy((byte*)PHeader->VAddress, P + PHeader->Offset, (int)PHeader->FileSize);
                            break;
                        default:
                            Debugger.Warn("Unsupported program type! Bail out!");
                            return;
                    }
                }
            }
        }

		#region Methods

        /// <summary>
        /// Main entry point to the ELF file.
        /// </summary>
        public void Main()
		{
            Assembly.JumpFar(Header.Entry);
		}

		#endregion

		#region Fields

		public List<ELFSectionHeader32> SectionHeaders;
        public List<uint> StringTable;
        public ELFHeader32 Header;
        public Debugger Debugger;

        #endregion
    }
}