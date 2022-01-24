using System;
using static PrismOS.Hexi.Runtime;
using static PrismOS.API.KernelAPI;

namespace PrismOS.Hexi
{
	internal class Executable
	{
		public Executable(byte[] Bytes)
		{
			Id = new Random().Next();

			ByteCode = Bytes;
		}

		private readonly byte[] ByteCode;

		public int Index = 0, Id;
		public byte[] Memory;

		public void Tick()
		{
			if (Index >= ByteCode.Length)
			{
				KernelStopProgram(this, null);
				return;
			}

			var func = (Funcs)ByteCode[Index++];

			foreach (var f in Functions)
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
