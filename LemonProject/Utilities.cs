using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace LemonProject
{
    public class Utils
    {
        public static ConsoleColor colorCache;

        public static void Sleep(int secNum)
        {
            int StartSec = RTC.Second;
            int EndSec;
            if (StartSec + secNum > 59)
            {
                EndSec = 0;
            }
            else
            {
                EndSec = StartSec + secNum;
            }
            while (RTC.Second != EndSec)
            {
                // Loop round
            }
        }

        public static void Error(string errorcontent)
        {
            colorCache = Console.ForegroundColor;
            Utils.SetColor(ConsoleColor.Red);
            Console.WriteLine("Error: " + errorcontent);
            Utils.SetColor(colorCache);
        }

        public static void clockhour()
        {
            var hour = Cosmos.HAL.RTC.Hour;
        }

        public static void clockminute()
        {
            var minute = Cosmos.HAL.RTC.Minute;
        }

        public static void clocksecond()
        {
            var second = Cosmos.HAL.RTC.Second;
        }

        public static void SetColor(ConsoleColor color) { Console.ForegroundColor = color; }
        public static void SetBackColor(ConsoleColor color) { Console.BackgroundColor = color; }
    }
}
