namespace PrismOS.Games.MineTest.Types;

public class VarInt
{
	#region Constructors

	/// <summary>
	/// Creates a new instance of the <see cref="VarInt"/> class.
	/// </summary>
	/// <param name="Value">The bytes of the integier.</param>
	/// <exception cref="Exception">Thrown when VarInt is too large.</exception>
	public VarInt(byte[] Value)
	{
		int Position = 0;

		foreach (byte B in Value)
		{
			this.Value |= (B & SEGMENT_BITS) << Position;

			if ((B & CONTINUE_BIT) == 0)
			{
				break;
			}

			Position += 7;

			if (Position >= 32)
			{
				throw new Exception("VarInt size is too large!");
			}
		}
	}

	/// <summary>
	/// Creates a new instance of the <see cref="VarInt"/> class.
	/// </summary>
	/// <param name="Value">The value to assign.</param>
	public VarInt(int Value)
	{
		this.Value = Value;
	}

	#endregion

	#region Constants

	private const int SEGMENT_BITS = 0x7F;

	private const int CONTINUE_BIT = 0x80;

	#endregion

	#region Operators

	public static VarInt operator +(VarInt Value1, VarInt Value2)
	{
		return new(Value1.Value + Value2.Value);
	}

	public static VarInt operator -(VarInt Value1, VarInt Value2)
	{
		return new(Value1.Value - Value2.Value);
	}

	public static VarInt operator /(VarInt Value1, VarInt Value2)
	{
		return new(Value1.Value / Value2.Value);
	}

	public static VarInt operator *(VarInt Value1, VarInt Value2)
	{
		return new(Value1.Value * Value2.Value);
	}

	public static bool operator ==(VarInt Value1, VarInt Value2)
	{
		return Value1.Value == Value2.Value;
	}

	public static bool operator !=(VarInt Value1, VarInt Value2)
	{
		return Value1.Value != Value2.Value;
	}

	public static implicit operator byte[](VarInt Value)
	{
		BinaryWriter Stream = new(new MemoryStream());

		int Clone = Value.Value;

		while (true)
		{
			if ((Clone & ~SEGMENT_BITS) == 0)
			{
				Stream.Write(Clone);
				return ((MemoryStream)Stream.BaseStream).ToArray();
			}

			Stream.Write((Clone & SEGMENT_BITS) | CONTINUE_BIT);

			// Note: >>> means that the sign bit is shifted with the rest of the number rather than being left alone
			//Clone >>>= 7;
		}
	}

	public static implicit operator VarInt(byte[] Value)
	{
		return new(Value);
	}

	public static implicit operator int(VarInt Value)
	{
		return Value.Value;
	}

	public static implicit operator VarInt(int Value)
	{
		return new(Value);
	}

	#endregion

	#region Fields

	private readonly int Value;

	#endregion
}