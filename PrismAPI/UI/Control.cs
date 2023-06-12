using PrismAPI.UI.Config;
using PrismAPI.Graphics;
using Cosmos.System;

namespace PrismAPI.UI;

public abstract class Control : Canvas
{
	#region Constructors

	internal Control(ushort Width, ushort Height) : base(Width, Height)
	{
		OnClick = new((int _, int _, MouseState _) => { });
		Layout = LayoutStyle.None;
		Theme = ThemeStyle.None;
		BackGround = Color.White;
		ForeGround = Color.Black;
		IsEnabled = true;
		Radius = 0;
	}

	#endregion

	#region Methods

	public abstract void Update(Canvas Canvas);

	#endregion

	#region Fields

	// The onclick event - Runs when the element is clicked on screen.
	public Action<int, int, MouseState> OnClick;

	public CursorStatus Status;
	public LayoutStyle Layout;
	public ThemeStyle Theme;
	public Color BackGround;
	public Color ForeGround;
	public bool IsEnabled;
	public ushort Radius;
	public int X;
	public int Y;

	#endregion
}