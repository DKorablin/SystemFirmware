using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>BIOS Information (Type 0)</summary>
	public class Bios : TypeBaseT<SmBios.Type0>
	{
		/// <summary>BIOS Vendor</summary>
		public String Vendor { get { return base.GetString(base.Type.Vendor); } }
		/// <summary>BIOS version</summary>
		public String Version { get { return base.GetString(base.Type.Version); } }
		/// <summary>BIOS release data</summary>
		public String ReleaseDate { get { return base.GetString(base.Type.ReleaseDate); } }
		/// <summary>BIOS ROM Size</summary>
		public String RomSize { get { return String.Format("{0:N0} {1}", base.Type.RomSize, base.Type.RomSizeUnits); } }

		internal Bios(SmBios.Type0 data)
			: base(data.Header, data)
		{
		}
	}
}