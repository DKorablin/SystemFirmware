using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>BIOS Information (Type 0)</summary>
	public class Bios : TypeBaseT<SmBios.Type0>
	{
		/// <summary>BIOS Vendor</summary>
		public String Vendor => base.GetString(base.Type.Vendor);
		/// <summary>BIOS version</summary>
		public String Version => base.GetString(base.Type.Version);
		/// <summary>BIOS release data</summary>
		public String ReleaseDate => base.GetString(base.Type.ReleaseDate);
		/// <summary>BIOS ROM Size</summary>
		public String RomSize => $"{base.Type.RomSize:N0} {base.Type.RomSizeUnits}";

		internal Bios(SmBios.Type0 data)
			: base(data.Header, data)
		{ }
	}
}