using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Processor Information (Type 4)</summary>
	public class Processor : TypeBaseT<SmBios.Type4>
	{
		/// <summary>Socket Designation</summary>
		public String SocketDesignation => base.GetString(base.Type.SocketDesignation);
		/// <summary>Processor manufacturer</summary>
		public String ProcessorManufacturer => base.GetString(base.Type.ProcessorManufacturer);
		/// <summary>Processor Version</summary>
		public String ProcessorVersion => base.GetString(base.Type.ProcessorVersion);
		/// <summary>Serial number</summary>
		public String SerialNumber => base.GetString(base.Type.SerialNumber);
		/// <summary>Asset type</summary>
		public String AssetType => base.GetString(base.Type.AssetType);
		/// <summary>Part number</summary>
		public String PartNumber => base.GetString(base.Type.PartNumber);

		internal Processor(SmBios.Type4 type4)
			: base(type4.Header, type4)
		{ }
	}
}