## GLang V1 documentation

<hr/>

GLang is a high level graphics drawing language, it currently has no use or goals and is more of a learning experience. GLang is compiled to a custom bytecode that is read and executed by the GLang runtime.

You are free to make your own ports of the GLang runtime and compiler, it is very simple to do as it is mostly ``for`` loops.

<hr/>

## Available functions

Glang has many functions, here is a list of all of them:

- SetMode(Width, Height);
	> Sets the mode of the canvas.
- Clear(ARGB Value);
	> Clears the canvas with a set color.
- Draw(Shape, (Points/Dimentions), ARGB Color);
    > Draws a shape with the given input.
- Exit();
    > Exit the application.

<hr/>