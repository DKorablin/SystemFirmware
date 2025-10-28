using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Cache Information (Type 7)</summary>
	public class Cache : TypeBaseT<SmBios.Type7>
	{
		/// <summary>Socket designation</summary>
		public String SocketDesignation => this.GetString(this.Type.SocketDesignation);

		internal Cache(SmBios.Type7 type7)
			: base(type7.Header,type7)
		{ }
	}
}