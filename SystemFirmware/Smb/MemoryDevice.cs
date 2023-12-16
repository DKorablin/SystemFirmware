using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Memory Device (Type 17)</summary>
	public class MemoryDevice : TypeBaseT<SmBios.Type17>
	{
		/// <summary>Device locator</summary>
		public String DeviceLocator => base.GetString(base.Type.DeviceLocator);
		/// <summary>Bank locator</summary>
		public String BankLocator => base.GetString(base.Type.BankLocator);
		/// <summary>Manufacturer</summary>
		public String Manufacturer => base.GetString(base.Type.Manufacturer);
		/// <summary>Serial number</summary>
		public String SerialNumber => base.GetString(base.Type.SerialNumber);
		/// <summary>Asset tag</summary>
		public String AssetTag => base.GetString(base.Type.AssetTag);
		/// <summary>Part number</summary>
		public String PartNumber => base.GetString(base.Type.PartNumber);
		/// <summary>Firmware version</summary>
		public String FirmwareVersion => base.GetString(base.Type.FirmwareVersion);

		internal MemoryDevice(SmBios.Type17 type17)
			: base(type17.Header, type17)
		{ }
	}
}