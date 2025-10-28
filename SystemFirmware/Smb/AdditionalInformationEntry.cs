using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Additional Information Entry</summary>
	public class AdditionalInformationEntry : TypeBaseT<SmBios.Type40.Type40_AdditionalInformation>
	{
		/// <summary>Optional string to be associated with the field referenced by the Referenced Offset</summary>
		public String String => this.GetString(this.Type.String);

		internal AdditionalInformationEntry(SmBios.Type40 type40, SmBios.Type40.Type40_AdditionalInformation type40Information)
			: base(type40.Header, type40Information)
		{ }
	}
}