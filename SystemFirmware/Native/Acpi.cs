using System;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace AlphaOmega.Debug.Native
{
	/// <summary>Known ACPI structures</summary>
	/// <example>
	/// https://www.intel.com/content/www/us/en/developer/topic-technology/open/acpica/overview.html
	/// https://uefi.org/specs/ACPI/6.4/05_ACPI_Software_Programming_Model/ACPI_Software_Programming_Model.html
	/// </example>
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
			/// <summary>Root System Description Table</summary>
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

			#region PlatformSpecific
			RTMA= 0x414D5452,
			OEML= 0x4C4D454F,
			/// <summary>Table for Audio</summary>
			NHLT = 0x544c484e,
			#endregion PlatformSpecific
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
			/// <summary>ASCII OEM identification</summary>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			private Byte[] _OemId;
			/// <summary>ASCII OEM table identification</summary>
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

		/// <summary>
		/// LPI state descriptor provides OSPM with additional characteristics including entry trigger, residency and latency requirements and associated residency counter descriptor.
		/// If multiple LPI states exist, the more shallow LPI states are expected to have smaller residency and latency requirements(and higher power).
		/// If multiple LPI states are defined, they must be ordered from shallowest to deepest with a zero-based monotonically increasing value(0..n)
		/// Unique ID.Multiple LPI state descriptors may exist for a single Unique ID, but only one may be enabled (as indicated in Flags) per Unique ID.
		/// </summary>
		/// <example>https://uefi.org/sites/default/files/resources/Intel_ACPI_Low_Power_S0_Idle.pdf</example>
		[StructLayout(LayoutKind.Sequential)]
		public struct Lpit
		{
			/// <summary>LPI State Descriptor Type 0</summary>
			public UInt32 Type;
			/// <summary>Length of LPI State Descriptor Structure</summary>
			public UInt32 Length;
			/// <summary>Unique LPI state identifier: zero based, monotonically increasing identifier</summary>
			public UInt16 UniqueID;
			/// <summary>Must be zero</summary>
			public UInt16 Reserved;
			/// <summary>See Flags descriptor in Table 4</summary>
			public UInt32 Flags;
			/// <summary>The LPI entry trigger, matching an existing _CST.Register object, represented as a Generic Address Structure.</summary>
			/// <remarks>All processors must request this state or deeper to trigger</remarks>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
			public Byte[] EntryTrigger;
			/// <summary>Minimum residency or “break-even” in microseconds(uS)</summary>
			public UInt32 Residency;
			/// <summary>Worst case exit latency in microseconds uS</summary>
			public UInt32 Latency;
			/// <summary>
			/// [optional] Residency counter, represented as a Generic Address Structure.
			/// If not present, Flags[1] bit should be set.
			/// </summary>
			[MarshalAs(UnmanagedType.ByValArray,SizeConst = 12)]
			public Byte[] ResidencyCounter;
			/// <summary>
			/// Residency counter frequency in cycles per second.
			/// A value of 0 indicates that counter runs at TSC frequency.
			/// Valid only if Residency Counter is present.
			/// </summary>
			public UInt64 ResidencyCounterFrequency;
		}

		/// <summary>FADT - Fixed ACPI Description Table (Signature "FACP") (Version=4)</summary>
		/// <remarks>Even if the pointer was found in another ACPI valid structure, you should anyway do the checksum to check that the table is valid.</remarks>
		[StructLayout(LayoutKind.Sequential)]
		public struct Fadt
		{
			/// <summary>GAS - Generic Address Structure (ACPI 2.0+)</summary>
			/// <remarks>
			/// Note: Since this structure is used in the ACPI tables, it is byte aligned.
			/// If misaligned access is not supported by the hardware, accesses to the
			/// 64-bit Address field must be performed with care.
			/// </remarks>
			[StructLayout(LayoutKind.Sequential)]
			public struct acpi_generic_address
			{
				/// <summary>Defines how many bytes at once you can read/write</summary>
				public enum AccessSizeType : Byte
				{
					/// <summary>Undefined</summary>
					/// <remarks>legacy reasons</remarks>
					Undefined = 0,
					/// <summary>Byte access</summary>
					ByteAccess = 1,
					/// <summary>16-bit (word) access</summary>
					Access16bit = 2,
					/// <summary>32-bit (dword) access</summary>
					Access32bit = 3,
					/// <summary>64-bit (qword) access</summary>
					Access64bit = 4,
				}

				/// <summary>Address space where struct or register exists</summary>
				public enum AddressSpaceType : Byte
				{
					/// <summary>System Memory</summary>
					SystemMemory = 0,
					/// <summary>System I/O</summary>
					SystemIO=1,
					/// <summary>PCI Configuration Space </summary>
					PCIConfigurationSpace = 2,
					/// <summary>Embedded Controller</summary>
					EmbeddedController = 3,
					/// <summary>System Management Bus</summary>
					SystemManagementBus = 4,
					/// <summary>System CMOS</summary>
					SystemCMOS = 5,
					/// <summary>PCI Device BAR Target</summary>
					PCIDeviceBARTarget = 6,
					/// <summary>Intelligent Platform Management Infrastructure</summary>
					IntelligentPlatformManagementInfrastructure = 7,
					/// <summary>General Purpose I/O</summary>
					GeneralPurposeIO = 8,
					/// <summary>Generic Serial Bus</summary>
					GenericSerialBus = 9,
					/// <summary>Platform Communication Channel</summary>
					PlatformCommunicationChannel = 0xA,
					// 0x0B to 0x7F Reserved
					// 0x80 to 0xFF OEM Defined
				}

				/// <summary>Address space where struct or register exists</summary>
				public AddressSpaceType AddressSpace;
				/// <summary>Size in bits of given register</summary>
				public Byte BitWidth;
				/// <summary>Bit offset within the register</summary>
				public Byte BitOffset;
				/// <summary>Defines how many bytes at once you can read/write (ACPI 3.0)</summary>
				public AccessSizeType AccessSize;
				/// <summary>Address is a 64-bit pointer in the defined address space to the data structure</summary>
				public UInt64 Address;
			}

			/// <summary>This field contains a value which should address you to a power management profile</summary>
			public enum PreferredPowerManagementType : Byte
			{
				Unspecified = 0,
				Desktop = 1,
				Mobile = 2,
				Workstation = 3,
				EnterpriseServer = 4,
				SohoServer = 5,
				AplliancePC = 6,
				PerformanceServer = 7,
			}

			/// <summary>This is a 32-bit pointer to the FACS</summary>
			/// <remarks>
			/// Since ACPI 2.0 a new field has been added to the table, <see cref="X_FirmwareControl"/> of type GAS, which is 64-bits wide.
			/// Only one of the two fields is used, the other contains 0.
			/// According to the Specs, the <see cref="X_FirmwareControl"/> is used only when the FACS is placed above the 4th GB.
			/// </remarks>
			public UInt32 FirmwareCtrl;
			/// <summary>32-bit physical address of DSDT</summary>
			/// <remarks>This has a <see cref="X_Dsdt"/> brother too.</remarks>
			public UInt32 Dsdt;
			/// <summary>System Interrupt Model (ACPI 1.0) - not used in ACPI 2.0+</summary>
			public Byte model;
			/// <summary>This field contains a value which should address you to a power management profile</summary>
			/// <remarks>For example if it contains 2, the computer is a laptop and you should configure power management in power saving mode.</remarks>
			public PreferredPowerManagementType PreferredPowerManagementProfile;
			/// <summary>The System Control Interrupt is used by ACPI to notify the OS about fixed events, such as for example, pressing the power button, or for General Purpose Events (GPEs), which are firmware specific</summary>
			/// <remarks>
			/// This member in the FADT structure indicates the PIC or IOAPIC interrupt pin for it.
			/// To know if it's a PIC IRQ, check if the dual 8259 interrupt controllers are present via the MADT.
			/// Otherwise, it's a GSI.
			/// If you are using the IOAPIC and the PIC is present, remember to check the Interrupt Source Overrides first to get the GSI associated with the IRQ source.
			/// </remarks>
			public UInt16 SCI_Interrupt;
			/// <summary>This is an I/O Port</summary>
			/// <remarks>
			/// This is where the OS writes AcpiEnable or AcpiDisable to get or release the ownership over the ACPI registers.
			/// This is 0 on systems where the System Management Mode is not supported.
			/// </remarks>
			public UInt32 SMI_CommandPort;
			/// <summary>Value to write to SMI_CMD to enable ACPI</summary>
			public Byte AcpiEnable;
			/// <summary>Value to write to SMI_CMD to disable ACPI</summary>
			public Byte AcpiDisable;
			/// <summary>Value to write to SMI_CMD to enter S4BIOS state</summary>
			public Byte S4BIOS_REQ;
			/// <summary>Processor performance state control</summary>
			public Byte PSTATE_Control;
			/// <summary>32-bit port address of Power Mgt 1a Event Reg Blk</summary>
			public UInt32 PM1aEventBlock;
			/// <summary>32-bit port address of Power Mgt 1b Event Reg Blk</summary>
			public UInt32 PM1bEventBlock;
			/// <summary>32-bit port address of Power Mgt 1a Control Reg Blk</summary>
			public UInt32 PM1aControlBlock;
			/// <summary>32-bit port address of Power Mgt 1b Control Reg Blk</summary>
			public UInt32 PM1bControlBlock;
			/// <summary>32-bit port address of Power Mgt 2 Control Reg Blk</summary>
			public UInt32 PM2ControlBlock;
			/// <summary>32-bit port address of Power Mgt Timer Ctrl Reg Blk</summary>
			public UInt32 PMTimerBlock;
			/// <summary>32-bit port address of General Purpose Event 0 Reg Blk</summary>
			public UInt32 GPE0Block;
			/// <summary>32-bit port address of General Purpose Event 1 Reg Blk</summary>
			public UInt32 GPE1Block;
			/// <summary>Byte Length of ports at pm1x_event_block</summary>
			public Byte PM1EventLength;
			/// <summary>Byte Length of ports at pm1x_control_block</summary>
			public Byte PM1ControlLength;
			/// <summary>Byte Length of ports at pm2_control_block</summary>
			public Byte PM2ControlLength;
			/// <summary>Byte Length of ports at pm_timer_block</summary>
			public Byte PMTimerLength;
			/// <summary>Byte Length of ports at gpe0_block</summary>
			public Byte GPE0BlockLength;
			/// <summary>Byte Length of ports at gpe1_block</summary>
			public Byte GPE1BlockLength;
			/// <summary>Offset in GPE number space where GPE1 events start</summary>
			public Byte GPE1Base;
			/// <summary>Support for the _CST object and C-States change notification</summary>
			public Byte CStateControl;
			/// <summary>Worst case HW latency to enter/exit C2 state</summary>
			public UInt16 WorstC2Latency;
			/// <summary>Worst case HW latency to enter/exit C3 state</summary>
			public UInt16 WorstC3Latency;
			/// <summary>Processor memory cache line width, in bytes</summary>
			public UInt16 FlushSize;
			/// <summary>Number of flush strides that need to be read</summary>
			public UInt16 FlushStride;
			/// <summary>Processor duty cycle index in processor P_CNT reg</summary>
			public Byte DutyOffset;
			/// <summary>Processor duty cycle value bit width in P_CNT register</summary>
			public Byte DutyWidth;
			/// <summary>Index to day-of-month alarm in RTC CMOS RAM</summary>
			public Byte DayAlarm;
			/// <summary>Index to month-of-year alarm in RTC CMOS RAM</summary>
			public Byte MonthAlarm;
			/// <summary>Index to century in RTC CMOS RAM</summary>
			public Byte Century;
			/// <summary>IA-PC Boot Architecture Flags (see below for individual flags)</summary>
			/// <remarks>Reserved in ACPI 1.0; used since ACPI 2.0+</remarks>
			public UInt16 BootArchitectureFlags;
			/// <summary>Reserved, must be zero</summary>
			public Byte Reserved2;
			/// <summary>Miscellaneous flag bits (see below for individual flags)</summary>
			public UInt32 Flags;
			/// <summary>64-bit address of the Reset register</summary>
			public acpi_generic_address ResetRegister;
			/// <summary>Value to write to the reset_register port to reset the system</summary>
			public Byte ResetValue;
			/// <summary>Reserved, must be zero</summary>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] 
			public Byte[] Reserved4;
			/// <summary>64-bit physical address of FACS</summary>
			public UInt64 X_FirmwareControl;
			/// <summary>64-bit physical address of DSDT</summary>
			public UInt64 X_Dsdt;
			/// <summary>64-bit Extended Power Mgt 1a Event Reg Blk address</summary>
			public acpi_generic_address X_PM1aEventBlock;
			/// <summary>64-bit Extended Power Mgt 1b Event Reg Blk address</summary>
			public acpi_generic_address X_PM1bEventBlock;
			/// <summary>64-bit Extended Power Mgt 1a Control Reg Blk address</summary>
			public acpi_generic_address X_PM1aControlBlock;
			/// <summary>64-bit Extended Power Mgt 1b Control Reg Blk address</summary>
			public acpi_generic_address X_PM1bControlBlock;
			/// <summary>64-bit Extended Power Mgt 2 Control Reg Blk address</summary>
			public acpi_generic_address X_PM2ControlBlock;
			/// <summary>64-bit Extended Power Mgt Timer Ctrl Reg Blk address</summary>
			public acpi_generic_address X_PMTimerBlock;
			/// <summary>64-bit Extended General Purpose Event 0 Reg Blk address</summary>
			public acpi_generic_address X_GPE0Block;
			/// <summary>64-bit Extended General Purpose Event 1 Reg Blk address</summary>
			//apublic acpi_generic_address X_GPE1Block;
			/// <summary>64-bit Sleep Control register (ACPI 5.0)</summary>
			//public acpi_generic_address sleep_control;
			/// <summary>64-bit Sleep Status register (ACPI 5.0)</summary>
			//public acpi_generic_address sleep_status;
		}
	}
}