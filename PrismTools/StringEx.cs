namespace PrismTools;

public static unsafe class StringEx
{
	/// <summary>
	/// Get the number of instances that the char 'C' is found in the string.
	/// </summary>
	/// <param name="S">The string to search</param>
	/// <param name="C">The char to find.</param>
	/// <returns>int containing how many times C was found in S.</returns>
	public static int NumberOf(string S, char C)
	{
		int N = 0;
		for (int I = 0; I < S.Length; I++)
		{
			if (S[I] == C)
			{
				N++;
			}
		}
		return N;
	}

	/// <summary>
	/// Convert an index in a string into the Line/Column values.
	/// </summary>
	/// <param name="S">String to check.</param>
	/// <param name="Index">Index to convert.</param>
	/// <returns>Tuple with the order of Line/Column.</returns>
	public static (int Line, int Column) GetLineColumn(string S, int Index)
	{
		return (NumberOf(S[Index..], '\n') + 1, Index - S.LastIndexOf('\n', Index));
	}
}