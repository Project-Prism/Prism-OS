namespace PrismOS.Time
{
    public class StopWatch
    {
        private int StartTime { get; set; } = 0;
        private int StopTime { get; set; } = 0;
        public int ElapsedMiliseconds { get; set; } = 0;

        public void Start()
        {
            StartTime = Values.UnixTimestampMils;
        }

        public void Stop()
        {
            StopTime = Values.UnixTimestampMils;
            ElapsedMiliseconds += Math.Calculate.Difference(StartTime, StopTime);
        }
    }
}
