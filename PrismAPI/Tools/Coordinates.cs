namespace PrismAPI.Tools;

public static class Coordinates
{
	public static float Scale(float Value, float NewSize, float OldSize)
	{
		return NewSize * (Value / OldSize);
	}
}