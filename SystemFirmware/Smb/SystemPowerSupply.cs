using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>System Power Supply (Type 39)</summary>
	public class SystemPowerSupply : TypeBaseT<SmBios.Type39>
	{
		/// <summary>Location</summary>
		public String Location { get { return base.GetString(base.Type.Location); } }
		/// <summary>Device name</summary>
		public String DeviceName { get { return base.GetString(base.Type.DeviceName); } }
		/// <summary>Manufacturer</summary>
		public String Manufacturer { get { return base.GetString(base.Type.Manufacturer); } }
		/// <summary>Serial number</summary>
		public String SerialNumber { get { return base.GetString(base.Type.SerialNumber); } }
		/// <summary>Asset Tag</summary>
		public String AssetTagNumber { get { return base.GetString(base.Type.AssetTagNumber); } }
		/// <summary>Model Part number</summary>
		public String ModelPartNumber { get { return base.GetString(base.Type.ModelPartNumber); } }
		/// <summary>Revision level</summary>
		public String RevisionLevel { get { return base.GetString(base.Type.RevisionLevel); } }

		internal SystemPowerSupply(SmBios.Type39 type39)
			: base(type39.Header, type39)
		{
		}
	}
}