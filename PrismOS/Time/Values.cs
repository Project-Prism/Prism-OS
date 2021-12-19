using System;

namespace PrismOS.Time
{
    public static class Values
    {
        public static int UnixTimestamp { get => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds; }
        public static int UnixTimestampMils { get => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds; }
    }
}
