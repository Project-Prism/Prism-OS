using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System.IO;
using System.Reflection;

namespace Prism.Essential
{
    public static class Resources
    {
        /*
        public static byte[] GetBytes(string Name)
        {
            using MemoryStream ms = new();
            Assembly.GetExecutingAssembly().GetManifestResourceStream(Name).CopyTo(ms);
            return ms.ToArray();
        }
        */

        [ManifestResourceStream(ResourceName = "PrismOS.Libraries.UI.Resources.Prism.bmp")] static readonly byte[] Byte_Prism;
        [ManifestResourceStream(ResourceName = "PrismOS.Libraries.UI.Resources.Mouse.bmp")] static readonly byte[] Byte_Mouse;
        [ManifestResourceStream(ResourceName = "PrismOS.Libraries.UI.Resources.Warning.bmp")] static readonly byte[] Byte_Warning;

        // Images
        public static Bitmap Prism = new(Byte_Prism);
        public static Bitmap Mouse = new(Byte_Mouse);
        public static Bitmap Warning = new(Byte_Warning);
    }
}
