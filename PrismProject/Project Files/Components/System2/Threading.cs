using Cosmos.HAL;

namespace PrismProject.System2
{
        internal class Threading
        {
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

