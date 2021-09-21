using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Typed facade for SMBIOS Type</summary>
	/// <typeparam name="T">Type of SMBIOS structure</typeparam>
	public class TypeBaseT<T> : TypeBase where T : struct
	{
		private readonly T _type;

		/// <summary>SMBIOS structure</summary>
		public T Type { get { return this._type; } }

		internal TypeBaseT(SmBios.Header header, T type)
			: base(header)
		{
			this._type = type;
			//Utils.ClearOverflowFields(base.Header, ref this._type);
		}
	}
}