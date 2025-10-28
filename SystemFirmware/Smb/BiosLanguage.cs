using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>BIOS Language Information (Type 13)</summary>
	public class BiosLanguage : TypeBaseT<SmBios.Type13>
	{
		/// <summary>Current language</summary>
		public String CurrentLanguage => this.GetString(this.Type.CurrentLanguage);

		/// <summary>All languages</summary>
		public String[] Languages => this.Strings;

		internal BiosLanguage(SmBios.Type13 type13)
			: base(type13.Header, type13)
		{ }
	}
}