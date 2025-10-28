using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Baseboard (or Module) Information (Type 2)</summary>
	public class Baseboard : TypeBaseT<SmBios.Type2>
	{
		/// <summary>Manufacturer</summary>
		public String Manufacturer => this.GetString(this.Type.Manufacturer);
		/// <summary>Product</summary>
		public String Product => this.GetString(this.Type.Product);
		/// <summary>Version</summary>
		public String Version => this.GetString(this.Type.Version);
		/// <summary>Serial number</summary>
		public String SerialNumber => this.GetString(this.Type.SerialNumber);
		/// <summary>Asset tag</summary>
		public String AssetTag => this.GetString(this.Type.AssetTag);
		/// <summary>Location in Chassis</summary>
		public String LocationInChassis => this.GetString(this.Type.LocationInChassis);

		internal Baseboard(SmBios.Type2 data)
			: base(data.Header, data)
		{ }
	}
}