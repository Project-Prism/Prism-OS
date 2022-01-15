using System.Collections.Generic;

namespace PrismOS.UI.Components
{
    public class Label : IBase
    {
        //internal delegate void Event();
        //internal Event OnClickDown;
        //internal Event OnClickUp;
        //internal Event OnHover;
        //internal Event OnDraw;
        //internal Event OnRemove;
        //internal Event OnAdd;

        public int X, Y;
        public string Text;

        public Label(int X, int Y, string Text)
        {
            this.X = X;
            this.Y = Y;
            this.Text = Text;
        }

        public void Draw()
        {
            // Need to add
        }
        public void ClickDown()
        {
        }
        public void ClickUp()
        {
        }
        public void Hover()
        {
        }
        public void Destroy()
        {
        }
        public byte[] ToBytes()
        {
            List<byte> Bytes = new();
            Bytes.Add((byte)Text.Length);
            foreach (char Char in Text)
            {
                Bytes.Add((byte)Char);
            }
            return Bytes.ToArray();
        }
        public static IBase FromBytes(byte[] Bytes)
        {
            return new Window(0, 0, Bytes[0], Bytes[1]);
        }
    }
}