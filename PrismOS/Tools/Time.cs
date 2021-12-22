using System;

namespace PrismOS.Tools
{
    public static class Time
    {
        public static int UnixTimestamp { get => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds; }
        public static int UnixTimestampMils { get => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds; }
    }
}
