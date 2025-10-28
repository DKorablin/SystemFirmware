using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>System Configuration Options (Type 12)</summary>
	public class SystemConfigurationOptions : TypeBaseT<SmBios.Type12>
	{
		/// <summary>The system configuration options information</summary>
		public String[] Options => this.Strings;

		internal SystemConfigurationOptions(SmBios.Type12 type12)
			: base(type12.Header, type12)
		{ }
	}
}