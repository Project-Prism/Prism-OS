using Cosmos.HAL;

namespace PrismProject.Functions.System2
{
    class Time
    {
        public static unsafe void* Year = (void*)RTC.Year;
        public static unsafe void* Month = (void*)RTC.Month;
        public static unsafe void* Day = (void*)RTC.DayOfTheMonth;
        public static unsafe void* DayOfWeek = (void*)RTC.DayOfTheWeek;
        public static unsafe void* Hour = (void*)RTC.Hour;
        public static unsafe void* Minute = (void*)RTC.Minute;
        public static unsafe void* Seccond = (void*)RTC.Second;
    }
}
