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
	C.DrawArc(400, 300, 19, Color.LightGray, Offset, LengthOffset);
	C.DrawArc(400, 300, 20, Color.Black, Offset, LengthOffset);
	C.DrawArc(400, 300, 21, Color.LightGray, Offset, LengthOffset);
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
	public Display Canvas;

	protected override BeforeRun()
	{
		// Set-up the mouse manager to fit in the canvas size.
		MouseManager.ScreenWidth = 800;
		MouseManager.ScreenHeight = 600;

		Canvas = Display.GetDisplay(800, 600); // Define the canvas instance.
	}

	protected override Run()
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

# ToDo

This project still has many things to finish, including video drivers.

> See [here](https://wiki.osdev.org/Accelerated_Graphic_Cards).

> NVidia: [here](https://nvidia.github.io/open-gpu-doc/).

