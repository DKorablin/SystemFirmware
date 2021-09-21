using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Port Connector Information (Type 8)</summary>
	public class PortConnector : TypeBaseT<SmBios.Type8>
	{
		/// <summary>Internal Reference Designator</summary>
		public String InternalReferenceDesignator { get { return base.GetString(base.Type.InternalReferenceDesignator); } }
		/// <summary>External Reference Designation</summary>
		public String ExternalReferenceDesignator { get { return base.GetString(base.Type.ExternalReferenceDesignator); } }

		internal PortConnector(SmBios.Type8 type8)
			: base(type8.Header, type8)
		{

		}
	}
}