using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Memory Controller Information (Type 5, Obsolete)</summary>
	public class MemoryController : TypeBaseT<SmBios.Type5>
	{
		internal MemoryController(SmBios.Type5 type5)
			: base(type5.Header, type5)
		{
		}
	}
}