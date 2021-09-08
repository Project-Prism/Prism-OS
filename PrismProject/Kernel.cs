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
                Source.Graphics.Drawables.Clear(Color.FromArgb(24,24,24));
                Source.Graphics.Drawables.Tst();
            }
            catch (Exception e) { Console.WriteLine("[ERROR] " + e); }
        }
    }
}