using Cosmos.System.Graphics;

namespace PrismOS.UI
{
    public static class SaltUI
    {
        public static Canvas Canvas { get; set; } = FullScreenCanvas.GetFullScreenCanvas();
        internal delegate void Event();

        public interface IForm
        {
            void Draw();
            void ClickDown();
            void ClickUp();
            void Hover();
            void Destroy();
        }

        public class Window : IForm
        {
            internal Event OnClickDown;
            internal Event OnClickUp;
            internal Event OnHover;
            internal Event OnDraw;
            internal Event OnRemove;
            internal Event OnAdd;

            public int X, Y, Width, Height;

            public Window(int aX, int aY, int aWidth, int aHeight)
            {
                X = aX;
                Y = aY;
                Width = aWidth;
                Height = aHeight;
            }

            public void Draw()
            {
                OnDraw();
                Canvas.DrawFilledRectangle(new Pen(System.Drawing.Color.White), X, Y, Width, Height);
            }
            public void ClickDown()
            {
                OnClickDown();
            }
            public void ClickUp()
            {
                OnClickUp();
            }
            public void Hover()
            {
                OnHover();
            }
            public void Destroy()
            {
                OnRemove();
            }
        }
    }
}
