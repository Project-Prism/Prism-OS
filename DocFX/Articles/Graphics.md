# Introduction - PrismGraphics

PrismGraphics is the two-dimensional/three-dimensional graphics rasterization platform that allows for drawing of text, images, shapes, gradients, and whatever else on a 2D canvas.

Try not to be intimidated by the sheer size of the graphics library! It is quite simple when you look at it from a high level perspective.

This readme is still in progress, so it is not finished!

<hr/>

> There is no hardware acceleration and is constantly WIP, expect bugs!

<hr/>

## We need to start somewhere... (Output pixels to screen)

Getting a display output using the ``PrismAPI.Graphics`` library is very simple. There are a few displays to choose from.

### Defining new instance

To get a display using VMware's SVGAII, define a new instance of his canvas canvas like so:

```cs
using PrismAPI.Hardware.GPU.VMware;

SVGAIICanvas Canvas = new(800, 600);
```

In this case ``800, 600`` are the width and height (in pixels) of the canvas.

## Making your OS more fancy (Adding animations)
To add some really fancy animations, you need to create a new instance of [AnimationController.cs](https://github.com/Project-Prism/Prism-OS/blob/main/PrismAPI/Graphics/Animation/AnimationController.cs) or class who inherits from.

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

## Still not much??

PrismGraphics includes gradients generation. They are rendered off of a [Gradient.cs](https://github.com/Project-Prism/Prism-OS/blob/main/PrismAPI/Graphics/Gradient.cs) abstracted from the normal graphics class.
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

### Resizing

The width and/or height can be changed at any time by modifying the ``Width`` and ``Height`` properties in the canvas instance like so:

```cs
Canvas.Width = NewWidth;
Canvas.Height = NewHeight;
```

Note that this is only intended for resizing things that will be re-drawn. For scaling, see [NOT DOCUMENTED, COMING SOON].

<hr/>

# ToDo

This project still has many things to finish, including video drivers.

> See [here](https://wiki.osdev.org/Accelerated_Graphic_Cards) for the OSDev Wiki's info on graphic cards.

> NVidia: See [here](https://nvidia.github.io/open-gpu-doc/) for the NVidia GPU docs.

