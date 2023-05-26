using PrismAPI.Graphics;
using Cosmos.System;
using PrismAPI.UI.Config;

namespace PrismAPI.UI;

public class Window : Control
{
    public Window(int X, int Y, ushort Width, ushort Height) : base(Width, Height)
    {
        // Initialize the control list.
        ShelfControls = new();
        Controls = new();

        // Initialize the window's buffers.
        TitleShelf = new(Width, 32);
        WindowBody = new(Width, Height);

        // Initialize the window fields.
        this.X = X;
        this.Y = Y;
    }

    #region Methods

    public override void OnClick(uint X, uint Y, MouseState State)
    {
        throw new NotImplementedException();
    }

    public override void Update(Canvas Canvas, CursorStatus Status)
    {
        // Resize if needed.
        TitleShelf.Width = Width;
        WindowBody.Height = Height;
        WindowBody.Width = Width;

        // Draw the window back panel.
        TitleShelf.Clear(Color.DeepGray);
        WindowBody.Clear(Color.White);

        // Draw the cache of each control for the title shelf.
        foreach (Control C in ShelfControls)
        {
            C.Update(TitleShelf, CursorStatus.Idle);
        }

        // Draw the cache of each control.
        foreach (Control C in Controls)
		{
			C.Update(WindowBody, CursorStatus.Idle);
		}

        // Draw the window to the buffer.
        Canvas.DrawImage(X, Y - 32, TitleShelf, false);
        Canvas.DrawImage(X, Y, WindowBody, false);
    }

    #endregion

    #region Fields

    public readonly List<Control> ShelfControls;
    public readonly List<Control> Controls;
    private readonly Canvas TitleShelf;
    private readonly Canvas WindowBody;

    #endregion
}