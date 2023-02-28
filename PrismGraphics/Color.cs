using System.Globalization;

namespace PrismGraphics
{
	/// <summary>
	/// Color class, used for drawing.
	/// </summary>
	public struct Color
	{
		#region Properties

		/// <summary>
		/// The brightness (or average value) of the color.
		/// </summary>
		public byte Brightness
		{
			get
			{
				return (byte)((A + R + G + B) / 4);
			}
		}

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
				_B = (byte)_ARGB;
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

		#endregion

		#region Operators

		public static implicit operator Color((byte, byte, byte, byte) ARGB)
		{
			return FromARGB(ARGB.Item1, ARGB.Item2, ARGB.Item3, ARGB.Item4);
		}
		public static implicit operator Color((byte, byte, byte) RGB)
		{
			return FromARGB(255, RGB.Item1, RGB.Item2, RGB.Item3);
		}
		public static implicit operator Color(string Color)
		{
			return FromString(Color);
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

		/// <summary>
		/// Returns a new color with custom values.
		/// </summary>
		/// <param name="A">The alpha channel of the color.</param>
		/// <param name="R">The red channel of the color.</param>
		/// <param name="G">The green channel of the color.</param>
		/// <param name="B">The blue channel of the color.</param>
		/// <returns>The color as specified by the color channels.</returns>
		public static Color FromARGB(byte A, byte R, byte G, byte B)
		{
			return new()
			{
				A = A,
				R = R,
				G = G,
				B = B,
			};
		}

		/// <summary>
		/// Gets a color from a packed value.
		/// </summary>
		/// <param name="ARGB">The packed value to extract a color from.</param>
		/// <returns>A color based on the input packed value.</returns>
		public static Color FromARGB(uint ARGB)
		{
			return new()
			{
				ARGB = ARGB,
			};
		}

		/// <summary>
		/// Returns a new color with custom values.
		/// </summary>
		/// <param name="R">The red channel of the color.</param>
		/// <param name="G">The green channel of the color.</param>
		/// <param name="B">The blue channel of the color.</param>
		/// <returns>The color as specified by the color channels.</returns>
		public static Color FromRGB(byte R, byte G, byte B)
		{
			return new()
			{
				A = 255,
				R = R,
				G = G,
				B = B,
			};
		}

		/// <summary>
		/// Gets a color from a packed value.
		/// </summary>
		/// <param name="RGB">The packed value to extract a color from.</param>
		/// <returns>A color based on the input packed value.</returns>
		public static Color FromRGB(uint RGB)
		{
			return new()
			{
				A = 255,
				R = (byte)(RGB >> 16),
				G = (byte)(RGB >> 8),
				B = (byte)RGB,
			};
		}

		/// <summary>
		/// Gets a color from a HSL value.
		/// </summary>
		/// <param name="H">The color's hue.</param>
		/// <param name="S">The color's saturation.</param>
		/// <param name="L">The color's lightness.</param>
		/// <returns>A color from the specified HSL value.</returns>
		public static Color FromHSL(float H, float S, float L)
		{
			S = (float)Math.Clamp(S, 0.0, 1.0);
			L = (float)Math.Clamp(L, 0.0, 1.0);

			// Zero-saturation optimization.
			if (S == 0)
			{
				return FromRGB((byte)L, (byte)L, (byte)L);
			}

			float Q = L < 0.5 ? L * S + L : L + S - L * S;
			float P = 2 * L - Q;

			return FromRGB(
			  (byte)FromHue(P, Q, H + (1 / 3)),
			  (byte)FromHue(P, Q, H),
			  (byte)FromHue(P, Q, H - (1 / 3)));
		}

		/// <summary>
		/// Internal method, used by <see cref="FromHSL(float, float, float)"/>./>
		/// See: <seealso cref="https://github.com/CharlesStover/hsl2rgb-js/blob/master/src/hsl2rgb.js"/>
		/// </summary>
		/// <param name="P">Unknown.</param>
		/// <param name="Q">Unknown.</param>
		/// <param name="T">Unknown.</param>
		/// <returns>Unknown.</returns>
		private static float FromHue(float P, float Q, float T)
		{
			if (T < 0)
			{
				T += 1;
			}
			if (T > 1)
			{
				T -= 1;
			}
			if (T < 1 / 6)
			{
				return P + (Q - P) * 6 * T;
			}
			if (T < 0.5)
			{
				return Q;
			}
			if (T < 2 / 3)
			{
				return P + (Q - P) * (2 / 3 - T) * 6;
			}

			return P;
		}

		/// <summary>
		/// Gets a color from an input string. These are the allowed inputs:
		/// <list type="bullet">
		/// cymk(c, y, m k)
		/// argb(a, r, g, b)
		/// argb(packed argb)
		/// rgb(r, g, b)
		/// rgb(packed rgb)
		/// #XXXXXXXX (Hex)
		/// #XXXXXX (Hex)
		/// </list>
		/// </summary>
		/// <param name="Input">The input string to process.</param>
		/// <returns>The color from the name of the input.</returns>
		/// <exception cref="FormatException">Thrown on incorrect format or color name.</exception>
		public static Color FromString(string Input)
		{
			// Check if input is invalid.
			if (string.IsNullOrEmpty(Input))
			{
				return Black;
			}

			// Normalize string.
			Input = Input.ToLower();

			// Remove spaces.
			Input = Input.Replace(" ", string.Empty);

			// Create temporary color value.
			Color Temp = new();

			// Get CYMK color.
			if (Input.StartsWith("cymk("))
			{
				// Get individual components.
				string[] Components = Input[5..].Split(',');

				// Parse component data.
				byte C = byte.Parse(Components[0]);
				byte Y = byte.Parse(Components[1]);
				byte M = byte.Parse(Components[2]);
				byte K = byte.Parse(Components[3]);

				if (K != 255)
				{
					Temp.A = 255;
					Temp.R = (byte)((255 - C) * (255 - K) / 255);
					Temp.G = (byte)((255 - M) * (255 - K) / 255);
					Temp.B = (byte)((255 - Y) * (255 - K) / 255);
				}
				else
				{
					Temp.A = 255;
					Temp.R = (byte)(255 - C);
					Temp.G = (byte)(255 - M);
					Temp.B = (byte)(255 - Y);
				}

				return Temp;
			}

			// Get ARGB color.
			if (Input.StartsWith("argb("))
			{
				// Check if value is packed.
				if (!Input.Contains(','))
				{
					return FromARGB(uint.Parse(Input[5..]));
				}

				// Get individual components.
				string[] Components = Input[5..].Split(',');

				// Parse component data.
				return FromARGB(
					byte.Parse(Components[0]),
					byte.Parse(Components[1]),
					byte.Parse(Components[2]),
					byte.Parse(Components[3]));
			}

			// Get RGB color.
			if (Input.StartsWith("rgb("))
			{
				// Check if value is packed.
				if (!Input.Contains(','))
				{
					return FromRGB(uint.Parse(Input));
				}

				// Get individual components.
				string[] Components = Input[5..].Split(',');

				// Parse component data.
				return FromRGB(
					byte.Parse(Components[0]),
					byte.Parse(Components[1]),
					byte.Parse(Components[2]));
			}

			// Get HSV color.
			if (Input.StartsWith("hsl("))
			{
				// Get individual components.
				string[] Components = Input[5..].Split(',');

				// Return color.
				return FromHSL(float.Parse(Components[0]), float.Parse(Components[1]), float.Parse(Components[2]));
			}

			// Get hex color.
			if (Input.StartsWith('#'))
			{
				// Fix the input.
				Input = Input[1..];

				// Get color with correct hex length.
				switch (Input.Length)
				{
					case 8:
						Temp.A = byte.Parse(Input[0..2], NumberStyles.HexNumber);
						Temp.R = byte.Parse(Input[2..4], NumberStyles.HexNumber);
						Temp.G = byte.Parse(Input[4..6], NumberStyles.HexNumber);
						Temp.B = byte.Parse(Input[6..8], NumberStyles.HexNumber);
						break;
					case 6:
						Temp.A = 255;
						Temp.R = byte.Parse(Input[0..2], NumberStyles.HexNumber);
						Temp.G = byte.Parse(Input[2..4], NumberStyles.HexNumber);
						Temp.B = byte.Parse(Input[4..6], NumberStyles.HexNumber);
						break;
					default:
						throw new FormatException("Hex value is not in correct format!");
				}

				return Temp;
			}

			// Check if color cache has the requested color name.
			if (Cache.ContainsKey(Input))
			{
				return Cache[Input];
			}

			#region Color Names

			// Assume input is a color name.
			Temp = Input switch
			{
				"AliceBlue" => 0xFFF0F8FF,
				"AntiqueWhite" => 0xFFFAEBD7,
				"Aqua" => 0xFF00FFFF,
				"Aquamarine" => 0xFF7FFFD4,
				"Azure" => 0xFFF0FFFF,
				"Beige" => 0xFFF5F5DC,
				"Bisque" => 0xFFFFE4C4,
				"Black" => 0xFF000000,
				"BlanchedAlmond" => 0xFFFFEBCD,
				"Blue" => 0xFF0000FF,
				"BlueViolet" => 0xFF8A2BE2,
				"Brown" => 0xFFA52A2A,
				"BurlyWood" => 0xFFDEB887,
				"CadetBlue" => 0xFF5F9EA0,
				"Chartreuse" => 0xFF7FFF00,
				"Chocolate" => 0xFFD2691E,
				"Coral" => 0xFFFF7F50,
				"CornflowerBlue" => 0xFF6495ED,
				"Cornsilk" => 0xFFFFF8DC,
				"Crimson" => 0xFFDC143C,
				"Cyan" => 0xFF00FFFF,
				"DarkBlue" => 0xFF00008B,
				"DarkCyan" => 0xFF008B8B,
				"DarkGoldenRod" => 0xFFB8860B,
				"DarkGray" => 0xFFA9A9A9,
				"DarkGrey" => 0xFFA9A9A9,
				"DarkGreen" => 0xFF006400,
				"DarkKhaki" => 0xFFBDB76B,
				"DarkMagenta" => 0xFF8B008B,
				"DarkOliveGreen" => 0xFF556B2F,
				"DarkOrange" => 0xFFFF8C00,
				"DarkOrchid" => 0xFF9932CC,
				"DarkRed" => 0xFF8B0000,
				"DarkSalmon" => 0xFFE9967A,
				"DarkSeaGreen" => 0xFF8FBC8F,
				"DarkSlateBlue" => 0xFF483D8B,
				"DarkSlateGray" => 0xFF2F4F4F,
				"DarkSlateGrey" => 0xFF2F4F4F,
				"DarkTurquoise" => 0xFF00CED1,
				"DarkViolet" => 0xFF9400D3,
				"DeepPink" => 0xFFFF1493,
				"DeepSkyBlue" => 0xFF00BFFF,
				"DimGray" => 0xFF696969,
				"DimGrey" => 0xFF696969,
				"DodgerBlue" => 0xFF1E90FF,
				"FireBrick" => 0xFFB22222,
				"FloralWhite" => 0xFFFFFAF0,
				"ForestGreen" => 0xFF228B22,
				"Fuchsia" => 0xFFFF00FF,
				"Gainsboro" => 0xFFDCDCDC,
				"GhostWhite" => 0xFFF8F8FF,
				"Gold" => 0xFFFFD700,
				"GoldenRod" => 0xFFDAA520,
				"Gray" => 0xFF808080,
				"Grey" => 0xFF808080,
				"Green" => 0xFF008000,
				"GreenYellow" => 0xFFADFF2F,
				"HoneyDew" => 0xFFF0FFF0,
				"HotPink" => 0xFFFF69B4,
				"IndianRed" => 0xFFCD5C5C,
				"Indigo" => 0xFF4B0082,
				"Ivory" => 0xFFFFFFF0,
				"Khaki" => 0xFFF0E68C,
				"Lavender" => 0xFFE6E6FA,
				"LavenderBlush" => 0xFFFFF0F5,
				"LawnGreen" => 0xFF7CFC00,
				"LemonChiffon" => 0xFFFFFACD,
				"LightBlue" => 0xFFADD8E6,
				"LightCoral" => 0xFFF08080,
				"LightCyan" => 0xFFE0FFFF,
				"LightGoldenRodYellow" => 0xFFFAFAD2,
				"LightGray" => 0xFFD3D3D3,
				"LightGrey" => 0xFFD3D3D3,
				"LightGreen" => 0xFF90EE90,
				"LightPink" => 0xFFFFB6C1,
				"LightSalmon" => 0xFFFFA07A,
				"LightSeaGreen" => 0xFF20B2AA,
				"LightSkyBlue" => 0xFF87CEFA,
				"LightSlateGray" => 0xFF778899,
				"LightSlateGrey" => 0xFF778899,
				"LightSteelBlue" => 0xFFB0C4DE,
				"LightYellow" => 0xFFFFFFE0,
				"Lime" => 0xFF00FF00,
				"LimeGreen" => 0xFF32CD32,
				"Linen" => 0xFFFAF0E6,
				"Magenta" => 0xFFFF00FF,
				"Maroon" => 0xFF800000,
				"MediumAquaMarine" => 0xFF66CDAA,
				"MediumBlue" => 0xFF0000CD,
				"MediumOrchid" => 0xFFBA55D3,
				"MediumPurple" => 0xFF9370DB,
				"MediumSeaGreen" => 0xFF3CB371,
				"MediumSlateBlue" => 0xFF7B68EE,
				"MediumSpringGreen" => 0xFF00FA9A,
				"MediumTurquoise" => 0xFF48D1CC,
				"MediumVioletRed" => 0xFFC71585,
				"MidnightBlue" => 0xFF191970,
				"MintCream" => 0xFFF5FFFA,
				"MistyRose" => 0xFFFFE4E1,
				"Moccasin" => 0xFFFFE4B5,
				"NavajoWhite" => 0xFFFFDEAD,
				"Navy" => 0xFF000080,
				"OldLace" => 0xFFFDF5E6,
				"Olive" => 0xFF808000,
				"OliveDrab" => 0xFF6B8E23,
				"Orange" => 0xFFFFA500,
				"OrangeRed" => 0xFFFF4500,
				"Orchid" => 0xFFDA70D6,
				"PaleGoldenRod" => 0xFFEEE8AA,
				"PaleGreen" => 0xFF98FB98,
				"PaleTurquoise" => 0xFFAFEEEE,
				"PaleVioletRed" => 0xFFDB7093,
				"PapayaWhip" => 0xFFFFEFD5,
				"PeachPuff" => 0xFFFFDAB9,
				"Peru" => 0xFFCD853F,
				"Pink" => 0xFFFFC0CB,
				"Plum" => 0xFFDDA0DD,
				"PowderBlue" => 0xFFB0E0E6,
				"Purple" => 0xFF800080,
				"RebeccaPurple" => 0xFF663399,
				"Red" => 0xFFFF0000,
				"RosyBrown" => 0xFFBC8F8F,
				"RoyalBlue" => 0xFF4169E1,
				"SaddleBrown" => 0xFF8B4513,
				"Salmon" => 0xFFFA8072,
				"SandyBrown" => 0xFFF4A460,
				"SeaGreen" => 0xFF2E8B57,
				"SeaShell" => 0xFFFFF5EE,
				"Sienna" => 0xFFA0522D,
				"Silver" => 0xFFC0C0C0,
				"SkyBlue" => 0xFF87CEEB,
				"SlateBlue" => 0xFF6A5ACD,
				"SlateGray" => 0xFF708090,
				"SlateGrey" => 0xFF708090,
				"Snow" => 0xFFFFFAFA,
				"SpringGreen" => 0xFF00FF7F,
				"SteelBlue" => 0xFF4682B4,
				"Tan" => 0xFFD2B48C,
				"Teal" => 0xFF008080,
				"Thistle" => 0xFFD8BFD8,
				"Tomato" => 0xFFFF6347,
				"Turquoise" => 0xFF40E0D0,
				"Violet" => 0xFFEE82EE,
				"Wheat" => 0xFFF5DEB3,
				"White" => 0xFFFFFFFF,
				"WhiteSmoke" => 0xFFF5F5F5,
				"Yellow" => 0xFFFFFF00,
				"YellowGreen" => 0xFF9ACD32,
				_ => throw new($"Color '{Input}' does not exist!"),
			};

			Cache.Add(Input, Temp);
			return Temp;

			#endregion
		}

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
		public static readonly Color LightPurple = 0xFFA0A5DD;
		public static readonly Color Minty = 0xFF74C68B;
		public static readonly Color SunsetRed = 0xFFE07572;
		public static readonly Color LightYellow = 0xFFF9C980;

		#endregion

		/// <summary>
		/// Color cache, used for caching color values.
		/// </summary>
		private readonly static Dictionary<string, Color> Cache = new();

		private uint _ARGB;
		private byte _A;
		private byte _R;
		private byte _G;
		private byte _B;

		#endregion

		public override int GetHashCode()
		{
			return A * R / G * B;
		}

		public override string ToString()
		{
			return $"{typeof(Color).FullName} [A: {A}, R: {R}, G: {G}, B: {B}]";
		}
	}
}