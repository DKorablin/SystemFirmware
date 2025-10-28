using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Additional Information (Type 40)</summary>
	public class AdditionalInformation : TypeBaseT<SmBios.Type40>
	{
		internal AdditionalInformation(SmBios.Type40 type40)
			: base(type40.Header, type40)
		{ }

		/// <summary>Gets additional information</summary>
		/// <returns></returns>
		public IEnumerable<AdditionalInformationEntry> GetAdditionalInformation()
		{
			if(this.Type.AdditionalInformationEntriesCount > 0)
			{
				_ = (UInt32)Marshal.SizeOf(typeof(SmBios.Type40.Type40_AdditionalInformation));
				using(PinnedBufferReader reader = new PinnedBufferReader(this.ExData))
				{
					UInt32 padding = 0;
					for(Int32 loop = 0; loop < this.Type.AdditionalInformationEntriesCount; loop++)
					{
						UInt32 offsetToData = padding;
						SmBios.Type40.Type40_AdditionalInformation result = reader.BytesToStructure<SmBios.Type40.Type40_AdditionalInformation>(ref offsetToData);
						padding += result.EntryLength;

						Byte[] exData = null;
						if(offsetToData < padding)//Payload available
							exData = reader.GetBytes(offsetToData, padding - offsetToData);

						yield return new AdditionalInformationEntry(this.Type, result) { Strings = this.Strings, ExData = exData, };
					}
				}
			}
		}
	}
}