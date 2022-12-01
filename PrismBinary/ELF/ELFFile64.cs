using PrismBinary.ELF.Structure.ELFSectionHeader;
using PrismBinary.ELF.Structure.ELFHeader;
using PrismBinary.ELF.Structure;

namespace PrismBinary.ELF
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

                // Load first section header.
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
            }
        }

        #region Fields

        public List<ELFSectionHeader64> SectionHeaders;
        public List<ulong> StringTable;
        public ELFHeader64 Header;

        #endregion
    }
}