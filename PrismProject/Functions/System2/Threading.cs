using Cosmos.HAL;

namespace PrismProject.Functions.System2
{
    class Threading
    {
        public class Thread
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
}
