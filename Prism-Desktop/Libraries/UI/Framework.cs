using G2 = Cosmos.System.Graphics;
using static Prism.Libraries.UI.Managment;
using static Prism.Libraries.UI.Colorizer;
using static Cosmos.System.Graphics.Fonts.PCScreenFont;

namespace Prism.Libraries.UI
{
    public static class Framework
    {

        public static void Draw_Radio_Button(int X, int Y, int Size, G2.Bitmap Icon)
        {
            Canvas.DrawFilledCircle(new G2.Pen(Button.Background), X, Y, Size);
            Canvas.DrawCircle(new G2.Pen(Button.Foreground), X, Y, Size + 4);
            Canvas.DrawImageAlpha(Icon, X - ((int)Icon.Width / 2), Y - ((int)Icon.Height / 2));
        }

        public static void Draw_Text_Button(int X, int Y, int Width, int Height, string Label)
        {
            Canvas.DrawFilledRectangle(new G2.Pen(Button.Background), X - (Width / 2), Y - (Height / 2), Width + (Width / 2), Height + (Height / 2));
            Canvas.DrawRectangle(new G2.Pen(Button.Background), X - (Width / 2), Y - (Height / 2), Width + (Width / 2) - 1, Height + (Height / 2) + 2);

            Canvas.DrawFilledCircle(new G2.Pen(Button.Background), X - (Width / 2), Y - (Height / 2), Height);
            Canvas.DrawFilledCircle(new G2.Pen(Button.Background), X + (Width / 2), Y + (Height / 2), Height);

            Canvas.DrawFilledCircle(new G2.Pen(Button.Background), X - (Width / 2), Y - (Height / 2), Height + 1);
            Canvas.DrawFilledCircle(new G2.Pen(Button.Background), X + (Width / 2), Y + (Height / 2), Height + 1);

            Canvas.DrawString(Label, Default, new G2.Pen(Colorizer.Button.Text), X - (Default.Width / 2), Y - (Default.Height / 2));
        }

        public static class Label
        {

        }
    }
}
