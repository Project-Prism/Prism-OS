using System.IO;

namespace PrismProject.System2.FileSystem
{
    class VolumeLabel
    {
        public static void Set(string Drive_ID, string Drive_name)
        {
            new DriveInfo(Drive_ID).VolumeLabel = Drive_name;
        }

        public static string Get(string Drive_ID, string Drive_name)
        {
            return new DriveInfo(Drive_ID).VolumeLabel;
        }
    }
}
