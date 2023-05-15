using PrismAPI.UI.Config;
using PrismAPI.Graphics;
using Cosmos.System;

namespace PrismAPI.UI;

public abstract class Control : Canvas
{
    #region Constructors

    public Control(ushort Width, ushort Height) : base(Width, Height)
    {
        Layout = LayoutStyle.None;
        Theme = ThemeStyle.None;
        BackGround = Color.White;
        ForeGround = Color.Black;
        Radius = 0;
    }

    #endregion

    #region Methods

    public abstract void OnClick(uint X, uint Y, MouseState State);

    public abstract void Update(Canvas Canvas, CursorStatus Status);

    #endregion

    #region Fields

    public LayoutStyle Layout;
    public ThemeStyle Theme;
    public Color BackGround;
    public Color ForeGround;
    public ushort Radius;
    public int X;
    public int Y;

    #endregion
}