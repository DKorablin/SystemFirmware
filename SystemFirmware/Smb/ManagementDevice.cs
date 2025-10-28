using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Management Device (Type 34)</summary>
	public class ManagementDevice : TypeBaseT<SmBios.Type34>
	{
		/// <summary>Description</summary>
		public String Description => this.GetString(this.Type.Description);

		internal ManagementDevice(SmBios.Type34 type34)
			: base(type34.Header, type34)
		{ }
	}
}