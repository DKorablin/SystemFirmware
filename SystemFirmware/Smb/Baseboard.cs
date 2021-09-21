using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Baseboard (or Module) Information (Type 2)</summary>
	public class Baseboard : TypeBaseT<SmBios.Type2>
	{
		/// <summary>Manufacturer</summary>
		public String Manufacturer { get { return base.GetString(base.Type.Manufacturer); } }
		/// <summary>Product</summary>
		public String Product { get { return base.GetString(base.Type.Product); } }
		/// <summary>Version</summary>
		public String Version { get { return base.GetString(base.Type.Version); } }
		/// <summary>Serial number</summary>
		public String SerialNumber { get { return base.GetString(base.Type.SerialNumber); } }
		/// <summary>Asset tag</summary>
		public String AssetTag { get { return base.GetString(base.Type.AssetTag); } }
		/// <summary>Location in Chassis</summary>
		public String LocationInChassis { get { return base.GetString(base.Type.LocationInChassis); } }

		internal Baseboard(SmBios.Type2 data)
			: base(data.Header, data)
		{
		}
	}
}