using Cosmos.System.Graphics;
using System.Drawing;
using PrismProject.System2.Drivers;

namespace PrismProject.System2.Drawing.Icons
{
    class Icons
    {

        private static void DrawCross(Color color, int X, int Y, int X2, int Y2)
        {
            Video.Screen.DrawLine(new Pen(color), X, Y, X2, Y2);
            Video.Screen.DrawLine(new Pen(color), X2, Y, X, Y2);
        }
        private static void DrawArrow(Color color, int X, int Y, int X2, int Y2)
        {
            Video.Screen.DrawLine(new Pen(color), X, Y, X2, Y2);
            Video.Screen.DrawLine(new Pen(color), X, Y, X, Y2/2);
            Video.Screen.DrawLine(new Pen(color), X, Y, X2/2, Y);
        }
        public static void DrawIcon(int X, int Y, int W, int H, Color color, string name)
        {
            switch (name)
            {
                case "cross":
                    DrawCross(color, X, Y, H, W);
                    break;
                case "arrow":
                    DrawArrow(color, X, Y, H, W);
                    break;
            }
        }
    }
}
