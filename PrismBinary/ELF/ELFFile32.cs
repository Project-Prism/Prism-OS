using PrismBinary.ELF.Structure.ELFSectionHeader;
using PrismBinary.ELF.Structure.ELFHeader;
using PrismBinary.ELF.Structure;

namespace PrismBinary.ELF
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

                // Load first section header.
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
            }
        }

        #region Fields

        public List<ELFSectionHeader32> SectionHeaders;
        public List<uint> StringTable;
        public ELFHeader32 Header;

		#endregion
	}
}