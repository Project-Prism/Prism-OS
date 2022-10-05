using System.Text;

namespace PrismBinary
{
	public static class BIN2HEX
	{
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