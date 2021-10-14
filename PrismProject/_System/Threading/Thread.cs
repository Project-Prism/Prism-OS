using Cosmos.HAL;

namespace PrismProject._System.Threading
{
    class Thread
    {
        public static void Sleep(int S)
        {
            int Starton = RTC.Second;
            int EndSec;

            if (Starton + S > 59)
                EndSec = 0;
            else
                EndSec = Starton + S;

            // Loop round
            while (RTC.Second != EndSec) ;
        }
    }
}
