# Introduction - PrismGraphics

PrismgGraphics is the 2D graphics rasterization platform that allows for drawing of text, images, shapes, gradients, and whatever else on a 2D canvas.

This readme is still in progress, not finished!

> There is no hardware acceleration and is constantly WIP, expect bugs!

<hr/>

## Animation - [AnimationController.cs](https://github.com/Project-Prism/Prism-OS/blob/main/PrismGraphics/Animation/AnimationController.cs)

This animation controler is a non-blocking number interpolator that lerps one number to another over a period of time based on the mode it is set in. The available modes are defined [Here](https://github.com/Project-Prism/Prism-OS/blob/main/PrismGraphics/Animation/AnimationMode.cs).

> The controler has an update 'fidelity' of once every ``50`` milliseconds.

### Example(s)

Interpolate ``0.0f`` to ``255.0f`` over the span of ``5`` seconds with the ``Ease`` mode.
```cs
AnimationController A = new(0.0f, 255.0f, new(0, 0, 5), AnimationMode.Ease);
```

<hr/>

## Animation - [ColorController.cs](https://github.com/Project-Prism/Prism-OS/blob/main/PrismGraphics/Animation/ColorController.cs)

This animation controler is a non-blocking color interpolator that lerps one color to another over a period of time based on the mode it is set in. The available modes are defined [Here](https://github.com/Project-Prism/Prism-OS/blob/main/PrismGraphics/Animation/AnimationMode.cs).

> The controler has an update 'fidelity' of once every ``50`` milliseconds.

### Example(s)

Interpolate ``Color.Black`` to ``Color.Blue`` over the span of ``5`` seconds with the ``Ease`` mode.
```cs
ColorController A = new(Color.Black, Color.Blue, new(0, 0, 5), AnimationMode.Ease);
```

<hr/>

## Graphics - [Color.cs](https://github.com/Project-Prism/Prism-OS/blob/main/PrismGraphics/Color.cs)

The Color class is used to represent colors with their ARGB values and provide static predefined colors.

### Properties:
- ``A``: The alpha component of the color.
- ``R``: The red component of the color.
- ``G``: The green component of the color.
- ``B``: The blue component of the color.

### Constructors:
- ``new Color()``: Creates a new instance of the Color class with ARGB value of 0x00000000 (fully transparent black).
- ``new Color(uint argb)``: Creates a new instance of the Color class with the specified ARGB value.
- ``new Color(byte a, byte r, byte g, byte b)``: Creates a new instance of the Color class with the specified alpha, red, green, and blue values.

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