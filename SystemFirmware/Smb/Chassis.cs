using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>System Enclosure or Chassis (Type 3)</summary>
	public class Chassis : TypeBaseT<SmBios.Type3>
	{
		/// <summary>Manufacturer</summary>
		public String Manufacturer => base.GetString(base.Type.Manufacturer);
		/// <summary>Version</summary>
		public String Version => base.GetString(base.Type.Version);
		/// <summary>Serial number</summary>
		public String SerialNumber => base.GetString(base.Type.SerialNumber);
		/// <summary>Asset tag</summary>
		public String AssetTagNumber => base.GetString(base.Type.AssetTagNumber);

		internal Chassis(SmBios.Type3 data)
			: base(data.Header, data)
		{ }
	}
}