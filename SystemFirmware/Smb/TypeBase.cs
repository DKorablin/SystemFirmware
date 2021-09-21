using System;
using System.Reflection;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug.Smb
{
	/// <summary>Base SMBIOS Type facade</summary>
	public class TypeBase
	{
		/// <summary>Type mapping for structures</summary>
		internal class TypeMapping
		{
			private readonly Type _structType;
			private readonly Type _tableType;
			private readonly ConstructorInfo _ctor;

			/// <summary>Facade type for SMBIOS type</summary>
			public Type TableType { get { return this._tableType; } }
			/// <summary>SMBIOS struct type</summary>
			public Type StructType { get { return this._structType; } }
			/// <summary>Constructor for facade</summary>
			public ConstructorInfo Ctor { get { return this._ctor; } }

			public TypeMapping(Type tableType)
			{
				this._tableType = tableType;
				this._ctor = this._tableType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
				this._structType = this._ctor.GetParameters()[0].ParameterType;
			}
		}

		private readonly SmBios.Header _header;

		/// <summary>SMBIOS header</summary>
		public SmBios.Header Header { get { return this._header; } }
		/// <summary>All strings in SMBIOS Type</summary>
		public String[] Strings { get; protected internal set; }
		/// <summary>Additional variable data</summary>
		public Byte[] ExData { get; protected internal set; }

		/// <summary>Creates instance if base type facade</summary>
		/// <param name="header">SMBIOS type header</param>
		protected internal TypeBase(SmBios.Header header)
		{
			this._header = header;
		}

		/// <summary>Gets the string by index</summary>
		/// <param name="index">Index</param>
		/// <returns>String</returns>
		protected String GetString(Byte index)
		{
			return index > 0
				? this.Strings[index - 1]
				: null;
		}
	}
}