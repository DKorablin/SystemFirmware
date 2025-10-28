using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>System Slots (Type 9)</summary>
	public class SystemSlots : TypeBaseT<SmBios.Type9>
	{
		/// <summary>Slot designation</summary>
		public String SlotDesignation => this.GetString(this.Type.SlotDesignation);

		internal SystemSlots(SmBios.Type9 type9)
			: base(type9.Header, type9)
		{ }
	}
}