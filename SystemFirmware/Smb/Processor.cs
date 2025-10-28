using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Processor Information (Type 4)</summary>
	public class Processor : TypeBaseT<SmBios.Type4>
	{
		/// <summary>Socket Designation</summary>
		public String SocketDesignation => this.GetString(this.Type.SocketDesignation);

		/// <summary>Processor manufacturer</summary>
		public String ProcessorManufacturer => this.GetString(this.Type.ProcessorManufacturer);

		/// <summary>Processor Version</summary>
		public String ProcessorVersion => this.GetString(this.Type.ProcessorVersion);

		/// <summary>Serial number</summary>
		public String SerialNumber => this.GetString(this.Type.SerialNumber);

		/// <summary>Asset type</summary>
		public String AssetType => this.GetString(this.Type.AssetType);

		/// <summary>Part number</summary>
		public String PartNumber => this.GetString(this.Type.PartNumber);

		internal Processor(SmBios.Type4 type4)
			: base(type4.Header, type4)
		{ }
	}
}