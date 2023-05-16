using Cosmos.Core;

namespace PrismAPI.Hardware;

//
// Created by Aaron Gill-Braun on 2020-09-30.
// Source: 
//
public static class Serial
{
	#region Constructors

	static Serial()
	{
		IOPort.Write8(Port + 0x01, 0); // disable interrupts
		IOPort.Write8(Port + 0x02, 0b00000001 | 0b11000000);
		IOPort.Write8(Port + 0x03, 0b10000000);
		IOPort.Write16(Port + 0x00, 1); // 115200 baud
		IOPort.Write8(Port + 0x03, 0b00000011 | 0b00000000);
		IOPort.Write8(Port + 0x04, 0b00000100);
	}

	#endregion

	#region Constants

	public const int Port = 0;

	#endregion

	#region Methods

	public static void WriteLine(string Text)
	{
		Write(Text + '\n');
	}

	public static void Write(string Text)
	{
		for (int I = 0; I < Text.Length; I++)
		{
			while ((IOPort.Read8(Port + 5) & 0x20) != 0) ; // wait for empty

			IOPort.Write8(Port, (byte)Text[I]);
		}
	}

	#endregion
}