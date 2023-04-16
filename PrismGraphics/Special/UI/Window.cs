using PrismGraphics.Special.UI.Controls;

namespace PrismGraphics.Special.UI
{
    public class Window
    {
        public Window(int X, int Y, ushort Width, ushort Height)
        {
            // Initialize the control list.
            ShelfControls = new();
            Controls = new();

            // Initialize the window's buffers.
            MainImage = new(Width, Height);
            TitleShelf = new(Width, 32);
            WindowBody = new(Width, Height);

            // Initialize the window fields.
            this.Height = Height;
            this.Width = Width;
            this.X = X;
            this.Y = Y;
        }

        #region Properties

        public ushort Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
                Render();
            }
        }
        public ushort Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
                Render();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Removes a control from the window.
        /// </summary>
        /// <param name="Control">The control to remove.</param>
        public void RemoveControl(Control Control)
        {
            Control.Window = null;
            Controls.Remove(Control);
            Render();
        }

        /// <summary>
        /// Adds a control to the window.
        /// </summary>
        /// <param name="Control">The control to add.</param>
        public void AddControl(Control Control)
        {
            Control.Window = this;
            Controls.Add(Control);
            Render();
        }

        /// <summary>
        /// Renders the window - Only use after changing something.
        /// </summary>
        public void Render()
        {
            // Resize if needed.
            MainImage.Height = Height;
            MainImage.Width = Width;
            TitleShelf.Width = Width;
            WindowBody.Height = Height;
            WindowBody.Width = Width;

            // Draw the window back panel.
            TitleShelf.Clear(Color.DeepGray);
            WindowBody.Clear(Color.White);

            // Draw the cache of each control for the title shelf.
            foreach (Control C in ShelfControls)
            {
                TitleShelf.DrawImage(C.X, C.Y, C.MainImage);
            }

            // Draw the cache of each control.
            foreach (Control C in Controls)
            {
                WindowBody.DrawImage(C.X, C.Y, C.MainImage);
            }

            // Draw the window to the buffer.
            MainImage.DrawImage(X, Y - 32, TitleShelf);
            MainImage.DrawImage(X, Y, WindowBody);
        }

        #endregion

        #region Fields

        private readonly List<Control> ShelfControls;
        private readonly List<Control> Controls;
        private readonly Graphics TitleShelf;
        private readonly Graphics WindowBody;
        public readonly Graphics MainImage;
        private ushort _Height;
        private ushort _Width;
        public int X;
        public int Y;

        #endregion
    }
}