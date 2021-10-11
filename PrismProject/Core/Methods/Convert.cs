using System.Text;

namespace PrismProject.Core.Methods
{
    abstract class Convert
    {
        public static byte[] ToByteArray(string Input) => Encoding.UTF8.GetBytes(Input);

        public static string[] ToStringArray(byte[] Input)
        {
            string[] NewStringArray = new string[] { };
            int place = 0;
            foreach(byte Set in Input)
            {
                NewStringArray[place++] = Set.ToString();
            }
            return NewStringArray;
        }

        public static int KMtoMile(int KM)
        {
            return KM / (int)1.609344;
        }
    }
}
