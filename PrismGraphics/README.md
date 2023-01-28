# Introduction - PrismGraphics

PrismgGraphics is the 2D graphics rasterization platform that allows for drawing of text, images, shapes, gradients, and whatever else on a 2D canvas.

This readme is still in progress, not finished!

> There is no hardware acceleration and is constantly WIP, expect bugs!

<hr/>

## PrismGraphics/Animators

This folder consains animation tools to aid in making special GFX.

### FadeControler

This animation controler fades from one color to another over a period of time based on the mode it is set in. The available modes are as follows:

<hr/>

- ``FastInSlowOut`` - The color will transition fast at the beginning but slow down as it continues.
- ``FastOutSlowIn`` - The color will transition slow at the beginning but speed up as it continues.
- ``Linear`` - The color will transition at the same speed for the entire duration of the transition.

<hr/>