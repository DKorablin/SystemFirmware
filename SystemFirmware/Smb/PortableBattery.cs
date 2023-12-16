using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Portable Battery (Type 22)</summary>
	public class PortableBattery : TypeBaseT<SmBios.Type22>
	{
		/// <summary>Location</summary>
		public String Location => base.GetString(base.Type.Location);
		/// <summary>Manufacturer</summary>
		public String Manufacturer => base.GetString(base.Type.Manufacturer);
		/// <summary>Manufacture date</summary>
		public String ManufactureDate => base.GetString(base.Type.ManufactureDate);
		/// <summary>Serial number</summary>
		/// <exception cref="NotImplementedException">Not implemented Serial Number</exception>
		public String SerialNumber
		{
			get
			{
				String result = base.GetString(base.Type.SerialNumber);
				if(result != null && result[0] != '0')
					return result;

				if(base.Type.SBDSSerialNumber == 0)
					throw new NotImplementedException();

				return String.Format("{0:x}-{1:x}-{2:x}-{3:x}", base.Type.Manufacturer, base.Type.DeviceName, base.Type.ManufactureDate, base.Type.SBDSSerialNumber);
			}
		}
		/// <summary>Device name</summary>
		public String DeviceName => base.GetString(base.Type.DeviceName);
		/// <summary>SBDS version number</summary>
		public String SBDSVersionNumber => base.GetString(base.Type.SBDSVersionNumber);
		/// <summary>SBDS Device chemistry</summary>
		public String SBDSDeviceChemistry => base.GetString(base.Type.SBDSDeviceChemistry);

		internal PortableBattery(SmBios.Type22 type22)
			: base(type22.Header, type22)
		{ }
	}
}