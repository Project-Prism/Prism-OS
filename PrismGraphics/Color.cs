using System.Globalization;

namespace PrismGraphics
{
	/// <summary>
	/// Color class, used for drawing.
	/// </summary>
	public struct Color
	{
		#region Operators

		public static implicit operator Color((byte, byte, byte, byte) Color)
		{
			return FromARGB(Color.Item1, Color.Item2, Color.Item3, Color.Item4);

			throw new ArgumentException("Unsuported Color Format!");
		}
		public static implicit operator Color((byte, byte, byte) RGB)
		{
			return FromARGB(255, RGB.Item1, RGB.Item2, RGB.Item3);
		}
		public static implicit operator Color(string Color)
		{
			return FromHex(Color);
		}
		public static implicit operator Color(uint ARGB)
		{
			return FromARGB(ARGB);
		}

		public static Color operator +(Color Original, Color ToAdd)
		{
			Original.R += ToAdd.R;
			Original.G += ToAdd.G;
			Original.B += ToAdd.B;

			return Original;
		}
		public static Color operator -(Color Original, Color ToAdd)
		{
			Original.R -= ToAdd.R;
			Original.G -= ToAdd.G;
			Original.B -= ToAdd.B;

			return Original;
		}

		public static Color operator /(Color Original, double Value)
		{
			Original.R = (byte)(Original.R / Value);
			Original.G = (byte)(Original.G / Value);
			Original.B = (byte)(Original.B / Value);

			return Original;
		}
		public static Color operator *(Color Original, double Value)
		{
			Original.R = (byte)(Original.R * Value);
			Original.G = (byte)(Original.G * Value);
			Original.B = (byte)(Original.B * Value);

			return Original;
		}

		public static Color operator /(Color Original, long Value)
		{
			Original.R = (byte)(Original.R / Value);
			Original.G = (byte)(Original.G / Value);
			Original.B = (byte)(Original.B / Value);

			return Original;
		}
		public static Color operator *(Color Original, long Value)
		{
			Original.R = (byte)(Original.R * Value);
			Original.G = (byte)(Original.G * Value);
			Original.B = (byte)(Original.B * Value);

			return Original;
		}

		public static bool operator ==(Color C1, Color C2)
		{
			return C1.ARGB == C2.ARGB;
		}
		public static bool operator !=(Color C1, Color C2)
		{
			return C1.ARGB != C2.ARGB;
		}

		#endregion

		#region Methods

		#region Methods [ Loading ]

		/// <summary>
		/// Loads color from printer CYMK colorspace.
		/// </summary>
		/// <param name="C">Cyan value.</param>
		/// <param name="Y">Yellow value.</param>
		/// <param name="M">Magenta value.</param>
		/// <param name="K">Black value</param>
		/// <returns>ARGB color from CYMK colorspace.</returns>
		public static Color FromCYMK(byte C, byte Y, byte M, byte K)
		{
			Color T = new();

			if (K != 255)
			{
				T.A = 255;
				T.R = (byte)((255 - C) * (255 - K) / 255);
				T.G = (byte)((255 - M) * (255 - K) / 255);
				T.B = (byte)((255 - Y) * (255 - K) / 255);
			}
			else
			{
				T.A = 255;
				T.R = (byte)(255 - C);
				T.G = (byte)(255 - M);
				T.B = (byte)(255 - Y);
			}
			return T;
		}

		/// <summary>
		/// Loads color from ARGB colorspace.
		/// </summary>
		/// <param name="A">Alpha value.</param>
		/// <param name="R">Red value.</param>
		/// <param name="G">Green value.</param>
		/// <param name="B">Blue value.</param>
		/// <returns>ARGB color from values.</returns>
		public static Color FromARGB(byte A, byte R, byte G, byte B)
		{
			return new() { A = A, R = R, G = G, B = B };
		}

		/// <summary>
		/// Gets ARGB color from packed value.
		/// </summary>
		/// <param name="ARGB">Packed ARGB value.</param>
		/// <returns>Color class from ARGB value.</returns>
		public static Color FromARGB(uint ARGB)
		{
			return new() { ARGB = ARGB };
		}

		/// <summary>
		/// Gets ARGB color from string hex value.
		/// </summary>
		/// <param name="Hex">Hex color value (like #FFFFFFFF)</param>
		/// <returns>ARGB color from hex value.</returns>
		public static Color FromHex(string Hex)
		{
			if (Hex.StartsWith('#'))
			{
				Hex = Hex[1..];
			}

			byte A, R, G, B;

			switch (Hex.Length)
			{
				case 8:
					A = byte.Parse(Hex[0..2], NumberStyles.HexNumber);
					R = byte.Parse(Hex[2..4], NumberStyles.HexNumber);
					G = byte.Parse(Hex[4..6], NumberStyles.HexNumber);
					B = byte.Parse(Hex[6..8], NumberStyles.HexNumber);
					break;
				case 6:
					A = 255;
					R = byte.Parse(Hex[0..2], NumberStyles.HexNumber);
					G = byte.Parse(Hex[2..4], NumberStyles.HexNumber);
					B = byte.Parse(Hex[4..6], NumberStyles.HexNumber);
					break;
				default:
					throw new FormatException("Hex value is not in correct format!");
			}

			return FromARGB(A, R, G ,B);
		}

		/// <summary>
		/// Converts the color to be only in grayscale.
		/// </summary>
		/// <param name="UseAlpha">Allow alpha when converting.</param>
		/// <returns>Grayscale color.</returns>
		public Color ToGrayscale(bool UseAlpha)
		{
			byte Average = (byte)((R / 3) + (G / 3) + (B / 3));
			return FromARGB(UseAlpha ? A : (byte)255, Average, Average, Average);
		}

		/// <summary>
		/// Returns a color from an HTML color name.
		/// </summary>
		/// <param name="ColorName">Valid HTML color name.</param>
		/// <returns>HTML color in <see cref="Color"/> format.</returns>
		public static Color FromName(string ColorName)
		{
			if (Cache.ContainsKey(ColorName))
			{
				return Cache[ColorName];
			}

			Color C = ColorName switch
			{
				"AliceBlue" => "#F0F8FF",
				"AntiqueWhite" => "#FAEBD7",
				"Aqua" => "#00FFFF",
				"Aquamarine" => "#7FFFD4",
				"Azure" => "#F0FFFF",
				"Beige" => "#F5F5DC",
				"Bisque" => "#FFE4C4",
				"Black" => "#000000",
				"BlanchedAlmond" => "#FFEBCD",
				"Blue" => "#0000FF",
				"BlueViolet" => "#8A2BE2",
				"Brown" => "#A52A2A",
				"BurlyWood" => "#DEB887",
				"CadetBlue" => "#5F9EA0",
				"Chartreuse" => "#7FFF00",
				"Chocolate" => "#D2691E",
				"Coral" => "#FF7F50",
				"CornflowerBlue" => "#6495ED",
				"Cornsilk" => "#FFF8DC",
				"Crimson" => "#DC143C",
				"Cyan" => "#00FFFF",
				"DarkBlue" => "#00008B",
				"DarkCyan" => "#008B8B",
				"DarkGoldenRod" => "#B8860B",
				"DarkGray" => "#A9A9A9",
				"DarkGrey" => "#A9A9A9",
				"DarkGreen" => "#006400",
				"DarkKhaki" => "#BDB76B",
				"DarkMagenta" => "#8B008B",
				"DarkOliveGreen" => "#556B2F",
				"DarkOrange" => "#FF8C00",
				"DarkOrchid" => "#9932CC",
				"DarkRed" => "#8B0000",
				"DarkSalmon" => "#E9967A",
				"DarkSeaGreen" => "#8FBC8F",
				"DarkSlateBlue" => "#483D8B",
				"DarkSlateGray" => "#2F4F4F",
				"DarkSlateGrey" => "#2F4F4F",
				"DarkTurquoise" => "#00CED1",
				"DarkViolet" => "#9400D3",
				"DeepPink" => "#FF1493",
				"DeepSkyBlue" => "#00BFFF",
				"DimGray" => "#696969",
				"DimGrey" => "#696969",
				"DodgerBlue" => "#1E90FF",
				"FireBrick" => "#B22222",
				"FloralWhite" => "#FFFAF0",
				"ForestGreen" => "#228B22",
				"Fuchsia" => "#FF00FF",
				"Gainsboro" => "#DCDCDC",
				"GhostWhite" => "#F8F8FF",
				"Gold" => "#FFD700",
				"GoldenRod" => "#DAA520",
				"Gray" => "#808080",
				"Grey" => "#808080",
				"Green" => "#008000",
				"GreenYellow" => "#ADFF2F",
				"HoneyDew" => "#F0FFF0",
				"HotPink" => "#FF69B4",
				"IndianRed" => "#CD5C5C",
				"Indigo" => "#4B0082",
				"Ivory" => "#FFFFF0",
				"Khaki" => "#F0E68C",
				"Lavender" => "#E6E6FA",
				"LavenderBlush" => "#FFF0F5",
				"LawnGreen" => "#7CFC00",
				"LemonChiffon" => "#FFFACD",
				"LightBlue" => "#ADD8E6",
				"LightCoral" => "#F08080",
				"LightCyan" => "#E0FFFF",
				"LightGoldenRodYellow" => "#FAFAD2",
				"LightGray" => "#D3D3D3",
				"LightGrey" => "#D3D3D3",
				"LightGreen" => "#90EE90",
				"LightPink" => "#FFB6C1",
				"LightSalmon" => "#FFA07A",
				"LightSeaGreen" => "#20B2AA",
				"LightSkyBlue" => "#87CEFA",
				"LightSlateGray" => "#778899",
				"LightSlateGrey" => "#778899",
				"LightSteelBlue" => "#B0C4DE",
				"LightYellow" => "#FFFFE0",
				"Lime" => "#00FF00",
				"LimeGreen" => "#32CD32",
				"Linen" => "#FAF0E6",
				"Magenta" => "#FF00FF",
				"Maroon" => "#800000",
				"MediumAquaMarine" => "#66CDAA",
				"MediumBlue" => "#0000CD",
				"MediumOrchid" => "#BA55D3",
				"MediumPurple" => "#9370DB",
				"MediumSeaGreen" => "#3CB371",
				"MediumSlateBlue" => "#7B68EE",
				"MediumSpringGreen" => "#00FA9A",
				"MediumTurquoise" => "#48D1CC",
				"MediumVioletRed" => "#C71585",
				"MidnightBlue" => "#191970",
				"MintCream" => "#F5FFFA",
				"MistyRose" => "#FFE4E1",
				"Moccasin" => "#FFE4B5",
				"NavajoWhite" => "#FFDEAD",
				"Navy" => "#000080",
				"OldLace" => "#FDF5E6",
				"Olive" => "#808000",
				"OliveDrab" => "#6B8E23",
				"Orange" => "#FFA500",
				"OrangeRed" => "#FF4500",
				"Orchid" => "#DA70D6",
				"PaleGoldenRod" => "#EEE8AA",
				"PaleGreen" => "#98FB98",
				"PaleTurquoise" => "#AFEEEE",
				"PaleVioletRed" => "#DB7093",
				"PapayaWhip" => "#FFEFD5",
				"PeachPuff" => "#FFDAB9",
				"Peru" => "#CD853F",
				"Pink" => "#FFC0CB",
				"Plum" => "#DDA0DD",
				"PowderBlue" => "#B0E0E6",
				"Purple" => "#800080",
				"RebeccaPurple" => "#663399",
				"Red" => "#FF0000",
				"RosyBrown" => "#BC8F8F",
				"RoyalBlue" => "#4169E1",
				"SaddleBrown" => "#8B4513",
				"Salmon" => "#FA8072",
				"SandyBrown" => "#F4A460",
				"SeaGreen" => "#2E8B57",
				"SeaShell" => "#FFF5EE",
				"Sienna" => "#A0522D",
				"Silver" => "#C0C0C0",
				"SkyBlue" => "#87CEEB",
				"SlateBlue" => "#6A5ACD",
				"SlateGray" => "#708090",
				"SlateGrey" => "#708090",
				"Snow" => "#FFFAFA",
				"SpringGreen" => "#00FF7F",
				"SteelBlue" => "#4682B4",
				"Tan" => "#D2B48C",
				"Teal" => "#008080",
				"Thistle" => "#D8BFD8",
				"Tomato" => "#FF6347",
				"Turquoise" => "#40E0D0",
				"Violet" => "#EE82EE",
				"Wheat" => "#F5DEB3",
				"White" => "#FFFFFF",
				"WhiteSmoke" => "#F5F5F5",
				"Yellow" => "#FFFF00",
				"YellowGreen" => "#9ACD32",
				_ => throw new($"Color '{ColorName}' does not exist."),
			};

			Cache.Add(ColorName, C);
			return C;
		}

		#endregion

		#region Methods [ Tools ]

		/// <summary>
		/// Mixes colors together based on their weights in the Values array.
		/// </summary>
		/// <param name="Colors">Colors array.</param>
		/// <param name="Values">Weights array.</param>
		/// <returns>Mixed color.</returns>
		public static Color Mix(Color[] Colors, float[] Values)
		{
			if (Colors.Length != Values.Length)
			{
				throw new("Every color must have an asociated percent value.");
			}

			Color T = new();
			for (int I = 0; I < Colors.Length; I++)
			{
				T.A += (byte)(Values[I] * Colors[I].A / Colors[I].A);
				T.R += (byte)(Values[I] * Colors[I].R / Colors[I].R);
				T.G += (byte)(Values[I] * Colors[I].G / Colors[I].G);
				T.B += (byte)(Values[I] * Colors[I].B / Colors[I].B);

			}
			return T;
		}

		/// <summary>
		/// Blends two colors together based on their alpha values.
		/// </summary>
		/// <param name="Source">The original color.</param>
		/// <param name="NewColor">The new color to mix.</param>
		/// <returns>Mixed color.</returns>
		public static Color AlphaBlend(Color Source, Color NewColor)
		{
			if (NewColor.A == 255)
			{
				return NewColor;
			}
			if (NewColor.A == 0)
			{
				return Source;
			}

			return FromARGB(
				(byte)((Source.A * (255 - NewColor.A) / 255) + NewColor.A),
				(byte)((Source.R * (255 - NewColor.A) / 255) + NewColor.R),
				(byte)((Source.G * (255 - NewColor.A) / 255) + NewColor.G),
				(byte)((Source.B * (255 - NewColor.A) / 255) + NewColor.B));
		}

		/// <summary>
		/// Converts an ARGB color to it's packed ARGB format.
		/// </summary>
		/// <param name="A">Alpha channel.</param>
		/// <param name="R">Red channel.</param>
		/// <param name="G">Green channel.</param>
		/// <param name="B">Blue channel.</param>
		/// <returns>Packed value.</returns>
		public static uint GetPacked(byte A, byte R, byte G, byte B)
		{
			return (uint)(A << 24 | R << 16 | G << 8 | B);
		}
		
		/// <summary>
		/// Converts a console color into a 32-BIT RGB color.
		/// </summary>
		/// <param name="Color">Console color to convert.</param>
		/// <returns>32-BIT RGB Color.</returns>
		public static Color FromConsleColor(ConsoleColor Color)
		{
			return Color switch
			{
				ConsoleColor.Black => Black,
				ConsoleColor.DarkBlue => Blue - 64,
				ConsoleColor.DarkGreen => Green - 64,
				ConsoleColor.DarkCyan => Cyan - 64,
				ConsoleColor.DarkRed => Red - 64,
				ConsoleColor.DarkMagenta => Magenta - 64,
				ConsoleColor.DarkYellow => Yellow - 64,
				ConsoleColor.Gray => LightGray,
				ConsoleColor.DarkGray => DeepGray - 64,
				ConsoleColor.Blue => Blue,
				ConsoleColor.Green => Green,
				ConsoleColor.Cyan => Cyan,
				ConsoleColor.Red => Red,
				ConsoleColor.Magenta => Magenta,
				ConsoleColor.Yellow => Yellow,
				ConsoleColor.White => White,
				_ => Black,
			};
		}

		#endregion

		#endregion

		#region Fields

		#region Extended Colors

		public static readonly Color White = FromARGB(255, 255, 255, 255);
		public static readonly Color Black = FromARGB(255, 0, 0, 0);
		public static readonly Color Cyan = FromARGB(255, 0, 255, 255);
		public static readonly Color Red = FromARGB(255, 255, 0, 0);
		public static readonly Color Green = FromARGB(255, 0, 255, 0);
		public static readonly Color Blue = FromARGB(255, 0, 0, 255);
		public static readonly Color CoolGreen = FromARGB(255, 54, 94, 53);
		public static readonly Color Magenta = FromARGB(255, 255, 0, 255);
		public static readonly Color Yellow = FromARGB(255, 255, 255, 0);
		public static readonly Color HotPink = FromARGB(255, 230, 62, 109);
		public static readonly Color UbuntuPurple = FromARGB(255, 66, 5, 22);
		public static readonly Color GoogleBlue = FromARGB(255, 66, 133, 244);
		public static readonly Color GoogleGreen = FromARGB(255, 52, 168, 83);
		public static readonly Color GoogleYellow = FromARGB(255, 251, 188, 5);
		public static readonly Color GoogleRed = FromARGB(255, 234, 67, 53);
		public static readonly Color DeepOrange = FromARGB(255, 255, 64, 0);
		public static readonly Color RubyRed = FromARGB(255, 204, 52, 45);
		public static readonly Color Transparent = FromARGB(0, 0, 0, 0);
		public static readonly Color StackOverflowOrange = FromARGB(255, 244, 128, 36);
		public static readonly Color StackOverflowBlack = FromARGB(255, 34, 36, 38);
		public static readonly Color StackOverflowWhite = FromARGB(255, 188, 187, 187);
		public static readonly Color DeepGray = FromARGB(255, 25, 25, 25);
		public static readonly Color LightGray = FromARGB(255, 125, 125, 125);
		public static readonly Color SuperOrange = FromARGB(255, 255, 99, 71);
		public static readonly Color FakeGrassGreen = FromARGB(255, 60, 179, 113);
		public static readonly Color DeepBlue = FromARGB(255, 51, 47, 208);
		public static readonly Color BloodOrange = FromARGB(255, 255, 123, 0);
		public static readonly Color LightBlack = FromARGB(255, 25, 25, 25);
		public static readonly Color LighterBlack = FromARGB(255, 50, 50, 50);
		public static readonly Color ClassicBlue = FromARGB(255, 52, 86, 139);
		public static readonly Color LivingCoral = FromARGB(255, 255, 111, 97);
		public static readonly Color UltraViolet = FromARGB(255, 107, 91, 149);
		public static readonly Color Greenery = FromARGB(255, 136, 176, 75);
		public static readonly Color Emerald = FromARGB(255, 0, 155, 119);

		#endregion

		/// <summary>
		/// Color cache, used for caching color values.
		/// </summary>
		private readonly static Dictionary<string, Color> Cache = new();

		/// <summary>
		/// Saturation of the color.
		/// </summary>
		public int Saturation
		{
			get
			{
				// Calculate the saturation of the color
				int Max = Math.Max(_R, Math.Max(_G, _B));
				int Min = Math.Min(_R, Math.Min(_G, _B));
				return (Max - Min) / 255;
			}
			set
			{
				// Set the saturation of the color
				int Max = Math.Max(_R, Math.Max(_G, _B));
				int Min = Math.Min(_R, Math.Min(_G, _B));
				int Diff = Max - Min;
				if (Diff == 0)
				{
					_R = _G = _B = (byte)value;
				}
				else
				{
					_R = (byte)((Max - _R) * value / Diff + _R);
					_G = (byte)((Max - _G) * value / Diff + _G);
					_B = (byte)((Max - _B) * value / Diff + _B);
				}
			}
		}

		/// <summary>
		/// Packed ARGB value of the color.
		/// </summary>
		public uint ARGB
		{
			get
			{
				return _ARGB;
			}
			set
			{
				_ARGB = value;
				_A = (byte)(_ARGB >> 24);
				_R = (byte)(_ARGB >> 16);
				_G = (byte)(_ARGB >> 8);
				_B = (byte)(_ARGB);
			}
		}

		/// <summary>
		/// Alpha channel of the color.
		/// </summary>
		public byte A
		{
			get
			{
				return _A;
			}
			set
			{
				_A = value;
				_ARGB = (uint)(_A << 24 | _R << 16 | _G << 8 | _B);
			}
		}

		/// <summary>
		/// Red channel of the color.
		/// </summary>
		public byte R
		{
			get
			{
				return _R;
			}
			set
			{
				_R = value;
				_ARGB = (uint)(_A << 24 | _R << 16 | _G << 8 | _B);
			}
		}

		/// <summary>
		/// Green channel of the color.
		/// </summary>
		public byte G
		{
			get
			{
				return _G;
			}
			set
			{
				_G = value;
				_ARGB = (uint)(_A << 24 | _R << 16 | _G << 8 | _B);
			}
		}

		/// <summary>
		/// Blue channel of the color.
		/// </summary>
		public byte B
		{
			get
			{
				return _B;
			}
			set
			{
				_B = value;
				_ARGB = (uint)(_A << 24 | _R << 16 | _G << 8 | _B);
			}
		}

		private uint _ARGB;
		private byte _A;
		private byte _R;
		private byte _G;
		private byte _B;

		#endregion

		public override string ToString()
		{
			return $"{typeof(Color).FullName} [A: {A}, R: {R}, G: {G}, B: {B}]";
		}
	}
}