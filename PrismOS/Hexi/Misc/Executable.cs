using PrismOS.Hexi.API;

using static PrismOS.Hexi.Main.Runtime;

namespace PrismOS.Hexi.Misc
{
	public class Executable
	{
		public Executable(byte[] Bytes)
		{
			ID = Executables.Count + 1;
			ByteCode = Bytes;
		}

		private readonly byte[] ByteCode;

		public int Index = 0, ID;
		public byte[] Memory;
		public UI.Elements.Window AppWindow;

		public void Tick()
		{
			if (Index >= ByteCode.Length)
			{
				ProgramAPI.StopProgram(this, null);
				return;
			}
			if (AppWindow != null)
            {
				AppWindow.Draw();
            }

			var func = ByteCode[Index++];

			foreach (var f in Function.Functions)
            {
                if (f.Type == func)
				{
					// Construct arguments, if any
					byte[] args = null;
					if (f.Arguments > 0)
					{
						args = new byte[f.Arguments];

						for (var i = 0; i < f.Arguments; i++)
							args[i] = ByteCode[Index++];
					}

					// Invoke function
					f.Definition(this, args);
					break;
				}
            }
        }
	}
}