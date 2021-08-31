using System.Text;

namespace PrismProject.System2.Extended
{
    internal class Convert
    {
        public static byte[] ToByteArray(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }
    }
}
