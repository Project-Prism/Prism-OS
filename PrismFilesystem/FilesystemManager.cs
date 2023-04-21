using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using PrismTools;

namespace PrismFilesystem;

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
			Debugger.WritePartial("Initializing FS...");
			VFSManager.RegisterVFS(new CosmosVFS(), false, false);
			Debugger.Finalize(Severity.Success);
		}
		catch
		{
			Debugger.Finalize(Severity.Fail);
		}
	}

	#endregion

	#region Fields

	public static Debugger Debugger { get; set; } = new("Filesystem");

	#endregion
}