using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Memory Module Information (Type 6, Obsolete) structure</summary>
	public class MemoryModule : TypeBaseT<SmBios.Type6>
	{
		internal MemoryModule(SmBios.Type6 type6)
			: base(type6.Header, type6)
		{ }
	}
}