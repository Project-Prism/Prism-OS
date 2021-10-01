using Cosmos.HAL;
using System.Drawing;
using static PrismProject.Functions.Graphics.Canvas2;
using static PrismProject.Functions.IO.SystemFiles;

namespace PrismProject.Functions.System2
{
    class Utils
    {

    }

    class Time
    {
        public static unsafe byte* Year = (byte*)RTC.Year;
        public static unsafe byte* Month = (byte*)RTC.Month;
        public static unsafe byte* Day = (byte*)RTC.DayOfTheMonth;
        public static unsafe byte* DayOfWeek = (byte*)RTC.DayOfTheWeek;
        public static unsafe byte* Hour = (byte*)RTC.Hour;
        public static unsafe byte* Minute = (byte*)RTC.Minute;
        public static unsafe byte* Seccond = (byte*)RTC.Second;
    }
}
