using System.Collections.Generic;
using System.Drawing;

namespace PrismOS.UI.Components
{
    public class Window : Base
    {
        public Window(int X, int Y, int Width, int Height, Color ForeGround, Color BackGround)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            this.ForeGround = ForeGround;
            this.BackGround = BackGround;
        }

        public override int X
        {
            get;
            set;
        }
        public override int Y
        {
            get;
            set;
        }
        public override int Width
        {
            get;
            set;
        }
        public override int Height
        {
            get;
            set;
        }
        public override Color ForeGround
        {
            get;
            set;
        }
        public override Color BackGround
        {
            get;
            set;
        }
        public override Base Parent
        {
            get;
            set;
        }
        public override List<Base> Children
        {
            get;
            set;
        }

        public override void Draw()
        {
        }
    }
}