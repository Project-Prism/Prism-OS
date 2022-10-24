namespace PrismBinary.Text.XML
{
	public unsafe class XMLNode : List<XMLNode>
	{
		public XMLNode(string Source, ref int I)
		{
			Attribs = new();
			TagName = "";

			for (; I < Source.Length; I++)
			{
				if (Source[I] == '<')
				{
					while (Source[I] != '>')
					{
						TagName += Source[I++];
					}

					while (I < Source.Length)
					{
						if (Source[I] == '<' && Source[I] != '/')
						{
							I++;
							Add(new(Source, ref I));
							continue;
						}
						if (Source[I] == '<' && Source[I] == '/')
						{
							I += TagName.Length + 2;
							continue;
						}

						while (Source[I] != '<')
						{
							SetAttribute("Value", GetAttribute("Value") + Source[I++]);
						}
					}
				}
			}
		}
		public XMLNode()
		{
			Attribs = new();
			TagName = "";
		}

		#region Methods

		public void SetAttribute(string Attribute, string Value)
		{
			if (!Attribs.ContainsKey(Attribute))
			{
				Attribs.Add(Attribute, Value);
				return;
			}

			Attribs[Attribute] = Value;
		}
		public string GetAttribute(string Attribute)
		{
			if (!Attribs.ContainsKey(Attribute))
			{
				return string.Empty;
			}

			return Attribs[Attribute];
		}
		public void SetTagName(string Value)
		{
			TagName = Value;
		}
		public string GetTagName()
		{
			return TagName;
		}

		#endregion

		#region Fields

		internal Dictionary<string, string> Attribs;
		internal string TagName;

		#endregion
	}
}