namespace PrismFilesystem.Formats.CSVG.Lexer;

public class Tokenizer
{
	public Tokenizer(string Input)
	{
		this.Input = Input;
		Index = 0;
	}

	#region Properties

	public bool IsEOF => Index >= Input.Length;

	#endregion

	#region Methods

	public Token Next()
	{
		Token Temp = new();

		while (!IsEOF)
		{
			if (!char.IsWhiteSpace(Input[Index]) && char.IsWhiteSpace(Input[Index + 1]))
			{
				Index++;
				return Temp;
			}
			if (char.IsSymbol(Input[Index]) || char.IsPunctuation(Input[Index]))
			{
				Temp.Value += Input[Index++];
				return Temp;
			}
			if (char.IsWhiteSpace(Input[Index]) || char.IsControl(Input[Index]))
			{
				Index++;
				continue;
			}

			Temp.Value += Input[Index++];
		}

		return Temp;
	}

	public Token Peek()
	{
		int Reset = Index;
		Token Temp = Next();
		Index = Reset;
		return Temp;
	}

	#endregion

	#region Fields

	public string Input;
	public int Index;

	#endregion
}