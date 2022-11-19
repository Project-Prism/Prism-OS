using PrismRuntime.GLang.Structure;
using PrismGL2D;

namespace PrismRuntime.GLang
{
	public unsafe class Executable
	{
		public Executable(byte[] Binary)
		{
			Reader = new(new MemoryStream(Binary));
			Canvas = new(0, 0);
			IsEnabled = true;
		}

		#region Methods

		public void Next()
		{
			if (Reader.BaseStream.Position == Reader.BaseStream.Length)
			{
				IsEnabled = false;
			}
			if (!IsEnabled)
			{
				return;
			}

			uint X;
			uint Y;
			uint Width;
			uint Height;
			uint Radius;
			uint Color;

			switch ((OPCode)Reader.ReadByte())
			{
				case OPCode.SetMode:
					Canvas.Width = Reader.ReadUInt32();
					Canvas.Height = Reader.ReadUInt32();
					break;
				case OPCode.Clear:
					Canvas.Clear(Reader.ReadUInt32());
					break;
				case OPCode.Draw:
					switch ((DrawCode)Reader.ReadByte())
					{
						#region Rectangle
						case DrawCode.FilledRectangle:
							X = Reader.ReadUInt32();
							Y = Reader.ReadUInt32();
							Width = Reader.ReadUInt32();
							Height = Reader.ReadUInt32();
							Color = Reader.ReadUInt32();
							Canvas.DrawFilledRectangle((int)X, (int)Y, Width, Height, 0, Color);
							break;

						case DrawCode.Rectangle:
							X = Reader.ReadUInt32();
							Y = Reader.ReadUInt32();
							Width = Reader.ReadUInt32();
							Height = Reader.ReadUInt32();
							Color = Reader.ReadUInt32();
							Canvas.DrawRectangle((int)X, (int)Y, Width, Height, 0, Color);
							break;
						#endregion

						#region Triangle
						case DrawCode.FilledTriangle:
							break;

						case DrawCode.Triangle:
							break;
						#endregion

						#region Circle
						case DrawCode.FilledCircle:
							X = Reader.ReadUInt32();
							Y = Reader.ReadUInt32();
							Radius = Reader.ReadUInt32();
							Color = Reader.ReadUInt32();
							Canvas.DrawFilledCircle((int)X, (int)Y, Radius, Color);
							break;

						case DrawCode.Circle:
							X = Reader.ReadUInt32();
							Y = Reader.ReadUInt32();
							Radius = Reader.ReadUInt32();
							Color = Reader.ReadUInt32();
							Canvas.DrawCircle((int)X, (int)Y, Radius, Color);
							break;
						#endregion

						#region Misc

						case DrawCode.Pixel:
							X = Reader.ReadUInt32();
							Y = Reader.ReadUInt32();
							Color = Reader.ReadUInt32();
							Canvas[X, Y] = Color;
							break;

						case DrawCode.Line:
							break;

						#endregion

						default:
							throw new("Unsupported OPCode.");
					}
					break;
				case OPCode.Exit:
					IsEnabled = false;
					break;
			}
		}

		#endregion

		#region Fields

		public BinaryReader Reader;
		public Graphics Canvas;
		public bool IsEnabled;

		#endregion
	}
}