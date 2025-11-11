using System;
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
			// <summary>Root System Description Table</summary>
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
			/// <summary>Banana banana banana</summary>
			RTMA= 0x414D5452,
			/// <summary>Banana banana banana</summary>
			OEML = 0x4C4D454F,
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
				/// <summary>Unspecified power management profile</summary>
				Unspecified = 0,
				/// <summary>Power management profile for desktop</summary>
				Desktop = 1,
				/// <summary>Power management profile for mobile</summary>
				Mobile = 2,
				/// <summary>Power management profile for workstation</summary>
				Workstation = 3,
				/// <summary>Power management profile for enterprise server</summary>
				EnterpriseServer = 4,
				/// <summary>Power management profile for SOHO server</summary>
				SohoServer = 5,
				/// <summary>Power management profile for Appliance PC</summary>
				AplliancePC = 6,
				/// <summary>Power management profile for performance server</summary>
				PerformanceServer = 7,
				/// <summary>Power management profile for tablet</summary>
				Tabled = 8,
			}

			/// <summary>Masks for FADT Boot Architecture Flags (boot_flags) [Vx]=Introduced in this FADT revision</summary>
			[Flags]
			public enum BootArchitectureFlags : UInt16
			{
				/// <summary>[V2] System has LPC or ISA bus devices</summary>
				LEGACY_DEVICES = 1<<0,
				/// <summary>[V3] System has an 8042 controller on port 60/64</summary>
				_8042 = 1<<1,
				/// <summary>[V4] It is not safe to probe for VGA hardware</summary>
				NO_VGA = 1<<2,
				/// <summary>[V4] Message Signaled Interrupts (MSI) must not be enabled</summary>
				NO_MSI = 1<<3,
				/// <summary>[V4] PCIe ASPM control must not be enabled</summary>
				NO_ASPM = 1<<4,
				/// <summary>[V5] No CMOS real-time clock present</summary>
				NO_CMOS_RTC = 1<<5,
			}

			/// <summary>Masks for FADT flags</summary>
			[Flags]
			public enum FadtFlags : UInt32
			{
				/// <summary>[V1] The WBINVD instruction works properly</summary>
				WBINVD = 1<<0,
				/// <summary>[V1] WBINVD flushes but does not invalidate caches</summary>
				WBINVD_FLUSH = 1<<1,
				/// <summary>[V1] All processors support C1 state</summary>
				C1_SUPPORTED = 1<<2,
				/// <summary>[V1] C2 state works on MP system</summary>
				C2_MP_SUPPORTED = 1<<3,
				/// <summary>[V1] Power button is handled as a control method device</summary>
				POWER_BUTTON = 1<<4,
				/// <summary>[V1] Sleep button is handled as a control method device</summary>
				SLEEP_BUTTON = 1<<5,
				/// <summary>[V1] RTC wakeup status is not in fixed register space</summary>
				FIXED_RTC = 1<<6,
				/// <summary>[V1] RTC alarm can wake system from S4</summary>
				S4_RTC_WAKE = 1<<7,
				/// <summary>[V1] ACPI timer width is 32-bit (0=24-bit)</summary>
				_32BIT_TIMER = 1<<8,
				/// <summary>[V1] Docking supported</summary>
				DOCKING_SUPPORTED = 1<<9,
				/// <summary>[V2] System reset via the FADT RESET_REG supported</summary>
				RESET_REGISTER = 1<<10,
				/// <summary>[V3] No internal expansion capabilities and case is sealed</summary>
				SEALED_CASE = 1<<11,
				/// <summary>[V3] No local video capabilities or local input devices</summary>
				HEADLESS = 1<<12,
				/// <summary>[V3] Must execute native instruction after writing SLP_TYPx register</summary>
				SLEEP_TYPE = 1<<13,
				/// <summary>[V4] System supports PCIEXP_WAKE (STS/EN) bits (ACPI 3.0)</summary>
				PCI_EXPRESS_WAKE = 1<<14,
				/// <summary>[V4] OSPM should use platform-provided timer (ACPI 3.0)</summary>
				PLATFORM_CLOCK = 1<<15,
				/// <summary>[V4] Contents of RTC_STS valid after S4 wake (ACPI 3.0)</summary>
				S4_RTC_VALID = 1<<16,
				/// <summary>[V4] System is compatible with remote power on (ACPI 3.0)</summary>
				REMOTE_POWER_ON = 1<<17,
				/// <summary>[V4] All local APICs must use cluster model (ACPI 3.0)</summary>
				APIC_CLUSTER = 1<<18,
				/// <summary>[V4] All local xAPICs must use physical dest mode (ACPI 3.0)</summary>
				APIC_PHYSICAL = 1<<19,
				/// <summary>[V5] ACPI hardware is not implemented (ACPI 5.0)</summary>
				HW_REDUCED = 1<<20,
				/// <summary>[V5] S0 power savings are equal or better than S3 (ACPI 5.0)</summary>
				LOW_POWER_S0 = 1<<21, 
			}

			/// <summary>Physical memory address of the FACS, where OSPM and Firmware exchange control information.</summary>
			/// <remarks>
			/// Since ACPI 2.0 a new field has been added to the table, <see cref="X_FirmwareControl"/> of type GAS, which is 64-bits wide.
			/// Only one of the two fields is used, the other contains 0.
			/// According to the Specs, the <see cref="X_FirmwareControl"/> is used only when the FACS is placed above the 4th GB.
			/// </remarks>
			public UInt32 FirmwareCtrl;
			/// <summary>Physical memory address of the DSDT.</summary>
			/// <remarks>If the <see cref="X_Dsdt"/> field contains a non-zero value which can be used by the OSPM, then this field must be ignored by the OSPM.</remarks>
			public UInt32 Dsdt;
			/// <summary>ACPI 1.0 defined this offset as a field named INT_MODEL, which was eliminated in ACPI 2.0.</summary>
			/// <remarks>Platforms should set this field to zero but field values of one are also allowed to maintain compatibility with ACPI 1.0.</remarks>
			public Byte model;
			/// <summary>
			/// This field is set by the OEM to convey the preferred power management profile to OSPM.
			/// OSPM can use this field to set default power management policy parameters during OS installation.
			/// </summary>
			/// <remarks>For example if it contains 2, the computer is a laptop and you should configure power management in power saving mode.</remarks>
			public PreferredPowerManagementType PreferredPowerManagementProfile;
			/// <summary>The System Control Interrupt is used by ACPI to notify the OS about fixed events, such as for example, pressing the power button, or for General Purpose Events (GPEs), which are firmware specific</summary>
			/// <remarks>
			/// This member in the FADT structure indicates the PIC or IOAPIC interrupt pin for it.
			/// To know if it's a PIC IRQ, check if the dual 8259 interrupt controllers are present via the MADT.
			/// Otherwise, it's a GSI.
			/// If you are using the IOAPIC and the PIC is present, remember to check the Interrupt Source Overrides first to get the GSI associated with the IRQ source.
			/// On systems that do not contain the 8259, this field contains the Global System interrupt number of the SCI interrupt. OSPM is required to treat the ACPI SCI interrupt as a shareable, level, active low interrupt.
			/// </remarks>
			public UInt16 SCI_Interrupt;
			/// <summary>
			/// System port address of the SMI Command Port.
			/// During ACPI OS initialization, OSPM can determine that the ACPI hardware registers are owned by SMI (by way of the SCI_EN bit), in which case the ACPI OS issues the ACPI_ENABLE command to the SMI_CMD port.
			/// The SCI_EN bit effectively tracks the ownership of the ACPI hardware registers.
			/// OSPM issues commands to the SMI_CMD port synchronously from the boot processor.</summary>
			/// <remarks>
			/// This is where the OS writes AcpiEnable or AcpiDisable to get or release the ownership over the ACPI registers.
			/// This field is reserved and must be zero on system that does not support System Management mode.
			/// </remarks>
			public UInt32 SMI_CommandPort;
			/// <summary>
			/// The value to write to SMI_CMD to disable SMI ownership of the ACPI hardware registers.
			/// The last action SMI does to relinquish ownership is to set the SCI_EN bit.
			/// </summary>
			/// <remarks>
			/// During the OS initialization process, OSPM will synchronously wait for the ntransfer of SMI ownership to complete, so the ACPI system releases SMI ownership as quickly as possible.
			/// This field is reserved and must be zero on systems that do not support Legacy Mode.
			/// </remarks>
			public Byte AcpiEnable;
			/// <summary>The value to write to SMI_CMD to re-enable SMI ownership of the ACPI hardware registers.</summary>
			/// <remarks>
			/// This can only be done when ownership was originally acquired from SMI by OSPM using ACPI_ENABLE.
			/// An OS can hand ownership back to SMI by relinquishing use to the ACPI hardware registers, masking off all SCI interrupts, clearing the SCI_EN bit and then writing ACPI_DISABLE to the SMI_CMD port from the boot processor.
			/// This field is reserved and must be zero on systems that do not support Legacy Mode.
			/// </remarks>
			public Byte AcpiDisable;
			/// <summary>Value to write to SMI_CMD to enter S4BIOS state</summary>
			/// <remarks>
			/// The S4BIOS state provides an alternate way to enter the S4 state where the firmware saves and restores the memory context.
			/// A value of zero in S4BIOS_F indicates S4BIOS_REQ is not supported.
			/// </remarks>
			public Byte S4BIOS_REQ;
			/// <summary>If non-zero, this field contains the value OSPM writes to the SMI_CMD register to assume processor performance state control responsibility.</summary>
			public Byte PSTATE_Control;
			/// <summary>System port address of the PM1a Event Register Block</summary>
			/// <remarks>
			/// This is a required field.
			/// If the <see cref="X_PM1aControlBlock"/> field contains a non zero value which can be used by the OSPM, then this field must be ignored by the OSPM.
			/// </remarks>
			public UInt32 PM1aEventBlock;
			/// <summary>System port address of the PM1b Event Register Block</summary>
			/// <remarks>
			/// This field is optional; if this register block is not supported, this field contains zero.
			/// If the <see cref="X_PM1bControlBlock"/> field contains a non zero value which can be used by the OSPM, then this field must be ignored by the OSPM.
			/// </remarks>
			public UInt32 PM1bEventBlock;
			/// <summary>System port address of the PM1a Control Register Block</summary>
			/// <remarks>
			/// This is a required field.
			/// If the <see cref="X_PM1aControlBlock"/> field contains a non zero value which can be used by the OSPM, then this field must be ignored by the OSPM.
			/// </remarks>
			public UInt32 PM1aControlBlock;
			/// <summary>System port address of the PM1b Control Register Block</summary>
			/// <remarks>
			/// This field is optional; if this register block is not supported, this field contains zero.
			/// If the <see cref="X_PM1bControlBlock"/> field contains a non zero value which can be used by the OSPM, then this field must be ignored by the OSPM.
			/// </remarks>
			public UInt32 PM1bControlBlock;
			/// <summary>System port address of the PM2 Control Register Block</summary>
			/// <remarks>
			/// This field is optional; if this register block is not supported, this field contains zero.
			/// If the X_PM2_CNT_BLK field contains a non zero value which can be used by the OSPM, then this field must be ignored by the OSPM.
			/// </remarks>
			public UInt32 PM2ControlBlock;
			/// <summary>System port address of the Power Management Timer Control Register Block</summary>
			/// <remarks>
			/// This is an optional field; if this register block is not supported, this field contains zero.
			/// If the X_PM_TMR_BLK field contains a non-zero value which can be used by the OSPM, then this field must be ignored by the OSPM
			/// </remarks>
			public UInt32 PMTimerBlock;
			/// <summary>System port address of General-Purpose Event 0 Register Block</summary>
			/// <remarks>
			/// If this register block is not supported, this field contains zero.
			/// If the X_GPE0_BLK field contains a nonzero value which can be used by the OSPM, then this field must be ignored by the OSPM.
			/// </remarks>
			public UInt32 GPE0Block;
			/// <summary>System port address of General-Purpose Event 1 Register Block</summary>
			/// <remarks>
			/// This is an optional field; if this register block is not supported, this field contains zero.
			/// If the X_GPE1_BLK field contains a nonzero value which can be used by the OSPM, then this field must be ignored by the OSPM.
			/// </remarks>
			public UInt32 GPE1Block;
			/// <summary>Number of bytes decoded by PM1a_EVT_BLK and, if supported, PM1b_ EVT_BLK. This value is >= 4.</summary>
			public Byte PM1EventLength;
			/// <summary>Number of bytes decoded by PM1a_CNT_BLK and, if supported, PM1b_CNT_BLK. This value is >= 2.</summary>
			public Byte PM1ControlLength;
			/// <summary>Number of bytes decoded by PM2_CNT_BLK</summary>
			/// <remarks>
			/// Support for the PM2 register block is optional.
			/// If supported, this value is >= 1.
			/// If not supported, this field contains zero.
			/// </remarks>
			public Byte PM2ControlLength;
			/// <summary>Number of bytes decoded by PM_TMR_BLK</summary>
			/// <remarks>
			/// If the PM Timer is supported, this field’s value must be 4.
			/// If not supported, this field contains zero.
			/// </remarks>
			public Byte PMTimerLength;
			/// <summary>The length of the register whose address is given by X_GPE0_BLK (if nonzero) or by GPE0_BLK (otherwise) in bytes</summary>
			/// <remarks>The value is a non-negative multiple of 2</remarks>
			public Byte GPE0BlockLength;
			/// <summary>The length of the register whose address is given by X_GPE1_BLK (if nonzero) or by GPE1_BLK (otherwise) in bytes</summary>
			/// <remarks>The value is a non-negative multiple of 2</remarks>
			public Byte GPE1BlockLength;
			/// <summary>Offset within the ACPI general-purpose event model where GPE1 based events start</summary>
			public Byte GPE1Base;
			/// <summary>If non-zero, this field contains the value OSPM writes to the SMI_CMD register to indicate OS support for the _CST object and C States Changed notification</summary>
			public Byte CStateControl;
			/// <summary>The worst-case hardware latency, in microseconds, to enter and exit a C2 state</summary>
			/// <remarks>A value > 100 indicates the system does not support a C2 state</remarks>
			public UInt16 WorstC2Latency;
			/// <summary>The worst-case hardware latency, in microseconds, to enter and exit a C3 state</summary>
			/// <remarks>A value > 1000 indicates the system does not support a C3 state</remarks>
			public UInt16 WorstC3Latency;
			/// <summary>
			/// If WBINVD=0, the value of this field is the number of flush strides that need to be read (using cacheable addresses) to completely flush dirty lines from any processor’s memory caches.
			/// Notice that the value in FLUSH_STRIDE is typically the smallest cache line width on any of the processor’s caches (for more information, see the FLUSH_STRIDE field definition).
			/// If the system does not support a method for flushing the processor’s caches, then FLUSH_SIZE and WBINVD are set to zero.
			/// Notice that this method of flushing the processor caches has limitations, and WBINVD=1 is the preferred way to flush the processors caches.
			/// This value is typically at least 2 times the cache size.
			/// The maximum allowed value for FLUSH_SIZE multiplied by FLUSH_STRIDE is 2 MB for a typical maximum supported cache size of 1 MB.
			/// Larger cache sizes are supported using WBINVD=1.
			/// This value is ignored if WBINVD=1.
			/// This field is maintained for ACPI 1.0 processor compatibility on existing systems.
			/// Processors in new ACPI-compatible systems are required to support the WBINVD function and indicate this to OSPM by setting the WBINVD field = 1.
			/// </summary>
			public UInt16 FlushSize;
			/// <summary>
			/// If WBINVD=0, the value of this field is the cache line width, in bytes, of the processor’s memory caches.
			/// This value is typically the smallest cache line width on any of the processor’s caches.
			/// For more information, see the description of the FLUSH_SIZE field.
			/// This value is ignored if WBINVD=1.
			/// This field is maintained for ACPI 1.0 processor compatibility on existing systems.
			/// Processors in new ACPI-compatible systems are required to support the WBINVD function and indicate this to OSPM by setting the WBINVD field = 1.
			/// </summary>
			public UInt16 FlushStride;
			/// <summary>The zero-based index of where the processor’s duty cycle setting is within the processor’s P_CNT register</summary>
			public Byte DutyOffset;
			/// <summary>
			/// The bit width of the processor’s duty cycle setting value in the P_CNT register.
			/// Each processor’s duty cycle setting allows the software to select a nominal processor frequency below its absolute frequency as defined by: THTL_EN = 1 BF * DC/(2DUTY_WIDTH) Where: BF-Base frequency DC-Duty cycle setting When THTL_EN is 0, the processor runs at its absolute BF.
			/// A DUTY_WIDTH value of 0 indicates that processor duty cycle is not supported and the processor continuously runs at its base frequency.
			/// </summary>
			public Byte DutyWidth;
			/// <summary>The RTC CMOS RAM index to the day-of-month alarm value</summary>
			/// <remarks>
			/// If this field contains a zero, then the RTC day of the month alarm feature is not supported.
			/// If this field has a non-zero value, then this field contains an index into RTC RAM space that OSPM can use to program the day of the month alarm
			/// </remarks>
			public Byte DayAlarm;
			/// <summary>The RTC CMOS RAM index to the month of year alarm value</summary>
			/// <remarks>
			/// If this field contains a zero, then the RTC month of the year alarm feature is not supported.
			/// If this field has a non-zero value, then this field contains an index into RTC RAM space that OSPM can use to program the month of the year alarm.
			/// If this feature is supported, then the DAY_ALRM feature must be supported also
			/// </remarks>
			public Byte MonthAlarm;//https://uefi.org/specs/ACPI/6.5/05_ACPI_Software_Programming_Model.html#fixed-acpi-description-table-fadt
			/// <summary>Index to century in RTC CMOS RAM</summary>
			public Byte Century;
			/// <summary>IA-PC Boot Architecture Flags</summary>
			/// <remarks>Reserved in ACPI 1.0; used since ACPI 2.0+</remarks>
			public BootArchitectureFlags BootArchitecture;
			/// <summary>Reserved, must be zero</summary>
			public Byte Reserved2;
			/// <summary>Miscellaneous flag bits</summary>
			public FadtFlags Flags;
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
			// <summary>64-bit Extended General Purpose Event 1 Reg Blk address</summary>
			//apublic acpi_generic_address X_GPE1Block;
			// <summary>64-bit Sleep Control register (ACPI 5.0)</summary>
			//public acpi_generic_address sleep_control;
			// <summary>64-bit Sleep Status register (ACPI 5.0)</summary>
			//public acpi_generic_address sleep_status;
		}

		/// <summary>MCFG - PCI Express Memory Mapped Configuration Space Base Address Description Table</summary>
		/// <remarks>
		/// This table describes the base address of the PCI Express memory-mapped configuration space.
		/// The memory-mapped configuration space is used for accessing PCI Express configuration registers.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential)]
		public struct Mcfg
		{
			/// <summary>Reserved - must be zero</summary>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public Byte[] Reserved;

			public Boolean IsValid => this.Reserved != null && Array.TrueForAll(this.Reserved, b => b == 0);

			// Variable-length array of allocation structures follows
			// Each allocation structure is 16 bytes and describes one PCI segment group
		}

		/// <summary>MCFG Allocation Structure</summary>
		/// <remarks>
		/// Each structure describes the base address and segment group for a contiguous range of PCI buses.
		/// Multiple allocation structures may exist to describe multiple PCI segment groups.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct McfgAllocation
		{
			/// <summary>Base Address of Enhanced Configuration Mechanism</summary>
			/// <remarks>
			/// 64-bit base address of the memory-mapped configuration space.
			/// This address is the base for the memory-mapped configuration space for the PCI segment group.
			/// </remarks>
			public UInt64 BaseAddress;

			/// <summary>PCI Segment Group Number</summary>
			/// <remarks>
			/// Segment group number as defined in the PCI Firmware Specification.
			/// For platforms with fewer than 255 PCI buses, this is 0.
			/// </remarks>
			public UInt16 PciSegmentGroupNumber;

			/// <summary>Start PCI Bus Number</summary>
			/// <remarks>
			/// First PCI bus number decoded by this host bridge.
			/// Start bus number in this segment group's memory-mapped configuration space.
			/// </remarks>
			public Byte StartBusNumber;

			/// <summary>End PCI Bus Number</summary>
			/// <remarks>
			/// Last PCI bus number decoded by this host bridge.
			/// End bus number in this segment group's memory-mapped configuration space.
			/// </remarks>
			public Byte EndBusNumber;

			/// <summary>Reserved - must be zero</summary>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public Byte[] Reserved;
		}
	}
}