using Cosmos.HAL;

namespace PrismProject.System2.Extentions
{
    internal class Threading
    {
        /// <summary>
        /// sleep on thread for a specified amout of time. 
        /// Depreciated. used real threading as it was patched.
        /// </summary>
        /// <param name="sec"></param>
        public static void Sleep(int sec)
        {
            int Starton = RTC.Second;
            int EndSec;

            if (Starton + sec > 59)
                EndSec = 0;
            else
                EndSec = Starton + sec;

            // Loop round
            while (RTC.Second != EndSec) ;
        }
    }
}