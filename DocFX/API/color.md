# Color
## Constructors
### Color(*float A (optional)*, float R, float G, float B)
Creates an instance of the **color** class with the channels *r* as Red, *g* as Green, and *b* as Blue. If *a* is supplied, then that is the Alpha, or else it defaults to 255.

### Color(string ColorInfo)
Creates an instance of the **color** class based off of a string provided through the *ColorInfo* variable. This string can start with 
* "cymk(" for a Cyan-Magenta-Yellow-K(Black) color, 
* "argb(" for an Alpha-Red-Green-Blue color, 
* "rgb(" for an Red-Green-Blue color, 
* "hsl(" for a Hue-Saturation-Lightness color,
* "#" for a hexadecimal color and
* Any colour name in the full [CSS Color List](https://www.w3.org/wiki/CSS/Properties/color/keywords) spectrum.

### Color(uint ARGB)
Creates an instance of the **color** class based off of a 32-bit packed ARGB value in the form of *ARGB*.

## Properties
### Brightness
The overall *Brightness* of a **color** class.

### ARGB
A 32-bit packed ARGB value of a **color** class.

### A
The *alpha* channel of a **color** class.

### R
The *red* channel of a **color** class.

### G
The *green* channel of a **color** class.

### B
The *blue* channel of a **color** class.

## Operators
### Color + Color
Returns the results of *adding* a **color** class and a **color** class.

### Color - Color
Returns the results of *subtracting* a **color** class and a **color** class.

### Color / Color
Returns the results of *dividing* a **color** class and a **color** class.

### Color * Color
Returns the *product* of a **color** class and a **color** class.

### Color + Float (or) Float + Color
Returns the results of *adding* a **color** class and a **float** class. If the float was 10, then this would *add* 10 to each value of the color.

### Color - Color (or) Float - Color
Returns the results of *subtracting* a **color** class and a **float** class. If the float was 10, then this would *subtract* 10 to each value of the color.

### Color / Color (or) Float / Color
Returns the results of *dividing* a **color** class and a **float** class. If the float was 10, then this would *divide* 10 to each value of the color.

### Color * Color (or) Float * Color
Returns the *product* of a **color** class and a **float** class. If the float was 10, then this would *multiply* 10 to each value of the color.

### Color == Color
Returns a bool. True if *ColorOne* == *ColorTwo*, False if *ColorOne* != *ColorTwo*.

### Color != Color
Returns a bool. True if *ColorOne* != *ColorTwo*, False if *ColorOne* == *ColorTwo*.

## Methods
### AlphaBlend(Color Source, Color NewColor)
Returns the blended result of two colors. They are blended by Alpha.

### GetPacked(float A, float R, float G, float B)
Returns a 32-bit packed ARGB value based on the inputs.

### Normalize(Color ToNormalize)
Returns a color with values between 0.0 and 1.0.

### Invert(Color ToInvert)
Returns an inverted color based off of *ToInvert*.

### Lerp(Color StartValue, Color EndValue, float Index)
Returns a color between _StartValue_ and _EndValue_ indexed by _Index_.

### Max(Color Color)
Returns a float of the value of the highest channel in *Color*.

### Min(Color Color)
Returns a float of the value of the lowest channel in *Color*.

### Color.ToGrayscale()
Returns the grayscale version of a **color** class.

## Fields
### Colors
* White 255, 255, 255, 255
* Black 255, 0, 0, 0
* Cyan 255, 0, 255, 255
* Red 255, 255, 0, 0
* Green 255, 0, 255, 0
* Blue 255, 0, 0, 255
* CoolGreen 255, 54, 94, 53
* Magenta 255, 255, 0, 255
* Yellow 255, 255, 255, 0
* HotPink 255, 230, 62, 109
* UbuntuPurple 255, 66, 5, 22
* GoogleBlue 255, 66, 133, 244
* GoogleGreen 255, 52, 168, 83
* GoogleYellow 255, 251, 188, 5
* GoogleRed 255, 234, 67, 53
* DeepOrange 255, 255, 64, 0
* RubyRed 255, 204, 52, 45
* Transparent 0, 0, 0, 0
* StackOverflowOrange 255, 244, 128, 36
* StackOverflowBlack 255, 34, 36, 38
* StackOverflowWhite 255, 188, 187, 187
* DeepGray 255, 25, 25, 25
* LightGray 255, 125, 125, 125
* SuperOrange 255, 255, 99, 71
* FakeGrassGreen 255, 60, 179, 113
* DeepBlue 255, 51, 47, 208
* BloodOrange 255, 255, 123, 0
* LightBlack 255, 25, 25, 25
* LighterBlack 255, 50, 50, 50
* ClassicBlue 255, 52, 86, 139
* LivingCoral 255, 255, 111, 97
* UltraViolet 255, 107, 91, 149
* Greenery 255, 136, 176, 75
* Emerald 255, 0, 155, 119
* LightPurple FFA0A5DD
* Minty FF74C68B
* SunsetRed FFE07572
* LightYellow FFF9C980
