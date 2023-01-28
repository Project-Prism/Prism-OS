using PrismBinary.Formats.ELF.Structure.ELFSectionHeader;
using PrismBinary.Formats.ELF.Structure.ELFProgramHeader;
using PrismBinary.Formats.ELF.Structure.ELFHeader;
using PrismBinary.Formats.ELF.Structure;

namespace PrismBinary.Formats.ELF
{
    public unsafe class ELFFile64
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ELFFile64"/> class.
        /// </summary>
        /// <param name="Binary"></param>
		public ELFFile64(byte[] Binary)
        {
            fixed (byte* P = Binary)
            {
                // Load main file header.
                ELFHeader64 Header = new((ELFHeader64*)P);

                // Assign entry point.
                Main = (delegate* unmanaged<void>)(Header.EntryPoint + 512);

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
                            Buffer.MemoryCopy((byte*)PHeader->VAddress, P + PHeader->Offset, PHeader->FileSize, PHeader->FileSize);
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