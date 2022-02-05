using PrismOS.Core.Hexi.API;
using static PrismOS.Core.Hexi.Main.Runtime;

namespace PrismOS.Core.Hexi.Misc
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

		public void Tick()
		{
			if (Index >= ByteCode.Length)
			{
				ProgramAPI.StopProgram(this, null);
				return;
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