# Introduction - PrismGraphics

PrismgGraphics is the 2D graphics rasterization platform that allows for drawing of text, images, shapes, gradients, and whatever else on a 2D canvas.

This readme is still in progress, not finished!

> There is no hardware acceleration and is constantly WIP, expect bugs!

<hr/>

## Animation - [AnimationController.cs](https://github.com/Project-Prism/Prism-OS/blob/main/PrismGraphics/Animation/AnimationController.cs)

The AnimationController class provides a way to add basic ease animations or transitions to anything. It supports several different types of animations and can be used to animate values over time. The class is located in the PrismGraphics.Animation namespace and can be used by instantiating an object of the AnimationController class.

### Constructor
The AnimationController class has a single constructor that takes in four arguments:

```cs
public AnimationController(float Source, float Target, TimeSpan Duration, AnimationMode Mode)
```
- Source: The starting value of the animation.
- Target: The value to end the animation at.
- Duration: The duration to play the animation over.
- Mode: The ease animation mode.

### Properties
The AnimationController class has a single property:

```cs
public bool IsFinished;
```

- IsFinished: A boolean that returns true when the animation has finished playing.

### Constants
The AnimationController class has a single constant:

```cs
public const int DelayMS = 25;
```

- DelayMS: The delay in milliseconds of each update. This value is used by the internal timer to increment the final value.

### Methods
The AnimationController class has three methods:

```cs
public void Reset();
```

- Reset(): Resets the progress and plays the new values if new ones were set.

```cs
private void Next();
```

- Next(): An internal method used by the timer to increment the final value.

```cs
private static float Flip(float X);
```

- Flip(float X): A static method used to flip the given value. Returns 1 - X.

### Fields
The AnimationController class has four public fields:

```cs
public float Current, Source, Target, ElapsedTime;
```

- Current: A value marking points in the animation.
- Source: The starting value of the animation.
- Target: The value to end the animation at.
- ElapsedTime: The elapsed time of the animation.

### Animation Modes
The AnimationController class supports the following ease animation modes:

- BounceOut: An ease-out bounce animation.
- BounceIn: An ease-in bounce animation.
- Bounce: A bounce animation.
- EaseOut: An ease-out animation.
- EaseIn: An ease-in animation.
- Ease: An ease animation.
- Linear: A linear animation.

### Easing Functions
The AnimationController class uses easing functions to create the different animation modes. The class contains the following easing functions:

```cs
private static float BounceOut(float T);
private static float BounceIn(float T);
private static float Bounce(float T);
private static float EaseOut(float T);
private static float EaseIn(float T);
private static float Ease(float T);
```

These functions are used by the Next() method to calculate the output value for the animation based on the elapsed time and the chosen animation mode.

### References
Easing Functions: The AnimationController class uses easing functions inspired by the work of Gabriele Cirulli.

### Examples
To know when the animation has finished, check the 'IsFinished' property like so:
```cs
if (C.IsFinished)
{
	// Code to run when animation is finished.
}
```

To make it looping (reverse & forward) add the following in the ``if`` statement:
```cs
(C.Source, C.Target) = (C.Target, C.Source);
C.Reset();
```

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

## Graphics - [Color.cs](https://github.com/Project-Prism/Prism-OS/blob/main/PrismGraphics/Color.cs)

The Color class is used to represent colors with their ARGB values and provide static predefined colors.

### Properties:
- ``A``: The alpha component of the color.
- ``R``: The red component of the color.
- ``G``: The green component of the color.
- ``B``: The blue component of the color.

### Methods:
- ``FromArgb(uint argb)``: Creates a new instance of the Color class with the specified ARGB value.
- ``FromArgb(byte a, byte r, byte g, byte b)``: Creates a new instance of the Color class with the specified alpha, red, green, and blue values.
- ``Equals(object obj)``: Determines whether the specified object is equal to the current Color instance.
- ``GetHashCode()``: Serves as the default hash function.
- ``ToString()``: Returns a string representation of the current Color instance.

<hr/>

# ToDo

This project still has many things to finish, including video drivers.
See [here](https://wiki.osdev.org/Accelerated_Graphic_Cards).
