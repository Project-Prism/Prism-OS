using System.Text;

namespace PrismBinary
{
	/// <summary>
	/// Single-use class for one method.
	/// </summary>
	public static class BIN2HEX
	{
		/// <summary>
		/// Convert byte array to list of byte as hex values in a string.
		/// </summary>
		/// <param name="Buffer">Bytes to convert.</param>
		/// <returns>Hex values in a string.</returns>
		public static string Main(byte[] Buffer)
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