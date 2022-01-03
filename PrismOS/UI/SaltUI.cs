using Cosmos.System.Graphics;
using System.Collections.Generic;

namespace PrismOS.UI
{
    public static class SaltUI
    {
        public static Canvas Canvas { get; set; } = FullScreenCanvas.GetFullScreenCanvas();
        public static List<Window> Windows { get; } = new();
        internal delegate void Event();

        public static void Tick()
        {
            foreach(Window Win in Windows)
            {
                Win.Draw();
            }
        }

        public interface IForm
        {
            void Draw();
            void ClickDown();
            void ClickUp();
            void Hover();
            void Destroy();
            byte[] ToBytes();
            static IForm FromBytes(byte[] Bytes)
            {
                throw new System.Exception("Function not implemented for base element. (" + Bytes + ")", new System.NotImplementedException());
            }
        }

        public class Window : IForm
        {
            internal Event OnClickDown;
            internal Event OnClickUp;
            internal Event OnHover;
            internal Event OnDraw;
            internal Event OnRemove;
            internal Event OnAdd;

            public List<IForm> Children;
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
            public byte[] ToBytes()
            {
                List<byte> Bytes = new();
                Bytes.Add((byte)Width);
                Bytes.Add((byte)Height);
                return Bytes.ToArray();
            }
            public static IForm FromBytes(byte[] Bytes)
            {
                return new Window(0, 0, Bytes[0], Bytes[1]);
            }
        }

        public class Text : IForm
        {
            internal Event OnClickDown;
            internal Event OnClickUp;
            internal Event OnHover;
            internal Event OnDraw;
            internal Event OnRemove;
            internal Event OnAdd;

            public int X, Y;
            public string Label;

            public Text(int aX, int aY, string aLabel)
            {
                X = aX;
                Y = aY;
                Label = aLabel;
            }

            public void Draw()
            {
                OnDraw();
                // Need to add
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
            public byte[] ToBytes()
            {
                List<byte> Bytes = new();
                Bytes.Add((byte)Label.Length);
                foreach(char Char in Label)
                {
                    Bytes.Add((byte)Char);
                }
                return Bytes.ToArray();
            }
            public static IForm FromBytes(byte[] Bytes)
            {
                return new Window(0, 0, Bytes[0], Bytes[1]);
            }
        }
    }
}
