using PrismGL2D;

namespace PrismUI
{
    public class ListBox<T> : Control where T : Control
    {
        public ListBox() : base(0, 0)
        {
            List = new();
        }

        public List<T> List { get; set; }
        public int Scroll { get; set; }

        internal override void OnDraw(Graphics G)
        {
            base.OnDraw(G);

            int UsedY = -Scroll;
            for (int I = 0; I < List.Count; I++)
            {
                UsedY += (int)List[I].Height;
                List[I].Y = UsedY;
                List[I].OnDrawEvent(this);
            }

            if (HasBorder)
            {
                G.DrawRectangle(X - 1, Y - 1, Width + 2, Height + 2, Config.Radius, Config.GetForeground(false, false));
            }

            G.DrawImage(X, Y, this, Config.ShouldContainAlpha(this));
        }
    }
}