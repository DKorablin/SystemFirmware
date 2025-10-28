using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Voltage Probe (Type 26)</summary>
	public class VoltageProbe : TypeBaseT<SmBios.Type26>
	{
		/// <summary>The voltage probe description</summary>
		public String Description => this.GetString(this.Type.Description);

		internal VoltageProbe(SmBios.Type26 type26)
			: base(type26.Header, type26)
		{ }
	}
}