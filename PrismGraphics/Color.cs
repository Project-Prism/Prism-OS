using System.Globalization;

namespace PrismGraphics;

/// <summary>
/// Color class, used for drawing.
/// </summary>
public struct Color
{
	/// <summary>
	/// Creates a new instance of the <see cref="Color"/> class with 4 channels specified.
	/// </summary>
	/// <param name="A">The Alpha channel.</param>
	/// <param name="R">The Red channel.</param>
	/// <param name="G">The Green channel.</param>
	/// <param name="B">The Blue channel.</param>
	public Color(float A, float R, float G, float B)
	{
		_ARGB = GetPacked(A, R, G, B);
		_A = A;
		_R = R;
		_G = G;
		_B = B;
	}

	/// <summary>
	/// Creates a new instance of the <see cref="Color"/> class with 3 channels specified.
	/// </summary>
	/// <param name="R">The Red channel.</param>
	/// <param name="G">The Green channel.</param>
	/// <param name="B">The Blue channel.</param>
	public Color(float R, float G, float B)
	{
		_ARGB = GetPacked(255, R, G, B);
		_A = 255;
		_R = R;
		_G = G;
		_B = B;
	}

	/// <summary>
	/// Creates a new instance of the <see cref="Color"/> class using an input string.
	/// <list type="table">
	/// <item>cymk(float, float, float, float)</item>
	/// <item>argb(float, float, float, float)</item>
	/// <item>argb(byte, byte, byte, byte)</item>
	/// <item>argb(uint)</item>
	/// <item>rgb(float, float, float)</item>
	/// <item>rgb(byte, byte, byte)</item>
	/// <item>hsl(float, float, float)</item>
	/// <item>#XXXXXXXX</item>
	/// <item>#XXXXXX</item>
	/// <item>Web color name</item>
	/// </list>
	/// </summary>
	/// <param name="ColorInfo">The string to read.</param>
	public Color(string ColorInfo)
	{
		// Initialize the values.
		_ARGB = 0;
		_A = 0;
		_R = 0;
		_G = 0;
		_B = 0;

		// Check if input is invalid.
		if (string.IsNullOrEmpty(ColorInfo))
		{
			return;
		}

		// Get CYMK color.
		if (ColorInfo.StartsWith("cymk("))
		{
			// Get individual components.
			string[] Components = ColorInfo[5..].Split(',');

			// Parse component data.
			byte C = byte.Parse(Components[0]);
			byte Y = byte.Parse(Components[1]);
			byte M = byte.Parse(Components[2]);
			byte K = byte.Parse(Components[3]);

			if (K != 255)
			{
				_A = 255;
				_R = (255 - C) * (255 - K) / 255;
				_G = (255 - M) * (255 - K) / 255;
				_B = (255 - Y) * (255 - K) / 255;
			}
			else
			{
				_A = 255;
				_R = 255 - C;
				_G = 255 - M;
				_B = 255 - Y;
			}

			// Assign the ARGB value.
			_ARGB = GetPacked(_A, _R, _G, _B);

			return;
		}

		// Get ARGB color.
		if (ColorInfo.StartsWith("argb("))
		{
			// Check if value is packed.
			if (!ColorInfo.Contains(','))
			{
				ARGB = uint.Parse(ColorInfo[5..]);
				return;
			}

			// Get individual components.
			string[] Components = ColorInfo[5..].Split(',');

			// Parse component data.
			try
			{
				_A = byte.Parse(Components[0]);
				_R = byte.Parse(Components[1]);
				_G = byte.Parse(Components[2]);
				_B = byte.Parse(Components[3]);
			}
			catch
			{
				_A = float.Parse(Components[0]);
				_R = float.Parse(Components[1]);
				_G = float.Parse(Components[2]);
				_B = float.Parse(Components[3]);
			}

			// Assign the ARGB value.
			_ARGB = GetPacked(_A, _R, _G, _B);

			return;
		}

		// Get RGB color.
		if (ColorInfo.StartsWith("rgb("))
		{
			// Get individual components.
			string[] Components = ColorInfo[5..].Split(',');

			// Parse component data.
			try
			{
				_A = 255;
				_R = byte.Parse(Components[0]);
				_G = byte.Parse(Components[1]);
				_B = byte.Parse(Components[2]);
			}
			catch
			{
				_A = 255;
				_R = float.Parse(Components[0]);
				_G = float.Parse(Components[1]);
				_B = float.Parse(Components[2]);
			}

			// Assign the ARGB value.
			_ARGB = GetPacked(_A, _R, _G, _B);

			return;
		}

		// Get HSV color.
		if (ColorInfo.StartsWith("hsl("))
		{
			// Alpha is always 100% with HSL.
			_A = 255;

			// Get individual components.
			string[] Components = ColorInfo[5..].Split(',');

			float H = float.Parse(Components[0]);
			float S = float.Parse(Components[1]);
			float L = float.Parse(Components[2]);

			S = (float)Math.Clamp(S, 0.0, 1.0);
			L = (float)Math.Clamp(L, 0.0, 1.0);

			// Zero-saturation optimization.
			if (S == 0)
			{
				_R = L;
				_G = L;
				_B = L;

				_ARGB = GetPacked(_A, _R, _G, _B);
				return;
			}

			float Q = L < 0.5 ? L * S + L : L + S - L * S;
			float P = 2 * L - Q;

			_R = FromHue(P, Q, H + (1 / 3));
			_G = FromHue(P, Q, H);
			_B = FromHue(P, Q, H - (1 / 3));

			// Assign the ARGB value.
			_ARGB = GetPacked(_A, _R, _G, _B);

			return;
		}

		// Get hex color.
		if (ColorInfo.StartsWith('#'))
		{
			// Get color with correct hex length.
			switch (ColorInfo.Length)
			{
				case 9:
					_A = byte.Parse(ColorInfo[1..3], NumberStyles.HexNumber);
					_R = byte.Parse(ColorInfo[3..5], NumberStyles.HexNumber);
					_G = byte.Parse(ColorInfo[5..7], NumberStyles.HexNumber);
					_B = byte.Parse(ColorInfo[7..9], NumberStyles.HexNumber);
					break;
				case 7:
					_A = 255;
					_R = byte.Parse(ColorInfo[1..3], NumberStyles.HexNumber);
					_G = byte.Parse(ColorInfo[3..5], NumberStyles.HexNumber);
					_B = byte.Parse(ColorInfo[5..7], NumberStyles.HexNumber);
					break;
				default:
					throw new FormatException("Hex value is not in correct format!");
			}

			// Assign the ARGB value.
			_ARGB = GetPacked(_A, _R, _G, _B);

			return;
		}

		#region Color Names

		// Assume input is a color name.
		ARGB = ColorInfo switch
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
			_ => throw new($"Color '{ColorInfo}' does not exist!"),
		};

		#endregion

		return;
	}

	/// <summary>
	/// Creates a new instance of the <see cref="Color"/> class.
	/// </summary>
	/// <param name="ARGB">A 32-bit packed ARGB value.</param>
	public Color(uint ARGB)
	{
		// Initialize values.
		_ARGB = 0;
		_A = 0;
		_R = 0;
		_G = 0;
		_B = 0;

		this.ARGB = ARGB;
	}

	#region Properties

	/// <summary>
	/// Property used to get the overall brightness of the color.
	/// </summary>
	public float Brightness
	{
		get
		{
			return (Max(this) + Min(this)) / (byte.MaxValue * 2f);
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
	public float A
	{
		get
		{
			return _A;
		}
		set
		{
			_A = value;
			_ARGB = GetPacked(_A, _R, _G, _B);
		}
	}

	/// <summary>
	/// Red channel of the color.
	/// </summary>
	public float R
	{
		get
		{
			return _R;
		}
		set
		{
			_R = value;
			_ARGB = GetPacked(_A, _R, _G, _B);
		}
	}

	/// <summary>
	/// Green channel of the color.
	/// </summary>
	public float G
	{
		get
		{
			return _G;
		}
		set
		{
			_G = value;
			_ARGB = GetPacked(_A, _R, _G, _B);
		}
	}

	/// <summary>
	/// Blue channel of the color.
	/// </summary>
	public float B
	{
		get
		{
			return _B;
		}
		set
		{
			_B = value;
			_ARGB = GetPacked(_A, _R, _G, _B);
		}
	}

	#endregion

	#region Operators

	public static implicit operator Color((float, float, float, float) ARGB)
	{
		return new(ARGB.Item1, ARGB.Item2, ARGB.Item3, ARGB.Item4);
	}
	public static implicit operator Color((float, float, float) RGB)
	{
		return new(255f, RGB.Item1, RGB.Item2, RGB.Item3);
	}
	public static implicit operator Color(string Color)
	{
		return new(Color);
	}
	public static implicit operator Color(uint ARGB)
	{
		return new(ARGB);
	}

	public static Color operator +(Color Original, Color ToAdd)
	{
		return new()
		{
			A = Original.A,
			R = Original.R + ToAdd.R,
			G = Original.G + ToAdd.G,
			B = Original.B + ToAdd.B,
		};
	}
	public static Color operator -(Color Original, Color ToSubtract)
	{
		return new()
		{
			A = Original.A,
			R = Original.R - ToSubtract.R,
			G = Original.G - ToSubtract.G,
			B = Original.B - ToSubtract.B,
		};
	}
	public static Color operator *(Color Original, Color ToMultiply)
	{
		return new()
		{
			A = Original.A,
			R = Original.R * ToMultiply.R,
			G = Original.G * ToMultiply.G,
			B = Original.B * ToMultiply.B,
		};
	}

	public static Color operator -(Color Original, float Value)
	{
		return new()
		{
			A = Original.A,
			R = Original.R - Value,
			G = Original.G - Value,
			B = Original.B - Value,
		};
	}
	public static Color operator /(Color Original, float Value)
	{
		return new()
		{
			A = Original.A,
			R = Original.R / Value,
			G = Original.G / Value,
			B = Original.B / Value,
		};
	}
	public static Color operator *(Color Original, float Value)
	{
		return new()
		{
			A = Original.A,
			R = Original.R * Value,
			G = Original.G * Value,
			B = Original.B * Value,
		};
	}

	public static Color operator -(Color Original, long Value)
	{
		return new()
		{
			A = Original.A,
			R = Original.R - Value,
			G = Original.G - Value,
			B = Original.B - Value,
		};
	}
	public static Color operator /(Color Original, long Value)
	{
		return new()
		{
			A = Original.A,
			R = Original.R / Value,
			G = Original.G / Value,
			B = Original.B / Value,
		};
	}
	public static Color operator *(Color Original, long Value)
	{
		return new()
		{
			A = Original.A,
			R = Original.R * Value,
			G = Original.G * Value,
			B = Original.B * Value,
		};
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

		return new(
			(byte)((Source.A * (255f - NewColor.A) / 255f) + NewColor.A),
			(byte)((Source.R * (255f - NewColor.A) / 255f) + NewColor.R),
			(byte)((Source.G * (255f - NewColor.A) / 255f) + NewColor.G),
			(byte)((Source.B * (255f - NewColor.A) / 255f) + NewColor.B));
	}

	/// <summary>
	/// Converts an ARGB color to it's packed ARGB format.
	/// </summary>
	/// <param name="A">Alpha channel.</param>
	/// <param name="R">Red channel.</param>
	/// <param name="G">Green channel.</param>
	/// <param name="B">Blue channel.</param>
	/// <returns>Packed value.</returns>
	public static uint GetPacked(float A, float R, float G, float B)
	{
		return (uint)((byte)A << 24 | (byte)R << 16 | (byte)G << 8 | (byte)B);
	}

	/// <summary>
	/// Normalizes the color to be between 0.0 and 1.0.
	/// </summary>
	/// <returns>A normalized color.</returns>
	public static Color Normalize(Color ToNormalize)
	{
		return ToNormalize / 255;
	}

	/// <summary>
	/// Inverts the specified color.
	/// </summary>
	/// <param name="ToInvert">The color that will be inverted.</param>
	/// <returns>An inverted variant of the input.</returns>
	public static Color Invert(Color ToInvert)
	{
		return White - ToInvert;
	}

	/// <summary>
	/// Gets the value of the channel with the most value.
	/// </summary>
	/// <param name="Color">The color to calculate.</param>
	/// <returns><see cref="R"/> if <see cref="R"/> is more than <see cref="G"/> and <see cref="B"/>, etc...</returns>
	public static float Max(Color Color)
	{
		// Get the minimum value of each channel.
		return MathF.Max(Color.R, MathF.Max(Color.G, Color.B));
	}

	/// <summary>
	/// Gets the value of the channel with the least value.
	/// </summary>
	/// <param name="Color">The color to calculate.</param>
	/// <returns><see cref="R"/> if <see cref="R"/> is less than <see cref="G"/> and <see cref="B"/>, etc...</returns>
	public static float Min(Color Color)
	{
		// Get the minimum value of each channel.
		return MathF.Min(Color.R, MathF.Min(Color.G, Color.B));
	}

	/// <summary>
	/// Converts the color to be only in grayscale.
	/// </summary>
	/// <param name="UseAlpha">Allow alpha when converting.</param>
	/// <returns>Grayscale color.</returns>
	public Color ToGrayscale(bool UseAlpha)
	{
		float Average = (R + G + B) / 3f;

		return new(UseAlpha ? Average : A, Average, Average, Average);
	}

	public override int GetHashCode()
	{
		return (int)(A * R / G * B);
	}

	public override string ToString()
	{
		return $"{typeof(Color).FullName} [A: {A}, R: {R}, G: {G}, B: {B}]";
	}

	#endregion

	#region Fields

	#region Extended Colors

	public static readonly Color White = new(255, 255, 255, 255);
	public static readonly Color Black = new(255, 0, 0, 0);
	public static readonly Color Cyan = new(255, 0, 255, 255);
	public static readonly Color Red = new(255, 255, 0, 0);
	public static readonly Color Green = new(255, 0, 255, 0);
	public static readonly Color Blue = new(255, 0, 0, 255);
	public static readonly Color CoolGreen = new(255, 54, 94, 53);
	public static readonly Color Magenta = new(255, 255, 0, 255);
	public static readonly Color Yellow = new(255, 255, 255, 0);
	public static readonly Color HotPink = new(255, 230, 62, 109);
	public static readonly Color UbuntuPurple = new(255, 66, 5, 22);
	public static readonly Color GoogleBlue = new(255, 66, 133, 244);
	public static readonly Color GoogleGreen = new(255, 52, 168, 83);
	public static readonly Color GoogleYellow = new(255, 251, 188, 5);
	public static readonly Color GoogleRed = new(255, 234, 67, 53);
	public static readonly Color DeepOrange = new(255, 255, 64, 0);
	public static readonly Color RubyRed = new(255, 204, 52, 45);
	public static readonly Color Transparent = new(0, 0, 0, 0);
	public static readonly Color StackOverflowOrange = new(255, 244, 128, 36);
	public static readonly Color StackOverflowBlack = new(255, 34, 36, 38);
	public static readonly Color StackOverflowWhite = new(255, 188, 187, 187);
	public static readonly Color DeepGray = new(255, 25, 25, 25);
	public static readonly Color LightGray = new(255, 125, 125, 125);
	public static readonly Color SuperOrange = new(255, 255, 99, 71);
	public static readonly Color FakeGrassGreen = new(255, 60, 179, 113);
	public static readonly Color DeepBlue = new(255, 51, 47, 208);
	public static readonly Color BloodOrange = new(255, 255, 123, 0);
	public static readonly Color LightBlack = new(255, 25, 25, 25);
	public static readonly Color LighterBlack = new(255, 50, 50, 50);
	public static readonly Color ClassicBlue = new(255, 52, 86, 139);
	public static readonly Color LivingCoral = new(255, 255, 111, 97);
	public static readonly Color UltraViolet = new(255, 107, 91, 149);
	public static readonly Color Greenery = new(255, 136, 176, 75);
	public static readonly Color Emerald = new(255, 0, 155, 119);
	public static readonly Color LightPurple = 0xFFA0A5DD;
	public static readonly Color Minty = 0xFF74C68B;
	public static readonly Color SunsetRed = 0xFFE07572;
	public static readonly Color LightYellow = 0xFFF9C980;

	#endregion

	private uint _ARGB;
	private float _A;
	private float _R;
	private float _G;
	private float _B;

	#endregion
}