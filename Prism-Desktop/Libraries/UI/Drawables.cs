using Cos = Cosmos.System.Graphics;
using static Prism.Libraries.UI.Primitives;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;

namespace Prism.Libraries.UI
{
    public static class Drawables
    {
        public static void DrawLabel(int X, int Y, string Label)
        {
            Canvas.DrawString(Label, Default, new Cos.Pen(Theme.Label.Text), X - (Default.Width / 2), Y - (Default.Height / 2));
        }
    }
}
