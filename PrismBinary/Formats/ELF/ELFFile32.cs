using PrismBinary.Formats.ELF.Structure.ELFSectionHeader;
using PrismBinary.Formats.ELF.Structure.ELFProgramHeader;
using PrismBinary.Formats.ELF.Structure.ELFHeader;
using PrismBinary.Formats.ELF.Structure;

namespace PrismBinary.Formats.ELF
{
    public unsafe class ELFFile32
	{
        /// <summary>
        /// Creates a new instance of the <see cref="ELFFile32"/> class.
        /// </summary>
        /// <param name="Binary"></param>
		public ELFFile32(byte[] Binary)
		{
            fixed (byte* P = Binary)
            {
                // Load main file header.
                ELFHeader32 Header = new((ELFHeader32*)P);

                // Assign entry point.
                Main = (delegate* unmanaged<void>)(Header.EntryPoint);

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
                            Buffer.MemoryCopy(PHeader + PHeader->Offset, (byte*)PHeader->VAddress, PHeader->FileSize, PHeader->FileSize);
                            break;
                        default:
                            throw new ArgumentException("Unsupported program type!");
                    }
                }
            }
        }

        #region Methods

        /// <summary>
        /// Main entry point to the ELF file.
        /// </summary>
        public delegate* unmanaged<void> Main;

		#endregion
	}
}