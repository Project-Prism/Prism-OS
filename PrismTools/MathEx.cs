namespace PrismTools
{
	public static class MathEx
	{
		public static string Eval(string Input)
		{
			string Temporary = "";
			string Result = "0";

			for (int I = 0; I < Input.Length; I++)
			{
				if (Input[I] == ' ' || Input[I] == '\n' || Input[I] == '\0')
				{
					continue;
				}
				if (ulong.TryParse(Input[I].ToString(), out _))
				{
					Temporary += Input[I];
					continue;
				}

				for (int I2 = I; long.TryParse(Input[I2].ToString(), out _); I2++)
				{
					Temporary += Input[I2];
					I++;
				}

				switch (Input[I])
				{
					case '*':
						Result = (long.Parse(Result) * long.Parse(Temporary)).ToString();
						break;
					case '/':
						Result = (long.Parse(Result) / long.Parse(Temporary)).ToString();
						break;
					case '+':
						Result = (long.Parse(Result) + long.Parse(Temporary)).ToString();
						break;
					case '-':
						Result = (long.Parse(Result) - long.Parse(Temporary)).ToString();
						break;
					default:
						continue;
				}
			}

			return Result;
		}
	}
}