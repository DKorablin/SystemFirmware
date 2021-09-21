using System;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug
{
	/// <summary>ACPI facade</summary>
	public class FirmwareAcpi : FirmwareTable
	{
		private Acpi.Header? _header;

		/// <summary>ACPI header</summary>
		public Acpi.Header Header
		{
			get
			{
				if(this._header == null)
					this.Parse();
				return this._header.Value;
			}
		}

		private void Parse()
		{
			UInt32 padding = 0;
			using(PinnedBufferReader reader = new PinnedBufferReader(base.Data))
				this._header = reader.BytesToStructure<Acpi.Header>(ref padding);

			/*// Удаляем заголовок
			this._payload = new Byte[payload.Length - padding];
			Array.Copy(payload, padding, this._payload, 0, this._payload.Length);*/
		}
	}
}