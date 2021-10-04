namespace PrismProject.Functions.System2
{
    class Locales
    {
        public static string Locale = "EN_US";
        public static string GetString(string string_name)
        {
            switch (Locale)
            {
                case "EN_US":
                    switch (string_name)
                    {
                        case "boot_net_status":
                            return "Staring net...";
                        case "boot_fs_status":
                            return "Starting FS...";
                        case "boot_text":
                            return "Prism OS (21.9.28)";
                    }
                    break;
            }
            Logging.AppendLog(Logging.Type.Error, "Incorect language was specified, Changing back to default...");
            Locale = "EN_US";
            return "LANG_ERR.";
        }
    }
}
