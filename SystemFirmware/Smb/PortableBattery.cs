using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Portable Battery (Type 22)</summary>
	public class PortableBattery : TypeBaseT<SmBios.Type22>
	{
		/// <summary>Location</summary>
		public String Location => this.GetString(this.Type.Location);

		/// <summary>Manufacturer</summary>
		public String Manufacturer => this.GetString(this.Type.Manufacturer);

		/// <summary>Manufacture date</summary>
		public String ManufactureDate => this.GetString(this.Type.ManufactureDate);

		/// <summary>Serial number</summary>
		/// <exception cref="NotImplementedException">Not implemented Serial Number</exception>
		public String SerialNumber
		{
			get
			{
				String result = this.GetString(this.Type.SerialNumber);
				if(result != null && result[0] != '0')
					return result;

				if(this.Type.SBDSSerialNumber == 0)
					throw new NotImplementedException();

				return String.Format("{0:x}-{1:x}-{2:x}-{3:x}", this.Type.Manufacturer, this.Type.DeviceName, this.Type.ManufactureDate, this.Type.SBDSSerialNumber);
			}
		}
		/// <summary>Device name</summary>
		public String DeviceName => this.GetString(this.Type.DeviceName);

		/// <summary>SBDS version number</summary>
		public String SBDSVersionNumber => this.GetString(this.Type.SBDSVersionNumber);

		/// <summary>SBDS Device chemistry</summary>
		public String SBDSDeviceChemistry => this.GetString(this.Type.SBDSDeviceChemistry);

		internal PortableBattery(SmBios.Type22 type22)
			: base(type22.Header, type22)
		{ }
	}
}