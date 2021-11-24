using System.IO;
using System.Reflection;

namespace Prism.Essential
{
    public static class Resources
    {
        public static byte[] GetBytes()
        {
            using MemoryStream ms = new();
            Assembly.GetExecutingAssembly().GetManifestResourceStream("").CopyTo(ms);
            return ms.ToArray();
        }

        //[ManifestResourceStream(ResourceName = Name)] readonly byte[] Byte_Prism;
        //[ManifestResourceStream(ResourceName = "Prism.Files.Icons.Mouse.bmp")] static readonly byte[] Byte_Mouse;
        //[ManifestResourceStream(ResourceName = "Prism.Files.Icons.Warning.bmp")] static readonly byte[] Byte_Warning;

        // Images
        //public static Bitmap Prism = new(Byte_Prism);
        //public static Bitmap Mouse = new(Byte_Mouse);
        //public static Bitmap Warning = new(Byte_Warning);
    }
}
