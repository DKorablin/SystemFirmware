using System;
using System.Collections.Generic;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>On Board Devices Information (Type 10, Obsolete)</summary>
	public class OnBoardDevices : TypeBaseT<SmBios.Type10>
	{
		/// <summary>Device info</summary>
		public class DeviceInfo
		{
			/// <summary>Enabled</summary>
			public Boolean IsEnabled { get; set; }
			/// <summary>Type</summary>
			public SmBios.Type10.DeviceType Type { get; set; }
			/// <summary>Name</summary>
			public String Name { get; set; }
		}

		internal OnBoardDevices(SmBios.Type10 type10)
			: base(type10.Header, type10)
		{
		}

		/// <summary>On board devices</summary>
		/// <returns>List on on board devices</returns>
		public IEnumerable<DeviceInfo> GetDevices()
		{
			for(Int32 loop = 0; loop < base.Type.NumberOfDevices; loop++)
			{
				Int32 offset = 2 * loop;
				Byte type = base.ExData[offset];
				Byte nameIndex = base.ExData[offset + 1];
				yield return new DeviceInfo()
				{
					IsEnabled = (type >> 7 & 0x01) == 0x01,
					Type = (SmBios.Type10.DeviceType)(type & 0x3f),
					Name = base.GetString(nameIndex),
				};
			}
		}
	}
}