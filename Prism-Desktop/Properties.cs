using System;

namespace Prism
{
    abstract class Environment
    {
        public static bool Is64BitOperatingSystem { get; } = false;
        public static string MachineName { get; set; } = "Prism OS device";
        public static string UserName { get; set; } = "LiveUser";
        public static string CurrentDirectory { get; set; } = @"0:\";
        public static OperatingSystem OSVersion { get; } = new OperatingSystem(new PlatformID(), new Version("Test Build"));
        public static string SystemDirectory { get; set; } = @"0:\Prism-Core\";
        public static bool IsShuttingDown { get; set; } = false;
    }
}
