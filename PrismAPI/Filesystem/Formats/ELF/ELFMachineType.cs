namespace PrismAPI.Filesystem.Formats.ELF;

public enum ELFMachineType : ushort
{
    X86 = 0x3,
    Mips = 0x8,
    Arm = 0x28,
    Amd64 = 0x3E,
    ArmV8 = 0xB7,
    RiscV = 0xF3,
}