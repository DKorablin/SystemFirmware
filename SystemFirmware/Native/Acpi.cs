using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AlphaOmega.Debug.Native
{
	/// <summary>Known ACPI structures</summary>
	/// <example>https://uefi.org/specs/ACPI/6.4/05_ACPI_Software_Programming_Model/ACPI_Software_Programming_Model.html</example>
	public static class Acpi
	{
		/// <summary>Known ACPI headers</summary>
		public enum Table : UInt32
		{
			/// <summary>Multiple APIC Description Table (MADT)</summary>
			APIC = 0x43495041,
			/// <summary>Boot Error Record Table</summary>
			BERT = 0x54524542,
			/// <summary>Boot Graphics Resource Table</summary>
			BGRT = 0x54524742,
			/// <summary>Corrected Platform Error Polling Table</summary>
			CPEP = 0x50455043,
			/// <summary>Differentiated System Description Table</summary>
			DSDT = 0x54445344,
			/// <summary>Embedded Controller Boot Resources Table</summary>
			ECDT = 0x54444345,
			/// <summary>Error Injection Table</summary>
			EINJ = 0x4a4e4945,
			/// <summary>Error Record Serialization Table</summary>
			ERST = 0x54535245,
			/// <summary>Fixed ACPI Description Table (FADT)</summary>
			FACP = 0x50434146,
			/// <summary>Firmware ACPI Control Structure</summary>
			FACS = 0x53434146,
			/// <summary>Firmware Performance Data Table</summary>
			FPDT = 0x54445046,
			/// <summary>Generic Timer Description Table</summary>
			GTDT = 0x54445447,
			/// <summary>Hardware Error Source Table</summary>
			HEST = 0x54534548,
			/// <summary>Maximum System Characteristics Table</summary>
			MSCT = 0x5443534d,
			/// <summary>Memory Power StateTable</summary>
			MPST = 0x5453504d,
			/// <summary>NVDIMM Firmware Interface Table</summary>
			NFIT = 0x5449464e,
			/// <summary>Platform Communications Channel Table</summary>
			PCCT = 0x54434350,
			/// <summary>Platform Health Assessment Table</summary>
			PHAT = 0x54414850,
			/// <summary>Platform Memory Topology Table</summary>
			PMTT = 0x54544d50,
			/// <summary>Persistent System Description Table</summary>
			PSDT = 0x54445350,
			/// <summary>ACPI RAS Feature Table</summary>
			RASF = 0x46534152,
			//RSDP = 0x20445352,//TODO: RSD PTR <-8 bytes
			//OEMx = //TODO: OEM Specific tables. All table signatures starting with “OEM” are reserved for OEM use.
			/// <summary>Root System Description Table</summary>
			RSDT = 0x54445352,
			/// <summary>Smart Battery Specification Table</summary>
			SBST = 0x54534253,
			/// <summary>Secure DEVices Table</summary>
			SDEV = 0x56454453,
			/// <summary>System Locality Distance Information Table</summary>
			SLIT = 0x54494c53,
			/// <summary>System Resource Affinity Table</summary>
			SRAT = 0x54415253,
			/// <summary>Secondary System Description Table</summary>
			SSDT = 0x54445353,
			/// <summary>Extended System Description Table</summary>
			XSDT = 0x54445358,

			#region Reserved
			/// <summary>Arm Error Source Table</summary>
			AEST = 0x54534541,
			/// <summary>BIOS Data ACPI Table – exposing platform margining data</summary>
			BDAT = 0x54414442,
			/// <summary>Reserved Signature</summary>
			BOOT = 0x544f4f42,
			/// <summary>Component Distance Information Table</summary>
			CDIT = 0x54494443,
			/// <summary>CXL Early Discovery Table</summary>
			CEDT = 0x54444543,
			///<summary>Component Resource Attribute Table</summary>
			CRAT = 0x54415243,
			///<summary>Core System Resource Table</summary>
			CSRT = 0x54525343,
			///<summary>Debug Port Table</summary>
			DBGP = 0x50474244,
			///<summary>DMA Remapping Table</summary>
			DMAR = 0x52414d44,
			///<summary>Dynamic Root of Trust for Measurement Table</summary>
			DRTM = 0x4d545244,
			///<summary>Event Timer Description Table (Obsolete)</summary>
			ETDT = 0x54445445,
			///<summary>IA-PC High Precision Event Timer Table</summary>
			HPET = 0x54455048,
			///<summary>iSCSI Boot Firmware Table</summary>
			IBFT = 0x54464249,
			///<summary>I/O Remapping Table</summary>
			IORT = 0x54524f49,
			///<summary>I/O Virtualization Reporting Structure</summary>
			IVRS = 0x53525649,
			///<summary>Low Power Idle Table</summary>
			LPIT = 0x5449504c,
			///<summary>PCI Express Memory-mapped Configuration Space base address description table</summary>
			MCFG = 0x4746434d,
			///<summary>Management Controller Host Interface table</summary>
			MCHI = 0x4948434d,
			///<summary>Memory Partitioning And Monitoring</summary>
			MPAM = 0x4d41504d,
			///<summary>Microsoft Data Management Table</summary>
			MSDM = 0x4d44534d,
			///<summary>Platform Runtime Mechanism Table</summary>
			PRMT = 0x544d5250,
			///<summary>Regulatory Graphics Resource Table</summary>
			RGRT = 0x54524752,
			///<summary>Software Delegated Exceptions Interface</summary>
			SDEI = 0x49454453,
			///<summary>Microsoft Software Licensing table</summary>
			SLIC = 0x43494c53,
			///<summary>Microsoft Serial Port Console Redirection table</summary>
			SPCR = 0x52435053,
			///<summary>Server Platform Management Interface table</summary>
			SPMI = 0x494d5053,
			///<summary>_STA Override table</summary>
			STAO = 0x4f415453,
			///<summary>Storage Volume Key Data table in the Intel Trusted Domain Extensions</summary>
			SVKL = 0x4c4b5653,
			///<summary>Trusted Computing Platform Alliance Capabilities Table</summary>
			TCPA = 0x41504354,
			///<summary>Trusted Platform Module 2 Table</summary>
			TPM2 = 0x324d5054,
			///<summary>Unified Extensible Firmware Interface Specification</summary>
			UEFI = 0x49464555,
			///<summary>Windows ACPI Emulated Devices Table</summary>
			WAET = 0x54454157,
			///<summary>Watch Dog Action Table</summary>
			WDAT = 0x54414457,
			///<summary>Watchdog Resource Table</summary>
			WDRT = 0x54524457,
			///<summary>Windows Platform Binary Table</summary>
			WPBT = 0x54425057,
			/// <summary>Windows Security Mitigations Table</summary>
			WSMT = 0x544d5357,
			///<summary>Xen Project</summary>
			XENV = 0x564e4558,
			#endregion Reserved
		}

		/// <summary>ACPI header</summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct Header
		{
			/// <summary>All the ACPI tables have a 4 byte Signature field (except the RSDP which has an 8 byte one)</summary>
			/// <remarks>Using the signature, you can determine what table are you working with</remarks>
			public Table Signature;

			/// <summary>Total size of the table, inclusive of the header</summary>
			public UInt32 Length;
			/// <summary>Revision</summary>
			public Byte Revision;
			/// <summary>A 8-bit checksum field of the whole table, inclusive of the header</summary>
			/// <remarks>
			/// All bytes of the table summed must be equal to 0 (mod 0x100).
			/// You should always do a checksum of the table before using it, even if you found the table linked by other tables
			/// </remarks>
			public Byte Checksum;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			private Byte[] _OemId;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			private Byte[] _OemTableId;

			/// <summary>OEM Revision</summary>
			public UInt32 OemRevision;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			private Byte[] _CreatorId;

			/// <summary>Creator Revision</summary>
			public UInt32 CreatorRevision;

			/// <summary>All the ACPI tables have a 4 byte Signature field (except the RSDP which has an 8 byte one)</summary>
			/// <remarks>Using the signature, you can determine what table are you working with</remarks>
			public String SignatureStr => Encoding.ASCII.GetString(BitConverter.GetBytes((Int32)this.Signature));

			/// <summary>OEM Id</summary>
			public String OemId => Encoding.ASCII.GetString(this._OemId);

			/// <summary>OEM table Id</summary>
			public String OemTableId => Encoding.ASCII.GetString(this._OemTableId);

			/// <summary>Creator Id</summary>
			public String CreatorId => Encoding.ASCII.GetString(this._CreatorId);
		}
	}
}