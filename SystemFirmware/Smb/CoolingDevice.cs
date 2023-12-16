using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Cooling Device (Type 27)</summary>
	public class CoolingDevice : TypeBaseT<SmBios.Type27>
	{
		/// <summary>Description</summary>
		public String Description => base.GetString(base.Type.Description);

		internal CoolingDevice(SmBios.Type27 type27)
			: base(type27.Header, type27)
		{ }
	}
}