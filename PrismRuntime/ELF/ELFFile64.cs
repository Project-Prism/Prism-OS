using PrismRuntime.ELF.Structure.ELFSectionHeader;
using PrismRuntime.ELF.Structure.ELFProgramHeader;
using PrismRuntime.ELF.Structure.ELFHeader;
using PrismRuntime.ELF.Structure;
using Cosmos.Core;
using PrismTools;

namespace PrismRuntime.ELF
{
    public unsafe class ELFFile64
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ELFFile64"/> class.
        /// </summary>
        /// <param name="Buffer"></param>
		public ELFFile64(byte[] Buffer)
        {
            fixed (byte* P = Buffer)
            {
                // Load main file header.
                Header = new((ELFHeader64*)P);
                SectionHeaders = new();
                StringTable = new();
                Debugger = new("ELF");

                // Load the first section header.
                ELFSectionHeader64 SHHeader = new((ELFSectionHeader64*)(P + Header.SHOffset));

                // Load all section headers.
                for (int I = 0; I < Header.SHCount; I++)
                {
                    ELFSectionHeader64 X = new(&SHHeader + I);
                    if (X.Type == ELFSectionType.StringTable)
                    {
                        StringTable.Add(X.Offset);
                    }
                    SectionHeaders.Add(X);
                }

                // Load the first program header.
                ELFProgramHeader64* PHeader = (ELFProgramHeader64*)(P + Header.PHOffset);

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

        public void Main()
        {
            throw new NotImplementedException();
            //JumpImpl.JumpFar(Header.Entry);
        }

        #endregion

        #region Fields

        public List<ELFSectionHeader64> SectionHeaders;
        public List<ulong> StringTable;
        public ELFHeader64 Header;
        public Debugger Debugger;

        #endregion
    }
}