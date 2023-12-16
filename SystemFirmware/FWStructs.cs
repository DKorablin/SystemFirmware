using System;
using System.Runtime.InteropServices;
using System.Text;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug
{
	internal static class FWStructs
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct Header
		{
			private const Int32 SyFW = 0x57467953;//SyFW
			public Int32 Signature;
			public Methods.FirmwareTableType Type;
			public UInt16 Count;
			public String SignatureStr => Encoding.ASCII.GetString(BitConverter.GetBytes(this.Signature));
			public Boolean IsValid => this.Signature == Header.SyFW;

			public Header(Methods.FirmwareTableType type, UInt16 count)
			{
				Signature = Header.SyFW;
				Type = type;
				Count = count;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Table
		{
			public UInt32 TableId;
			public UInt32 Size;

			public Table(UInt32 tableId, UInt32 size)
			{
				this.TableId = tableId;
				this.Size = size;
			}
		}
	}
}