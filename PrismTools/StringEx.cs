namespace PrismTools
{
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
		/// Converts an ascii string pointer to a normal string.
		/// </summary>
		/// <param name="C">String pointer.</param>
		/// <returns>Converted string.</returns>
		public static string GetString(char* C)
		{
			string S = "";
			for (int I = 0; C[I] != 0; I++)
			{
				S += C[I];
			}
			return S[..^1];
		}
	}
}