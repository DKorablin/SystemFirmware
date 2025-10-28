using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Management Device Component (Type 35) </summary>
	public class ManagementDeviceComponent : TypeBaseT<SmBios.Type35>
	{
		/// <summary>Description</summary>
		public String Description => this.GetString(this.Type.Description);

		internal ManagementDeviceComponent(SmBios.Type35 type35)
			: base(type35.Header, type35)
		{ }
	}
}