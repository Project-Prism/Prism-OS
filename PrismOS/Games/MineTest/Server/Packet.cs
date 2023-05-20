//using PrismOS.Games.MineTest.Types;
using System.Numerics;

namespace PrismOS.Games.MineTest.Server;

/// <summary>
/// Reference: https://wiki.vg/Protocol
/// </summary>
public class Packet
{
	#region Methods

	public static long WritePosition(Vector3 Position)
	{
		return (((long)Position.X & 0x3FFFFFF) << 38) | (((long)Position.Z & 0x3FFFFFF) << 12) | ((long)Position.Y & 0xFFF);
	}

	public static Vector3 ReadPosition(long Position)
	{
		return new()
		{
			X = Position >> 38,
			Y = Position << 52 >> 52,
			Z = Position << 26 >> 38,
		};
	}

	#endregion

	#region Fields

	//public VarInt Length;
	//public VarInt PacketID;
	//public byte[] Contents;

	#endregion
}