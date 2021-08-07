## What are "Components"?
Components are basicly just extentios for the system. Some functions such as system.console have extentions, like System2.Console.WriteColoredLine("");
they allow for less code to be used in core apps and (eventualy) apps. with the given example above, you will be abe to write a line to the console, but coloring it is made easier.
Heres an example:
	> System2.Console.WriteColoredLine("Some text\nNew line!", ConsoleColor.Red);

#### More about System2
System2 is basicly the core for prism, it contains custom workarounds, cool functions, code-reducing helpers, GUI (for later use)
so it would probably be best if you dont delete that folder.

#### Hexi as a component
Hexi is a component because we plan to have it as another part of the core functions for prism.
it will allow easy app creation, it will be able to interface with a prompt, GUI, and real hardware.

### Ideas for Compoents/System2
| Idea               | Complexety     | Started?|
|--------------------|----------------|---------|
| TUI interface      | Medium         | No      |
| components in hexi | Medium/High    | No      | > See component1.hx
| Properly port hexi | High           | No      | 

###### Remember: These are just ideas and they might not become a reality.