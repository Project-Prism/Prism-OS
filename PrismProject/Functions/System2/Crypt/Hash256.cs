using System.Security.Cryptography;
using static System.Text.Encoding;

namespace PrismProject.Functions.System2.Crypt
{
    class Hash256
    {
        public static void GetHash(string Input)
        {
            /* Not plugged yet
            using (SHA256 ag = SHA256.Create())
            {
                return ASCII.GetString(ag.ComputeHash(ASCII.GetBytes(Input)));
            }
            */
        }
    }
}
