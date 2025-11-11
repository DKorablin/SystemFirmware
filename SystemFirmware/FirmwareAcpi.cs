using System;
using System.Runtime.InteropServices;
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
			{
				this._header = reader.BytesToStructure<Acpi.Header>(ref padding);

				switch(this._header.Value.Signature)
				{
				case Acpi.Table.FACP:
					Int32 size = Marshal.SizeOf(typeof(Acpi.Fadt));
					if(this.Data.Length - padding == size)
					{
						Acpi.Fadt fadt = reader.BytesToStructure<Acpi.Fadt>(ref padding);
					} else
						throw new NotImplementedException("The size of FADT structure belongs to different ACPI version and not supported");
					break;
				case Acpi.Table.MCFG:
					Int32 sizeMcfg = Marshal.SizeOf(typeof(Acpi.Mcfg));
					if(this.Data.Length - padding >= sizeMcfg)
					{
						Acpi.Mcfg mcfg = reader.BytesToStructure<Acpi.Mcfg>(ref padding);
						if(mcfg.IsValid)
						{
							Int32 sizeMcfgAllocation = Marshal.SizeOf(typeof(Acpi.McfgAllocation));
							Int32 entries = (Int32)((this.Data.Length - padding) / sizeMcfgAllocation);
							Acpi.McfgAllocation[] allocations = new Acpi.McfgAllocation[entries];
							for(Int32 i = 0; i < entries; i++)
								allocations[i] = reader.BytesToStructure<Acpi.McfgAllocation>(ref padding);
						}
					} else
						throw new NotImplementedException("The size of FADT structure belongs to different ACPI version and not supported");
					break;
				default:
					break;
				}
			}
			/*// Removing header
			this._payload = new Byte[payload.Length - padding];
			Array.Copy(payload, padding, this._payload, 0, this._payload.Length);*/
		}
	}
}