using PrismAPI.Filesystem.Formats.ELF.ELFProgramHeader;
using PrismAPI.Filesystem.Formats.ELF.ELFHeader;
using PrismAPI.Filesystem.Formats.ELF;
using PrismAPI.Runtime.SystemCall;

namespace PrismAPI.Runtime;

/// <summary>
/// The base abstract level of an executable.
/// Includes a pointer to an entry point and a permissions level indicator.
/// </summary>
public unsafe class Executable
{
    public Executable()
    {
        Main = (delegate* unmanaged<void>)0;
        Permissions = AccessLevel.User;
    }

    #region Methods

    /// <summary>
    /// Loads an executable from an ELF (32-bit) binary.
    /// </summary>
    /// <param name="Binary">The input ELF file as binary.</param>
    /// <returns>An executable with the entry point of the input ELF binary.</returns>
    /// <exception cref="ArgumentException">Thrown when program isn't supported.</exception>
    public static Executable FromELF32(byte[] Binary)
    {
        Executable Temp = new();

        fixed (byte* P = Binary)
        {
            // Load main file header.
            ELFHeader32 Header = new((ELFHeader32*)P);

            // Assign entry point.
            Temp.Main = (delegate* unmanaged<void>)Header.EntryPoint;

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

        return Temp;
    }

    /// <summary>
    /// Loads an executable from an ELF (64-bit) binary.
    /// </summary>
    /// <param name="Binary">The input ELF file as binary.</param>
    /// <returns>An executable with the entry point of the input ELF binary.</returns>
    /// <exception cref="ArgumentException">Thrown when program isn't supported.</exception>
    public static Executable FromELF64(byte[] Binary)
    {
        Executable Temp = new();

        fixed (byte* P = Binary)
        {
            // Load main file header.
            ELFHeader64 Header = new((ELFHeader64*)P);

            // Assign entry point.
            Temp.Main = (delegate* unmanaged<void>)(Header.EntryPoint + 512);

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

        return Temp;
    }

    /// <summary>
    /// Loads an executable from a flat binary.
    /// </summary>
    /// <param name="Binary">The input flat binary.</param>
    /// <returns>An executable with the entry point of the input binary.</returns>
    public static Executable FromBIN(byte[] Binary)
    {
        Executable Temp = new();

        fixed (byte* P = Binary)
        {
            // This assigns the delegate pointer to the entry point of the program.
            // It is a delegate as it can be called by just using Main as a method.
            Temp.Main = (delegate* unmanaged<void>)P;
        }

        return Temp;
    }

    #endregion

    #region Fields

    public delegate* unmanaged<void> Main;
    public AccessLevel Permissions;

    #endregion
}