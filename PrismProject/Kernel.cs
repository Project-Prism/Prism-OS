using System;using PrismProject.Source.Graphics;

namespace PrismProject
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            try
            { Source.Sequence.Boot.DisplayScreen.Main(); }
            catch (Exception e)
            { Drawables.Screen.Disable(); Console.WriteLine(e.Message); Stop(); }
        }
    }
}