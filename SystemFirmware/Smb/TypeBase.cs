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
			/// <summary>Facade type for SMBIOS type</summary>
			public Type TableType { get ; }

			/// <summary>SMBIOS struct type</summary>
			public Type StructType { get; }

			/// <summary>Constructor for facade</summary>
			public ConstructorInfo Ctor { get; }

			public TypeMapping(Type tableType)
			{
				this.TableType = tableType ?? throw new ArgumentNullException(nameof(tableType));
				this.Ctor = this.TableType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
				this.StructType = this.Ctor.GetParameters()[0].ParameterType;
			}
		}

		/// <summary>SMBIOS header</summary>
		public SmBios.Header Header { get ; }

		/// <summary>All strings in SMBIOS Type</summary>
		public String[] Strings { get; protected internal set; }

		/// <summary>Additional variable data</summary>
		public Byte[] ExData { get; protected internal set; }

		/// <summary>Creates instance if base type facade</summary>
		/// <param name="header">SMBIOS type header</param>
		protected internal TypeBase(SmBios.Header header)
			=> this.Header = header;

		/// <summary>Gets the string by index</summary>
		/// <param name="index">Index</param>
		/// <returns>String</returns>
		protected String GetString(Byte index)
			=> index > 0
				? this.Strings[index - 1]
				: null;
	}
}