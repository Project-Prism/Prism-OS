namespace PrismRuntime
{
	public static class UserEnviroment
	{
		/// <summary>
		/// Initialize the system enviroment.
		/// </summary>
		public static void Init()
		{
			if (!Directory.Exists("0:\\System\\"))
			{
				Directory.CreateDirectory("0:\\System\\");
			}
			if (!Directory.Exists("0:\\Users\\"))
			{
				Directory.CreateDirectory("0:\\Users\\");
			}
		}
	}
}