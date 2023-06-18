# Introduction - PrismGraphics

PrismgGraphics is the 2D graphics rasterization platform that allows for drawing of text, images, shapes, gradients, and whatever else on a 2D canvas.

Try not to be intimidated by the sheer size of the graphics library! It is quite simple when you look at it from a high level perspective.

This readme is still in progress, so it is not finished!

<hr/>

> There is no hardware acceleration and is constantly WIP, expect bugs!

<hr/>

## Animation - [AnimationController.cs](https://github.com/Project-Prism/Prism-OS/blob/main/PrismAPI/Graphics/Animation/AnimationController.cs)

### Examples

Interpolate ``0.0f`` to ``255.0f`` over the span of ``5`` seconds with the ``Ease`` mode.
```cs
AnimationController A = new(0.0f, 255.0f, new(0, 0, 5), AnimationMode.Ease);
```


Interpolate ``Color.Black`` to ``Color.Blue`` over the span of ``5`` seconds with the ``Ease`` mode.
```cs
ColorController A = new(Color.Black, Color.Blue, new(0, 0, 5), AnimationMode.Ease);
```

Display the material-theme cirlce spinning progress bar
```cs
AnimationController A = new(25f, 270f, new(0, 0, 0, 0, 750), AnimationMode.Ease);
AnimationController P = new(0f, 360f, new(0, 0, 0, 0, 500), AnimationMode.Linear);

int X = C.Width / 2;
int Y = C.Height / 2;

while (true)
{
	if (A.IsFinished)
	{
		(A.Source, A.Target) = (A.Target, A.Source);
		A.Reset();
	}
	if (P.IsFinished)
	{
		P.Reset();
	}

	int LengthOffset = (int)(P.Current + A.Current);
	int Offset = (int)P.Current;

	C.Clear();
	C.DrawFilledRectangle(X - 32, Y - 32, 64, 64, 6, Color.White);
	C.DrawArc(X, Y, 19, Color.LightGray, Offset, LengthOffset);
	C.DrawArc(X, Y, 20, Color.Black, Offset, LengthOffset);
	C.DrawArc(X, Y, 21, Color.LightGray, Offset, LengthOffset);
	C.Update();
}
```
The output result should look as follows:

https://user-images.githubusercontent.com/76945439/220498920-b9d7a999-f8d1-4d00-a6d4-ed7e97e2a2de.mp4

#### The algorithm
It's two actually. The absolute starting point circles around continuously and linearly and the length of the arc eases in and out between a short and long length

<hr/>

## Graphics - [Gradient.cs](https://github.com/Project-Prism/Prism-OS/blob/main/PrismAPI/Graphics/Gradient.cs)

PrismGraphics includes a file to generate gradients. They are rendered off of a class abstracted from the normal graphics class.
To generate a new gradient, try:

```cs
Gradient G = new(200, 200, Color.White, Color.Green);
```

This will make a gradient which fades from white to green over the 200 pixels in height.
To rotate this, pass it through the rotate filter:

```cs
Filters.Rotate(Angle, G);
```

<hr/>

## Graphics - Getting a display output

Getting a display output using the ``PrismAPI.Graphics`` library is very simple. There are a few modes to choose from, but for now ``SVGAIICanvas`` is the only type that is working.

### Defining new instance

To get a display, define a new instance of the SVGAII canvas like so:

```cs
using PrismAPI.Hardware.GPU;

Display Canvas = new(800, 600);
```

In this case ``800, 600`` are the width and height (in pixels) of the canvas.

### Resizing

The width and/or height can be changed at any time by modifying the ``Width`` and ``Height`` properties in the canvas instance like so:

```cs
Canvas.Width = NewWidth;
Canvas.Height = NewHeight;
```

Note that this is only intended for resizing things that will be re-drawn. For scaling, see [NOT DOCUMENTED, COMING SOON].

### Get a basic screen output

To draw a basic screen with a mouse, Something simillar to the following should be used:

```cs
using PrismAPI.Hardware.GPU;
using PrismAPI.Graphics;
using Cosmos.System;

public class YourKernelName : Kernel
{
	public Display Canvas = null!;

	protected override void BeforeRun()
	{
		// Set-up the mouse manager to fit in the canvas size.
		MouseManager.ScreenWidth = 800;
		MouseManager.ScreenHeight = 600;

		Canvas = Display.GetDisplay(800, 600); // Define the canvas instance.
	}

	protected override void Run()
	{
		Canvas.Clear(Color.CoolGreen); // Draw a green background.
		Canvas.DrawFilledRectangle((int)MouseManager.X, (int)MouseManager.Y, 16, 16, 0, Color.White); // Draw the mouse.
		Canvas.DrawString(15, 15, $"{Canvas.GetFPS()} FPS", default, Color.White); // The default value will become the default font.
		Canvas.Update(); // Copy buffer to the screen.
	}
}
```

This can be modified to fit any need and should work. Never forget to clear and update the screen each frame.

<hr/>

What if we wanted it to look prettier though? Well, how about we put it in a window?
```cs
using PrismAPI.Hardware.GPU;
using PrismAPI.Graphics;
using Cosmos.System;
using PrismAPI.UI;
using PrismAPI.UI.Controls;

namespace PrismOS;

public class Program : Kernel
{

	public Display Canvas = null!;
	public Window MyWindow = null!;
	public Label MyLabel = null!;

	protected override void BeforeRun()
	{
		// Set-up the mouse manager to fit in the canvas size.
		MouseManager.ScreenWidth = 800;
		MouseManager.ScreenHeight = 600;

		Canvas = Display.GetDisplay(800, 600); // Define the canvas instance.
		MyWindow = new(50, 50, 500, 400, "My Window");
		MyLabel = new(15, 15, $"{Canvas.GetFPS()} FPS");

		MyWindow.Controls.Add(MyLabel);
		WindowManager.Windows.Add(MyWindow);
	}

	protected override void Run()
	{
		Canvas.Clear(Color.CoolGreen); // Draw a green background.
		Canvas.DrawFilledRectangle((int)MouseManager.X, (int)MouseManager.Y, 16, 16, 0, Color.White); // Draw the mouse.
		WindowManager.Update(Canvas);
		Canvas.Update();
	}
}
```

This is a relatively basic script to add a window.

First, we make an instance of Window, via `MyWindow`. The `50, 50` is the X and Y coordinates respectively, the `500, 400` is the Width and Height and the `"My Window"` is the title. We then add it to the WindowManager's list of Windows, so that it can be rendered. We run roughly the same code as before, but instead of drawing the FPS on the Canvas, we are doing so on the window. We then need to do `WindowManager.Update(Canvas);` to put the window on the canvas, then we draw the canvas to the screen.

We use a label as they are much more convenient to work with, and DrawString() doesn't work with windows. We put it at the same place as the String was on the main Canvas.

But there's a problem; The mouse is rendering behind the window, not that you can tell due to its white color. Let's fix this, shall we?

The mouse is being rendered BEFORE the Window is drawn, so how about we move the mouse drawing code above `WindowManager.Update(Canvas);`, so it's drawn above it? But it gets lost in the vibrant whiteness of the window's background! We should change its color. I've changed it to StackoverflowOrange, as it's a unique color, but you can change it to whatever you want!

Please note that you should never move `Canvas.Clear()` above anything else! This will make it clear all over whatever that is!

Now, as you may have guessed from the Mouse problem, the text is just white and blending into the background. So let's make the backround a different color! I'm going to use Black, but you can use whatever you'd like. We just need to add two arguments; the first for the task bar color, the second for the main background color. I'm going to be adding `Color.DeepGray, Color.Black` to mine. And perfect! We have a pretty window! But still no visible label.

```cs
using PrismAPI.Hardware.GPU;
using PrismAPI.Graphics;
using Cosmos.System;
using PrismAPI.UI;
using PrismAPI.UI.Controls;

namespace PrismOS;

public class Program : Kernel
{

	public Display Canvas = null!;
	public Window MyWindow = null!;
	public Label MyLabel = null!;

	protected override void BeforeRun()
	{
		// Set-up the mouse manager to fit in the canvas size.
		MouseManager.ScreenWidth = 800;
		MouseManager.ScreenHeight = 600;

		Canvas = Display.GetDisplay(800, 600); // Define the canvas instance.
		MyWindow = new(50, 50, 500, 400, "My Window", Color.DeepGray, Color.Black);
		MyLabel = new(15, 15, $"{Canvas.GetFPS()} FPS");

		MyWindow.Controls.Add(MyLabel);
		WindowManager.Windows.Add(MyWindow);
	}

	protected override void Run()
	{
		Canvas.Clear(Color.CoolGreen); // Draw a green background.
		WindowManager.Update(Canvas);
		Canvas.DrawFilledRectangle((int)MouseManager.X, (int)MouseManager.Y, 16, 16, 0, Color.StackOverflowOrange); // Draw the mouse.
		Canvas.Update();
	}
}
```
I'll come back to this later once there's a way to get the label to actually show up, as right now there isn't.

<hr/>

# ToDo

This project still has many things to finish, including video drivers.

> See [here](https://wiki.osdev.org/Accelerated_Graphic_Cards) for the OSDev Wiki's info on graphic cards.

> NVidia: See [here](https://nvidia.github.io/open-gpu-doc/) for the NVidia GPU docs.

