using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Electrical Current Probe (Type 29)</summary>
	public class ElectricalCurrentProbe : TypeBaseT<SmBios.Type29>
	{
		/// <summary>Description</summary>
		public String Description => this.GetString(this.Type.Description);

		internal ElectricalCurrentProbe(SmBios.Type29 type29)
			: base(type29.Header, type29)
		{ }
	}
}