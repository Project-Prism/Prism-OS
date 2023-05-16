namespace PrismAPI.Hardware.GPU.Intel.Skylake;

public enum MMIOUnit
{
	MediaEngine    = 0x00002000,
	MemoryArbiter  = 0x00004000,
	MFXControl     = 0x00012000,
	MediaUnits     = 0x00012400,
	MFXArbiter     = 0x00014000,
	BlitterEngine  = 0x00022000,
	BlitterArbiter = 0x00024000,
	FenceRegisters = 0x00100000,
	MCHBAR         = 00140000,
}