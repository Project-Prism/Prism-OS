namespace PrismOS.Games.MineTest.Types;

public class Fixed
{
    #region Constructors

    /// <summary>
    /// Creates a new instance of the <see cref="Fixed"/> number class.
    /// </summary>
    /// <param name="Value">The value to assign.</param>
    public Fixed(double Value)
    {
        this.Value = Value;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Fixed"/> number class.
    /// </summary>
    /// <param name="Value">The value to assign.</param>
    public Fixed(int Value)
    {
        this.Value = Value;
    }

    #endregion

    #region Operators

    public static Fixed operator +(Fixed Value1, Fixed Value2)
    {
        return new(Value1.Value + Value2.Value);
    }

    public static Fixed operator -(Fixed Value1, Fixed Value2)
    {
        return new(Value1.Value - Value2.Value);
    }

    public static Fixed operator /(Fixed Value1, Fixed Value2)
    {
        return new(Value1.Value / Value2.Value);
    }

    public static Fixed operator *(Fixed Value1, Fixed Value2)
    {
        return new(Value1.Value * Value2.Value);
    }

    public static bool operator ==(Fixed Value1, Fixed Value2)
    {
        return Value1.Value == Value2.Value;
    }

    public static bool operator !=(Fixed Value1, Fixed Value2)
    {
        return Value1.Value != Value2.Value;
    }

    public static implicit operator Fixed(int Value)
    {
        return new(Value / 32.0D);
    }


    public static implicit operator int(Fixed Value)
    {
        return (int)(Value.Value * 32.0D);
    }

    #endregion

    #region Fields

    private readonly double Value;

    #endregion
}