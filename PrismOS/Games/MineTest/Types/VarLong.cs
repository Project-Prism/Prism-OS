namespace PrismOS.Games.MineTest.Types;

public class VarLong
{
	#region Constructors

	/// <summary>
	/// Creates a new instance of the <see cref="VarLong"/> class.
	/// </summary>
	/// <param name="Value">The bytes of the integier.</param>
	/// <exception cref="Exception">Thrown when VarInt is too large.</exception>
	public VarLong(byte[] Value)
	{
		int Position = 0;

		foreach (byte B in Value)
		{
			this.Value |= (long)(B & SEGMENT_BITS) << Position;

			if ((B & CONTINUE_BIT) == 0)
			{
				break;
			}

			Position += 7;

			if (Position >= 64)
			{
				throw new Exception("VarLong is too big!");
			}
		}
	}

	/// <summary>
	/// Creates a new instance of the <see cref="VarInt"/> class.
	/// </summary>
	/// <param name="Value">The value to assign.</param>
	public VarLong(long Value)
	{
		this.Value = Value;
	}

	#endregion

	#region Constants

	private const int SEGMENT_BITS = 0x7F;

	private const int CONTINUE_BIT = 0x80;

	#endregion

	#region Operators

	public static VarLong operator +(VarLong Value1, VarLong Value2)
	{
		return new(Value1.Value + Value2.Value);
	}

	public static VarLong operator -(VarLong Value1, VarLong Value2)
	{
		return new(Value1.Value - Value2.Value);
	}

	public static VarLong operator /(VarLong Value1, VarLong Value2)
	{
		return new(Value1.Value / Value2.Value);
	}

	public static VarLong operator *(VarLong Value1, VarLong Value2)
	{
		return new(Value1.Value * Value2.Value);
	}

	public static bool operator ==(VarLong Value1, VarLong Value2)
	{
		return Value1.Value == Value2.Value;
	}

	public static bool operator !=(VarLong Value1, VarLong Value2)
	{
		return Value1.Value != Value2.Value;
	}

	public static implicit operator byte[](VarLong Value)
	{
		BinaryWriter Stream = new(new MemoryStream());

		long Clone = Value.Value;

		while (true)
		{
			if ((Clone & ~SEGMENT_BITS) == 0)
			{
				Stream.Write(Clone);
				((MemoryStream)Stream.BaseStream).ToArray();
			}

			Stream.Write((Clone & SEGMENT_BITS) | CONTINUE_BIT);

			// Note: >>> means that the sign bit is shifted with the rest of the number rather than being left alone
			//Clone >>>= 7;
		}
	}

	public static implicit operator VarLong(byte[] Value)
	{
		return new(Value);
	}

	public static implicit operator long(VarLong Value)
	{
		return Value.Value;
	}

	public static implicit operator VarLong(long Value)
	{
		return new(Value);
	}

	#endregion

	#region Fields

	private readonly long Value;

	#endregion
}