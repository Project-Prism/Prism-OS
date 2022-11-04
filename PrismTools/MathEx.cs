namespace PrismTools
{
	/// <summary>
	/// Prism OS Math extentions class.
	/// </summary>
	public static class MathEx
	{
		/// <summary>
		/// Evaluate a string.
		/// </summary>
		/// <param name="Input">Math operation input.</param>
		/// <returns>Evaluated string.</returns>
		public static string Eval(string Input)
		{ 
			string Temporary = "";
			string Result = "";

			for (int I = 0; I < Input.Length; I++)
			{
				if (Input[I] == ' ' || Input[I] == '\n' || Input[I] == '\0')
				{
					continue;
				}
				if (Input[I] == '.' || ulong.TryParse(Input[I].ToString(), out _))
				{
					Result += Input[I];
					continue;
				}

				switch (Input[I])
				{
					case '*':
						for (int I2 = I; long.TryParse(Input[I2].ToString(), out _); I2++)
						{
							Temporary += Input[I2];
							I++;
						}
						Result = (long.Parse(Result) * long.Parse(Temporary)).ToString();
						break;
					case '/':
						for (int I2 = I; long.TryParse(Input[I2].ToString(), out _); I2++)
						{
							Temporary += Input[I2];
							I++;
						}
						Result = (long.Parse(Result) / long.Parse(Temporary)).ToString();
						break;
					case '+':
						for (int I2 = I; long.TryParse(Input[I2].ToString(), out _); I2++)
						{
							Temporary += Input[I2];
							I++;
						}
						Result = (long.Parse(Result) + long.Parse(Temporary)).ToString();
						break;
					case '-':
						for (int I2 = I; long.TryParse(Input[I2].ToString(), out _); I2++)
						{
							Temporary += Input[I2];
							I++;
						}
						Result = (long.Parse(Result) - long.Parse(Temporary)).ToString();
						break;
					case '^':
						for (int I2 = I; double.TryParse(Input[I2].ToString(), out _); I2++)
						{
							Temporary += Input[I2];
							I++;
						}
						Result = (long.Parse(Result) ^ long.Parse(Temporary)).ToString();
						break;
					case '%':
						for (int I2 = I; double.TryParse(Input[I2].ToString(), out _); I2++)
						{
							Temporary += Input[I2];
							I++;
						}
						Result = (long.Parse(Result) & long.Parse(Temporary)).ToString();
						break;
					default:
						continue;
				}
			}

			return Result;
		}
	}
}