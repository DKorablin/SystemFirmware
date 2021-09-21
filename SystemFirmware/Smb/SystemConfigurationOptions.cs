using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>System Configuration Options (Type 12)</summary>
	public class SystemConfigurationOptions : TypeBaseT<SmBios.Type12>
	{
		/// <summary>Options</summary>
		public String[] Options { get { return base.Strings; } }

		internal SystemConfigurationOptions(SmBios.Type12 type12)
			: base(type12.Header, type12)
		{
		}
	}
}