using PrismRuntime.SShell.Structure;

namespace PrismRuntime.SShell.Scripts
{
	public class Help : Script
	{
		public Help() : base("help", "Lists all commands and their function.") { }

		public override ReturnCode Invoke(string[] Args)
		{
			foreach (Script S in Shell.Scripts)
			{
				Console.WriteLine($"{S.Name} : {S.Description}");
			}

			return ReturnCode.Success;
		}
	}
}