using System;
using System.Collections.Generic;
using System.Text;

namespace PrismProject.System2
{
    internal class Properties
    {
        public static string Version()
        {
            return "21.7.8"; //always set to the current date when commiting/releasing (YY.MM.DD)
        }
        public static string Default_Lang()
        {
            return "EN_US";
        }
        public static string Default_Name()
        {
            return "User-PC";
        }
        public static string SystemPath()
        {
            return "System2\\";
        }
        public static string ComponentsPath()
        {
            return "System2\\Components\\";
        }
        public static string SystemSettings()
        {
            return "System2\\Options\\Global\\";
        }
        public static string TempPath()
        {
            return "System2\\Temp\\";
        }
        public static string UsersFolder()
        {
            return "Users\\";
        }
        public static bool IsFreshBoot()
        {
            return VFS.CE("0:" + SystemPath);
        }
    }
}
