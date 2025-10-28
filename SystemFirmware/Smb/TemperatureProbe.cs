using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Temperature Probe (Type 28)</summary>
	public class TemperatureProbe : TypeBaseT<SmBios.Type28>
	{
		/// <summary>The temperature probe description</summary>
		public String Description => this.GetString(this.Type.Description);

		internal TemperatureProbe(SmBios.Type28 type28)
			: base(type28.Header, type28)
		{ }
	}
}