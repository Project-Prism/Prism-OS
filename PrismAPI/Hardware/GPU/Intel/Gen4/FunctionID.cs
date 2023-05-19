namespace PrismAPI.Hardware.GPU.Intel.Gen4;

public enum FunctionID
{
	NULL = 0x0,
	ExtendedMath = 0x1,
	Sampler = 0x2,
	Gateway = 0x3,
	ReadDataPort = 0x4,
	WriteDataPort = 0x5,
	URB = 0x6,
	ThreadSpawner = 0x7,
	VideoFrontEnd = 0x8,
	VertexShader = 0x9,
	CommandStream = 0xA,
	VertexFetch = 0xB,
	GeometryShader = 0xC,
	ClipperUnit = 0xD,
	FanUnit = 0xE,
	MaskerUnit = 0xF,
}