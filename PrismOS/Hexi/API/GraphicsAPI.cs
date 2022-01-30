using PrismOS.Hexi.Misc;
using System.Drawing;

namespace PrismOS.Hexi.API
{
    public static class GraphicsAPI
    {
        public static void Start(Executable exe, byte[] args)
        {
            Graphics.Canvas Canvas = new(args[0], args[1]);
        }

        public static void DrawSqr(Executable exe, byte[] args)
        {
            if (args[0] == 1)
            {
                Graphics.Canvas.GetCanvas.DrawFilledRectangle(args[1], args[2], args[3], args[4], Color.FromArgb(args[5]));
            }
            else
            {
                Graphics.Canvas.GetCanvas.DrawRectangle(args[1], args[2], args[3], args[4], Color.FromArgb(args[5]));
            }
        }

        public static void Update(Executable exe, byte[] args)
        {
            Graphics.Canvas.GetCanvas.Update();
        }
    }
}
