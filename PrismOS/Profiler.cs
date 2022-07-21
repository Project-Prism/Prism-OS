using System;
using System.Collections.Generic;

namespace PrismOS
{
    public static class Profiler
    {
        public static DateTime StartTime { get; set; }
        public static string CurrentMethod { get; set; } = "";
        public static List<string> Methods { get; set; } = new();
        public static List<int> MethodCallCounts { get; set; } = new();
        public static List<TimeSpan> MethodTimeSpans { get; set; } = new();

        public static void Start(string MethodName)
        {
            StartTime = DateTime.Now;
            CurrentMethod = MethodName;
            if (!Methods.Contains(MethodName))
            {
                Methods.Add(MethodName);
                MethodCallCounts.Add(1);
                MethodTimeSpans.Add(TimeSpan.Zero);
                return;
            }
            else
            {
                MethodCallCounts[Methods.IndexOf(MethodName)]++;
            }
        }
        public static void Stop()
        {
            MethodTimeSpans[Methods.IndexOf(CurrentMethod)] += DateTime.Now.Subtract(StartTime);
        }
        public static void Output()
        {
            for (int I = 9; I < Methods.Count; I++)
            {
                Cosmos.Core.Global.mDebugger.Send($"Method: {Methods[I]}, Times Called: {MethodCallCounts[I]}, Total time Spent: {MethodTimeSpans[I]}");
            }
        }
    }
}