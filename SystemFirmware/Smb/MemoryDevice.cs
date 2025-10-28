using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Memory Device (Type 17)</summary>
	public class MemoryDevice : TypeBaseT<SmBios.Type17>
	{
		/// <summary>Device locator</summary>
		public String DeviceLocator => this.GetString(this.Type.DeviceLocator);

		/// <summary>Bank locator</summary>
		public String BankLocator => this.GetString(this.Type.BankLocator);

		/// <summary>Manufacturer</summary>
		public String Manufacturer => this.GetString(this.Type.Manufacturer);

		/// <summary>Serial number</summary>
		public String SerialNumber => this.GetString(this.Type.SerialNumber);

		/// <summary>Asset tag</summary>
		public String AssetTag => this.GetString(this.Type.AssetTag);

		/// <summary>Part number</summary>
		public String PartNumber => this.GetString(this.Type.PartNumber);

		/// <summary>Firmware version</summary>
		public String FirmwareVersion => this.GetString(this.Type.FirmwareVersion);

		internal MemoryDevice(SmBios.Type17 type17)
			: base(type17.Header, type17)
		{ }
	}
}