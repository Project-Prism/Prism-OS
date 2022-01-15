using System;

namespace PrismOS.Tools
{
    /// <summary>
    /// A basic fps monitor from https://stackoverflow.com/a/31849722
    /// </summary>
    public class FPSMon
    {
        public DateTime LT;
        public int FPS;
        public int Frames;

        public void Tick()
        {
            Frames++;
            if ((DateTime.Now - LT).TotalSeconds >= 1)
            {
                // one second has elapsed 

                FPS = Frames;
                Frames = 0;
                LT = DateTime.Now;
            }
        }
    }
}
