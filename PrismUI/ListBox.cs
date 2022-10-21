using Cosmos.System;

namespace PrismUI
{
    public class ListBox : Control
    {
        public ListBox(string[] Items) : base(128, (uint)Items.Length * 32)
        {
            _List = Items;
            List = Items;
            Index = -1;
        }

        private string[] _List;
        public string[] List
        {
            get
			{
                return _List;
			}
			set
			{
                Controls.Clear();
                _List = value;
                Height = 32 * (uint)value.Length;
                for (int I = 0; I < value.Length; I++)
                {
                    Controls.Add(new Button(128, 32)
                    {
                        Y = I * 32,
                        Text = value[I],
                        OnClickEvent = (int X, int Y, MouseState State) => { Index = I; },
                    });
                }
            }
        }
        public int Index { get; set; }
    }
}