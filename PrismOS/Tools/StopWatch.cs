namespace PrismOS.Tools
{
    public class StopWatch
    {
        private int StartTime { get; set; } = 0;
        private int StopTime { get; set; } = 0;
        public int ElapsedMiliseconds { get; set; } = 0;

        public void Start()
        {
            StartTime = Time.UnixTimestampMils;
        }

        public void Stop()
        {
            StopTime = Time.UnixTimestampMils;
            ElapsedMiliseconds += Math.Difference(StartTime, StopTime);
        }
    }
}
