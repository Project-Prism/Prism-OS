namespace PrismProject.System2.FileSystem
{
    class Format
    {
        /// <summary>
        /// Format a mounted disk to fat32
        /// </summary>
        /// <param name="driveID"></param>
        /// <param name="quickformat"></param>
        /// <returns>Weather the disk was succesfuly formatted</returns>
        public static bool FormatDisk(string driveID, bool quickformat)
        {
            try
            {
                Config.fs.Format(driveID, "FAT32", quickformat);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
