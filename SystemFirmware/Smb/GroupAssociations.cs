using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Group Associations (Type 14)</summary>
	public class GroupAssociations : TypeBaseT<SmBios.Type14>
	{
		/// <summary>Group name</summary>
		public String GroupName => base.GetString(base.Type.GroupName);

		internal GroupAssociations(SmBios.Type14 type14)
			: base(type14.Header, type14)
		{ }
	}
}