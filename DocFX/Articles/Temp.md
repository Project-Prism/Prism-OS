# An idea of a scripting languge

This is an idea as defined by my (Terminal.cs's) perfered coding style & 'attitude'.
I have not imeplented it because i am not smart enough, but i feel if it were to be a thing, it could make development in almost any programming field easier.

This 'standard' defines all the syntax, types, and misc implementations that should be included in this language.

<hr/>

## Syntax, Standard 'library', & naming

Syntax should be kept mostly minimal, with it being only used to promote cleaner and more understandable structure.
An example of this could be as follows:

```cs
import Library; // Standard 'library' name.
import ""; // Imports external assembly, possibly web URLs to projects aswell. The project will be managed by a lightweight manager that should 100% be cross platform and 100% open source.

namespace "MyNamespace1"; // Define the file's namespace, dis-allow putting multiple namespaces in a single file to dicourage bad design.

// 'public' and 'static' are keywords to modify how the structure will work. 'public' defines it as available to all external code willing to act on it and 'static' marks it as a initialize-once object.
public static MyStructure1 // Only have one type of structure to prevent confusion and promote good design, it will be a combination of a 'class' and an unsafe 'structure', allowing it to be used for any purpose the programmer sees needed.
{
	public MyStructure1 // Allow for static structures to have initializers - This promotes good design and makes organization easier. The visibility level of the constructor cannot be higher than the class itself, and the constructor won't have '()' as static structures cannot be instantiated.
	{
		// Initialize statics here.
		StructurePointer = new(); // Allow pointers to be assigned normally.
		V1 = 50;

		StructurePointer->F = string.Empty; // Empty string, but not null.
	}

	// Do not allow structures to be defined inside another to promote good design - nested classes add lots of clutter.

	#region Fields // Add regions so that code can be organized in IDEs. Common names to be used should be as followed: Properties, Operators, Methods, and Fields.

	public static MyStructure2* StructurePointer;
	public static int V1; // Variables inside the class must be static as it is in a static context, and it's visibility level must not be higher than the class itself.

	#endregion
}

public MyStructure2 // A structure to be instantiated as an object.
{
	public MyStructure2() // Object structures
	{
		MyProperty = 55;
	}

	#region Properties

	public int MyProperty
	{
		get // Run whenever something tries to get the value.
		{
			return MyProperty; // Allow a property to define itself so that aditional variable declaratoins are not needed.
		}
		set // Set, run whenever the property or a value inside of the property is changed, this promotes event-based design and leads to greater performance.
		{
			MyMethod1(); // run some method
			MyProperty = Value; // Value is the name of what the property was set to, and setting the property inside of the property again just sets an automatically internal variable so there is no recursion.
		}
	}

	#endregion

	#region

	public string F;

	#endregion
}
```

## Naming schemes

### Library names

#### Primitive Types

- Vector2
- Vector3
- Vector4
- string
- bool
- ulong
- long
- uint
- int
- ushort
- short
- byte
- char
- double
- float

### Tools

- Library.Console
	- WriteLine(type ToWrite);
	- Write(type ToWrite);
	- Clear();
- Library.Math
	- Sin();
	- SinF
	- Cos
	- CosF
	- Tan
	- TanF
	- Sqrt
	- SqrtF
	- Cbrt
	- CbrtF,
	- PI (constant)