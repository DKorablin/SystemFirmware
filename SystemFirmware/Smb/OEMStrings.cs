using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>OEM Strings (Type 11)</summary>
	public class OEMStrings : TypeBaseT<SmBios.Type11>
	{
		/// <summary>OEM Strings</summary>
		public String[] OEM => this.Strings;

		internal OEMStrings(SmBios.Type11 type11)
			: base(type11.Header, type11)
		{ }
	}
}