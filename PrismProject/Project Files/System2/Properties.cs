namespace PrismProject.System2.Extentions
{
    /// <summary>
    /// System properties
    /// </summary>
    internal class Properties
    {
        /// <summary>
        /// Get system version ID
        /// </summary>
        /// <returns>Version (String)</returns>
        public static string Version()
        {
            return "21.7.8"; //always set to the current date when commiting/releasing (YY.MM.DD)
        }

        /// <summary>
        /// Get default lang
        /// </summary>
        /// <returns>EN_US (String)</returns>
        public static string Default_Lang()
        {
            return "EN_US";
        }

        /// <summary>
        /// Default PC name
        /// </summary>
        /// <returns>Default name (String)</returns>
        public static string Default_Name()
        {
            return "User-PC";
        }

        /// <summary>
        /// Get system path
        /// </summary>
        /// <returns>System files path (String)</returns>
        public static string SystemPath()
        {
            return "System2\\";
        }

        /// <summary>
        /// Get System2 components path
        /// </summary>
        /// <returns>Components path (String)</returns>
        public static string ComponentsPath()
        {
            return "System2\\Components\\";
        }

        /// <summary>
        /// Get system settings path
        /// </summary>
        /// <returns>System configs path (String)</returns>
        public static string SystemSettingsPath()
        {
            return "System2\\Options\\Global\\";
        }

        /// <summary>
        /// Temporary system temporary path
        /// </summary>
        /// <returns>Temporary path (String)</returns>
        public static string TempPath()
        {
            return "System2\\Temp\\";
        }

        /// <summary>
        /// Default user folder
        /// </summary>
        /// <returns>default user path (String)</returns>
        public static string UsersFolderPath()
        {
            return "Users\\";
        }

        /// <summary>
        /// Check if OS should be in OOBE mode
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsFreshBoot()
        {
            return System2.FileSystem.Explore.Exists("0:" + SystemPath());
        }
    }
}
