using PrismBinary.Text.XML;

namespace PrismTools.Tests
{
	public static class XMLTest
	{
		public static bool Run()
		{
			try
			{
				XMLDocument XML = new(
					"<note>\n" +
						"<to>Tove</to>\n" +
						"<from>Jani</from>\n" +
						"<heading>Reminder</heading>\n" +
						"<body>Don't forget me this weekend!</body>\n" +
					"</note>");

				if (XML.GetRootNode().GetTagName() != "note")
				{
					return false;
				}
				if (XML.GetRootNode()[0].GetTagName() != "to")
				{
					return false;
				}
				if (XML.GetRootNode()[1].GetTagName() != "from")
				{
					return false;
				}
				if (XML.GetRootNode()[2].GetTagName() != "heading")
				{
					return false;
				}
				if (XML.GetRootNode()[3].GetTagName() != "body")
				{
					return false;
				}

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}