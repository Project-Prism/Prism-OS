using PrismOS.Hexi.Misc;
using System.Drawing;

namespace PrismOS.Hexi.API
{
    public static class WindowAPI
    {
        public static void DefineWindow(Executable exe, byte[] args)
        {
            exe.AppWindow = new(args[0], args[1], args[2], args[3], Color.FromArgb(args[4]), Color.FromArgb(args[5]), Color.FromArgb(args[6]), UI.Canvas.GetCanvas);
        }
    }
}
