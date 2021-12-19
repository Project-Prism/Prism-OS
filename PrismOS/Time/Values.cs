using System;

namespace PrismOS.Time
{
    public static class Values
    {
        public static int UnixTimestampMils { get => int.Parse(DateTime.UtcNow.ToString("fffffff")); }
    }
}
