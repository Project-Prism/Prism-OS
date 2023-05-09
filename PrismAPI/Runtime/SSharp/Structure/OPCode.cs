namespace PrismAPI.Runtime.SSharp.Structure;

public enum OPCode
{
	#region Console

	System_Console_WriteLine,
	System_Console_Write,

	#endregion

	#region Runtime

	System_Runtime_ThrowException,
	System_Runtime_Exit,

	#endregion

	#region Inline

	System_Inline_Jump,

	#endregion
}