using System.Globalization;
using System.Text;

namespace PrismBinary
{
	/// <summary>
	/// Single-use class for one method.
	/// </summary>
	public static class BIN2HEX
	{
		/// <summary>
		/// Gets bytes from a hex value.
		/// </summary>
		/// <param name="Hex">Hex value to decode.</param>
		/// <returns>Raw binary from decoded hex value.</returns>
		public static byte[] GetBytes(string Hex)
		{
			byte[] Result = new byte[Hex.Length / 2];
			for (int I = 0; I < Result.Length; I++)
			{
				Result[I] = byte.Parse(Hex[(I * 2)..((I * 2) + 2)], NumberStyles.HexNumber);
			}
			return Result;
		}
		/// <summary>
		/// Convert byte array to list of byte as hex values in a string.
		/// </summary>
		/// <param name="Buffer">Bytes to convert.</param>
		/// <returns>Hex values in a string.</returns>
		public static string GetHex(byte[] Buffer)
		{
			StringBuilder Builder = new(Buffer.Length * 2);
			for (int I = 0; I < Buffer.Length; I++)
			{
				Builder.AppendFormat("{0:x2}", Buffer[I]);
			}
			return Builder.ToString();
		}
	}
}