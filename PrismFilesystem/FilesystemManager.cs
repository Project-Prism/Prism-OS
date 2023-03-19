using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using PrismTools.IO;

namespace PrismFilesystem
{
	public static class FilesystemManager
	{
		#region Methods

		/// <summary>
		/// Method to initialize all the filesystems for the disks connected to the machine.
		/// </summary>
		public static void Init()
		{
			try
			{
				Debugger.WritePartial("Initializing FS");
				VFSManager.RegisterVFS(new CosmosVFS(), false, false);
				Debugger.Success();
			}
			catch
			{
				Debugger.Fail();
			}
		}

		#endregion

		#region Fields

		public static Debugger Debugger { get; set; } = new("Filesystem");

		#endregion
	}
}