using System.Numerics;

namespace PrismOS
{
	public class Lump
	{
		public Lump(string Name)
		{
			this.Name = Name;
		}

		#region Properties

		public bool IsEOF => Reader.BaseStream.Position >= Reader.BaseStream.Length;

		#endregion

		#region Methods

		public Vector2 ReadVertex()
		{
			return new(Reader.ReadInt16(), Reader.ReadInt16());
		}

		public Map GetMap()
		{
			Map M = new();

			while (!IsEOF)
			{
				M.Vertecies.Add(ReadVertex());
			}

			return M;
		}

		#endregion

		#region Fields

		public BinaryReader Reader;
		public string Name;

		#endregion
	}
}