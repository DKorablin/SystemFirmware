using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>TPM Device (Type 43)</summary>
	public class TpmDevice : TypeBaseT<SmBios.Type43>
	{
		/// <summary>Descriptive information of the TPM device</summary>
		public String Description => base.GetString(base.Type.Description);

		internal TpmDevice(SmBios.Type43 type43)
			: base(type43.Header, type43)
		{ }
	}
}