using System.Runtime.InteropServices;

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
	}
}