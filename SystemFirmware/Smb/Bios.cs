using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>BIOS Information (Type 0)</summary>
	public class Bios : TypeBaseT<SmBios.Type0>
	{
		/// <summary>BIOS Vendor</summary>
		public String Vendor => this.GetString(this.Type.Vendor);
		/// <summary>BIOS version</summary>
		public String Version => this.GetString(this.Type.Version);
		/// <summary>BIOS release data</summary>
		public String ReleaseDate => this.GetString(this.Type.ReleaseDate);
		/// <summary>BIOS ROM Size</summary>
		public String RomSize => $"{this.Type.RomSize:N0} {this.Type.RomSizeUnits}";

		internal Bios(SmBios.Type0 data)
			: base(data.Header, data)
		{ }
	}
}