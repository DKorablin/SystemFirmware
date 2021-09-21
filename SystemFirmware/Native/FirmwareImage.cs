using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AlphaOmega.Debug.Native
{
	internal static class FirmwareImage
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct FFS_FILE_HEADER
		{
			public Guid EFI_GUID;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public Byte[] INTEGRITY_CHECK;
			public UInt16 INTEGRITY_CHECK_Checksum16;
			public Byte DataChecksum;
			public Byte Type;
			public Byte Attributes;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
			public Byte[] Size;
			public Byte State;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Pcir_header
		{
			private const Int32 PCIR_Signature = 0x52494350;//PCIR

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public Byte[] signature;
			public UInt16 VendorId;
			public UInt16 DeviceId;
			public UInt16 VitalProductDataPtr;
			public UInt16 pce_size;
			public Byte Revision;
			public Byte ProgrammingInterfaceCode;
			public Byte SubclassCode;
			public Byte ClassCode;
			public UInt16 ImageLength;//512Bytes
			public UInt16 RevisionLevel;
			public Byte CodeType;
			public Byte IndicatorByte;//Last image=80
			public UInt16 Reserved;

			public Boolean IsValid { get { return BitConverter.ToInt32(this.signature, 0) == PCIR_Signature; } }
			public String SignatureStr { get { return Encoding.ASCII.GetString(this.signature); } }
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct vbt_header
		{
			private const Int32 VBT_Signature = 0x54425624;//$VBT

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public Byte[] signature;
			public UInt16 version;
			public UInt16 header_size;
			public UInt16 vbt_size;
			public Byte vbt_checksum;
			public Byte reserved0;
			public UInt32 bdb_offset;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public UInt32[] aim_offset;


			public Boolean IsValid { get { return BitConverter.ToInt32(this.signature, 0) == VBT_Signature; } }
			public String SignatureStr { get { return Encoding.ASCII.GetString(this.signature); } }
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct bdb_header
		{
			private static Byte[] BDB_Signature = new Byte[] { 0x42, 0x49, 0x4F, 0x53, 0x5F, 0x44, 0x41, 0x54, 0x41, 0x5F, 0x42, 0x4C, 0x4F, 0x43, 0x4B, 0x20 };//BIOS_DATA_BLOCK
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public Byte[] signature;
			/// <summary>version of data block definitions</summary>
			public UInt16 version;
			/// <summary>size of this structure</summary>
			public UInt16 header_size;
			/// <summary>size of this structure and all data blocks</summary>
			public UInt16 bdb_size;

			public Boolean IsValid { get { return BDB_Signature.SequenceEqual(this.signature); } }
			public String SignatureStr { get { return Encoding.ASCII.GetString(this.signature); } }
		}
	}
}