namespace System
{
    abstract class Environment
    {
        public static bool Is64BitOperatingSystem { get; } = false;
        public static string MachineName { get; set; } = "Prism OS device";
        public static string UserName { get; set; } = "";
        public static string CurrentDirectory { get; set; } = @"0:\";
        public static OperatingSystem OSVersion { get; } = new OperatingSystem(new PlatformID(), new Version("1.0"));
        public static string SystemDirectory { get; set; } = @"0:\Prism-Core\";
    }
}
