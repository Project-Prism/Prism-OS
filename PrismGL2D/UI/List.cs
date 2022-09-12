namespace PrismGL2D.UI
{
    public class ListBox<T> : Control where T : Control
    {
        public ListBox()
        {
            List = new();
        }

        public List<T> List { get; set; }
        public int Scroll { get; set; }

        public override void OnDrawEvent(Graphics Buffer)
        {
            base.OnDrawEvent(this);

            Clear(Theme.Background);

            int UsedY = -Scroll;
            for (int I = 0; I < List.Count; I++)
            {
                UsedY += (int)List[I].Height;
                List[I].Y = UsedY;
                List[I].OnDrawEvent(this);
            }

            if (HasBorder)
            {
                DrawRectangle(0, 0, (int)Width - 1, (int)Height - 1, (int)Theme.Radius, Theme.Accent);
            }

            Buffer.DrawImage(X, Y, this, false);
        }
    }
}