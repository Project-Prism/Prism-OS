using PrismAPI.UI.Config;
using PrismAPI.Graphics;
using Cosmos.System;

namespace PrismAPI.UI;

public abstract class Control : Canvas
{
	#region Constructors

	internal Control(int X, int Y, ushort Width, ushort Height, ThemeStyle Theme) : base(Width, Height)
	{
		// Initialize the event handlers.
		OnClick = new((int _, int _, MouseState _) => { });
		OnKey = new((ConsoleKeyInfo _) => { });

		Layout = LayoutStyle.None;
		Background = Color.White;
		Foreground = Color.Black;
		this.Theme = Theme;
		IsEnabled = true;
		Radius = 0;
		Accent = Color.CoolGreen;
		this.X = X;
		this.Y = Y;
	}

	#endregion

	#region Methods

	public abstract void Update(Canvas Canvas);

	#endregion

	#region Fields

	// The onclick event - Runs when the element is clicked on screen.
	public Action<int, int, MouseState> OnClick;

	// The onkey event - Runs when the element is typed into;
	public Action<ConsoleKeyInfo> OnKey;

	public CursorStatus Status;
	public LayoutStyle Layout;
	public Color Foreground;
	public Color Background;
	public ThemeStyle Theme;
	public bool IsEnabled;
	public ushort Radius;
	public Color Accent;
	public int X;
	public int Y;

	#endregion
}