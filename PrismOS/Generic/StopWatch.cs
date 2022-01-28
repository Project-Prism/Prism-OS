using System;

namespace PrismOS.Generic
{
    public class StopWatch
    {
        private int StartTime { get; set; } = 0;
        private int StopTime { get; set; } = 0;
        public TimeSpan Elapsed { get; internal set; } = new();

        public void Start()
        {
            StartTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        public void Stop()
        {
            StopTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            Elapsed = new(0, 0, 0, 0, Math.Difference(StartTime, StopTime));
        }
    }
}
