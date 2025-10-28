using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>System Information (Type 1)</summary>
	public class SystemType : TypeBaseT<SmBios.Type1>
	{
		/// <summary>Manufacturer</summary>
		public String Manufacturer => this.GetString(this.Type.Manufacturer);

		/// <summary>Product name</summary>
		public String ProductName => this.GetString(this.Type.ProductName);

		/// <summary>Version</summary>
		public String Version => this.GetString(this.Type.Version);

		/// <summary>Serial number</summary>
		public String SerialNumber => this.GetString(this.Type.SerialNumber);

		/// <summary>SKU Number</summary>
		public String SKUNumber => this.GetString(this.Type.SKUNumber);

		/// <summary>Family</summary>
		public String Family => this.GetString(this.Type.Family);

		internal SystemType(SmBios.Type1 data)
			: base(data.Header, data)
		{ }
	}
}