using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>System Power Supply (Type 39)</summary>
	public class SystemPowerSupply : TypeBaseT<SmBios.Type39>
	{
		/// <summary>Location</summary>
		public String Location => this.GetString(this.Type.Location);

		/// <summary>Device name</summary>
		public String DeviceName => this.GetString(this.Type.DeviceName);

		/// <summary>Manufacturer</summary>
		public String Manufacturer => this.GetString(this.Type.Manufacturer);

		/// <summary>Serial number</summary>
		public String SerialNumber => this.GetString(this.Type.SerialNumber);

		/// <summary>Asset Tag</summary>
		public String AssetTagNumber => this.GetString(this.Type.AssetTagNumber);

		/// <summary>Model Part number</summary>
		public String ModelPartNumber => this.GetString(this.Type.ModelPartNumber);

		/// <summary>Revision level</summary>
		public String RevisionLevel => this.GetString(this.Type.RevisionLevel);

		internal SystemPowerSupply(SmBios.Type39 type39)
			: base(type39.Header, type39)
		{ }
	}
}