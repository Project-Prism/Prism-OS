using System;
using System.Collections.Generic;
using System.Text;

namespace PrismProject.Tests
{
    /// <summary>
    /// UT (Unified theme) class
    /// </summary>
    class UT
    {
        public static string Name;
        public static string Description;
        public static string Revision;
        public static string Organization;
        public static void SetUT(string UT_Data)
        {
            string[] Info = UT_Data.Split("###")[0].Split("?+?");
            string[] Colors = UT_Data.Split("###")[1].Split("?+?");
            string[] Configs = UT_Data.Split("###")[2].Split("?+?");
            

            #region About
            Name = Info[0];
            Description = Info[1];
            Revision = Info[2];
            Organization = Info[3];
            #endregion About
        }
    }
}
