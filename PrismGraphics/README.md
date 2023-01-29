# Introduction - PrismGraphics

PrismgGraphics is the 2D graphics rasterization platform that allows for drawing of text, images, shapes, gradients, and whatever else on a 2D canvas.

This readme is still in progress, not finished!

> There is no hardware acceleration and is constantly WIP, expect bugs!

<hr/>

## PrismGraphics/AnimatorsFadeControler.cs

This animation controler fades from one color to another over a period of time based on the mode it is set in. The available modes are as follows:

<hr/>

- ``FastInSlowOut`` - The color will transition fast at the beginning but slow down as it continues.
- ``FastOutSlowIn`` - The color will transition slow at the beginning but speed up as it continues.
- ``Linear`` - The color will transition at the same speed for the entire duration of the transition.

<hr/>

## PrismGraphics/Color.cs

The Color class is used to represent colors with their ARGB values and provide static predefined colors.

<hr/>

### Properties:
- ``A``: The alpha component of the color.
- ``R``: The red component of the color.
- ``G``: The green component of the color.
- ``B``: The blue component of the color.

<hr/>

### Constructors:
- ``new Color()``: Creates a new instance of the Color class with ARGB value of 0x00000000 (fully transparent black).
- ``new Color(uint argb)``: Creates a new instance of the Color class with the specified ARGB value.
- ``new Color(byte a, byte r, byte g, byte b)``: Creates a new instance of the Color class with the specified alpha, red, green, and blue values.

<hr/>

### Methods:
- ``FromArgb(uint argb)``: Creates a new instance of the Color class with the specified ARGB value.
- ``FromArgb(byte a, byte r, byte g, byte b)``: Creates a new instance of the Color class with the specified alpha, red, green, and blue values.
- ``Equals(object obj)``: Determines whether the specified object is equal to the current Color instance.
- ``GetHashCode()``: Serves as the default hash function.
- ``ToString()``: Returns a string representation of the current Color instance.

<hr/>