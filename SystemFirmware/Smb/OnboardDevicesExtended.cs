using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Onboard Devices Extended Information (Type 41)</summary>
	public class OnboardDevicesExtended : TypeBaseT<SmBios.Type41>
	{
		/// <summary>Reference Designation</summary>
		public String ReferenceDesignation => base.GetString(base.Type.ReferenceDesignation);

		internal OnboardDevicesExtended(SmBios.Type41 type41)
			: base(type41.Header, type41)
		{ }
	}
}