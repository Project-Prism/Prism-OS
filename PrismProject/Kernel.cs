using System;
using System.Drawing;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            {
                Source.Graphics.Drawables.TestScreen();
            }
            catch (Exception e) { Console.WriteLine("[ERROR] " + e); }
        }
    }
}