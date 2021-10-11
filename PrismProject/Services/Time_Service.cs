using Cosmos.HAL;

namespace PrismProject.Functions.Services
{
    class Time_Service
    {
        public static string Year { get; } = RTC.Year.ToString();
        public static string Month { get; } = RTC.Month.ToString();
        public static string Day { get; } = RTC.DayOfTheMonth.ToString();
        public static string DayOfWeek { get; } = RTC.DayOfTheWeek.ToString();
        public static string Hour { get; } = RTC.Hour.ToString();
        public static string Minute { get; } = RTC.Minute.ToString();
        public static string Seccond { get; } = RTC.Second.ToString();
    }
}
