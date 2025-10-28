using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>System Enclosure or Chassis (Type 3)</summary>
	public class Chassis : TypeBaseT<SmBios.Type3>
	{
		/// <summary>Manufacturer</summary>
		public String Manufacturer => this.GetString(this.Type.Manufacturer);
		/// <summary>Version</summary>
		public String Version => this.GetString(this.Type.Version);
		/// <summary>Serial number</summary>
		public String SerialNumber => this.GetString(this.Type.SerialNumber);
		/// <summary>Asset tag</summary>
		public String AssetTagNumber => this.GetString(this.Type.AssetTagNumber);

		internal Chassis(SmBios.Type3 data)
			: base(data.Header, data)
		{ }
	}
}