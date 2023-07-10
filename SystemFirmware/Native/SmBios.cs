using System;
using System.Runtime.InteropServices;

namespace AlphaOmega.Debug.Native
{
	/// <example>https://www.dmtf.org/standards/smbios</example>
	public static class SmBios
	{
		/// <summary>System Management BIOS (SMBIOS) Reference Specification</summary>
		public enum Type : byte
		{
			/// <summary>BIOS Information (Type 0)</summary>
			/// <remarks>
			/// One and only one structure is present in the structure-table.
			/// BIOS Version and BIOS Release Date strings are non-null; the date field uses a 4-digit year (for example, 1999).
			/// All other fields reflect full BIOS support information.
			/// </remarks>
			Bios = 0,
			/// <summary>System Information (Type 1)</summary>
			/// <remarks>
			/// Manufacturer and Product Name strings are non-null. UUID field identifies the system’s non-zero UUID value.
			/// Wake-up Type field identifies the wake-up source and cannot be Unknown. One and only one structure is present in the structure-table.
			/// </remarks>
			System = 1,
			/// <summary>Baseboard (or Module) Information (Type 2)</summary>
			Baseboard = 2,
			/// <summary>System Enclosure or Chassis (Type 3)</summary>
			/// <remarks>Manufacturer string is non-null; the Type field identifies the type of enclosure. (Unknown is disallowed.)</remarks>
			Chassis = 3,
			/// <summary>Processor Information (Type 4)</summary>
			/// <remarks>
			/// One structure is required for each system processor.
			/// The presence of two structures with the Processor Type field set to Central Processor, for instance, identifies that the system is capable of dual-processor operations.
			/// 
			/// Socket Designation string is non-null.
			/// Processor Type, Max Speed, and Processor Upgrade fields are all set to “known” values (that is, the Unknown value is disallowed for each field).
			/// 
			/// If the associated processor is present (that is, the CPU Socket Populated sub-field of the Status field indicates that the socket is populated),
			/// the Processor Manufacturer string is non-null and the Processor Family, Current Speed, and CPU Status sub-field of the Status field are all set to “known” values.
			/// 
			/// Each of the Lx Cache Handle fields is either set to 0xFFFF (no further cache description) or references a valid Cache Information structure.
			/// </remarks>
			Processor = 4,
			/// <summary>Memory Controller Information (Type 5, Obsolete)</summary>
			MemoryController = 5,
			/// <summary>Memory Module Information (Type 6, Obsolete)</summary>
			MemoryModule = 6,
			/// <summary>Cache Information (Type 7)</summary>
			/// <remarks>
			/// One structure is required for each cache that is external to the processor.
			/// 
			/// Socket Designation string is non-null if the cache is external to the processor.
			/// If the cache is present (that is, the Installed Size is non-zero), the Cache Configuration field is set to a “known” value (that is, the Unknown value is disallowed).
			/// </remarks>
			Cache = 7,
			/// <summary>Port Connector Information (Type 8)</summary>
			PortConnector = 8,
			/// <summary>System Slots (Type 9)</summary>
			/// <remarks>
			/// One structure is required for each upgradeable system slot.
			/// A structure is not required if the slot must be populated for proper system operation (for example, the system contains a single memory-card slot).
			/// 
			/// Slot Designation string is non-null.
			/// Slot Type, Slot Data Bus Width, Slot ID, and Slot Characteristics 1 &amp; 2 are all set to “known” values.
			/// 
			/// If device presence is detectable within the slot (for example, PCI), the Current Usage field must be set to either Available or In-use.
			/// Otherwise (for example, ISA), the Unknown value for the field is also allowed.
			/// </remarks>
			SystemSlots = 9,
			/// <summary>On Board Devices Information (Type 10, Obsolete)</summary>
			OnBoardDevices = 10,
			/// <summary>OEM Strings (Type 11)</summary>
			OEMStrings = 11,
			/// <summary>System Configuration Options (Type 12)</summary>
			SystemConfigurationOptions = 12,
			/// <summary>BIOS Language Information (Type 13)</summary>
			BiosLanguage = 13,
			/// <summary>Group Associations (Type 14)</summary>
			GroupAssociations = 14,
			/// <summary>System Event Log (Type 15)</summary>
			SystemEventLog = 15,
			/// <summary>Physical Memory Array (Type 16)</summary>
			/// <remarks>
			/// One structure is required for the system memory.
			/// 
			/// Location, Use, and Memory Error Correction are all set to “known” values.
			/// Either Maximum Capacity or Extended Maximum Capacity must be set to a known, non-zero value.
			/// Number of Memory Devices is non-zero and identifies the number of Memory Device structures that are associated with this Physical Memory Array.
			/// </remarks>
			PhysicalMemoryArray = 16,
			/// <summary>Memory Device (Type 17)</summary>
			/// <remarks>
			/// One structure is required for each socketed system-memory device, whether or not the socket is currently populated;
			/// if the system includes soldered system-memory, one additional structure is required to identify that memory device. 
			/// 
			/// Device Locator string is set to a non-null value.
			/// Memory Array Handle contains the handle associated with the Physical Memory Array structure to which this device belongs.
			/// Data Width, Size, Form Factor, and Device Set are all set to “known” values.
			/// If the device is present (for instance, Size is non-zero), the Total Width field is not set to 0xFFFF (Unknown).
			/// </remarks>
			MemoryDevice = 17,
			/// <summary>32-Bit Memory Error Information (Type 18)</summary>
			MemoryError32Bit = 18,
			/// <summary>Memory Array Mapped Address (Type 19)</summary>
			/// <remarks>
			/// One structure is required for each contiguous block of memory addresses mapped to a Physical Memory Array.
			/// 
			/// Either the pair of Starting Address and Ending Address is set to a valid address range or the pair of Extended Starting Address and Extended Ending Address is set to a valid address range.
			/// If the pair of Starting Address and Ending Address is used, Ending Address must be larger than Starting Address.
			/// If the pair of Extended Starting Address and Extended Ending Address is used, Extended Ending Address must be larger than Extended Starting Address.
			/// Each structure’s address range is unique and non-overlapping.
			/// Memory Array Handle references a Physical Memory Array structure.
			/// Partition Width is non-zero.
			/// </remarks>
			MemoryArrayMappedAddress = 19,
			/// <summary>Memory Device Mapped Address (Type 20)</summary>
			MemoryDeviceMappedAddress = 20,
			/// <summary>Built-in Pointing Device (Type 21)</summary>
			BuiltInPointingDevice = 21,
			/// <summary>Portable Battery (Type 22)</summary>
			PortableBattery = 22,
			/// <summary>System Reset (Type 23)</summary>
			SystemReset = 23,
			/// <summary>Hardware Security (Type 24)</summary>
			HardwareSecurity = 24,
			/// <summary>System Power Controls (Type 25)</summary>
			SystemPowerControls = 25,
			/// <summary>Voltage Probe (Type 26)</summary>
			VoltageProbe = 26,
			/// <summary>Cooling Device (Type 27)</summary>
			CoolingDevice = 27,
			/// <summary>Temperature Probe (Type 28)</summary>
			TemperatureProbe = 28,
			/// <summary>Electrical Current Probe (Type 29)</summary>
			ElectricalCurrentProbe = 29,
			/// <summary> Out-of-Band Remote Access (Type 30)</summary>
			OobRemoteAccess = 30,
			/// <summary>Boot Integrity Services (BIS) Entry Point (Type 31)</summary>
			BisEntryPoint = 31,
			/// <summary>System Boot Information (Type 32)</summary>
			/// <remarks>Structure’s length is at least 0x0B (for instance, at least one byte of System Boot Status is provided).</remarks>
			SystemBootInformation = 32,
			/// <summary>64-Bit Memory Error Information (Type 33)</summary>
			MemoryError64Bit = 33,
			/// <summary>Management Device (Type 34)</summary>
			ManagementDevice = 34,
			/// <summary>Management Device Component (Type 35)</summary>
			ManagementDeviceComponent = 35,
			/// <summary>Management Device Threshold Data (Type 36)</summary>
			ManagementDeviceThresholdData = 36,
			/// <summary>Memory Channel (Type 37)</summary>
			MemoryChannel = 37,
			/// <summary>IPMI Device Information (Type 38)</summary>
			IPMIDeviceInformation = 38,
			/// <summary>System Power Supply (Type 39)</summary>
			SystemPowerSupply = 39,
			/// <summary>Additional Information (Type 40)</summary>
			AdditionalInformation = 40,
			/// <summary>Onboard Devices Extended Information (Type 41)</summary>
			OnboardDevicesExtended = 41,
			/// <summary>Management Controller Host Interface (Type 42)</summary>
			ManagementControllerHostInterface = 42,
			/// <summary>TPM Device (Type 43)</summary>
			TpmDevice = 43,
			/// <summary>Processor Additional Information (Type 44)</summary>
			ProcessorAdditionalInformation = 44,
			/// <summary>Inactive (Type 126)</summary>
			Inactive = 126,
			/// <summary>End-of-Table (Type 127)</summary>
			EoT = 127,
		}

		/// <summary>System Enclosure or Chassis Security Status field</summary>
		public enum ChasisSecurityStatus : byte
		{
			/// <summary>Other</summary>
			Other = 0x01,
			/// <summary>Unknown</summary>
			Unknown = 0x02,
			/// <summary>None</summary>
			None = 0x03,
			/// <summary>External interface locked out</summary>
			ExternalInterfaceLockedOut = 0x04,
			/// <summary>External interface enabled</summary>
			ExternalInterfaceEnabled = 0x05,
		}

		/// <summary>SMBIOS Header</summary>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct RawSMBIOSHeader
		{
			/// <summary>Used 2.0 Calling method</summary>
			public Byte Used20CallingMethod;
			/// <summary>SMBIOS Major version</summary>
			public Byte SMBIOSMajorVersion;
			/// <summary>SMBIOS Minor version</summary>
			public Byte SMBIOSMinorVersion;
			/// <summary>DMI Revision</summary>
			public Byte DmiRevision;
			/// <summary>Length</summary>
			public UInt32 Length;
			/// <summary>SMBIOS version</summary>
			public Version SMBIOSVersion { get { return new Version(this.SMBIOSMajorVersion, this.SMBIOSMinorVersion); } }
			//[MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
			//public Byte[] SMBIOSTableData;
		}

		/// <summary>SMBIOS unified type header</summary>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Header
		{
			/// <summary>Specifies the type of structure</summary>
			/// <remarks>
			/// Types 0 through 127 (0x7f) are reserved for and defined by this specification.
			/// Types 128 through 256 (0x80 to 0xff) are available for system- and OEM-specific information
			/// </remarks>
			public Type Type;
			/// <summary>Specifies the length of the formatted area of the structure, starting at the Type field</summary>
			/// <remarks>The length of the structure’s string-set is not included.</remarks>
			public Byte Length;
			/// <summary>Specifies the structure’s handle, a unique 16-bit number in the range 0 to 0x0fffe (for version 2.0) or 0 to 0x0feff (for version 2.1 and later)</summary>
			/// <remarks>
			/// The handle can be used with the Get SMBIOS Structure function to retrieve a specific structure; the handle numbers are not required to be contiguous.
			/// For version 2.1 and later, handle values in the range 0x0ff00 to 0x0ffff are reserved for use by this specification.[1] 
			/// 
			/// If the system configuration changes, a previously assigned handle might no longer exist.
			/// However, after a handle has been assigned by the BIOS, the BIOS cannot re-assign that handle number to another structure.
			/// </remarks>
			public Int16 Handle;
		}

		/// <summary>BIOS Information (Type 0)</summary>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type0
		{
			/// <summary>Array of BIOS characteristics supported by the system as defined by the System Management BIOS Reference Specification</summary>
			[Flags]
			public enum BiosCharacteristics : ulong
			{
				/// <summary>Reserved</summary>
				Reserved0 = 1L << 0,
				/// <summary>Reserved</summary>
				Reserved1 = 1L << 1,
				/// <summary>Unknown</summary>
				Unknown2 = 1L << 2,
				/// <summary>BIOS Characteristics are not supported</summary>
				BIOSCharacteristicsNotSupported = 1L << 3,
				/// <summary>ISA is supported</summary>
				IsaSupported = 1L << 4,
				/// <summary>MCA is supported</summary>
				McaSupported = 1L << 5,
				/// <summary>EISA is supported</summary>
				EisaSupported = 1L << 6,
				/// <summary>PCI is supported</summary>
				PciSupported = 1L << 7,
				/// <summary>PC Card (PCMCIA) is supported</summary>
				PcmciaSupported = 1L << 8,
				/// <summary>Plug and Play is supported</summary>
				PnPSupported = 1L << 9,
				/// <summary>APM is supported</summary>
				ApmSupported = 1L << 10,
				/// <summary>BIOS is Upgradeable (Flash)</summary>
				BiosUpgradeable = 1L << 11,
				/// <summary>BIOS shadowing is allowed</summary>
				BiosShadowingAllowed = 1L << 12,
				/// <summary>VL-VESA is supported</summary>
				VL_VesaSupported = 1L << 13,
				/// <summary>ESCD support is available</summary>
				EscdSupportAvailable = 1L << 14,
				/// <summary>Boot from CD is supported</summary>
				BootFromCdSupported = 1L << 15,
				/// <summary>Selectable Boot is supported</summary>
				SelectableBootSupported = 1L << 16,
				/// <summary>BIOS ROM is socketed</summary>
				BiosRomSocketed = 1L << 17,
				/// <summary>Boot From PC Card (PCMCIA) is supported</summary>
				BootFromPcmciaSupported = 1L << 18,
				/// <summary>EDD (Enhanced Disk Drive) Specification is supported</summary>
				EddSpecificationSupported = 1L << 19,
				/// <summary>Int 13h - Japanese Floppy for NEC 9800 1.2mb (3.5\", 1k Bytes/Sector, 360 RPM) is supported</summary>
				Floppy35_Nec9800Supported = 1L << 20,
				/// <summary>Int 13h - Japanese Floppy for Toshiba 1.2mb (3.5\", 360 RPM) is supported</summary>
				Floppy35_ToshibaSupported = 1L << 21,
				/// <summary>Int 13h - 5.25\" / 360 KB Floppy Services are supported</summary>
				Floppy525_360KbSupported = 1L << 22,
				/// <summary>Int 13h - 5.25\" /1.2MB Floppy Services are supported</summary>
				Floppy525_12MbSupported = 1L << 23,
				/// <summary>Int 13h - 3.5\" / 720 KB Floppy Services are supported</summary>
				Floppy35_720KbSupported = 1L << 24,
				/// <summary>Int 13h - 3.5\" / 2.88 MB Floppy Services are supported</summary>
				Floppy35_288MbSupported = 1L << 25,
				/// <summary>Int 5h, Print Screen Service is supported</summary>
				ServicePrintScreenSupported = 1L << 26,
				/// <summary>Int 9h, 8042 Keyboard services are supported</summary>
				Keyboard8042Supported = 1L << 27,
				/// <summary>Int 14h, Serial Services are supported</summary>
				ServiceSerialSupported = 1L << 28,
				/// <summary>Int 17h, printer services are supported </summary>
				ServicePrinterSuppoerted = 1L << 29,
				/// <summary>Int 10h, CGA/Mono Video Services are supported</summary>
				ServiceMonoVideoSupported = 1L << 30,
				/// <summary>NEC PC-98</summary>
				NecPC_98 = 1L << 31,
				/// <summary>ACPI supported</summary>
				AcpiSupported = 1L << 32,
				/// <summary>USB Legacy is supported</summary>
				UsbLegacySupported = 1L << 33,
				/// <summary>AGP is supported</summary>
				AgpSupported = 1L << 34,
				/// <summary>I2O boot is supported</summary>
				BootI2OSupported = 1L << 35,
				/// <summary>LS-120 boot is supported</summary>
				BootLS_120Supported = 1L << 36,
				/// <summary>ATAPI ZIP Drive boot is supported</summary>
				BootAtapiZipDriveSupported = 1L << 37,
				/// <summary>1394 boot is supported</summary>
				Boot1394Supported = 1L << 38,
				/// <summary>Smart Battery supported</summary>
				SmartBatterySupported = 1L << 39,
			}

			/// <summary>BIOS size type</summary>
			public enum SizeType : byte
			{
				/// <summary>Size in megabytes</summary>
				Mb=0x00,
				/// <summary>Size in gigabytes</summary>
				Gb=0x01,
				/// <summary>Size in kilobytes</summary>
				Kb = 0x07,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>BIOS Vendor</summary>
			/// <remarks>String number of the BIOS Vendor’s Name</remarks>
			public Byte Vendor;
			/// <summary>BIOS Version</summary>
			/// <remarks>String number of the BIOS Version. This value is a free-form string that may contain Core and OEM version information</remarks>
			public Byte Version;
			/// <summary>BIOS Starting Address Segment</summary>
			/// <remarks>
			/// Segment location of BIOS starting address (for example, 0x0e800).
			/// The size of the runtime BIOS image can be computed by subtracting the Starting Address Segment from 0x10000 and multiplying the result by 16.
			/// </remarks>
			public UInt16 StartingAddrSeg;
			/// <summary>BIOS Release Date</summary>
			/// <remarks>
			/// String number of the BIOS release date.
			/// The date string, if supplied, is in either mm/dd/yy or mm/dd/yyyy format.
			/// If the year portion of the string is two digits, the year is assumed to be 19yy.
			/// The mm/dd/yyyy format is required for SMBIOS version 2.3 and late
			/// </remarks>
			public Byte ReleaseDate;
			/// <summary>BIOS ROM Size</summary>
			/// <remarks>
			/// Size (n) where 64K * (n+1) is the size of the physical device containing the BIOS, in bytes.
			/// 0xff - size is 16MB or greater, see <see cref="ExtendedBiosRomSize"/> for actual size
			/// </remarks>
			private Byte _RomSize;
			/// <summary>BIOS Characteristics</summary>
			/// <remarks>Defines which functions the BIOS supports: PCI, PCMCIA, Flash, etc.</remarks>
			public BiosCharacteristics Characteristics;
			/// <summary>BIOS Characteristics Extension Bytes</summary>
			/// <remarks>
			/// Optional space reserved for future supported functions.
			/// The number of Extension Bytes that is present is indicated by the Length in offset 1 minus 0x12.
			/// For version 2.4 and later implementations, two BIOS Characteristics Extension Bytes are defined (0x12-0x13) and bytes 0x14-0x17 are also defined.
			/// </remarks>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public Byte[] CharacteristicsExtension;
			/// <summary>Identifies the major release of the System BIOS; for example, the value is 0x0a for revision 10.22 and 0x02 for revision 2</summary>
			/// <remarks>
			/// This field or the System BIOS Minor Release field or both are updated each time a System BIOS update for a given system is released.
			/// If the system does not support the use of this field, the value is 0FFh for both this field and the System BIOS Minor Release field.
			/// </remarks>
			private Byte MajorRelease;
			/// <summary>Identifies the minor release of the System BIOS; for example, the value is 0x16 for revision 10.22 and 0x01 for revision 2.1</summary>
			private Byte MinorRelease;
			/// <summary>Embedded Controller Firmware Major Release</summary>
			/// <remarks>
			/// Identifies the major release of the embedded controller firmware; for example, the value would be 0Ah for revision 10.22 and 0x02 for revision 2.1.
			/// This field or the Embedded Controller Firmware Minor Release field or both are updated each time an embedded controller firmware update for a given system is released.
			/// If the system does not have field upgradeable embedded controller firmware, the value is 0x0ff.
			/// </remarks>
			private Byte EmbeddedControllerFirmwareMajor;
			/// <summary>Embedded Controller Firmware Minor Release</summary>
			/// <remarks>
			/// Identifies the minor release of the embedded controller firmware; for example, the value is 0x16 for revision 10.22 and 0x01 for revision 2.1.
			/// If the system does not have field upgradeable embedded controller firmware, the value is 0x0ff
			/// </remarks>
			private Byte EmbeddedControllerFirmwareMinor;
			/// <summary>Extended BIOS ROM Size</summary>
			/// <remarks>
			/// Extended size of the physical device(s) containing the BIOS, rounded up if needed.
			/// Bits 15:14  Unit 
			///		00b - megabytes 
			///		01b - gigabytes 
			///		10b - reserved 
			///		11b - reserved
			///		Bits 13:0   Size
			/// Examples: a 16 MB device would be represented as 0x0010
			/// A 48 GB device set would be represented as 0100_0000_0011_0000b or 0x4030.
			/// </remarks>
			private UInt16 ExtendedBiosRomSize;

			/// <summary>BIOS ROM size</summary>
			public UInt32 RomSize
			{
				get
				{
					return this._RomSize != 0xff
						? (UInt32)(this._RomSize + 1 * 64)
						: (UInt32)(this.ExtendedBiosRomSize >> 0 & 0x3fff);
				}
			}

			/// <summary>BIOS ROM size units</summary>
			public SizeType RomSizeUnits
			{
				get
				{
					return this._RomSize != 0xff
						? SizeType.Kb
						: (SizeType)(this.ExtendedBiosRomSize >> 14 & 0x03);
				}
			}

			/// <summary>System BIOS release version</summary>
			public Version ReleaseVersion
			{
				get
				{
					return this.Header.Length > 0x14
						? new Version(this.MajorRelease, this.MinorRelease)
						: null;
				}
			}

			/// <summary>Embedded Controller Firmware</summary>
			/// <remarks>Identifies release of the embedded controller firmware</remarks>
			public Version EmbeddedControllerFirmwareRelease
			{
				get
				{
					return this.Header.Length > 0x14 && this.EmbeddedControllerFirmwareMajor != 0xff && this.EmbeddedControllerFirmwareMinor != 0xff
						? new Version(this.EmbeddedControllerFirmwareMajor, this.EmbeddedControllerFirmwareMinor)
						: null;
				}
			}
		}

		/// <summary>System Information (Type 1)</summary>
		/// <remarks>
		/// The information in this structure defines attributes of the overall system and is intended to be associated with the Component ID group of the system’s MIF.
		/// An SMBIOS implementation is associated with a single system instance and contains one and only one System Information (Type 1) structure.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type1
		{
			private enum VersionOffset : byte
			{
				V21 = 0x08,
			}

			/// <summary>Shows what the byte values mean for the System — Wake-up Type</summary>
			public enum WakeUpType : byte
			{
				/// <summary>Reserved</summary>
				Reserved = 0x00,
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>APM Timer</summary>
				APMTimer = 0x03,
				/// <summary>Modem Ring</summary>
				ModemRing = 0x04,
				/// <summary>LAN Remote</summary>
				LanRemote = 0x05,
				/// <summary>Power Switch</summary>
				PowerSwitch = 0x06,
				/// <summary>PCI PME#</summary>
				PciPme = 0x07,
				/// <summary>AC Power Restored</summary>
				ACPowerRestored = 0x08,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Manufacturer</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte Manufacturer;
			/// <summary>Product Name</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte ProductName;
			/// <summary>Version</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte Version;
			/// <summary>Serial number</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte SerialNumber;
			/// <summary>UUID</summary>
			/// <remarks>Universal unique ID number</remarks>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			private Byte[] _UUID;

			/// <summary>Wake-up Type</summary>
			/// <remarks>Identifies the event that caused the system to power up</remarks>
			public WakeUpType WakeUp;
			/// <summary>SKU Number</summary>
			/// <remarks>
			/// Number of null-terminated string
			/// This text string identifies a particular computer configuration for sale. It is sometimes also called a product ID or purchase order number.
			/// This number is frequently found in existing fields, but there is no standard format.
			/// Typically for a given system board from a given OEM, there are tens of unique processor, memory, hard drive, and optical drive configurations.
			/// </remarks>
			public Byte SKUNumber;
			/// <summary>Family</summary>
			/// <remarks>
			/// Number of null-terminated string.
			/// This text string identifies the family to which a particular computer belongs.
			/// A family refers to a set of computers that are similar but not identical from a hardware or software point of view.
			/// Typically, a family is composed of different computer models, which have different configurations and pricing points.
			/// Computers in the same family often have similar branding and cosmetic features
			/// </remarks>
			public Byte Family;

			/// <summary>UUID</summary>
			/// <remarks>
			/// A UUID is an identifier that is designed to be unique across both time and space.
			/// It requires no central registration process. The UUID is 128 bits long.
			/// Its format is described in RFC4122, but the actual field contents are opaque and not significant to the SMBIOS specification, which is only concerned with the byte order.
			/// </remarks>
			public Guid? UUID { get { return this.Header.Length > (Byte)Type1.VersionOffset.V21 ? new Guid(this._UUID) : (Guid?)null; } }
		}

		/// <summary>Baseboard (or Module) Information (Type 2)</summary>
		/// <remarks>
		/// As shown in Table 13, the information in this structure defines attributes of a system baseboard (for example, a motherboard, planar, server blade, or other standard system module).
		/// </remarks>
		[StructLayout(LayoutKind.Sequential, Size = 11, Pack = 1)]
		public struct Type2
		{
			/// <summary>Collection of flags that identify features of this baseboard</summary>
			[Flags]
			public enum FeatureFlags : byte
			{
				/// <summary>Set to 1 if the board is a hosting board (for example, a motherboard)</summary>
				Motherboard = 1 << 0,
				/// <summary>Set to 1 if the board requires at least one daughter board or auxiliary card to function properly</summary>
				RequiresDaughter = 1 << 1,
				/// <summary>Set to 1 if the board is removable; it is designed to be taken in and out of the chassis without impairing the function of the chassis</summary>
				Removable = 1 << 2,
				/// <summary>
				/// Set to 1 if the board is replaceable; it is possible to replace (either as a field repair or as an upgrade) the board with a physically different board.
				/// The board is inherently removable.
				/// </summary>
				Replaceable = 1 << 3,
				/// <summary>
				/// Set to 1 if the board is hot swappable; it is possible to replace the board with a physically different but equivalent board while power is applied to the board.
				/// The board is inherently replaceable and removable.
				/// </summary>
				Swappable = 1 << 4,
			}

			/// <summary>Baseboard — Board Type</summary>
			public enum BoardType : byte
			{
				/// <summary>Unknown</summary>
				Unknown = 0x01,
				/// <summary>Other</summary>
				Other = 0x02,
				/// <summary>Server Blade</summary>
				ServerBlade0x03,
				/// <summary>Connectivity Switch</summary>
				ConnectivitySwitch = 0x04,
				/// <summary>System Management Module</summary>
				SystemManagementModule = 0x05,
				/// <summary>Processor Module</summary>
				ProcessorModule = 0x06,
				/// <summary>I/O Module</summary>
				IOModule = 0x07,
				/// <summary>Memory Module</summary>
				MemoryModule = 0x08,
				/// <summary>Daughter board</summary>
				DaughterBoard = 0x09,
				/// <summary>Motherboard (includes processor, memory, and I/O</summary>
				Motherboard = 0x0a,
				/// <summary>Processor/Memory Module</summary>
				ProcessorMemoryModule = 0x0b,
				/// <summary>Processor/IO Module</summary>
				ProcessorIOModule = 0x0c,
				/// <summary>Interconnect board</summary>
				InterconnectBoard = 0x0d,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Manufacturer</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte Manufacturer;
			/// <summary>Product</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte Product;
			/// <summary>Version</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte Version;
			/// <summary>Serial number</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte SerialNumber;
			/// <summary>Asset Tag</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte AssetTag;
			/// <summary>Feature</summary>
			/// <remarks>Collection of flags that identify features of this baseboard</remarks>
			public FeatureFlags Feature;
			/// <summary>Location in Chassis</summary>
			/// <remarks>Number of a null-terminated string that describes this board's location within the chassis referenced by the Chassis Handle</remarks>
			public Byte LocationInChassis;
			/// <summary>Chassis Handle</summary>
			/// <remarks>Handle, or instance number, associated with the chassis in which this board resides</remarks>
			public UInt16 ChassisHandle;
			/// <summary>Board Type</summary>
			public BoardType Board;
			/// <summary>Number of Contained Object Handles(n)</summary>
			/// <remarks>Number (0 to 255) of Contained Object Handles that follow</remarks>
			public Byte NumberContainedObjectHandles;
			// <summary>
			// List of handles of other structures (for example, Baseboard, Processor, Port, System Slots, Memory Device) that are contained by this baseboard.
			// </summary>
			//public Byte[] ContainedObjectHandles;
		}

		/// <summary>System Enclosure or Chassis (Type 3)</summary>
		/// <remarks>
		/// The information in this structure (see Table 16) defines attributes of the system’s mechanical enclosure(s).
		/// For example, if a system included a separate enclosure for its peripheral devices, two structures would be returned:
		/// one for the main system enclosure and the second for the peripheral device enclosure.
		/// The additions to this structure in version 2.1 of this specification support the population of the CIM_Chassis class.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type3
		{
			/// <summary>Chassis type</summary>
			public enum EnclosureType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Desktop</summary>
				Desktop = 0x03,
				/// <summary>Low Profile Desktop</summary>
				LowProfileDesktop = 0x04,
				/// <summary>Pizza Box</summary>
				PizzaBox = 0x05,
				/// <summary>Mini Tower </summary>
				MiniTower = 0x06,
				/// <summary>Tower</summary>
				Tower = 0x07,
				/// <summary>Portable</summary>
				Portable = 0x08,
				/// <summary>Laptop</summary>
				Laptop = 0x09,
				/// <summary>Notebook</summary>
				Notebook = 0x0a,
				/// <summary>HandHeld</summary>
				HandHeld = 0x0b,
				///<summary>Docking Station</summary>
				DockingStation = 0x0c,
				/// <summary>All in One</summary>
				AllInOne = 0x0d,
				/// <summary>Sub Notebook</summary>
				SubNotebook = 0x0e,
				/// <summary>Space-saving</summary>
				SpaceSaving = 0x0f,
				/// <summary>Lunch Box</summary>
				LunchBox = 0x10,
				/// <summary>Main Server Chassis</summary>
				MainServerChassis = 0x11,
				/// <summary>Expansion Chassis</summary>
				ExpansionChassis = 0x12,
				/// <summary>SubChassis</summary>
				SubChassis = 0x13,
				/// <summary>Bus Expansion Chassis</summary>
				BusExpansionChassis = 0x14,
				/// <summary>Peripheral Chassis</summary>
				PeripheralChassis = 0x15,
				/// <summary>RAID Chassis</summary>
				RAIDChassis = 0x16,
				/// <summary>Rack Mount Chassis</summary>
				RackMountChassis = 0x17,
				/// <summary>Sealed-case PC </summary>
				SealedCasePC = 0x18,
				/// <summary>Multi-system chassis</summary>
				/// <remarks>
				/// When this value is specified by an SMBIOS implementation, the physical chassis associated with this structure supports multiple, independently reporting physical systems—regardless of the chassis' current configuration.
				/// Systems in the same physical chassis are required to report the same value in this structure's Serial Number field.
				/// For a chassis that may also be configured as either a single system or multiple physical systems, the Multi-system chassis value is reported even if the chassis is currently configured as a single system.
				/// This allows management applications to recognize the multi-system potential of the chassis.
				/// </remarks>
				MultiSystemChassis = 0x19,
				/// <summary>Compact PCI</summary>
				CompactPCI = 0x1a,
				/// <summary>Advanced TCA</summary>
				AdvancedTCA = 0x1b,
				/// <summary>Blade</summary>
				/// <remarks>
				/// An SMBIOS implementation for a Blade would contain a Type 3 Chassis structure for the individual Blade system as well as one for the Blade Enclosure that completes the Blade system
				/// </remarks>
				Blade = 0x1c,
				/// <summary>Blade Enclosure</summary>
				/// <remarks>
				/// A Blade Enclosure is a specialized chassis that contains a set of Blades.
				/// It provides much of the non-core computing infrastructure for a set of Blades (power, cooling, networking, etc.).
				/// A Blade Enclosure may itself reside inside a Rack or be a standalone chassis.
				/// </remarks>
				BladeEnclosure = 0x1d,
				/// <summary>Tablet</summary>
				Tablet = 0x1e,
				/// <summary>Convertible</summary>
				Convertible = 0x1f,
				/// <summary>Detachable</summary>
				Detachable = 0x20,
				/// <summary>IoT Gateway</summary>
				IoTGateway = 0x21,
				/// <summary>Embedded PC</summary>
				EmbeddedPC = 0x22,
				/// <summary>Mini PC</summary>
				MiniPC = 0x23,
				/// <summary>Stick PC</summary>
				StickPC = 0x24,
			}

			/// <summary>System Enclosure or Chassis States</summary>
			public enum ChassisState : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Safe</summary>
				Safe = 0x03,
				/// <summary>Warning</summary>
				Warning = 0x04,
				/// <summary>Critical</summary>
				Critical = 0x05,
				/// <summary>Non-recoverable</summary>
				NonRecoverable = 0x06,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Manufacturer</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte Manufacturer;
			/// <summary>Type</summary>
			/// <remarks>
			/// Bit 7		Chassis lock is present if 1. 
			///					Otherwise, either a lock is not present or it is unknown if the enclosure has a lock.
			/// Bits 6:0	Enumeration value; see below.
			/// </remarks>
			private Byte _Type;
			/// <summary>Version</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte Version;
			/// <summary>Serial number</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte SerialNumber;
			/// <summary>Asset Tag Number</summary>
			/// <remarks>Number of null-terminated string</remarks>
			public Byte AssetTagNumber;
			/// <summary>Boot-up State</summary>
			/// <remarks>State of the enclosure when it was last booted</remarks>
			/// <value>v2.1+</value>
			public ChassisState BootupState;
			/// <summary>Power Supply State</summary>
			/// <remarks>State of the enclosure’s power supply (or supplies) when last booted</remarks>
			/// <value>v2.1+</value>
			public ChassisState PowerSupplyState;
			/// <summary>Thermal State</summary>
			/// <remarks>Thermal state of the enclosure when last booted</remarks>
			/// <value>v2.1+</value>
			public ChassisState ThermalState;
			/// <summary>Security Status</summary>
			/// <remarks>Physical security status of the enclosure when last booted</remarks>
			/// <value>v2.1+</value>
			public ChasisSecurityStatus SecurityStatus;
			/// <summary>OEM-defined</summary>
			/// <remarks>OEM- or BIOS vendor-specific information</remarks>
			/// <value>v2.3+</value>
			public UInt32 OEMDefined;
			/// <summary>Height</summary>
			/// <remarks>
			/// Height of the enclosure, in 'U's A U is a standard unit of measure for the height of a rack or rack-mountable component and is equal to 1.75 inches or 4.445 cm.
			/// A value of 0x00 indicates that the enclosure height is unspecified.
			/// </remarks>
			/// <value>v2.3+</value>
			public Byte Height;
			/// <summary>Number of Power Cords</summary>
			/// <remarks>
			/// Number of power cords associated with the enclosure or chassis.
			/// A value of 0x00 indicates that the number is unspecified.
			/// </remarks>
			/// <value>v2.3+</value>
			public Byte NumberOfPowerCords;
			/// <summary>Contained Element Count(n)</summary>
			/// <remarks>
			/// Number of Contained Element records that follow, in the range 0 to 255 Each Contained Element group comprises m bytes, as specified by the Contained Element Record Length field that follows.
			/// If no Contained Elements are included, this field is set to 0.
			/// </remarks>
			/// <value>v2.3+</value>
			public Byte ContainedElementCount;
			/// <summary>Contained Element Record Length(m)</summary>
			/// <remarks>
			/// Byte length of each Contained Element record that follows, in the range 0 to 255 If no Contained Elements are included, this field is set to 0.
			/// For version 2.3.2 and later of this specification, this field is set to at least 03h when Contained Elements are specified.
			/// </remarks>
			/// <value>v2.3+</value>
			public Byte ContainedElementRecordLength;
			//TODO: Varable length array follows
			/*/// <summary>Elements, possibly defined by other SMBIOS structures, present in this chassis</summary>
			/// <value>v2.3+</value>
			public Byte ContainedElements;
			
			<summary>Number of null-terminated string describing the chassis or enclosure SKU number</summary>
			<value>v2.7+</value>
			public Byte SKUNumber;*/


			/// <summary>Lock is present</summary>
			public Boolean ChassisLockPresent { get { return (this._Type >> 7 & 0x01) == 0x01; } }
			/// <summary>Type</summary>
			public EnclosureType Type { get { return (EnclosureType)(this._Type >> 0 & 0x3f); } }
		}

		/// <summary>Processor Information (Type 4)</summary>
		/// <remarks>
		/// he information in this structure (see Table 21) defines the attributes of a single processor; a separate structure instance is provided for each system processor socket/slot.
		/// For example, a system with an IntelDX2™ processor would have a single structure instance while a system with an IntelSX2™ processor would have a structure to describe the main CPU and a second structure to describe the 80487 co-processor.
		/// </remarks>
		/// <value>v2.0+</value>
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		public struct Type4
		{
			/// <summary>Processor type</summary>
			public enum ProcessorType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Central Processor</summary>
				CentralProcessor = 0x03,
				/// <summary>Math processor</summary>
				MathProcessor = 0x04,
				/// <summary>DSP processor</summary>
				DspProcessor = 0x05,
				/// <summary>Video processor</summary>
				VideoProcessor = 0x06,
			}

			/// <summary>
			/// Two forms of information can be specified by the SMBIOS in this field, dependent on the value present in bit 7 (the most-significant bit).
			/// If bit 7 is 0 (legacy mode), the remaining bits of the field represent the specific voltages that the processor socket can accept
			/// </summary>
			/// <remarks>If bit 7 is set to 1, the remaining seven bits of the field are set to contain the processor’s current voltage times 10.</remarks>
			/// <example>
			/// The field value for a processor voltage of 1.8 volts would be:
			/// 0x92 = 0x80 + (1.8 * 10) = 0x80 + 18 = 0x80 +12
			/// </example>
			[Flags]
			public enum VoltageFlags : byte
			{
				/// <summary>Voltage Capability 5V</summary>
				_5V = 1 << 0,
				/// <summary>Voltage Capability 3.3V</summary>
				_33V = 1 << 1,
				/// <summary>Voltage Capability 2.9V</summary>
				_29V = 1 << 2,
				/// <summary>Set to 0, indicating "legacy" mode for processor voltag</summary>
				Legacy = 1 << 7,
			}

			/// <summary>Processor Status</summary>
			[Flags]
			public enum ProcessorStatusFlags : byte
			{
				/// <summary>Unknown</summary>
				Unknown = 1 << 0,
				/// <summary>CPU Enabled</summary>
				CpuEnabled = 1 << 1,
				/// <summary>CPU Disabled by User through BIOS Setup</summary>
				CpuDisabledByUser = 1 << 2,
				/// <summary>CPU Disabled By BIOS (POST Error)</summary>
				CpuDisabledByBios = 1 << 3,
				/// <summary>CPU is Idle, waiting to be enabled</summary>
				CpuIdle = 1 << 4,
				/// <summary>CPU Socket Populated</summary>
				CpuSocketPopulated = 1 << 6,
			}

			/// <summary>What the byte values mean for the Processor Information — Processor Upgrade</summary>
			public enum ProcessorUpgradeType : byte
			{
				/// <summary></summary>
				Other = 0x01,
				/// <summary></summary>
				Unknown = 0x02,
				/// <summary>Daughter Board</summary>
				DaughterBoard = 0x03,
				/// <summary>ZIF Socket</summary>
				ZifSocket = 0x04,
				/// <summary>Replaceable Piggy Back</summary>
				ReplaceablePiggyBack = 0x05,
				/// <summary>None</summary>
				None = 0x06,
				/// <summary>LIF Socket</summary>
				LifSocket = 0x07,
				/// <summary>Slot 1</summary>
				Slot1 = 0x08,
				/// <summary>Slot 2</summary>
				Slot2 = 0x09,
				/// <summary>370-pin socket</summary>
				Socket370Pin = 0x0A,
				/// <summary>Slot A</summary>
				SlotA = 0x0B,
				/// <summary>Slot M</summary>
				SlotM = 0x0C,
				/// <summary>Socket 423</summary>
				Socket423 = 0x0D,
				/// <summary>Socket A (Socket 462)</summary>
				SocketA = 0x0E,
				/// <summary>Socket 478</summary>
				Socket478 = 0x0F,
				/// <summary>Socket 754</summary>
				Socket754 = 0x10,
				/// <summary>Socket 940</summary>
				Socket940 = 0x11,
				/// <summary>Socket 939</summary>
				Socket939 = 0x12,
				/// <summary>Socket mPGA604</summary>
				SocketMPGA604 = 0x13,
				/// <summary>Socket LGA771</summary>
				SocketLGA771 = 0x14,
				/// <summary>Socket LGA775</summary>
				SocketLGA775 = 0x15,
				/// <summary>Socket S1</summary>
				SocketS1 = 0x16,
				/// <summary>Socket AM2</summary>
				SocketAM2 = 0x17,
				/// <summary>Socket F (1207)</summary>
				SocketF = 0x18,
				/// <summary>Socket LGA1366</summary>
				SocketLGA1366 = 0x19,
				/// <summary>Socket G34</summary>
				SocketG34 = 0x1A,
				/// <summary>Socket AM3</summary>
				SocketAM3 = 0x1B,
				/// <summary>Socket C32</summary>
				SocketC32 = 0x1C,
				/// <summary>Socket LGA1156</summary>
				SocketLGA1156 = 0x1D,
				/// <summary>Socket LGA1567</summary>
				SocketLGA1567 = 0x1E,
				/// <summary>Socket PGA988A</summary>
				SocketPGA988A = 0x1F,
				/// <summary>Socket BGA1288</summary>
				SocketBGA1288 = 0x20,
				/// <summary>Socket rPGA988B</summary>
				SocketRPGA988B = 0x21,
				/// <summary>Socket BGA1023</summary>
				SocketBGA1023 = 0x22,
				/// <summary>Socket BGA1224</summary>
				SocketBGA1224 = 0x23,
				/// <summary>Socket LGA1155</summary>
				SocketLGA1155 = 0x24,
				/// <summary>Socket LGA1356</summary>
				SocketLGA1356 = 0x25,
				/// <summary>Socket LGA2011</summary>
				SocketLGA2011 = 0x26,
				/// <summary>Socket FS1</summary>
				SocketFS1 = 0x27,
				/// <summary>Socket FS2</summary>
				SocketFS2 = 0x28,
				/// <summary>Socket FM1</summary>
				SocketFM1 = 0x29,
				/// <summary>Socket FM2</summary>
				SocketFM2 = 0x2A,
				/// <summary>Socket LGA2011-3</summary>
				SocketLGA2011_3 = 0x2B,
				/// <summary>Socket LGA1356-3</summary>
				SocketLGA1356_3 = 0x2C,
				/// <summary>Socket LGA1150</summary>
				SocketLGA1150 = 0x2D,
				/// <summary>Socket BGA1168</summary>
				SocketBGA1168 = 0x2E,
				/// <summary>Socket BGA1234</summary>
				SocketBGA1234 = 0x2F,
				/// <summary>Socket BGA1364</summary>
				SocketBGA1364 = 0x30,
				/// <summary>Socket AM4</summary>
				SocketAM4 = 0x31,
				/// <summary>Socket LGA1151</summary>
				SocketLGA1151 = 0x32,
				/// <summary>Socket BGA1356</summary>
				SocketBGA1356 = 0x33,
				/// <summary>Socket BGA1440</summary>
				SocketBGA1440 = 0x34,
				/// <summary>Socket BGA1515</summary>
				SocketBGA1515 = 0x35,
				/// <summary>Socket LGA3647-1</summary>
				SocketLGA3647_1 = 0x36,
				/// <summary>Socket SP3</summary>
				SocketSP3 = 0x37,
				/// <summary>Socket SP3r2</summary>
				SocketSP3r2 = 0x38,
				/// <summary>Socket LGA2066</summary>
				SocketLGA2066 = 0x39,
				/// <summary>Socket BGA1392</summary>
				SocketBGA1392 = 0x3A,
				/// <summary>Socket BGA1510</summary>
				SocketBGA1510 = 0x3B,
				/// <summary>Socket BGA1528</summary>
				SocketBGA1528 = 0x3C,
				/// <summary>Socket LGA4189</summary>
				SocketLGA4189 = 0x3D,
				/// <summary>Socket LGA1200</summary>
				SocketLGA1200 = 0x3E,
			}

			/// <summary>Defines which functions the processor support</summary>
			[Flags]
			public enum ProcessorCharacteristicsFlags : ushort
			{
				/// <summary>Reserved</summary>
				Reserved = 1 << 0,
				/// <summary>Unknown</summary>
				Unknown = 1 << 1,
				/// <summary>64-bit Capable</summary>
				Capable64Bit = 1 << 2,
				/// <summary>Multi-Core</summary>
				MultiCore = 1 << 3,
				/// <summary>Hardware Thread</summary>
				HardwareThread = 1 << 4,
				/// <summary>Execute Protection</summary>
				ExecuteProtection = 1 << 5,
				/// <summary>Enhanced Virtualization</summary>
				EnhancedVirtualizatio = 1 << 6,
				/// <summary>Power/Performance Control</summary>
				PowerPerformanceControl = 1 << 7,
				/// <summary>128-bit Capable</summary>
				Capable128Bit = 1 << 8,
				/// <summary>Arm64 SoC ID</summary>
				Arm64SoCID = 1 << 9,
			}

			/// <summary>Processor family</summary>
			public enum ProcessorFamilyType : ushort
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>8086</summary>
				_8086 = 0x03,
				/// <summary>80286</summary>
				_80286 = 0x04,
				/// <summary>Intel386™ processor</summary>
				Intel386Processor = 0x05,
				/// <summary>Intel486™ processor</summary>
				Intel486Processor = 0x06,
				/// <summary>8087</summary>
				_8087 = 0x07,
				/// <summary>80287</summary>
				_80287 = 0x08,
				/// <summary>_80387</summary>
				_80387 = 0x09,
				/// <summary>80487</summary>
				_80487 = 0x0a,
				/// <summary>Intel® Pentium® processor</summary>
				IntelPentium = 0x0b,
				/// <summary>Intel® Pentium® Pro processor</summary>
				IntelPentiumPro = 0x0c,
				/// <summary>Pentium® II processor</summary>
				IntelPentiumII = 0x0d,
				/// <summary>Pentium® processor with MMX™ technology</summary>
				IntelPentiumMMX = 0x0e,
				/// <summary>Intel® Celeron® processor</summary>
				IntelCeleron = 0x0f,
				/// <summary>Pentium® II Xeon™ processor</summary>
				IntelPentiumIIXeon = 0x10,
				/// <summary>Pentium® III processor</summary>
				IntelPentiumIII = 0x11,
				/// <summary>M1 Family</summary>
				M1Family = 0x12,
				/// <summary>M2 Family</summary>
				M2Family = 0x13,
				/// <summary>Intel® Celeron® M processor</summary>
				IntelCeleronM = 0x14,
				/// <summary>Intel® Pentium® 4 HT processor</summary>
				IntelPentium4HT = 0x15,
				/// <summary>AMD Duron™ Processor Family</summary>
				AmdDuron = 0x18,
				/// <summary>K5 Family</summary>
				K5 = 0x19,
				/// <summary>K6 Family</summary>
				K6 = 0x1a,
				/// <summary>K6-2</summary>
				K6_2 = 0x1b,
				/// <summary>K6-3</summary>
				K6_3 = 0x1c,
				/// <summary>AMD Athlon™ Processor Family</summary>
				AmdAthlon = 0x1d,
				/// <summary>AMD29000 Family</summary>
				Amd29000Family = 0x1e,
				/// <summary>K6-2+</summary>
				K6_2P = 0x1f,
				/// <summary>Power PC Family</summary>
				PowerPCFamily = 0x20,
				/// <summary>Power PC 601</summary>
				PowerPC601 = 0x21,
				/// <summary>Power PC 603</summary>
				PowerPC603 = 0x22,
				/// <summary>Power PC 603+</summary>
				PowerPC603P = 0x23,
				/// <summary>Power PC 604</summary>
				PowerPC604 = 0x24,
				/// <summary>Power PC 620</summary>
				PowerPC620 = 0x25,
				/// <summary>Power PC x704</summary>
				PowerPCx704 = 0x26,
				/// <summary>Power PC 750</summary>
				PowerPC750 = 0x27,
				/// <summary>Intel® Core™ Duo processor</summary>
				IntelCoreDuo = 0x28,
				/// <summary>Intel® Core™ Duo mobile processor</summary>
				IntelCoreDuoMobile = 0x29,
				/// <summary>Intel® Core™ Solo mobile processor</summary>
				IntelCoreSoloMobile = 0x2a,
				/// <summary>Intel® Atom™ processor</summary>
				IntelAtom = 0x2b,
				/// <summary>Intel® Core™ M processor</summary>
				IntelCoreM = 0x2c,
				/// <summary>Intel(R) Core(TM) m3 processor</summary>
				IntelCoreM3 = 0x2D,
				/// <summary>Intel(R) Core(TM) m5 processor</summary>
				IntelCoreM5 = 0x2E,
				///<summary>Intel(R) Core(TM) m7 processor</summary>
				IntelCoreM7 = 0x2F,
				/// <summary>Alpha Family</summary>
				AlphaFamily = 0x30,
				/// <summary>Alpha 21064</summary>
				Alpha21064 = 0x31,
				/// <summary>Alpha 21066</summary>
				Alpha21066 = 0x32,
				/// <summary>Alpha 21164</summary>
				Alpha21164 = 0x33,
				/// <summary>Alpha 21164PC</summary>
				Alpha21164PC = 0x34,
				/// <summary>Alpha 21164a</summary>
				Alpha21164a = 0x35,
				/// <summary>Alpha 21264</summary>
				Alpha21264 = 0x36,
				/// <summary>Alpha 21364</summary>
				Alpha21364 = 0x37,
				/// <summary>AMD Turion™ II Ultra Dual-Core Mobile M Processor Family</summary>
				AmdTurionIIUltraDualCoreMobileM = 0x38,
				/// <summary>AMD Turion™ II Dual-Core Mobile M Processor Family</summary>
				AmdTurionIIDualCoreMobileM = 0x39,
				/// <summary>AMD Athlon™ II Dual-Core M Processor Family</summary>
				AmdAthlonIIDualCoreM = 0x3A,
				/// <summary>AMD Opteron™ 6100 Series Processor</summary>
				AmdOpteron6100 = 0x3B,
				/// <summary>AMD Opteron™ 4100 Series Processor</summary>
				AmdOpteron4100 = 0x3C,
				/// <summary>AMD Opteron™ 6200 Series Processor</summary>
				AmdOpteron6200 = 0x3D,
				/// <summary>AMD Opteron™ 4200 Series Processor</summary>
				AmdOpteron4200 = 0x3E,
				/// <summary>AMD FX™ Series Processor</summary>
				AmdFX = 0x3F,
				/// <summary>MIPS Family</summary>
				Mips = 0x40,
				/// <summary>MIPS R4000</summary>
				MipsR4000 = 0x41,
				/// <summary>MIPS R4200</summary>
				MipsR4200 = 0x42,
				/// <summary>MIPS R4400</summary>
				MipsR4400 = 0x43,
				/// <summary>MIPS R4600</summary>
				MipsR4600 = 0x44,
				/// <summary>MIPS R10000</summary>
				MipsR10000 = 0x45,
				/// <summary>AMD C-Series Processor</summary>
				AmdCSeries = 0x46,
				/// <summary>AMD E-Series Processor</summary>
				AmdESeries = 0x47,
				/// <summary>AMD A-Series Processor</summary>
				AmdASeries = 0x48,
				/// <summary>AMD G-Series Processor</summary>
				AmdGSeries = 0x49,
				/// <summary>AMD Z-Series Processor</summary>
				AmdZSeries = 0x4A,
				/// <summary>AMD R-Series Processor</summary>
				AmdRSeries = 0x4B,
				/// <summary>AMD Opteron™ 4300 Series Processor</summary>
				AmdOpteron4300 = 0x4C,
				/// <summary>AMD Opteron™ 6300 Series Processor</summary>
				AmdOpteron6300 = 0x4D,
				/// <summary>AMD Opteron™ 3300 Series Processor</summary>
				AmdOpteron3300 = 0x4E,
				/// <summary>AMD FirePro™ Series Processor</summary>
				AMDFirePro = 0x4F,
				/// <summary>SPARC Family</summary>
				Spark = 0x50,
				/// <summary>SuperSPARC</summary>
				SuperSpark = 0x51,
				/// <summary>microSPARC II </summary>
				microSparkII = 0x52,
				/// <summary>microSPARC IIep </summary>
				microSparkIIep = 0x53,
				/// <summary>UltraSPARC</summary>
				UltraSpark = 0x54,
				/// <summary>UltraSPARC II</summary>
				UltraSparkII = 0x55,
				/// <summary>UltraSPARC Iii</summary>
				UltraSparcIii = 0x56,
				/// <summary>UltraSPARC III</summary>
				UltraSparcIII = 0x57,
				/// <summary>UltraSPARC IIIi</summary>
				UltraSparcIIIi = 0x58,
				/// <summary>68040 Family</summary>
				_68040 = 0x60,
				///<summary>68xxx</summary>
				_68xxx = 0x61,
				/// <summary>68000</summary>
				_68000 = 0x62,
				/// <summary>68010</summary>
				_68010 = 0x63,
				/// <summary>68020</summary>
				_68020 = 0x64,
				/// <summary>68030</summary>
				_68030 = 0x65,
				/// <summary>AMD Athlon(TM) X4 Quad-Core Processor Family</summary>
				AmdAthlonX4QuadCore = 0x66,
				/// <summary>AMD Opteron(TM) X1000 Series Processor</summary>
				AmdOpteronX1000 = 0x67,
				/// <summary>AMD Opteron(TM) X2000 Series APU</summary>
				AmdOpteronX2000 = 0x68,
				/// <summary>AMD Opteron(TM) A-Series Processor</summary>
				AmdOpteronASeries = 0x69,
				/// <summary>AMD Opteron(TM) X3000 Series APU</summary>
				AmdOpteronX3000 = 0x6A,
				/// <summary>AMD Zen Processor Family</summary>
				AmdZen = 0x6B,
				/// <summary>Hobbit Family </summary>
				HobbitFamily = 0x70,
				/// <summary>Crusoe™ TM5000 Family</summary>
				CrusoeTM5000 = 0x78,
				/// <summary>Crusoe™ TM3000 Family</summary>
				CrusoeTM3000Family = 0x79,
				/// <summary>Efficeon™ TM8000 Family</summary>
				EfficeonTM8000 = 0x7A,
				/// <summary>Weitek</summary>
				Weitek = 0x80,
				/// <summary>Itanium™ processor</summary>
				Itanium = 0x82,
				/// <summary>AMD Athlon™ 64 Processor Family</summary>
				AmdAthlon64 = 0x83,
				/// <summary>AMD Opteron™ Processor Family</summary>
				AmdOpteron = 0x84,
				/// <summary>AMD Sempron™ Processor Family </summary>
				AmdSempron = 0x85,
				/// <summary>AMD Turion™ 64 Mobile Technology</summary>
				AmdTurion64Mobile = 0x86,
				/// <summary>Dual-Core AMD Opteron™ Processor Family</summary>
				AmdOpteronDualCore = 0x87,
				/// <summary>AMD Athlon™ 64 X2 Dual-Core Processor Family</summary>
				AmdAthlon64X2DualCore = 0x88,
				/// <summary>AMD Turion™ 64 X2 Mobile Technology</summary>
				AmdTurion64X2Mobile = 0x89,
				/// <summary>Quad-Core AMD Opteron™ Processor Family</summary>
				AmdOpteronQuadCore = 0x8A,
				/// <summary>Third-Generation AMD Opteron™ Processor Family</summary>
				AmdOpteronThirdGeneration = 0x8B,
				/// <summary>AMD Phenom™ FX Quad-Core Processor Family</summary>
				AmdPhenomFXQuadCore = 0x8C,
				/// <summary>AMD Phenom™ X4 Quad-Core Processor Family</summary>
				AmdPhenomX4QuadCore = 0x8D,
				/// <summary>AMD Phenom™ X2 Dual-Core Processor Family</summary>
				AmdPhenomX2DualCore = 0x8E,
				/// <summary>AMD Athlon™ X2 Dual-Core Processor Family</summary>
				AmdAthlonX2DualCore = 0x8F,
				/// <summary>PA-RISC Family</summary>
				PA_Risc = 0x90,
				/// <summary>PA-RISC 8500</summary>
				PA_Risc8500 = 0x91,
				/// <summary>PA-RISC 8000</summary>
				PA_Risc8000 = 0x92,
				/// <summary>PA-RISC 7300LC</summary>
				PA_Risc7300LC = 0x93,
				/// <summary>PA-RISC 7200</summary>
				PA_Risc7200 = 0x94,
				/// <summary>PA-RISC 7100LC</summary>
				PA_Risc7100LC = 0x95,
				/// <summary>PA-RISC 7100</summary>
				PA_Risc7100 = 0x96,
				/// <summary>V30 Family</summary>
				V30Family = 0xA0,
				/// <summary>Quad-Core Intel® Xeon® processor 3200 Series</summary>
				IntelXeonQuadCore3200 = 0xA1,
				/// <summary>Dual-Core Intel® Xeon® processor 3000 Series</summary>
				IntelXeonDualCore3000 = 0xA2,
				/// <summary>Quad-Core Intel® Xeon® processor 5300 Series</summary>
				IntelXeonQuadCore5300 = 0xA3,
				/// <summary>Dual-Core Intel® Xeon® processor 5100 Series</summary>
				IntelXeonDualCore5100 = 0xA4,
				/// <summary>Dual-Core Intel® Xeon® processor 5000 Series</summary>
				IntelXeonDualCore5000 = 0xA5,
				/// <summary>Dual-Core Intel® Xeon® processor LV</summary>
				IntelXeonDualCoreLV = 0xA6,
				/// <summary>Dual-Core Intel® Xeon® processor ULV</summary>
				IntelXeonDualCoreULV = 0xA7,
				/// <summary>Dual-Core Intel® Xeon® processor 7100 Series</summary>
				IntelXeonDualCore7100 = 0xA8,
				/// <summary>Quad-Core Intel® Xeon® processor 5400 Series</summary>
				IntelXeon5400QuadCore = 0xA9,
				/// <summary>Quad-Core Intel® Xeon® processor</summary>
				IntelXeonQuadCore = 0xAA,
				/// <summary>Dual-Core Intel® Xeon® processor 5200 Series</summary>
				IntelXeon5200DualCore = 0xAB,
				/// <summary>Dual-Core Intel® Xeon® processor 7200 Series</summary>
				IntelXeon7200DualCore = 0xAC,
				/// <summary>Quad-Core Intel® Xeon® processor 7300 Series</summary>
				IntelXeon7300QuadCore = 0xAD,
				/// <summary>Quad-Core Intel® Xeon® processor 7400 Series</summary>
				IntelXeon7400QuadCore = 0xAE,
				/// <summary>Multi-Core Intel® Xeon® processor 7400 Series</summary>
				IntelXeonMultiCore7400 = 0xAF,
				/// <summary>Pentium® III Xeon™ processor</summary>
				IntelPentiumIIIXeon = 0xB0,
				/// <summary>Pentium® III Processor with Intel® SpeedStep™ Technology</summary>
				IntelPentiumIIISpeedStep = 0xB1,
				/// <summary>Pentium® 4 Processor</summary>
				IntelPentium4 = 0xB2,
				/// <summary>Intel® Xeon® processor</summary>
				IntelXeon = 0xB3,
				/// <summary>AS400 Family</summary>
				AS400 = 0xB4,
				/// <summary>Intel® Xeon™ processor MP</summary>
				IntelXeonMP = 0xB5,
				/// <summary>AMD Athlon™ XP Processor Family</summary>
				AmdAthlonXP = 0xB6,
				/// <summary>AMD Athlon™ MP Processor Family</summary>
				AmdAthlonMP = 0xB7,
				/// <summary>Intel® Itanium® 2 processor</summary>
				IntelItanium2 = 0xB8,
				/// <summary>Intel® Pentium® M processor</summary>
				IntelPentiumM = 0xB9,
				/// <summary>Intel® Celeron® D processor</summary>
				IntelCeleronD = 0xBA,
				/// <summary>Intel® Pentium® D processor</summary>
				IntelPentiumD = 0xBB,
				/// <summary>Intel® Pentium® Processor Extreme Edition</summary>
				IntelPentiumExtremeEdition = 0xBC,
				/// <summary>Intel® Core™ Solo Processor</summary>
				IntelCoreSolo = 0xBD,
				/// <summary>Intel® Core™ 2 Duo Processor</summary>
				IntelCore2Duo = 0xBF,
				/// <summary>Intel® Core™ 2 Solo processor</summary>
				IntelCore2Solo = 0xC0,
				/// <summary>Intel® Core™ 2 Extreme processor</summary>
				IntelCore2Extreme = 0xC1,
				/// <summary>Intel® Core™ 2 Quad processor</summary>
				IntelCore2Quad = 0xC2,
				/// <summary>Intel® Core™ 2 Extreme mobile processor</summary>
				IntelCore2ExtremeMobile = 0xC3,
				/// <summary>Intel® Core™ 2 Duo mobile processor</summary>
				IntelCore2DuoMobile = 0xC4,
				/// <summary>Intel® Core™ 2 Solo mobile processor</summary>
				IntelCore2SoloMobile = 0xC5,
				/// <summary>Intel® Core™ i7 processor</summary>
				IntelCoreI7 = 0xC6,
				/// <summary>Dual-Core Intel® Celeron® processor</summary>
				DualCoreIntelCeleron = 0xc7,
				/// <summary>IBM390 Family</summary>
				Ibm390Family = 0xc8,
				/// <summary>G4</summary>
				G4 = 0xc9,
				/// <summary>G5</summary>
				G5 = 0xca,
				/// <summary>ESA/390 G6</summary>
				ESA_390_G6 = 0xcb,
				/// <summary>z/Architecture base</summary>
				zArchitectureBase = 0xcc,
				/// <summary>Intel® Core™ i5 processor</summary>
				IntelCoreI5 = 0xcd,
				/// <summary>Intel® Core™ i3 processor</summary>
				IntelCoreI3 = 0xce,
				/// <summary>Intel® Core™ i9 processor</summary>
				IntelCoreI9 = 0xcf,
				/// <summary>VIA C7™-M Processor Family</summary>
				ViaC7_M = 0xd2,
				/// <summary>VIA C7™-D Processor Family</summary>
				ViaC7_D = 0xd3,
				/// <summary>VIA C7™ Processor Family</summary>
				ViaC7 = 0xd4,
				/// <summary>VIA Eden™ Processor Family</summary>
				ViaEden = 0xd5,
				/// <summary>Multi-Core Intel® Xeon® processor</summary>
				IntelXeonMultiCore = 0xd6,
				/// <summary>Dual-Core Intel® Xeon® processor 3xxx Series</summary>
				IntelXeonDualCore3xxx = 0xd7,
				/// <summary>Quad-Core Intel® Xeon® processor 3xxx Series</summary>
				IntelXeonQuadCore3xxx = 0xd8,
				/// <summary>VIA Nano™ Processor Family</summary>
				ViaNano = 0xd9,
				/// <summary>Dual-Core Intel® Xeon® processor 5xxx Series</summary>
				IntelXeonDualCore5xxx = 0xda,
				/// <summary>Quad-Core Intel® Xeon® processor 5xxx Series</summary>
				IntelXeonQuadCore5xxx = 0xdb,
				/// <summary>Dual-Core Intel® Xeon® processor 7xxx Series</summary>
				IntelXeonDualCore7xxx = 0xdd,
				/// <summary>Quad-Core Intel® Xeon® processor 7xxx Series</summary>
				IntelXeonQuadCore7xxx = 0xde,
				/// <summary>Multi-Core Intel® Xeon® processor 7xxx Series</summary>
				IntelXeonMultiCore7xxx = 0xdf,
				/// <summary>Multi-Core Intel® Xeon® processor 3400 Series</summary>
				IntelXeon3400MultiCore = 0xe0,
				/// <summary>AMD Opteron™ 3000 Series Processor</summary>
				AmdOpteron3000 = 0xe4,
				/// <summary>AMD Sempron™ II Processor</summary>
				AmdSempronII = 0xe5,
				/// <summary>Embedded AMD Opteron™ Quad-Core Processor Family</summary>
				AmdOpteronQuadCoreEmbedded = 0xe6,
				/// <summary>AMD Phenom™ Triple-Core Processor Family</summary>
				AmdPhenomTripleCore = 0xe7,
				/// <summary>AMD Turion™ Ultra Dual-Core Mobile Processor Family</summary>
				AmdTurionUltraDualCoreMobile = 0xe8,
				/// <summary>AMD Turion™ Dual-Core Mobile Processor Family</summary>
				AmdTurionDualCoreMobile = 0xe9,
				/// <summary>AMD Athlon™ Dual-Core Processor Family</summary>
				AmdAthlonDualCore = 0xea,
				/// <summary>AMD Sempron™ SI Processor Family</summary>
				AmdSempronSI = 0xeb,
				/// <summary>AMD Phenom™ II Processor Family</summary>
				AmdPhenomII = 0xec,
				/// <summary>AMD Athlon™ II Processor Family</summary>
				AmdAthlonII = 0xed,
				/// <summary>Six-Core AMD Opteron™ Processor Family</summary>
				AmdOpteronSixCore = 0xee,
				/// <summary>AMD Sempron™ M Processor Family</summary>
				AmdSempronM = 0xef,
				/// <summary>i860</summary>
				i860 = 0xfa,
				/// <summary>i960</summary>
				i960 = 0xfb,
				///<summary>Indicator to obtain the processor family from the Processor Family 2 field</summary>
				Indicator = 0xfe,
				/// <summary>Reserved</summary>
				Reserved = 0xff,
				/// <summary>ARMv7</summary>
				Armv7 = 0x100,
				/// <summary>ARMv8</summary>
				Armv8 = 0x101,
				/// <summary>SH-3</summary>
				SH3 = 0x104,
				/// <summary>SH-4</summary>
				SH4 = 0x105,
				/// <summary>ARM</summary>
				ARM = 0x118,
				/// <summary>StrongARM</summary>
				StrongARM = 0x119,
				/// <summary>6x86</summary>
				_6x86 = 0x12C,
				/// <summary>MediaGX</summary>
				MediaGX = 0x12D,
				/// <summary>MII</summary>
				MII = 0x12E,
				/// <summary>WinChip</summary>
				WinChip = 0x140,
				/// <summary>DSP</summary>
				DSP = 0x15E,
				/// <summary>Video Processor</summary>
				VideoProcessor = 0x1F4,
				/// <summary>RISC-V RV32</summary>
				RiscV_RV32 = 0x200,
				/// <summary>RISC-V RV64</summary>
				RiscV_RV64 = 0x201,
				/// <summary>RISC-V RV128</summary>
				RiscV_RV128 = 0x202,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Socket Designation</summary>
			/// <remarks>String number for Reference Designation</remarks>
			public Byte SocketDesignation;
			/// <summary>Processor Type</summary>
			public ProcessorType Processor;
			/// <summary>Processor family</summary>
			private Byte ProcessorFamily1;
			/// <summary>Processor manufacturer</summary>
			/// <remarks>String number of Processor Manufacturer</remarks>
			public Byte ProcessorManufacturer;
			/// <summary>Processor ID</summary>
			/// <remarks>
			/// Raw processor identification data.
			/// The Processor ID field contains processor-specific information that describes the processor’s features
			/// <list type="square">
			///		<item>
			///			<term>x86-class CPUs</term>
			///			<description>
			///			For x86 class CPUs, the field’s format depends on the processor’s support of the CPUID instruction.
			///			If the instruction is supported, the Processor ID field contains two DWORD-formatted values.
			///			The first (offsets 0x08-0x0b) is the EAX value returned by a CPUID instruction with input EAX set to 1; the second (offsets 0x0c-0x0f) is the EDX value returned by that instruction.
			///			</description>
			///		</item>
			///		<item>
			///			<term>ARM32-class CPUs</term>
			///			<description>
			///			For ARM32-class CPUs, the Processor ID field contains two DWORD-formatted values.
			///			The first (offsets 0x08-0x0b) is the contents of the Main ID Register (MIDR); the second (offsets 0x0c-0x0f) is zero.
			///			</description>
			///		</item>
			///		<item>
			///			<term>ARM64-class CPUs</term>
			///			<description>
			///			For ARM64-class CPUs, the Processor ID field contains two DWORD-formatted values.
			///			The field's format depends on the processor's support of the SMCCC_ARCH_SOC_ID architectural call, as defined in the Arm SMC Calling Convention Specification v1.2 at https://developer.arm.com/architectures/system-1058 architectures/software-standards/smccc.
			///			Software can determine the support for SoC ID by examining the Processor Characteristics field for “Arm64 SoC ID” bit as defined in Table 27 – Processor Characteristics.
			///			If SoC ID is supported, the first DWORD (offsets 0x08-0x0b) is the JEP-106 code for the SiP value returned by a SMCCC_ARCH_SOC_ID call with input parameter SoC_ID_type set to 0;
			///			the second DWORD (offsets 0x0c-0x0f) is the SoC revision value returned by the SMCCC_ARCH_SOC_ID call with input parameter SoC_ID_type set to 1.
			///			System Management BIOS (SMBIOS) Reference Specification DSP0134 56 Published  Version 3.4.0 If SoC ID is not supported, the first DWORD (offsets 0x08-0x0b) is the contents of the MIDR_EL1 register; the second DWORD (offsets 0x0c-0x0f) is zero.
			///			</description>
			///		</item>
			///		<item>
			///			<term>RISC-V-class CPUs</term>
			///			<description>
			///			For RISC-V class CPUs, the processor ID contains a QWORD Machine Vendor ID CSR (mvendorid) of RISC-V processor hart 0.
			///			More information of RISC-V class CPU feature is described in RISC-V processor additional information (SMBIOS structure Type 44, 7.45).
			///			</description>
			///		</item>
			/// </list>
			/// </remarks>
			public UInt64 ProcessorID;
			/// <summary>Processor Version</summary>
			/// <remarks>String number describing the Processor</remarks>
			public Byte ProcessorVersion;
			/// <summary>Voltage</summary>
			/// <remarks>
			/// Two forms of information can be specified by the SMBIOS in this field, dependent on the value present in bit 7 (the most-significant bit).
			/// If bit 7 is 0 (legacy mode), the remaining bits of the field represent the specific voltages that the processor socket can accept
			/// </remarks>
			public VoltageFlags Voltage;
			/// <summary>External Clock</summary>
			/// <remarks>External Clock Frequency, in MHz If the value is unknown, the field is set to 0.</remarks>
			public UInt16 ExternalClock;
			/// <summary>Max Speed</summary>
			/// <remarks>
			/// Maximum processor speed (in MHz) supported by the system for this processor socket 0x0e9 is for a 233 MHz processor.
			/// If the value is unknown, the field is set to 0.
			/// </remarks>
			public UInt16 MaxSpeed;
			/// <summary>Current Speed</summary>
			/// <remarks>Same format as Max Speed</remarks>
			public UInt16 CurrentSpeed;
			/// <summary>Status</summary>
			/// <remarks>Processor Status</remarks>
			private Byte _Status;
			/// <summary>Processor Upgrade</summary>
			public ProcessorUpgradeType ProcessorUpgrade;
			/// <summary>L1 Cache Handle</summary>
			/// <remarks>
			/// Handle of a Cache Information structure that defines the attributes of the primary (Level 1) cache for this processor For version 2.1 and version 2.2 implementations, the value is 0FFFFh if the processor has no L1 cache.
			/// For version 2.3 and later implementations, the value is 0FFFFh if the Cache Information structure is not provided.
			/// </remarks>
			/// <value>v2.1+</value>
			public UInt16 L1CacheHandle;
			/// <summary>L2 Cache Handle</summary>
			/// <remarks>
			/// Handle of a Cache Information structure that defines the attributes of the secondary (Level 2) cache for this processor For version 2.1 and version 2.2 implementations, the value is 0FFFFh if the processor has no L2 cache.
			/// For version 2.3 and later implementations, the value is 0FFFFh if the Cache Information structure is not provided.
			/// </remarks>
			/// <value>v2.1+</value>
			public UInt16 L2CacheHandle;
			/// <summary>L3 Cache Handle</summary>
			/// <remarks>
			/// Handle of a Cache Information structure that defines the attributes of the tertiary(Level 3) cache for this processor.
			/// For version 2.1 and version 2.2 implementations, the value is 0x0ffff if the processor has no L3 cache.
			/// For version 2.3 and later implementations, the value is 0x0ffff if the Cache Information structure is not provided.
			/// </remarks>
			/// <value>v2.1+</value>
			public UInt16 L3CacheHandle;
			/// <summary>Serial number</summary>
			/// <remarks>
			/// String number for the serial number of this processor.
			/// This value is set by the manufacturer and normally not changeable.
			/// </remarks>
			/// <value>v2.1+</value>
			public Byte SerialNumber;
			/// <summary>Asset Tag</summary>
			/// <remarks>String number for the asset tag of this processor</remarks>
			/// <value>v2.3+</value>
			public Byte AssetType;
			/// <summary>Part Number</summary>
			/// <remarks>
			/// String number for the part number of this processor.
			/// This value is set by the manufacturer and normally not changeable.
			/// </remarks>
			/// <value>v2.3+</value>
			public Byte PartNumber;
			/// <summary>Core Count</summary>
			/// <remarks>
			/// Core Count is the number of cores detected by the BIOS for this processor socket.
			/// It does not necessarily indicate the full capability of the processor. For example, platform hardware may have the capability to limit the number of cores reported by the processor without BIOS intervention or knowledge.
			/// For a dual-core processor installed in a platform where the hardware is set to limit it to one core, the BIOS reports a value of 1 in <see cref="_CoreCount"/>.
			/// For a dual-core processor with multi-core support disabled by BIOS, the BIOS reports a value of 2 in <see cref="_CoreCount"/>.
			/// 
			/// If the value is unknown, the field is set to 0.
			/// For core counts of 256 or greater, the <see cref="_CoreCount"/> field is set to 0xff and the <see cref="_CoreCount2"/> field is set to the number of cores.
			/// </remarks>
			/// <value>v2.5+</value>
			public Byte _CoreCount;

			/// <summary>Core Enabled</summary>
			/// <remarks>
			/// Number of enabled cores per processor socket.
			/// Core Enabled is the number of cores that are enabled by the BIOS and available for Operating System use.
			/// For example, if the BIOS detects a dual-core processor, it would report a value of 2 if it leaves both cores enabled, and it would report a value of 1 if it disables multi-core support.
			/// 
			/// The Core Enabled 2 field supports core enabled counts > 255.
			/// For core enabled counts of 256 or greater, the Core Enabled field is set to FFh and the Core Enabled 2 field is set to the number of enabled cores.
			/// For core enabled counts of 255 or fewer, if Core Enabled 2 is present it shall be set to the same value as Core Enabled.
			/// This follows the approach used for the Core Count and Core Count 2 fields.
			/// </remarks>
			/// <example>
			/// If the value is unknown, the field is set 0.
			/// For core counts of 256 or greater, the Core Enabled field is set to FFh and the Core Enabled 2 field is set to the number of enabled cores.
			/// </example>
			private Byte _CoreEnabled;
			/// <summary>Thread Count</summary>
			/// <remarks>
			/// <see cref="_ThreadCount"/> is the total number of threads detected by the BIOS for this processor socket.
			/// It is a processor-wide count, not a thread-per-core count. It does not necessarily indicate the full capability of the processor.
			/// For example, platform hardware may have the capability to limit the number of threads reported by the processor without BIOS intervention or knowledge.
			/// For a dual-thread processor installed in a platform where the hardware is set to limit it to one thread, the BIOS reports a value of 1 in <see cref="_ThreadCount"/>.
			/// For a dual-thread processor with multi-threading disabled by BIOS, the BIOS reports a value of 2 in <see cref="_ThreadCount"/>.
			/// For a dual-core, dual-thread-per-core processor, the BIOS reports a value of 4 in <see cref="_ThreadCount"/>.
			/// 
			/// If the value is unknown, the field is set to 0.
			/// For thread counts of 256 or greater, the <see cref="_ThreadCount"/> field is set to 0xff and the <see cref="_ThreadCount2"/> field is set to the number of threads.
			/// </remarks>
			/// <value>v2.5+</value>
			private Byte _ThreadCount;
			/// <summary>Processor Characteristics</summary>
			/// <remarks>
			/// Enhanced Virtualization indicates that the processor is capable of executing enhanced virtualization instructions.
			/// This bit does not indicate the present state of this feature.
			/// 
			/// Power/Performance Control indicates that the processor is capable of load-based power savings.
			/// This bit does not indicate the present state of this feature.
			/// 
			/// Arm64 SoC ID indicates that the processor supports returning a SoC ID value using the SMCCC_ARCH_SOC_ID architectural call, as defined in the Arm SMC Calling Convention Specification v1.2 at
			/// https://developer.arm.com/architectures/system-architectures/software-standards/smccc.
			/// </remarks>
			/// <value>v2.5+</value>
			public ProcessorCharacteristicsFlags ProcessorCharacteristics;
			/// <summary>Processor family</summary>
			/// <remarks>Details the values for the Processor Information — Processor Family field.</remarks>
			/// <value>v2.6+</value>
			private UInt16 ProcessorFamily2;
			/// <summary>Core Count 2</summary>
			/// <remarks>
			/// The Core Count 2 field supports core counts > 255.
			/// For core counts of 256 or greater, the <see cref="_CoreCount"/> field is set to 0xff and the Core Count 2 field is set to the number of cores.
			/// For core counts of 255 or fewer, if <see cref="_CoreCount2"/> is present it shall be set the same value as <see cref="_CoreCount"/>.
			/// 
			/// Number of Cores per processor socket.
			/// Supports core counts >255.
			/// If this field is present, it holds the core count for the processor socket.
			/// Core Count will also hold the core count, except for core counts that are 256 or greater.
			/// In that case, <see cref="_CoreCount"/> shall be set to FFh and Core Count 2 will hold the count.
			/// </remarks>
			/// <example>
			/// Legal values:
			/// 0x0000 = unknown 
			/// 0x0001-0x00ff =  Core counts 1 to 255. Matches Core Count value. 
			/// 0x0100-0xfffe = Core counts 256 to 65534, respectively. 
			/// 0xffff = reserved
			/// </example>
			/// <value>v3.0+</value>
			public UInt16 _CoreCount2;
			/// <summary>Core Enabled 2</summary>
			/// <remarks>
			/// Number of enabled cores per processor socket.
			/// Supports core enabled counts >255.
			/// If this field is present, it holds the core enabled count for the processor socket.
			/// <see cref="_CoreEnabled"/> will also hold the core enabled count, except for core counts that are 256 or greater.
			/// In that case, Core Enabled shall be set to 0xff and <see cref="_CoreEnabled2"/> will hold the count.
			/// </remarks>
			/// <example>
			/// Legal values: 
			/// 0x0000 = unknown 
			/// 0x0001-0x00ff =  core enabled counts 1 to 255. Matches Core Enabled value. 
			/// 0x0100-0xfffe = core enabled counts 256 to 65534, respectively. 
			/// FFFFh = reserved
			/// </example>
			/// <value>v3.0+</value>
			private UInt16 _CoreEnabled2;
			/// <summary>Thread Count 2</summary>
			/// <remarks>
			/// Number of threads per processor socket.
			/// Supports thread counts >255.
			/// If this field is present, it holds the thread count for the processor socket.
			/// <see cref="_ThreadCount"/> will also hold the thread count, except for thread counts that are 256 or greater.
			/// In that case, <see cref="_ThreadCount"/> shall be set to 0xff and <see cref="_ThreadCount2"/> will hold the count
			///
			/// The <see cref="_ThreadCount2"/> field supports thread counts > 255.
			/// For thread counts of 256 or greater, the <see cref="_ThreadCount"/> field is set to 0xff and the <see cref="_ThreadCount2"/> field is set to the number of threads.
			/// For thread counts of 255 or fewer, if <see cref="_ThreadCount2"/> is present it shall be set to the same value as Thread Count.
			/// This follows the approach used for the <see cref="_CoreCount"/> and <see cref="_CoreCount2"/> fields.
			/// </remarks>
			/// <example>
			/// Legal values:
			/// 0x0000 = unknown 
			/// 0x0001-0x00ff = thread counts 1 to 255. Matches Thread Count value. 
			/// 0x0100-0xfffe = thread counts 256 to 65534, respectively. 
			/// 0xffff = reserved.
			/// </example>
			/// <value>v3.0+</value>
			private UInt16 _ThreadCount2;

			/// <summary>Processor family</summary>
			public ProcessorFamilyType ProcessorFamily
			{
				get
				{
					return (ProcessorFamilyType)(this.ProcessorFamily1 <= (Byte)ProcessorFamilyType.Indicator
						? this.ProcessorFamily1
						: (this.ProcessorFamily2 << 8) + this.ProcessorFamily1);
				}
			}

			/// <summary>CPU status</summary>
			public ProcessorStatusFlags Status { get { return (ProcessorStatusFlags)(this._Status >> 0 & 0x03); } }
			/// <summary>CPU Socket populated</summary>
			public Boolean StatusPopulated { get { return (this._Status >> 6 & 0x01) == 0x01; } }

			/// <summary>Thread count</summary>
			public UInt32 ThreadCount { get { return this._ThreadCount == Byte.MaxValue ? this._ThreadCount2 : this._ThreadCount; } }
			/// <summary>Core count</summary>
			public UInt16 CoreCount { get { return this._CoreCount == 0xff ? this._CoreCount2 : this._CoreCount; } }
			/// <summary>Core enabled count</summary>
			public UInt16 CoreEnabled { get { return this._CoreEnabled == 0xff ? this._CoreEnabled2 : this._CoreEnabled; } }
		}

		/// <summary>Memory Controller Information (Type 5, Obsolete)</summary>
		/// <remarks>
		/// This structure, and its companion, Memory Module Information (Type 6, Obsolete), are obsolete starting with version 2.1 of this specification;
		/// the Physical Memory Array (Type 16) and Memory Device (Type 17) structures should be used instead.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type5
		{
			/// <summary>Byte values for the Memory Controller Error Detecting Method field</summary>
			public enum ErrorDetectingMethodType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>None</summary>
				None = 0x03,
				/// <summary>8-bit Parity</summary>
				Parity8Bit = 0x04,
				/// <summary>32-bit ECC</summary>
				Ecc32Bit = 0x05,
				/// <summary>64-bit ECC</summary>
				Ecc64Bit = 0x06,
				/// <summary>128-bit ECC</summary>
				Ecc128Bit = 0x07,
				/// <summary>CRC</summary>
				CRC = 0x08,
			}

			/// <summary>Byte values for the Memory Controller Information — Interleave Support field</summary>
			[Flags]
			public enum ErrorCorrectingCapabilityFlags : byte
			{
				/// <summary>Other</summary>
				Other = 1 << 0,
				/// <summary>Unknown</summary>
				Unknown = 1 << 1,
				/// <summary>None</summary>
				None = 1 << 2,
				/// <summary>Single-Bit Error Correcting</summary>
				SingleBitErrorCorrecting = 1 << 3,
				/// <summary>Double-Bit Error Correcting</summary>
				DoubleBitErrorCorrecting = 1 << 4,
				/// <summary>Error Scrubbing</summary>
				ErrorScrubbing = 1 << 5,
			}

			/// <summary>Interleave Support field</summary>
			public enum InterleaveType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>One-Way Interleave</summary>
				OneWayInterleave = 0x03,
				/// <summary>Two-Way Interleave</summary>
				TwoWayInterleave = 0x04,
				/// <summary>Four-Way Interleave</summary>
				FourWayInterleave = 0x05,
				/// <summary>Eight-Way Interleave</summary>
				EightWayInterleave = 0x06,
				/// <summary>Sixteen-Way Interleave</summary>
				SixteenWayInterleave = 0x07,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Error Detecting Method</summary>
			public ErrorDetectingMethodType ErrorDetectingMethod;
			/// <summary>Error Correcting Capability</summary>
			/// <remarks>Byte values for the Memory Controller Information — Interleave Support field</remarks>
			public ErrorCorrectingCapabilityFlags ErrorCorrectingCapability;
			/// <summary>Supported Interleave</summary>
			/// <remarks>Interleave Support field</remarks>
			public InterleaveType SupportedInterleave;
			/// <summary>Current Interleave</summary>
			/// <remarks>Interleave Support field</remarks>
			public InterleaveType CurrentInterleave;
			/// <summary>Maximum Memory Module Size</summary>
			/// <remarks>
			/// Size of the largest memory module supported (per slot), specified as n, where 2**n is themaximum size in MB.
			/// The maximum amount of memory supported by this controller is that value times the number ofslots, as specified in offset 0Eh of this structure.
			/// </remarks>
			public Byte MaximumMemoryModuleSize;
			/// <summary>Supported Speeds</summary>
			public UInt16 SupportedSpeeds;
			/// <summary>Supported Memory Types</summary>
			public UInt16 SupportedMemoryTypes;
			/// <summary>Memory Module Voltage</summary>
			/// <remarks>
			/// Describes the required voltages for each of the memory module sockets controlled by this controller: 
			///		Bits 7:3 Reserved, must be zero
			///		Bit 2  2.9V
			///		Bit 1  3.3V
			///		Bit 0  5V
			/// NOTE:  Setting of multiple bits indicates that the sockets are configurable.
			/// </remarks>
			public Byte MemoryModuleVoltage;
			/// <summary>Number of Associated Memory Slots</summary>
			/// <remarks>Defines how many of the Memory Module Information blocks are controlled by this controller</remarks>
			public Byte NumberOfAssociatedMemorySlots;
			//TODO: Varable length array follows
		}

		/// <summary>Memory Module Information (Type 6, Obsolete) structure</summary>
		/// <remarks>
		/// This structure and its companion Memory Controller Information (Type 5, Obsolete) are obsolete starting with version 2.1 of this specification;
		/// the Physical Memory Array (Type 16) and Memory Device (Type 17) structures should be used instead.
		/// BIOS providers might choose to implement both memory description types to allow existing DMI browsers to properly display the system’s memory attributes. 
		/// </remarks>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type6
		{
			/// <summary>Memory module type</summary>
			[Flags]
			public enum MemoryTypeFlags : short
			{
				/// <summary>Other</summary>
				Other = 1 << 0,
				/// <summary>Unknown</summary>
				Unknown = 1 << 1,
				/// <summary>Standard</summary>
				Standard = 1 << 2,
				/// <summary>Fast Page Mode</summary>
				FastPageMode = 1 << 3,
				/// <summary>EDO</summary>
				EDO = 1 << 4,
				/// <summary>Parity</summary>
				Parity = 1 << 5,
				/// <summary>ECC</summary>
				ECC = 1 << 6,
				/// <summary>SIMM</summary>
				SIMM = 1 << 7,
				/// <summary>DIMM</summary>
				DIMM = 1 << 8,
				/// <summary>Burst EDO</summary>
				BurstEDO = 1 << 9,
				/// <summary>SDRAM</summary>
				SDRAM = 1 << 10,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>String number for reference designation</summary>
			/// <example>"J202",0</example>
			public Byte SocketDesignation;
			/// <summary>Each nibble indicates a bank (RAS#) connection; 0xF means no connection</summary>
			/// <example>
			/// If banks 1 and 3 (RAS# 1 and 3) were connected to a SIMM socket the byte for that socket would be 0x13.
			/// If only bank 2 (RAS 2) were connected, the byte for that socket would be 0x2f
			/// </example>
			public Byte BankConnections;
			/// <summary>
			/// Speed of the memory module, in ns (for example, 70d for a 70ns module)
			/// If the speed is unknown, the field is set to 0.
			/// </summary>
			public Byte CurrentSpeed;
			/// <summary>Describes the physical characteristics of the memory modules that are supported by (and currently installed in) the system</summary>
			public MemoryTypeFlags CurrentMemoryType;
			/// <summary>
			/// The Installed Size fields identify the size of the memory module that is installed in the socket, as determined by reading and correlating the module’s presence-detect information.
			/// If the system does not support presence-detect mechanisms, the Installed Size field is set to 0x7d to indicate that the installed size is not determinable.
			/// </summary>
			public Byte InstalledSize;
			/// <summary>
			/// The Enabled Size field identifies the amount of memory currently enabled for the system’s use from the module.
			/// If a module is known to be installed in a connector, but all memory in the module has been disabled due to error, the Enabled Size field is set to 0x7e
			/// </summary>
			public Byte EnabledSize;
			/// <summary>
			/// Bits 7:3	Reserved, set to 0s
			/// Bit 2		If set, the Error Status information should be obtained from the event log; bits 1and 0 are reserved. 
			/// Bit 1		Correctable errors received for the module, if set. This bit is reset only during a system reset.
			/// Bit 0		Uncorrectable errors received for the module, if set.
			///				All or a portion of the module has been disabled.
			///				This bit is only reset on power-on.
			/// </summary>
			public Byte ErrorStatus;
		}

		/// <summary>Cache Information (Type 7)</summary>
		/// <remarks>
		/// The information in this structure defines the attributes of CPU cache device in the system.
		/// One structure is specified for each such device, whether the device is internal to or external to the CPU module.
		/// Cache modules can be associated with a processor structure in one or two ways depending on the SMBIOS version
		/// </remarks>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type7
		{
			/// <summary>SRAM Type</summary>
			[Flags]
			public enum SramTypeFlags : ushort
			{
				/// <summary>Other</summary>
				Other = 1 << 0,
				/// <summary>Unknown</summary>
				Unknown = 1 << 1,
				/// <summary>Non-Burst</summary>
				NonBurst = 1 << 2,
				/// <summary>Burst</summary>
				Burst = 1 << 3,
				/// <summary>Pipeline Burst</summary>
				PipelineBurst = 1 << 4,
				/// <summary>Synchronous</summary>
				Synchronous = 1 << 5,
				/// <summary>Asynchronous</summary>
				Asynchronous = 1 << 6,
			}

			/// <summary>Error-correction scheme supported by this cache component</summary>
			public enum ErrorCorrectionType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>None</summary>
				None = 0x03,
				/// <summary>Parity</summary>
				Parity = 0x04,
				/// <summary>Single-bit ECC</summary>
				EccSingleBit = 0x05,
				/// <summary>Multi-bit ECC</summary>
				EccMultiBit = 0x06,
			}

			/// <summary>System Cache Type</summary>
			public enum SystemCacheType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Instruction</summary>
				Instruction = 0x03,
				/// <summary>Data</summary>
				Data = 0x04,
				/// <summary>Unified</summary>
				Unified = 0x05,
			}

			/// <summary>Associativity of the cache</summary>
			public enum AssociativityType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Direct Mapped</summary>
				DirectMapped = 0x03,
				/// <summary>2-way Set-Associative</summary>
				SetAssociative2Way = 0x04,
				/// <summary>4-way Set-Associative</summary>
				SetAssociative4Way = 0x05,
				/// <summary>Fully Associative</summary>
				FullyAssociative = 0x06,
				/// <summary>8-way Set-Associative</summary>
				SetAssociative8Way = 0x07,
				/// <summary>16-way Set-Associative</summary>
				SetAssociative16Way = 0x08,
				/// <summary>12-way Set-Associative</summary>
				SetAssociative12Way = 0x09,
				/// <summary>24-way Set-Associative</summary>
				SetAssociative24Way = 0x0A,
				/// <summary>32-way Set-Associative</summary>
				SetAssociative32Way = 0x0B,
				/// <summary>48-way Set-Associative</summary>
				SetAssociative48Way = 0x0C,
				/// <summary>64-way Set-Associative</summary>
				SetAssociative64Way = 0x0D,
				/// <summary>20-way Set-Associative</summary>
				SetAssociative20Way = 0x0E,
			}

			/// <summary>Cache configuration Operational Mode</summary>
			public enum CacheConfigurationOperationalModeType : byte
			{
				/// <summary>Write Through</summary>
				WriteThrough = 0x00,
				/// <summary>Write Back</summary>
				WriteBack = 0x01,
				/// <summary>Memory Address Vary</summary>
				MemoryAddressVary = 0x02,
				/// <summary>Unknown</summary>
				Unknown = 0x03,
			}

			/// <summary>Cache configuration Location, relative to the CPU module</summary>
			public enum CacheConfigurationLocationType : byte
			{
				/// <summary>Internal</summary>
				Internal = 0x00,
				/// <summary>External</summary>
				External = 0x01,
				/// <summary>Reserved</summary>
				Reserved = 0x02,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Socket Designation</summary>
			/// <remarks>String number for reference designation</remarks>
			/// <example>"CACHE1", 0</example>
			public Byte SocketDesignation;
			/// <summary>Cache Configuration</summary>
			/// <remarks>
			/// Bits 15:10 Reserved, must be zero 
			/// Bits 9:8 Operational Mode 
			///  00b – Write Through 
			///  01b – Write Back 
			///  10b – Varies with Memory Address 
			///  11b – Unknown 
			/// Bit 7  Enabled/Disabled (at boot time) 
			///  1b – Enabled 
			///  0b – Disabled 
			/// Bits 6:5 Location, relative to the CPU module:  
			///  00b – Internal  
			///  01b – External 
			///  10b – Reserved 
			///  11b – Unknown Bit 4 Reserved, must be zero 
			/// Bit 3  Cache Socketed (e.g. Cache on a Stick) 
			///  1b – Socketed 
			///  0b – Not Socketed 
			/// Bits 2:0 Cache Level – 1 through 8 (For example, an L1 cache would use value 000b and an L3 cache would use 010b.) 
			/// </remarks>
			public UInt16 CacheConfiguration;

			/// <summary>Maximum Cache Size</summary>
			/// <remarks>
			/// Maximum size that can be installed
			/// Bit 15 Granularity 
			///		0 – 1K granularity 
			///		1 – 64K granularity 
			/// Bits 14:0  Max size in given granularity
			/// 
			/// For multi-core processors, the cache size for the different levels of the cache (L1, L2, L3) is the total amount of cache per level per processor socket.
			/// The cache size is independent of the core count.
			/// For example, the cache size is 2 MB for both a dual core processor with a 2 MB L3 cache shared between the cores and a dual core processor with 1 MB L3 cache (non-shared) per core.
			/// Refer to the descriptions of the Maximum Cache Size 2 and Installed Cache 2 fields for information on representing cache sizes >2047MB.
			/// </remarks>
			public UInt16 MaximumCacheSize;
			/// <summary>Installed Size</summary>
			/// <remarks>Same format as Max Cache Size field; set to 0 if no cache is installed</remarks>
			public UInt16 InstalledSize;
			/// <summary>Supported SRAM Type</summary>
			public SramTypeFlags SupportedSRAMType;
			/// <summary>Current SRAM Type</summary>
			/// <value>v2.0+</value>
			public SramTypeFlags CurrentSRAMType;
			/// <summary>Cache Speed</summary>
			/// <remarks>
			/// Cache module speed, in nanoseconds.
			/// The value is 0 if the speed is unknown.
			/// </remarks>
			/// <value>v2.1+</value>
			public Byte CacheSpeed;
			/// <summary>Error Correction Type</summary>
			/// <remarks>Error-correction scheme supported by this cache component</remarks>
			/// <value>v2.1+</value>
			public ErrorCorrectionType ErrorCorrection;
			/// <summary>System Cache Type</summary>
			/// <remarks>
			/// Logical type of cache.
			/// The cache type for a cache level (L1, L2, L3, ...) is type 03h (Instruction) when all the caches at that level are Instruction caches.
			/// The cache type for a specific cache level (L1, L2, L3, ...) is type 0x04 (Data) when all the caches at that level are Data caches.
			/// The cache type for a cache level (L1, L2, L3, ...) is type 05h (Unified) when the caches at that level are a mix of Instruction and Data caches.
			/// </remarks>
			/// <value>v2.1+</value>
			public SystemCacheType SystemCache;
			/// <summary>Associativity</summary>
			/// <remarks>Associativity of the cache</remarks>
			/// <value>v2.1+</value>
			public AssociativityType Associativity;
			/// <summary>Maximum Cache Size 2</summary>
			/// <remarks>
			/// If this field is present, for cache sizes of 2047 MB or smaller the value in the Max size in given granularity portion of the field equals the size given in the corresponding portion of the Maximum Cache Size field, and the Granularity bit matches the value of the Granularity bit in the <see cref="MaximumCacheSize"/> field. 
			/// 
			/// For Cache sizes greater than 2047 MB, the <see cref="MaximumCacheSize"/> field is set to 0xFFFF and the <see cref="MaximumCacheSize2"/> field is present,
			/// the Granularity bit is set to 1b, and the size set as required; see <see cref="SystemCacheType"/>. 
			/// 
			/// Bit 31 Granularity 
			///		0 – 1K granularity 
			///		1 – 64K granularity (always 1b for cache sizes >2047 MB) 
			/// Bits 30:0  Max size in given granularity
			/// </remarks>
			/// <value>v3.1+</value>
			public UInt32 MaximumCacheSize2;
			/// <summary>Installed Cache Size 2</summary>
			/// <remarks>Same format as <see cref="MaximumCacheSize2"/> field; Absent or set to 0 if no cache is installed. <see cref="SystemCacheType"/></remarks>
			/// <value>v3.1+</value>
			public UInt32 InstalledCacheSize2;

			/// <summary>Operational Mode</summary>
			public CacheConfigurationOperationalModeType CacheConfigurationOperationalMode { get { return (CacheConfigurationOperationalModeType)(this.CacheConfiguration >> 8 & 3); } }

			/// <summary>Enabled/Disabled (at boot time)</summary>
			public Boolean CacheConfigurationEnabled { get { return (this.CacheConfiguration >> 7 & 1) == 1; } }

			/// <summary>Location, relative to the CPU module</summary>
			public CacheConfigurationLocationType CacheConfigurationLocation { get { return (CacheConfigurationLocationType)(this.CacheConfiguration >> 5 & 3); } }

			/// <summary>Socketed/not socketed</summary>
			public Boolean CacheConfigurationSoceted { get { return (this.CacheConfiguration >> 3 & 1) == 1; } }

			/// <summary>Cache level</summary>
			public Byte CacheConfigurationLevel { get { return (Byte)(this.CacheConfiguration >> 0 & 3); } }

			/// <summary>True - 64K granularity; False - 1K granularity</summary>
			public Boolean MaximumCacheSize64KGranularity { get { return (this.MaximumCacheSize >> 15 & 1) == 1; } }
			/// <summary>Maximum cache size</summary>
			public UInt16 MaximumCacheSizeNoGranularity { get { return (UInt16)(this.MaximumCacheSize >> 0 & 0x7fff); } }
		}

		/// <summary>Port Connector Information (Type 8)</summary>
		/// <remarks>
		/// the information in this structure defines the attributes of a system port connector (for example, parallel, serial, keyboard, or mouse ports).
		/// The port’s type and connector information are provided.
		/// One structure is present for each port provided by the system.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type8
		{
			/// <summary>Shows the values of the bytes in the Port Information — Connector Types field</summary>
			public enum ConnectorType : byte
			{
				/// <summary>None</summary>
				None = 0x00,
				/// <summary>Centronics</summary>
				Centronics = 0x01,
				/// <summary>Mini Centronics</summary>
				MiniCentronics = 0x02,
				/// <summary>Proprietary</summary>
				Proprietary = 0x03,
				/// <summary>DB-25 pin male</summary>
				DB25PinMale = 0x04,
				/// <summary>DB-25 pin female</summary>
				DB25PinFemale = 0x05,
				/// <summary>DB-15 pin male</summary>
				DB15PinMale = 0x06,
				/// <summary>DB-15 pin female</summary>
				DB15PinFemale = 0x07,
				/// <summary>DB-9 pin male</summary>
				DB9PinMale = 0x08,
				/// <summary>DB-9 pin female</summary>
				DB9PinFemale = 0x09,
				/// <summary>RJ-11</summary>
				RJ11 = 0x0a,
				/// <summary>RJ-45</summary>
				RJ45 = 0x0b,
				/// <summary>50-pin MiniSCSI</summary>
				MiniScsi50Pin = 0x0C,
				/// <summary>Mini-DIN</summary>
				MiniDIN = 0x0D,
				/// <summary>Micro-DIN</summary>
				MicroDIN = 0x0E,
				/// <summary>PS/2</summary>
				PS2 = 0x0F,
				/// <summary>Infrared</summary>
				Infrared = 0x10,
				/// <summary>HP-HIL</summary>
				HpHil = 0x11,
				/// <summary>Access Bus (USB)</summary>
				AccessBusUSB = 0x12,
				/// <summary>SSA SCSI</summary>
				SsaScsi = 0x13,
				/// <summary>Circular DIN-8 male</summary>
				CircularDin8Male = 0x14,
				/// <summary>Circular DIN-8 female</summary>
				CircularDin8Female = 0x15,
				/// <summary>On Board IDE</summary>
				OnBoardIde = 0x16,
				/// <summary>On Board Floppy</summary>
				OnBoardFloppy = 0x17,
				/// <summary>9-pin Dual Inline (pin 10 cut)</summary>
				DualInline9Pin = 0x18,
				/// <summary>25-pin Dual Inline (pin 26 cut)</summary>
				DualInline25Pin = 0x19,
				/// <summary>50-pin Dual Inline</summary>
				DualInline50Pin = 0x1A,
				/// <summary>68-pin Dual Inline</summary>
				DualInline68Pin = 0x1B,
				/// <summary>On Board Sound Input from CD-ROM</summary>
				OnBoardSoundInput = 0x1C,
				/// <summary>Mini-Centronics Type-14</summary>
				MiniCentronicsType14 = 0x1D,
				/// <summary>Mini-Centronics Type-26</summary>
				MiniCentronicsType26 = 0x1E,
				/// <summary>Mini-jack (headphones)</summary>
				MiniJack = 0x1F,
				/// <summary>BNC</summary>
				Bnc = 0x20,
				/// <summary>1394</summary>
				_1394 = 0x21,
				/// <summary>SAS/SATA Plug Receptacle</summary>
				SAS_SATAPlug = 0x22,
				/// <summary>USB Type-C Receptacle</summary>
				UsbTypeC = 0x23,
				/// <summary>PC-98</summary>
				Pc98 = 0xA0,
				/// <summary>PC-98Hireso</summary>
				Pc98Hireso = 0xA1,
				/// <summary>PC-H98</summary>
				PcH98 = 0xA2,
				/// <summary>PC-98Note</summary>
				Pc98Note = 0xA3,
				/// <summary>PC-98Full</summary>
				Pc98Full = 0xA4,
			}

			/// <summary>Describes the function of the port</summary>
			public enum PortType : byte
			{
				/// <summary>None</summary>
				None = 0x00,
				/// <summary>Parallel Port XT/AT Compatible</summary>
				ParallelPortXT_AT = 0x01,
				/// <summary>Parallel Port PS/2</summary>
				ParallelPortPS_2 = 0x02,
				/// <summary>Parallel Port ECP</summary>
				ParallelPortECP = 0x03,
				/// <summary>Parallel Port EPP</summary>
				ParallelPortEPP = 0x04,
				/// <summary>Parallel Port ECP/EPP</summary>
				ParallelPortECP_EPP = 0x05,
				/// <summary>Serial Port XT/AT Compatible</summary>
				SerialPortXT_AT = 0x06,
				/// <summary>Serial Port 16450 Compatible</summary>
				SerialPort16450 = 0x07,
				/// <summary>Serial Port 16550 Compatible</summary>
				SerialPort16550 = 0x08,
				/// <summary>Serial Port 16550A Compatible</summary>
				SerialPort16550A = 0x09,
				/// <summary>SCSI Port</summary>
				Scsi = 0x0A,
				/// <summary>MIDI Port</summary>
				Midi = 0x0B,
				/// <summary>Joy Stick Port</summary>
				JoyStick = 0x0C,
				/// <summary>Keyboard Port</summary>
				Keyboard = 0x0D,
				/// <summary>Mouse Port</summary>
				Mouse = 0x0E,
				/// <summary>SSA SCSI</summary>
				SsaScsi = 0x0F,
				/// <summary>USB</summary>
				USB = 0x10,
				/// <summary>FireWire (IEEE P1394)</summary>
				FireWire = 0x11,
				/// <summary>PCMCIA Type I2</summary>
				PcmciaI2 = 0x12,
				/// <summary>PCMCIA Type II</summary>
				PcmciaII = 0x13,
				/// <summary>PCMCIA Type III</summary>
				PcmciaIII = 0x14,
				/// <summary>Cardbus</summary>
				CardBus = 0x15,
				/// <summary>Access Bus Port</summary>
				AccessBus = 0x16,
				/// <summary>SCSI II</summary>
				ScsiII = 0x17,
				/// <summary>SCSI Wide</summary>
				ScsiWide = 0x18,
				/// <summary>PC-98</summary>
				Pc98 = 0x19,
				/// <summary>PC-98-Hireso</summary>
				Pc98Hireso = 0x1A,
				/// <summary>PC-H98</summary>
				PcH98 = 0x1B,
				/// <summary>Video Port</summary>
				Video = 0x1C,
				/// <summary>Audio Port</summary>
				Audio = 0x1D,
				/// <summary>Modem Port</summary>
				Modem = 0x1E,
				/// <summary>Network Port</summary>
				Network = 0x1F,
				/// <summary>SATA</summary>
				Sata = 0x20,
				/// <summary>SAS</summary>
				Sas = 0x21,
				/// <summary>MFDP (Multi-Function Display Port)</summary>
				Mfdp = 0x22,
				/// <summary>Thunderbolt</summary>
				Thunderbolt = 0x23,
				/// <summary>8251 Compatible </summary>
				Compatible8251 = 0xa0,
				/// <summary>8251 FIFO Compatible</summary>
				Compatible8251Fifo = 0xa1,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Internal Reference Designator</summary>
			/// <remarks>String number for Internal Reference Designator, that is, internal to the system enclosure</remarks>
			/// <example> "J101", 0</example>
			public Byte InternalReferenceDesignator;
			/// <summary>Internal Connector type</summary>
			public ConnectorType InternalConnectorType;
			/// <summary>External Reference Designator</summary>
			/// <remarks>String number for the External Reference Designation external to the system enclosure</remarks>
			/// <example>"COM A", 0</example>
			public Byte ExternalReferenceDesignator;
			/// <summary>External Connector type</summary>
			public ConnectorType ExternalConnectorType;
			/// <summary>Port</summary>
			/// <remarks>Describes the function of the port</remarks>
			public PortType Port;
		}

		/// <summary>System Slots (Type 9)</summary>
		/// <remarks>
		/// The information in this structure defines the attributes of a system slot.
		/// One structure is provided for each slot in the system
		/// </remarks>
		/// <value>v2.0+</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type9
		{
			/// <summary>System Slots — Slot Type</summary>
			public enum SlotType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>ISA</summary>
				Isa = 0x03,
				/// <summary></summary>
				Mcs = 0x04,
				/// <summary>EISA</summary>
				Eisa = 0x05,
				/// <summary>PCI</summary>
				PCI = 0x06,
				/// <summary>PC Card (PCMCIA)</summary>
				Pcmcia = 0x07,
				/// <summary>VL-VESA</summary>
				VlVesa = 0x08,
				/// <summary>Proprietary</summary>
				Proprietary = 0x09,
				/// <summary>Processor Card Slot</summary>
				ProcessorCard = 0x0A,
				/// <summary>Proprietary Memory Card Slot</summary>
				MemoryCardProprietary = 0x0B,
				/// <summary>I/O Riser Card Slot</summary>
				IORiserCardSlot = 0x0C,
				/// <summary>NuBus</summary>
				NuBus = 0x0D,
				/// <summary>PCI – 66MHz Capable</summary>
				Pci_66MHzCapable = 0x0E,
				/// <summary>AGP</summary>
				Agp = 0x0F,
				/// <summary>0x10</summary>
				Agp2X = 0x10,
				/// <summary>AGP 4X</summary>
				Agp4X = 0x11,
				/// <summary>PCI-X</summary>
				PciX = 0x12,
				/// <summary>AGP 8X</summary>
				Agp8X = 0x13,
				/// <summary>M.2 Socket 1-DP (Mechanical Key A)</summary>
				M2Socket1DPKeyA = 0x14,
				/// <summary>M.2 Socket 1-SD (Mechanical Key E)</summary>
				M2Socket1KeyE = 0x15,
				/// <summary>M.2 Socket 2 (Mechanical Key B)</summary>
				M2Socket2KeyB = 0x16,
				/// <summary>M.2 Socket 3 (Mechanical Key M)</summary>
				M2Socket3KeyM = 0x17,
				/// <summary>MXM Type I</summary>
				MxmTypeI = 0x18,
				/// <summary>MXM Type II</summary>
				MxmTypeII = 0x19,
				/// <summary>MXM Type III (standard connector)</summary>
				MxmTypeIIIStandard = 0x1A,
				/// <summary>MXM Type III (HE connector)</summary>
				MxmTypeIII_He = 0x1B,
				/// <summary>MXM Type IV</summary>
				MxmTypeIV = 0x1C,
				/// <summary>MXM 3.0 Type A</summary>
				Mxm30TypeA = 0x1D,
				/// <summary>MXM 3.0 Type B</summary>
				Mxm30TypeB = 0x1E,
				/// <summary>PCI Express Gen 2 SFF-8639 (U.2)</summary>
				PciExpressGen2_Sff8639 = 0x1F,
				/// <summary>PCI Express Gen 3 SFF-8639 (U.2)</summary>
				PciExpressGen3_Sff8639 = 0x20,
				/// <summary>PCI Express Mini 52-pin (CEM spec. 2.0) with bottom-side keep-outs</summary>
				/// <remarks>
				/// Use Slot Length field value
				/// 0x03 (short length) for "half-Mini card" -only support,
				/// 0x04 (long length) for "full-Mini card" or dual support.
				/// </remarks>
				PciExpressMini52Pin_WithBottomSize = 0x21,

				/// <summary>PCI Express Mini 52-pin (CEM spec. 2.0) without bottom-side keep-outs</summary>
				/// <remarks>
				/// Use Slot Length field value
				/// 0x03 (short length) for "half-Mini card" -only support,
				/// 0x04 (long length) for "full-Mini card" or dual support.
				/// </remarks>
				PciExpressMini52Pin_WithoutBottomSide = 0x22,
				/// <summary>PCI Express Mini 76-pin (CEM spec. 2.0) Corresponds to Display-Mini card</summary>
				PciExpressMini76Pin = 0x23,
				/// <summary>PCI Express Gen 4 SFF-8639 (U.2)</summary>
				PciExpressGen4_Sff8639 = 0x24,
				/// <summary>PCI Express Gen 5 SFF-8639 (U.2)</summary>
				PciExpressGen5_Sff8639 = 0x25,
				/// <summary>OCP NIC 3.0 Small Form Factor (SFF)</summary>
				OcpNic30_SFF = 0x26,
				/// <summary>OCP NIC 3.0 Large Form Factor (LFF)</summary>
				OcpNic30_LFF = 0x27,
				/// <summary>OCP NIC Prior to 3.0</summary>
				OcpNic = 0x28,
				/// <summary>CXL Flexbus 1.0 (deprecated, see note below)</summary>
				/// <remarks>
				/// CXL Flexbus-capable slots can be described in Table 51 – Slot Characteristics 2 (section 7.10.7), Bits[6:5] for any PCIe Gen 5 or above (all lengths) slot types.
				/// For example, if Slot Type is PCIe Gen 5 x4 and bit 5 of Slot Characteristics 2 is set, this indicates a CXL 1.0-capable x4 slot that can operate at PCIe Gen 5 data rate.
				/// </remarks>
				CxlFlexbus10 = 0x30,
				/// <summary>PC-98/C20</summary>
				PC98_C20 = 0xA0,
				/// <summary>PC-98/C24</summary>
				PC98_C24 = 0xA1,
				/// <summary>PC-98/E</summary>
				PC98_E = 0xA2,
				/// <summary>PC-98/Local Bus</summary>
				PC98_LocalBus = 0xA3,
				/// <summary>PC-98/Card</summary>
				PC98_Card = 0xA4,
				/// <summary>PCI Express</summary>
				/// <remarks>
				/// Slot types A5h, ABh, B1h, B8h, and BEh should be used only for PCI Express slots where the physical width is identical to the electrical width;
				/// in that case the System Slots – Slot Data Bus Width field specifies the width.
				/// Other PCI Express slot types (A6h-AAh, ACh-B0h, B2h-B6h, B9h-BDh, BFh-C3h) should be used to describe slots where the physical width is different from the maximum electrical width;
				/// in these cases the width indicated in this field refers to the physical width of the slot, while electrical width is described in the System Slots – Slot Data Bus Width field. 
				/// Although not expressly defined in the table above, slot types A5h through AAh are PCI Express Generation 1 values. 
				/// </remarks>
				PciExpress = 0xA5,
				/// <summary>PCI Express x1</summary>
				PciExpressX1 = 0xA6,
				/// <summary>PCI Express x2</summary>
				PciExpressX2 = 0xA7,
				/// <summary>PCI Express x4</summary>
				PciExpressX4 = 0xA8,
				/// <summary>PCI Express x8</summary>
				PciExpressX8 = 0xA9,
				/// <summary></summary>
				PciExpressX16 = 0xAA,
				/// <summary>PCI Express Gen 2</summary>
				/// <remarks>
				/// Slot types A5h, ABh, B1h, B8h, and BEh should be used only for PCI Express slots where the physical width is identical to the electrical width;
				/// in that case the System Slots – Slot Data Bus Width field specifies the width.
				/// Other PCI Express slot types (A6h-AAh, ACh-B0h, B2h-B6h, B9h-BDh, BFh-C3h) should be used to describe slots where the physical width is different from the maximum electrical width;
				/// in these cases the width indicated in this field refers to the physical width of the slot, while electrical width is described in the System Slots – Slot Data Bus Width field. 
				/// Although not expressly defined in the table above, slot types A5h through AAh are PCI Express Generation 1 values.
				/// </remarks>
				PciExpressGen2 = 0xAB,
				/// <summary>PCI Express Gen 2 x1</summary>
				PciExpressGen2x1 = 0xAC,
				/// <summary>PCI Express Gen 2 x2</summary>
				PciExpressGen2x2 = 0xAD,
				/// <summary>PCI Express Gen 2 x4</summary>
				PciExpressGen2x4 = 0xAE,
				/// <summary>PCI Express Gen 2 x8</summary>
				PciExpressGen2x8 = 0xAF,
				/// <summary>PCI Express Gen 2 x16</summary>
				PciExpressGen2x16 = 0xB0,
				/// <summary>PCI Express Gen 3</summary>
				/// <remarks>
				/// Slot types A5h, ABh, B1h, B8h, and BEh should be used only for PCI Express slots where the physical width is identical to the electrical width;
				/// in that case the System Slots – Slot Data Bus Width field specifies the width.
				/// Other PCI Express slot types (A6h-AAh, ACh-B0h, B2h-B6h, B9h-BDh, BFh-C3h) should be used to describe slots where the physical width is different from the maximum electrical width;
				/// in these cases the width indicated in this field refers to the physical width of the slot, while electrical width is described in the System Slots – Slot Data Bus Width field. 
				/// Although not expressly defined in the table above, slot types A5h through AAh are PCI Express Generation 1 values.
				/// </remarks>
				PciExpressGen3 = 0xB1,
				/// <summary>PCI Express Gen 3 x1</summary>
				PciExpressGen3x1 = 0xB2,
				/// <summary>PCI Express Gen 3 x2</summary>
				PciExpressGen3x2 = 0xB3,
				/// <summary>PCI Express Gen 3 x4</summary>
				PciExpressGen3x4 = 0xB4,
				/// <summary>PCI Express Gen 3 x8</summary>
				PciExpressGen3x8 = 0xB5,
				/// <summary>PCI Express Gen 3 x16</summary>
				PciExpressGen3x16 = 0xB6,
				/// <summary>PCI Express Gen 4</summary>
				/// <remarks>
				/// Slot types A5h, ABh, B1h, B8h, and BEh should be used only for PCI Express slots where the physical width is identical to the electrical width;
				/// in that case the System Slots – Slot Data Bus Width field specifies the width.
				/// Other PCI Express slot types (A6h-AAh, ACh-B0h, B2h-B6h, B9h-BDh, BFh-C3h) should be used to describe slots where the physical width is different from the maximum electrical width;
				/// in these cases the width indicated in this field refers to the physical width of the slot, while electrical width is described in the System Slots – Slot Data Bus Width field. 
				/// Although not expressly defined in the table above, slot types A5h through AAh are PCI Express Generation 1 values.
				/// </remarks>
				PciExpressGen4 = 0xB8,
				/// <summary>PCI Express Gen 4 x1</summary>
				PciExpressGen4x1 = 0xB9,
				/// <summary>PCI Express Gen 4 x2</summary>
				PciExpressGen4x2 = 0xBA,
				/// <summary>PCI Express Gen 4 x4</summary>
				PciExpressGen4x4 = 0xBB,
				/// <summary>PCI Express Gen 4 x8</summary>
				PciExpressGen4x8 = 0xBC,
				/// <summary>PCI Express Gen 4 x16</summary>
				PciExpressGen4x16 = 0xBD,
				/// <summary>PCI Express Gen 5</summary>
				/// <remarks>
				/// Slot types A5h, ABh, B1h, B8h, and BEh should be used only for PCI Express slots where the physical width is identical to the electrical width;
				/// in that case the System Slots – Slot Data Bus Width field specifies the width.
				/// Other PCI Express slot types (A6h-AAh, ACh-B0h, B2h-B6h, B9h-BDh, BFh-C3h) should be used to describe slots where the physical width is different from the maximum electrical width;
				/// in these cases the width indicated in this field refers to the physical width of the slot, while electrical width is described in the System Slots – Slot Data Bus Width field. 
				/// Although not expressly defined in the table above, slot types A5h through AAh are PCI Express Generation 1 values.
				/// </remarks>
				PciExpressGen5 = 0xBE,
				/// <summary>PCI Express Gen 5 x1</summary>
				PciExpressGen5x1 = 0xBF,
				/// <summary>PCI Express Gen 5 x2</summary>
				PciExpressGen5x2 = 0xC0,
				/// <summary>PCI Express Gen 5 x4</summary>
				PciExpressGen5x4 = 0xC1,
				/// <summary>PCI Express Gen 5 x8</summary>
				PciExpressGen5x8 = 0xC2,
				/// <summary>PCI Express Gen 5 x16</summary>
				PciExpressGen5x16 = 0xC3,
				/// <summary>PCI Express Gen 6 and Beyond</summary>
				/// <remarks>see Slot Information and Slot Physical Width fields for more details</remarks>
				PciExpressGen6Plus = 0xC4,
				/// <summary>
				/// Enterprise and Datacenter 1U E1 Form Factor Slot (EDSFF E1.S, E1.L) 
				/// E1 slot length is reported in Slot Length field. 
				/// E1 slot pitch is reported in Slot Pitch field. 
				/// </summary>
				/// <remarks>See specifications SFF-TA-1006 and SFF-TA-1007 for more details on values for slot length and pitch.</remarks>
				EnterpriseAndDatacenter = 0xc5,
				/// <summary>
				/// Enterprise and Datacenter 3" E3 Form Factor Slot (EDSFF E3.S, E3.L) 
				/// E3 slot length is reported in Slot Length field. 
				/// E3 slot pitch is reported in Slot Pitch field. See specification SFF-TA-1008 for details on values for slot length and pitch
				/// </summary>
				EnterpriseAndDatacenter3 = 0xc6,
			}

			/// <summary>
			/// Slot Data Bus Width meanings of type “n bit” are for parallel buses such as PCI.
			/// Slot Data Bus Width meanings of type “nx or xn” are for serial buses such as PCI Express.
			/// </summary>
			/// <remarks>
			/// For PCI Express, width refers to the maximum supported electrical width of the “data bus”;
			/// physical slot width is described in System Slots – Slot Type, and the actual link width resulting from PCI Express link training can be read from configuration space.
			/// </remarks>
			public enum SlotWidthType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>8 bit</summary>
				Bit8 = 0x03,
				/// <summary>16 bit</summary>
				Bit16 = 0x04,
				/// <summary>32 bit</summary>
				Bit32 = 0x05,
				/// <summary>64 bit</summary>
				Bit64 = 0x06,
				/// <summary>128 bit</summary>
				Bit128 = 0x07,
				/// <summary>1x or x1</summary>
				_1xORx1 = 0x08,
				/// <summary>2x or x2</summary>
				_2xORx2 = 0x09,
				/// <summary>4x or x4</summary>
				_4xORx4 = 0x0A,
				/// <summary>8x or x8</summary>
				_8xORx8 = 0x0B,
				/// <summary>12x or x12</summary>
				_12xORx12 = 0x0C,
				/// <summary>16x or x16</summary>
				_16xORx16 = 0x0D,
				/// <summary>32x or x32</summary>
				_32xORx32 = 0x0E,
			}

			/// <summary>System Slots — Current Usage field</summary>
			public enum CurrentUsageType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Available</summary>
				Available = 0x03,
				/// <summary>In Use</summary>
				InUse = 0x04,
				/// <summary>Unavailable (e.g., connected to a processor that is not installed)</summary>
				Unavailable = 0x05,
			}

			/// <summary>System Slots — Slot Length field</summary>
			/// <remarks>
			/// For EDSFF E1.S slots, use "short length". For EDSFF E1.L slots, use "long length".
			/// For EDSFF E3.S slots, use "short length". For EDSFF E3.L slots, use "long length".
			/// </remarks>
			public enum SlotLengthType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Short Length</summary>
				ShortLength = 0x03,
				/// <summary>Long Length</summary>
				LongLength = 0x04,
				/// <summary>2.5" drive form factor</summary>
				Drive25 = 0x05,
				/// <summary>3.5" drive form factor</summary>
				Drive35 = 0x06,
			}

			/// <summary>Slot Characteristics 1</summary>
			[Flags]
			public enum SlotCharacteristicsFlags1 : byte
			{
				/// <summary>Characteristics unknown</summary>
				Unknown = 1 << 0,
				/// <summary>Provides 5.0 volts</summary>
				Volts50 = 1 << 1,
				/// <summary>Provides 3.3 volts</summary>
				Volts33 = 1 << 2,
				/// <summary>Slot’s opening is shared with another slot (for example, PCI/EISA shared slot)</summary>
				SharedSlot = 1 << 3,
				/// <summary>PC Card slot supports PC Card-16</summary>
				PcCard16_Supported = 1 << 4,
				/// <summary>PC Card slot supports CardBus</summary>
				CardBus_Supported = 1 << 5,
				/// <summary>PC Card slot supports Zoom Video</summary>
				ZoomVideo_Supported = 1 << 6,
				/// <summary>PC Card slot supports Modem Ring Resume</summary>
				ModemRingResume_Supported = 1 << 7,
			}

			/// <summary>Slot Characteristics 2</summary>
			[Flags]
			public enum SlotCharacteristicsFlags2 : byte
			{
				/// <summary>PCI slot supports Power Management Event (PME#) signal</summary>
				PowerManagementEvent = 1 << 0,
				/// <summary>Slot supports hot-plug devices</summary>
				HotPlug = 1 << 1,
				/// <summary>PCI slot supports SMBus signal</summary>
				SmBus = 1 << 2,
				/// <summary>PCIe slot supports bifurcation. This slot can partition its lanes into two or more PCIe devices plugged into the slot</summary>
				/// <remarks>This field does not indicate complete details on what levels of bifurcation are supported by the slot, but only that the slot supports some level of bifurcation.</remarks>
				Bifurcation = 1 << 3,
				/// <summary>Slot supports async/surprise removal (i.e., removal without prior notification to the operating system, device driver, or applications)</summary>
				AsyncSurpriseRemoval = 1 << 4,
				/// <summary>Flexbus slot, CXL 1.0 capable</summary>
				/// <remarks>
				/// CXL capability of slots should be reported as below:
				/// Bit5=0 and Bit6=0 - Non CXL-capable slot
				/// Bit5=X and Bit6=1 - Flexbus slot, CXL 2.0 capable (backward compatible to 1.0)
				/// Bit5=1 and Bit6=0 - Flexbus slot, CXL 1.0 capable 
				/// </remarks>
				CxlCapable10 = 1 << 5,
				/// <summary>Flexbus slot, CXL 2.0 capable</summary>
				/// <remarks>
				/// CXL capability of slots should be reported as below:
				/// Bit5=0 and Bit6=0 - Non CXL-capable slot
				/// Bit5=X and Bit6=1 - Flexbus slot, CXL 2.0 capable (backward compatible to 1.0)
				/// Bit5=1 and Bit6=0 - Flexbus slot, CXL 1.0 capable 
				/// </remarks>
				CxlCapable20 = 1 << 6,
				/// <summary>Reserved, set to 0</summary>
				Reserved = 1 << 7,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Slot Designation</summary>
			/// <remarks>String number for reference designation</remarks>
			/// <example>"PCI-1",0</example>
			public Byte SlotDesignation;
			/// <summary>Slot</summary>
			/// <remarks>System Slots — Slot Type</remarks>
			public SlotType Slot;
			/// <summary>Slot Data Bus width</summary>
			/// <remarks>
			/// Slot Data Bus Width meanings of type “n bit” are for parallel buses such as PCI.
			/// Slot Data Bus Width meanings of type “nx or xn” are for serial buses such as PCI Express.
			/// </remarks>
			public SlotWidthType SlotDataBusWidth;
			/// <summary>Current Usage</summary>
			/// <remarks>System Slots — Current Usage field</remarks>
			public CurrentUsageType CurrentUsage;
			/// <summary>Slot length</summary>
			/// <remarks>System Slots — Slot Length field</remarks>
			public SlotLengthType SlotLength;
			/// <summary>Slot ID</summary>
			/// <remarks>
			/// The Slot ID field of the System Slot structure provides a mechanism to correlate the physical attributes of the slot to its logical access method (which varies based on the Slot Type field).
			/// 
			/// MCA - Identifies the logical Micro Channel slot number, in the range 1 to 15, in offset 09h. Offset 0Ah is set to 0.
			/// EISA - Identifies the logical EISA slot number, in the range 1 to 15, in offset 09h. Offset 0Ah is set to 0.
			/// PCI, AGP, PCI-X, PCI Express - 
			///		On a system that supports ACPI, identifies the value returned in the _SUN object for this slot.
			///		On a system that supports the PCI IRQ Routing Table Specification, identifies the value present in the Slot Number field of the PCI Interrupt Routing table entry that is associated with this slot, 
			///		in offset 09h—offset 0Ah is set to 0.
			///		The table is returned by the “Get PCI Interrupt Routing Options” PCI BIOS function call and provided directly in the PCI IRQ Routing Table Specification ($PIRQ).
			///		Software can determine the PCI bus number and device associated with the slot by matching the "Slot ID" to an entry in the routing-table and ultimately determine what device is present in that slot.
			/// PCMCIA - Identifies the Adapter Number (offset 09h) and Socket Number (offset 0Ah) to be passed to PCMCIA Socket Services to identify this slot.
			/// </remarks>
			public UInt16 SlotID;
			/// <summary>Slot Characteristics 1</summary>
			public SlotCharacteristicsFlags1 SlotCharacteristics1;
			/// <summary>Slot Characteristics 2</summary>
			/// <value>v2.1+</value>
			public SlotCharacteristicsFlags2 SlotCharacteristics2;
			/// <summary>Segment Group Number</summary>
			/// <remarks>
			/// For slots that are not of types PCI, AGP, PCI-X, or PCI-Express that do not have bus/device/function information, 0FFh should be populated in the fields of Segment Group Number, Bus Number, Device/Function Number.
			/// 
			/// Segment Group Number is defined in the PCI Firmware Specification. The value is 0 for a single-segment topology.
			/// 
			/// For PCI Express slots, Bus Number and Device/Function Number refer to the endpoint in the slot, not the upstream switch.
			/// </remarks>
			/// <value>v2.6+</value>
			public UInt16 SegmentGroupNumber;
			/// <summary>Bus Number</summary>
			/// <remarks>
			/// Because some slots can be partitioned into smaller electrical widths, additional peer device Segment/Bus/Device/Function are defined.
			/// These peer groups are defined in Table 53.
			/// The base device is the lowest ordered Segment/Bus/Device/Function and is listed first (offsets 0x0d-0x11).
			/// Peer devices are listed in the peer grouping section.
			/// This definition does not cover children devices i.e., devices behind a PCIe bridge in the slot.
			/// </remarks>
			/// <value>v2.6+</value>
			public Byte BusNumber;
			/// <summary>Device Function Number</summary>
			/// <remarks>
			/// For slots that are not of types PCI, AGP, PCI-X, or PCI-Express that do not have bus/device/function information, 0x0ff should be populated in the fields of Segment Group Number, Bus Number, Device/Function Number.
			/// Segment Group Number is defined in the PCI Firmware Specification. The value is 0 for a single-segment topology.
			/// For PCI Express slots, Bus Number and Device/Function Number refer to the endpoint in the slot, not the upstream switch.
			/// 
			/// Bits 7:3 – device number
			/// Bits 2:0 – function number
			/// </remarks>
			/// <value>v2.6+</value>
			public Byte DeviceFunctionNumber;
			/// <summary>Data Bus Width</summary>
			/// <remarks>Indicate electrical bus width of base Segment/Bus/Device/Function/Width</remarks>
			/// <value>v3.2</value>
			public Byte DataBusWidth;
			/// <summary>Peer Count</summary>
			/// <remarks>Number of peer Segment/Bus/Device/Function/Width groups that follow</remarks>
			/// <value>v3.2</value>
			public Byte PeerCount;
			//TODO: Varable length array follows
			/*/// <summary>Peer Segment/Bus/Device/Function present in the slot;</summary>
			/// <value>v3.2</value>
			[MarshalAs(UnmanagedType.ByValArray, SizeParamIndex = 14)]
			public Byte[] Peer;
			/// <summary>
			/// The contents of this field depend on what is contained in the Slot Type field.
			/// For Slot Type of C4h this field must contain the numeric value of the PCI Express Generation (e.g., Gen6 would be 0x06).
			/// For other PCI Express Slot Types, this field may be used but it is not required (if not used it should be set to 0x00).
			/// For all other Slot Types, this field should be set to 0x00.
			/// </summary>
			/// <value>v3.4</value>
			public Byte SlotInformation;
			/// <summary>This field indicates the physical width of the slot whereas Slot Data Bus Width (offset 06h) indicates the electrical width of the slot</summary>
			/// <value>v3.4</value>
			public SlotWidth SlotPhysicalWidth;
			/// <summary>
			/// The Slot Pitch field contains a numeric value that indicates the pitch of the slot in units of 1/100 millimeter.
			/// The pitch is defined by each slot/card specification, but typically describes add-in card to add-in card pitch.
			/// 
			/// For EDSFF slots, the pitch is defined in SFF-TA-1006 table 7.1, SFF-TA-1007 table 7.1 (add-in card to add-in card pitch), and SFF-TA-1008 table 6-1 (SSD to SSD pitch).
			/// </summary>
			/// <example>
			/// For example, if the pitch for the slot is 12.5 mm, the value 1250 would be used.
			/// A value of 0 implies that the slot pitch is not given or is unknown.
			/// </example>
			/// <value>v3.4</value>
			public UInt16 SlotPitch;*/

			/// <summary>Device number</summary>
			public Int32 DeviceNumber { get { return this.DeviceFunctionNumber >> 3 & 15; } }
			/// <summary>Function number</summary>
			public Int32 FunctionNumber { get { return this.DeviceFunctionNumber >> 0 & 7; } }
		}

		/// <summary>On Board Devices Information (Type 10, Obsolete)</summary>
		/// <remarks>
		/// Because this structure was originally defined with the Length implicitly defining the number of devices present,
		/// no further fields can be added to this structure without adversely affecting existing software’s ability to properly parse the data.
		/// Thus, if additional fields are required for this structure type a brand new structure must be defined to add a device count field, carry over the existing fields, and add the new information.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type10
		{
			/// <summary>Type of the onBoard device</summary>
			public enum DeviceType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Video</summary>
				Video = 0x03,
				/// <summary>SCSI Controller</summary>
				SCSI = 0x04,
				/// <summary>Ethernet</summary>
				Ethernet = 0x05,
				/// <summary>Token Ring</summary>
				TokenRing = 0x06,
				/// <summary>Sound</summary>
				Sound = 0x07,
				/// <summary>PATA Controller</summary>
				PATA = 0x08,
				/// <summary>SATA Controller</summary>
				SATA = 0x09,
				/// <summary>SAS Controller</summary>
				SAS = 0x0A,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Number of Devices</summary>
			public UInt32 NumberOfDevices { get { return (UInt32)(this.Header.Length - 4) / 2; } }
			//TODO: Varable length array follows
		}

		/// <summary>OEM Strings (Type 11)</summary>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type11
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Count</summary>
			public Byte Count;
		}

		/// <summary>System Configuration Options (Type 12)</summary>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type12
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Count</summary>
			public Byte Count;
		}

		/// <summary>BIOS Language Information (Type 13)</summary>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type13
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Installable Languages</summary>
			/// <remarks>
			/// Number of languages available Each available language has a description string.
			/// This field contains the number of strings that follow the formatted area of the structure.
			/// </remarks>
			public Byte InstallableLanguages;
			/// <summary>Flags</summary>
			/// <remarks>
			/// Bits 7:1 Reserved 
			/// Bit 0 If set to 1, the Current Language strings use the abbreviated format. 
			/// Otherwise, the strings use the long format.
			/// 
			/// If the bit is 0, each language string is in the form “ISO 639-1 Language Name | ISO 3166-1-alpha-2 Territory Name | Encoding Method”. See Example 1 below.
			/// If the bit is 1, each language string consists of the two-character “ISO 639-1 Language Name” directly followed by the two-character “ISO 3166-1-alpha-2 Territory Name”.
			/// </remarks>
			/// <example>
			/// EXAMPLE 1: BIOS Language Information (Long Format)
			/// db	13		; language information
			/// db	16h		; length 
			/// dw	??		; handle 
			/// db	3		; three languages available 
			/// db	0		; use long-format for language strings 
			/// db	15 dup (0)		; reserved
			/// db	2		; current language is French Canadian 
			/// db	‘en|US|iso8859-1’,0		; language 1 is US English 
			/// db	‘fr|CA|iso8859-1’,0		; language 2 is French Canadian 
			/// db	‘ja|JP|unicode’,0		; language 3 is Japanese 
			/// db	0		; Structure termination
			/// 
			/// EXAMPLE 2: BIOS Language Information (Abbreviated Format)
			/// db	13		; language information 
			/// db	16h		; length 
			/// dw	??		; handle 
			/// db	3		; three languages available 
			/// db	01h		; use abbreviated format for language strings 
			/// db	15 dup (0)	; reserved 
			/// db	2		; current language is French Canadian 
			/// db	‘enUS’,0	; language 1 is US English 
			/// db	‘frCA’,0	; language 2 is French Canadian 
			/// db	‘jaJP’,0	; language 3 is Japanese 
			/// db	0		; Structure termination 
			/// </example>
			public Byte Flags;
			/// <summary>Reserved</summary>
			/// <remarks>Reserved for future use</remarks>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
			private Byte[] Reserved;
			/// <summary>Current language</summary>
			/// <remarks>String number (one-based) of the currently installed language</remarks>
			public Byte CurrentLanguage;
		}

		/// <summary>Group Associations (Type 14)</summary>
		/// <remarks>
		/// The Group Associations structure is provided for OEMs who want to specify the arrangement or hierarchy of certain components (including other Group Associations) within the system.
		/// For example, you can use the Group Associations structure to indicate that two CPUs share a common external cache system.
		/// These structures might look similar to the examples shown in Example 1 and Example 2.
		/// </remarks>
		/// <example>
		/// EXAMPLE 1: First Group Association Structure
		/// db	14	; Group Association structure 
		/// db	11	; Length 
		/// dw	28h	; Handle 
		/// db	01h	; String Number (First String) 
		/// db	04	; CPU Structure 
		/// dw	08h	; CPU Structure’s Handle 
		/// db	07	; Cache Structure 
		/// dw	09h	; Cache Structure’s Handle 
		/// db	‘Primary CPU Module’, 0 
		/// db	0
		/// 
		/// EXAMPLE 2: Second Group Association Structure
		/// db	14	; Group Association structure 
		/// db	11	; Length 
		/// dw	29h	; Handle 
		/// db	01h	; String Number (First String) 
		/// db	04	; CPU Structure 
		/// dw	0Ah	; CPU Structure’s Handle 
		/// db	07	; Cache Structure 
		/// dw	09h	; Cache Structure’s Handle 
		/// db	‘Secondary CPU Module’, 0 
		/// db	0
		/// In the examples above, CPU structures 08h and 0Ah are associated with the same cache, 09h.
		/// This relationship could also be specified as a single group, as shown in Example 3.
		/// 
		/// EXAMPLE 3:  
		/// db	14	; Group Association structure 
		/// db	14	; Length (5 + 3 * 3) 
		/// dw	28h	; Structure handle for Group Association 
		/// db	1	; String Number (First string) 
		/// db	4	; 1st CPU
		/// dw	08h	; CPU Structure’s Handle 
		/// db	4	; 2nd CPU 
		/// dw	0Ah	; CPU Structure’s Handle 
		/// db	7	; Shared cache 
		/// dw	09h	; Cache Structure’s Handle 
		/// db	‘Dual-Processor CPU Complex’, 0 
		/// db	0
		/// </example>
		/// <version>
		/// Because this structure was originally defined with the Length implicitly defining the number of items present,
		/// no further fields can be added to this structure without adversely affecting existing software’s ability to properly parse the data.
		/// Thus, if additional fields are required for this structure type, a brand new structure must be defined to add an item count field, carry over the existing fields, and add the new information.
		/// </version>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type14
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Group name</summary>
			/// <remarks>String number of string describing the group</remarks>
			public Byte GroupName;
			/// <summary>Item type</summary>
			/// <remarks>Item (Structure) Type of this member</remarks>
			public Byte ItemType;
			/// <summary>Item handle</summary>
			/// <remarks>Handle corresponding to this structure</remarks>
			public UInt16 ItemHandle;
		}

		/// <summary>System Event Log (Type 15)</summary>
		/// <remarks>
		/// The presence of this structure within the SMBIOS data returned for a system indicates that the system supports an event log.
		/// An event log is a fixed-length area within a non-volatile storage element, starting with a fixed-length (and vendor-specific) header record, followed by one or more variable-length log records.
		/// 
		/// An application can implement event-log change notification by periodically reading the System Event Log structure (by its assigned handle)
		/// and looking for a change in the Log Change Token.
		/// This token uniquely identifies the last time the event log was updated.
		/// When it sees the token changed, the application can retrieve the entire event log and determine the changes since the last time it read the event log.
		/// </remarks>
		/// <value>v2.0</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type15
		{
			/// <summary>Defines the Location and Method used by higher-level software to access the log area</summary>
			[Flags]
			public enum AccessMethodType : byte
			{
				/// <summary>
				/// Indexed I/O: 1 8-bit index port, 1 8-bit data port.
				/// The Access Method Address field contains the 16-bit I/O addresses for the index and data ports.
				/// </summary>
				IndexedIO_1_8Bit = 0x00,
				/// <summary>
				/// Indexed I/O: 2 8-bit index ports, 1 8-bit data port.
				/// The Access Method Address field contains the 16-bit I/O address for the index and data ports
				/// </summary>
				IndexedIO_2_8Bit = 0x01,
				/// <summary>
				/// Indexed I/O: 1 16-bit index port, 1 8-bit data port.
				/// The Access Method Address field contains the 16-bit I/O address for the index and data ports.
				/// </summary>
				IndexedIO_1_16Bit = 0x02,
				/// <summary>
				/// Memory-mapped physical 32-bit address.
				/// The Access Method Address field contains the 4-byte (Intel DWORD format) starting physical address
				/// </summary>
				MemoryMapped32Bit = 0x03,
				/// <summary>Available through General-Purpose NonVolatile Data functions</summary>
				GeneralPurpose = 0x04,
			}

			/// <summary>Format of the log header area</summary>
			public enum LogHeaderType : byte
			{
				/// <summary>No header (for example, the header is 0 bytes in length)</summary>
				NoHeader = 0x00,
				/// <summary>Type 1 log header</summary>
				Type1LogHeader = 0x01,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Log Area Length</summary>
			/// <remarks>Length, in bytes, of the overall event log area, from the first byte of header to the last byte of data</remarks>
			public UInt16 LogAreaLength;
			/// <summary>Log Header Start Offset</summary>
			/// <remarks>
			/// Defines the starting offset (or index) within the nonvolatile storage of the event-log’s header,
			/// from the Access Method Address For single-byte indexed I/O accesses, the most-significant byte of the start offset is set to 0x00.
			/// </remarks>
			public UInt16 LogHeaderStartOffset;
			/// <summary>Log Data Start Offset</summary>
			/// <remarks>
			/// Defines the starting offset (or index) within the nonvolatile storage of the event-log’s first data byte,
			/// from the Access Method Address For single-byte indexed I/O accesses, the most-significant byte of the start offset is set to 0x00.
			///
			/// The data directly follows any header information. Therefore, the header length can be determined by subtracting the Header Start Offset from the Data Start Offset.
			/// </remarks>
			public UInt16 LogDataStartOffset;
			/// <summary>Access method</summary>
			/// <remarks>Defines the Location and Method used by higher-level software to access the log area</remarks>
			public AccessMethodType AccessMethod;
			/// <summary>Log status</summary>
			/// <remarks>
			/// Current status of the system event-log: 
			/// Bits 7:2	Reserved, set to 0s 
			/// Bit 1		Log area full, if 1 
			/// Bit 0		Log area valid, if 1
			/// </remarks>
			public Byte LogStatus;
			/// <summary>Log Change Token</summary>
			/// <remarks>Unique token that is reassigned every time the event log changes Can be used to determine if additional events have occurred since the last time the log was read.</remarks>
			public UInt16 LogChangeToken;
			/// <summary>Access Method Address</summary>
			/// <remarks>
			/// Address associated with the access method.
			/// The data present depends on the Access Method field value The area’s format can be described by the following 1-byte-packed "C" union:
			/// union 
			/// { 
			///		struct 
			///		{ 
			///			short IndexAddr; 
			///			short DataAddr; 
			///		} IO;
			///		long PhysicalAddr32; 
			///		short GPNVHandle;  
			/// } AccessMethodAddress;
			/// </remarks>
			public UInt16 AccessMethodAddress;
			/// <summary>Log Header Format</summary>
			/// <remarks>Format of the log header area</remarks>
			public LogHeaderType LogHeaderFormat;
			/// <summary>Number of Supported Log Type Descriptors (x)</summary>
			/// <remarks>If the value is 0, the list that starts at offset 0x17 is not present</remarks>
			public Byte NumberOfSupportedLogTypeDescriptors;
			/// <summary>Length of each Log Type Descriptor (y)</summary>
			/// <remarks>
			/// The value is currently “hard-coded” as 2, because each entry consists of two bytes.
			/// This field’s presence allows future additions to the type list. Software that interprets the following list should not assume a list entry’s length.
			/// </remarks>
			public Byte LengthOfEachLogTypeDescriptor;
			//TODO: Varable length array follows
		}

		/// <summary>Physical Memory Array (Type 16)</summary>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type16
		{
			/// <summary>Physical location of the Memory Array, whether on the system board or an add-in board</summary>
			public enum LocationType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>System board or motherboard</summary>
				Motherboard = 0x03,
				/// <summary>ISA add-on card</summary>
				ISA = 0x04,
				/// <summary>EISA add-on card</summary>
				EISA = 0x05,
				/// <summary>PCI add-on card</summary>
				PCI = 0x06,
				/// <summary>MCA add-on card</summary>
				MCA = 0x07,
				/// <summary>PCMCIA add-on card</summary>
				PCMCIA = 0x08,
				/// <summary>Proprietary add-on card</summary>
				Proprietary = 0x09,
				/// <summary>NuBus</summary>
				NuBus = 0x0A,
				/// <summary>PC-98/C20 add-on card</summary>
				PC98_C20 = 0xA0,
				/// <summary>PC-98/C24 add-on card</summary>
				PC98_C24 = 0xA1,
				/// <summary>PC-98/E add-on card</summary>
				PC98_E = 0xA2,
				/// <summary>PC-98/Local bus add-on card</summary>
				PC98_Local = 0xA3,
				/// <summary>CXL add-on card</summary>
				CXL = 0xA4,
			}

			/// <summary>Function for which the array is used</summary>
			public enum UseType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>System memory</summary>
				System = 0x03,
				/// <summary>Video memory</summary>
				Video = 0x04,
				/// <summary>Flash memory</summary>
				Flash = 0x05,
				/// <summary>Non-volatile RAM</summary>
				NonVolatile = 0x06,
				/// <summary>Cache memory</summary>
				Cache = 0x07,
			}

			/// <summary>Primary hardware error correction or detection method supported by this memory array</summary>
			public enum MemoryErrorCorrectionType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>None</summary>
				None = 0x03,
				/// <summary>Parity</summary>
				Parity = 0x04,
				/// <summary>Single-bit ECC</summary>
				EccSingleBit = 0x05,
				/// <summary>Multi-bit ECC</summary>
				EccMultiBit = 0x06,
				/// <summary>CRC</summary>
				CRC = 0x07,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Location</summary>
			/// <remarks>Physical location of the Memory Array, whether on the system board or an add-in board</remarks>
			public LocationType Location;
			/// <summary>Use</summary>
			/// <remarks>Function for which the array is used</remarks>
			public UseType Use;
			/// <summary>Memory error correction</summary>
			/// <remarks>Primary hardware error correction or detection method supported by this memory array</remarks>
			public MemoryErrorCorrectionType MemoryErrorCorrection;
			/// <summary>Maximum capacity</summary>
			/// <remarks>
			/// Maximum memory capacity, in kilobytes, for this array If the capacity is not represented in this field, then this field contains 0x8000_0000 and the Extended Maximum Capacity field should be used.
			/// Values 2 TB (0x8000_0000) or greater must be represented in the Extended Maximum Capacity field.
			/// </remarks>
			public UInt32 MaximumCapacity;
			/// <summary>Memory error information handle</summary>
			/// <remarks>
			/// Handle, or instance number, associated with any error that was previously detected for the array
			/// If the system does not provide the error information structure, the field contains 0xfffe; 
			/// otherwise, the field contains either 0xffff (if no error was detected) or the handle of the error-information structure.
			/// </remarks>
			public UInt16 MemoryErrorInformationHandle;
			/// <summary>Number of memory devices</summary>
			/// <remarks>
			/// Number of slots or sockets available for Memory Devices in this array
			/// This value represents the number of Memory Device structures that compose this Memory Array.
			/// Each Memory Device has a reference to the “owning” Memory Array.
			/// </remarks>
			public UInt16 NumberOfMemoryDevices;
			/// <summary>Extended maximum capacity</summary>
			/// <remarks>
			/// Maximum memory capacity, in bytes, for this array
			/// This field is only valid when the Maximum Capacity field contains 0x8000_0000.
			/// When Maximum Capacity contains a value that is not 0x8000_0000, Extended Maximum Capacity must contain zeros.
			/// </remarks>
			/// <value>v2.7+</value>
			public UInt64 ExtendedMaximumCapacity;
		}

		/// <summary>Memory Device (Type 17)</summary>
		/// <remarks>
		/// This structure describes a single memory device that is part of a larger Physical Memory Array (Type 16) structure.
		/// 
		/// If a system includes memory-device sockets, the SMBIOS implementation includes a Memory Device structure instance for each slot, whether or not the socket is currently populated.
		/// </remarks>
		/// <value>v2.1+</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type17
		{
			/// <summary>Refer to 6.3 for the CIM properties associated with this enumerated value</summary>
			public enum FormFactorType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>SIMM</summary>
				SIMM = 0x03,
				/// <summary>SIP</summary>
				SIP = 0x04,
				/// <summary>Chip</summary>
				Chip = 0x05,
				/// <summary>DIP</summary>
				DIP = 0x06,
				/// <summary>ZIP</summary>
				ZIP = 0x07,
				/// <summary>Proprietary Card</summary>
				ProprietaryCard = 0x08,
				/// <summary>DIMM</summary>
				DIMM = 0x09,
				/// <summary>TSOP</summary>
				TSOP = 0x0A,
				/// <summary>Row of chips</summary>
				RowOfChips = 0x0B,
				/// <summary>RIMM</summary>
				RIMM = 0x0C,
				/// <summary>SODIMM</summary>
				SODIMM = 0x0D,
				/// <summary>SRIMM</summary>
				SRIMM = 0x0E,
				/// <summary>FB-DIMM</summary>
				FBDimm = 0x0F,
				/// <summary>Die</summary>
				Die = 0x10,
			}

			/// <summary>Type of memory used in this device</summary>
			public enum MemoryType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>DRAM</summary>
				DRAM = 0x03,
				/// <summary>EDRAM</summary>
				EDRAM = 0x04,
				/// <summary>VRAM</summary>
				VRAM = 0x05,
				/// <summary>SRAM</summary>
				SRAM = 0x06,
				/// <summary>RAM</summary>
				RAM = 0x07,
				/// <summary>ROM</summary>
				ROM = 0x08,
				/// <summary>FLASH</summary>
				FLASH = 0x09,
				/// <summary>EEPROM</summary>
				EEPROM = 0x0A,
				/// <summary>FEPROM</summary>
				FEPROM = 0x0B,
				/// <summary>EPROM</summary>
				EPROM = 0x0C,
				/// <summary>CDRAM</summary>
				CDRAM = 0x0D,
				/// <summary>RAM</summary>
				_3DRAM = 0x0E,
				/// <summary>SDRAM</summary>
				SDRAM = 0x0F,
				/// <summary>SGRAM</summary>
				SGRAM = 0x10,
				/// <summary>RDRAM</summary>
				RDRAM = 0x11,
				/// <summary>DDR</summary>
				DDR = 0x12,
				/// <summary>DDR2</summary>
				DDR2 = 0x13,
				/// <summary>DDR2 FB-DIMM</summary>
				DDR2_FBDIMM = 0x14,
				/// <summary>DDR3</summary>
				DDR3 = 0x18,
				/// <summary>FBD2</summary>
				FBD2 = 0x19,
				/// <summary>DDR4</summary>
				DDR4 = 0x1A,
				/// <summary>LPDDR</summary>
				LPDDR = 0x1B,
				/// <summary>LPDDR2</summary>
				LPDDR2 = 0x1C,
				/// <summary>LPDDR3</summary>
				LPDDR3 = 0x1D,
				/// <summary>LPDDR4</summary>
				LPDDR4 = 0x1E,
				/// <summary>Logical non-volatile device</summary>
				LogicalDevice = 0x1F,
				/// <summary>HBM (High Bandwidth Memory)</summary>
				HBM = 0x20,
				/// <summary>HBM2 (High Bandwidth Memory Generation 2)</summary>
				HBM2 = 0x21,
				/// <summary>DDR5</summary>
				DDR5 = 0x22,
				/// <summary>LPDDR5</summary>
				LPDDR5 = 0x23,
			}

			/// <summary>Additional detail on the memory device type</summary>
			public enum MemoryDeviceTypeDetailFlags : ushort
			{
				/// <summary>Reserved</summary>
				Reserved = 1 << 0,
				/// <summary>Other</summary>
				Other = 1 << 1,
				/// <summary>Unknown</summary>
				Unknown = 1 << 2,
				/// <summary>Fast-paged</summary>
				FastPaged = 1 << 3,
				/// <summary>Static column</summary>
				StaticColumn = 1 << 4,
				/// <summary>Pseudo-static</summary>
				PseudoStatic = 1 << 5,
				/// <summary>RAMBUS</summary>
				RAMBUS = 1 << 6,
				/// <summary>Synchronous</summary>
				Synchronous = 1 << 7,
				/// <summary>CMOS</summary>
				CMOS = 1 << 8,
				/// <summary>EDO</summary>
				EDO = 1 << 9,
				/// <summary>Window DRAM</summary>
				WindowDRAM = 1 << 10,
				/// <summary>Cache DRAM</summary>
				CacheDRAM = 1 << 11,
				/// <summary>Non-volatile</summary>
				NonVolatile = 1 << 12,
				/// <summary>Registered (Buffered) </summary>
				Buffered = 1 << 13,
				/// <summary>Unbuffered (Unregistered)</summary>
				Unbuffered = 1 << 14,
				/// <summary>LRDIMM</summary>
				LRDIMM = 1 << 15,
			}

			/// <summary>Memory technology type for this memory device</summary>
			public enum MemoryTechnologyType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>DRAM</summary>
				DRAM = 0x03,
				/// <summary>NVDIMM-N</summary>
				NVDIMM_N = 0x04,
				/// <summary>NVDIMM-F</summary>
				NVDIMM_F = 0x05,
				/// <summary>NVDIMM-P</summary>
				NVDIMM_P = 0x06,
				/// <summary>Intel® Optane™ persistent memory</summary>
				IntelOptane = 0x07,
			}

			/// <summary>The operating modes supported by this memory device</summary>
			[Flags]
			public enum MemoryOperatingModeCapabilityFlags : ushort
			{
				/// <summary>Reserved, set to 0</summary>
				Reserved = 1 << 0,
				/// <summary>Other</summary>
				Other = 1 << 1,
				/// <summary>Unknown</summary>
				Unknown = 1 << 2,
				/// <summary>Volatile Memory</summary>
				VolatileMemory = 1 << 3,
				/// <summary>Byte-accessible persistent memory</summary>
				ByteAccessible = 1 << 4,
				/// <summary>Block-accessible persistent memory</summary>
				BlockAccessible = 1 << 5,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Physical memory array handle</summary>
			/// <remarks>Handle, or instance number, associated with the Physical Memory Array to which this device belongs</remarks>
			public UInt16 PhysicalMemoryArrayHandle;
			/// <summary>Memory error information handle</summary>
			/// <remarks>
			/// Handle, or instance number, associated with any error that was previously detected for the device
			/// If the system does not provide the error information structure, the field contains 0xfffe; otherwise, the field contains either 0xffff (if no error was detected) or the handle of the error-information structure.
			/// </remarks>
			public UInt16 MemoryErrorInformationHandle;
			/// <summary>Total width</summary>
			/// <remarks>
			/// Total width, in bits, of this memory device, including any check or error-correction bits If there are no error-correction bits, this value should be equal to Data Width.
			/// If the width is unknown, the field is set to 0xffff.
			/// </remarks>
			public UInt16 TotalWidth;
			/// <summary>Data width</summary>
			/// <remarks>
			/// Data width, in bits, of this memory device A Data Width of 0 and a Total Width of 8 indicates that the device is being used solely to provide 8 error-correction bits.
			/// If the width is unknown, the field is set to 0xffff.
			/// </remarks>
			public UInt16 DataWidth;
			/// <summary>Size</summary>
			/// <remarks>
			/// Size of the memory device If the value is 0, no memory device is installed in the socket; if the size is unknown, the field value is 0xffff.
			/// If the size is 32 GB-1 MB or greater, the field value is 0x7fff and the actual size is stored in the <see cref="_ExtendedSize"/> field.
			/// 
			/// The granularity in which the value is specified depends on the setting of the most-significant bit (bit 15).
			/// If the bit is 0, the value is specified in megabyte units; if the bit is 1, the value is specified in kilobyte units.
			/// For example, the value 0x8100 identifies a 256 KB memory device and 0x0100 identifies a 256 MB memory device.
			/// </remarks>
			private UInt16 _Size;
			/// <summary>Form Factor</summary>
			/// <remarks>Implementation form factor for this memory device</remarks>
			public FormFactorType FormFactor;
			/// <summary>Device Set</summary>
			/// <remarks>
			/// Identifies when the Memory Device is one of a set of Memory Devices that must be populated with all devices of the same type and size, and the set to which this device belongs
			/// A value of 0 indicates that the device is not part of a set; a value of FFh indicates that the attribute is unknown.
			/// A Device Set number must be unique within the context of the Memory Array containing this Memory Device.
			/// </remarks>
			public Byte DeviceSet;
			/// <summary>Device locator</summary>
			/// <remarks>String number of the string that identifies the physically-labeled socket or board position where the memory device is located</remarks>
			/// <example>"SIMM 3"</example>
			public Byte DeviceLocator;
			/// <summary>Bank locator</summary>
			/// <remarks>String number of the string that identifies the physically labeled bank where the memory device is located</remarks>
			/// <example>"Bank 0" or "A"</example>
			public Byte BankLocator;
			/// <summary>Memory</summary>
			/// <remarks>Type of memory used in this device</remarks>
			public MemoryType Memory;
			/// <summary>Type Detail</summary>
			/// <remarks>Additional detail on the memory device type</remarks>
			public MemoryDeviceTypeDetailFlags TypeDetail;
			/// <summary>Speed</summary>
			/// <remarks>
			/// Identifies the maximum capable speed of the device, in megatransfers per second (MT/s).
			/// Memory speed is expressed in megatransfers per second (MT/s).
			/// Previous revisions (3.0.0 and earlier) of this specification used MHz to indicate clock speed.
			/// With double data rate memory, clock speed is distinct from transfer rate, since data is transferred on both the rising and the falling edges of the clock signal.
			/// This maintains backward compatibility with observed DDR implementations prior to this revision, which already reported transfer rate instead of clock speed, e.g., DDR4-2133 (PC4-17000) memory was reported as 2133 instead of 1066.
			/// </remarks>
			/// <example>
			/// 0x0000 = the speed is unknown 
			/// 0xffff = the speed is 65,535 MT/s or greater, and the actual speed is stored in the <see cref="ExtendedSpeed"/> field
			/// </example>
			/// <value>v2.3+</value>
			public UInt16 Speed;
			/// <summary>Manufacturer</summary>
			/// <remarks>String number for the manufacturer of this memory device</remarks>
			/// <value>v2.3+</value>
			public Byte Manufacturer;
			/// <summary>Serail number</summary>
			/// <remarks>
			/// String number for the serial number of this memory device.
			/// This value is set by the manufacturer and normally is not changeable.
			/// </remarks>
			/// <value>v2.3+</value>
			public Byte SerialNumber;
			/// <summary>Asset tag</summary>
			/// <remarks>String number for the asset tag of this memory device</remarks>
			/// <value>v2.3+</value>
			public Byte AssetTag;
			/// <summary>Part number</summary>
			/// <remarks>
			/// String number for the part number of this memory device.
			/// This value is set by the manufacturer and normally is not changeable.
			/// </remarks>
			/// <value>v2.3+</value>
			public Byte PartNumber;
			/// <summary>Attributes</summary>
			/// <remarks>
			/// Bits 7-4:	reserved
			/// Bits 3-0:	rank
			/// Value=0		for unknown rank information
			/// </remarks>
			/// <value>v2.6+</value>
			private Byte Attributes;
			/// <summary>Extended size</summary>
			/// <remarks>
			/// Extended size of the memory device (complements the Size field at offset 0x0c).
			/// The Extended Size field is intended to represent memory devices larger than 32,767 MB (32 GB - 1 MB), which cannot be described using the <see cref="_Size"/> field.
			/// This field is only meaningful if the value in the <see cref="_Size"/> field is 0x7fff.
			/// For compatibility with older SMBIOS parsers, memory devices smaller than (32 GB - 1 MB) should be represented using their size in the <see cref="Size"/> field, leaving the Extended Size field set to 0. 
			/// Bit 31 is reserved for future use and must be set to 0. 
			/// Bits 30:0 represent the size of the memory device in megabytes.
			/// </remarks>
			/// <example>
			/// EXAMPLE: 0x0000_8000 indicates a 32 GB memory device (32,768 MB), 0x0002_0000 represents a 128 GB memory device (131,072 MB), and 0x0000_7fff represents a 32,767 MB (32 GB – 1 MB) device.
			/// </example>
			/// <value>v2.7+</value>
			private UInt32 _ExtendedSize;

			/// <summary>Configured memory speed</summary>
			/// <remarks>
			/// Identifies the configured speed of the memory device, in megatransfers per second (MT/s).
			/// Memory speed is expressed in megatransfers per second (MT/s).
			/// Previous revisions (3.0.0 and earlier) of this specification used MHz to indicate clock speed.
			/// With double data rate memory, clock speed is distinct from transfer rate, since data is transferred on both the rising and the falling edges of the clock signal.
			/// This maintains backward compatibility with observed DDR implementations prior to this revision, which already reported transfer rate instead of clock speed, e.g., DDR4-2133 (PC4-17000) memory was reported as 2133 instead of 1066.
			/// </remarks>
			/// <example>
			/// 0x0000 = the speed is unknown 
			/// 0xffff = the speed is 65,535 MT/s or greater, and the actual speed is stored in the <see cref="ExtendedConfiguredMemorySpeed"/> field 
			/// </example>
			/// <value>v2.7+</value>
			public UInt16 ConfiguredMemorySpeed;
			/// <summary>Minimum voltage</summary>
			/// <remarks>
			/// Minimum operating voltage for this device, in millivolts
			/// If the value is 0, the voltage is unknown
			/// </remarks>
			/// <value>v2.8+</value>
			public UInt16 MinimumVoltage;
			/// <summary>Maximum voltage</summary>
			/// <remarks>
			/// Maximum operating voltage for this device, in millivolts.
			/// If the value is 0, the voltage is unknown
			/// </remarks>
			/// <value>v2.8+</value>
			public UInt16 MaximumVoltage;
			/// <summary>Configured voltage</summary>
			/// <remarks>
			/// Configured voltage for this device, in millivolts.
			/// If the value is 0, the voltage is unknown
			/// </remarks>
			/// <value>v2.8+</value>
			public UInt16 ConfiguredVoltage;
			/// <summary>Memory technology</summary>
			/// <remarks>Memory technology type for this memory device</remarks>
			/// <value>v3.2+</value>
			public MemoryTechnologyType MemoryTechnology;
			/// <summary>Memory operating mode capability</summary>
			/// <remarks>
			/// The operating modes supported by this memory device.
			/// What the word bit positions mean for the Memory Device - Memory Operating Mode Capability field.
			/// This field indicates the supported operating mode(s);
			/// it does not indicate the current configured operating mode(s).
			/// </remarks>
			/// <value>v3.2+</value>
			public MemoryOperatingModeCapabilityFlags MemoryOperatingModeCapability;
			/// <summary>Firmware version</summary>
			/// <remarks>String number for the firmware version of this memory device</remarks>
			/// <value>v3.2+</value>
			public Byte FirmwareVersion;
			/// <summary>Module manufacturer ID</summary>
			/// <remarks>
			/// The two-byte module manufacturer ID found in the SPD of this memory device; LSB first.
			/// 
			/// The Module Manufacturer ID indicates the manufacturer of the memory device.
			/// This field shall be set to the value of the SPD Module Manufacturer ID Code.
			/// Refer to JEDEC Standard JEP106AV for the list of manufacturer IDs.
			/// A value of 0x0000 indicates the Module Manufacture ID is unknown.
			/// </remarks>
			/// <value>v3.2+</value>
			public UInt16 ModuleManufacturerID;
			/// <summary>Module product ID</summary>
			/// <remarks>
			/// The two-byte module product ID found in the SPD of this memory device; LSB first.
			/// 
			/// The Module Product ID is the identifier of the memory device, which is assigned by the manufacturer of the memory device.
			/// This field shall be set to the value of the SPD Module Product Identifier.
			/// A value of 0x0000 indicates the Module Product ID is unknown. 
			/// 
			/// Note that the location (byte addresses) of the SPD Module Product Identifier may vary and is defined by the memory type/technology SPD Standard.
			/// For example, for NVDIMM-N DDR4, this field will have the first byte correspond to the value in byte 192 and the second byte corresponds to the value in byte 193.
			/// </remarks>
			/// <value>v3.2+</value>
			public UInt16 ModuleProductID;
			/// <summary>Memory subsystem controller manufacturer ID</summary>
			/// <remarks>
			/// The two-byte memory subsystem controller manufacturer ID found in the SPD of this memory device; LSB first
			/// 
			/// The Memory Subsystem Controller Manufacturer ID indicates the vendor of the memory subsystem controller.
			/// This field shall be set to the value of the SPD Memory Subsystem Controller Manufacturer ID Code.
			/// Refer to JEDEC Standard JEP106AV for the list of manufacturer IDs.
			/// A value of 0x0000 indicates the Memory Subsystem Controller Manufacturer ID is unknown. 
			/// 
			/// Note that the location (byte addresses) of the SPD Memory Subsystem Controller Manufacturer ID Code may vary and is defined by the memory type/technology SPD Standard.
			/// For example, for NVDIMM-N 1658 DDR4, this field will have the first byte correspond to the value in byte 194 and the second byte corresponds to the value in byte 195.
			/// </remarks>
			/// <value>v3.2+</value>
			public UInt16 MemorySubsystemControllerManufacturerID;
			/// <summary>Memory subsystem controller product ID</summary>
			/// <remarks>
			/// The two-byte memory subsystem controller product ID found in the SPD of this memory device; LSB first.
			/// 
			/// The Memory Subsystem Controller Product ID is the identifier of the memory subsystem controller, which is assigned by the vendor of the memory subsystem controller.
			/// This field shall be set to the value of the SPD Memory Subsystem Controller Product Identifier.
			/// A value of 0x0000 indicates the Memory Subsystem Controller Product ID is unknown. 
			/// 
			/// Note that the location (byte addresses) of the SPD Memory Subsystem Controller Product Identifier may vary and is defined by the memory type/technology SPD Standard.
			/// For example, for NVDIMM-N DDR4, this field will have the first byte correspond to the value in byte 196 and the second byte corresponds to the value in byte 197. 
			/// </remarks>
			/// <value>v3.2+</value>
			public UInt16 MemorySubsystemControllerProductID;
			/// <summary>Non volatile size</summary>
			/// <remarks>
			/// Size of the Non-volatile portion of the memory device in Bytes, if any.
			/// If the value is 0, there is no non-volatile portion.
			/// If the Non-volatile Size is unknown, the field is set to 0xffffffffffffffff. 
			/// 
			/// These fields are intended to represent the size of the portions of the memory device used for volatile, non-volatile and cache respectively.
			/// The existing Size and ExtendedSize fields shall continue to report the total physical capacity of the device, except when the Memory Device – Type is set to 1Fh (Logical).
			/// It is not required that the Volatile Size, Non-volatile Size and Cache Size add up to the total physical capacity of the device.
			/// 
			/// If the memory device has any non-volatile capacity, the Non-volatile size field shall be set to a non-zero value or all Fs and Bit 12 (Non-volatile) in the Memory Device – Type Detail field shall be set to 1.
			/// 
			/// If the memory device has no non-volatile capacity, the Non-volatile size field shall be set to 0 or all 0xFs and Bit 12 (Non-volatile) in the Memory Device – Type Detail field shall be set to 0.
			/// </remarks>
			/// <example>
			/// For volatile memory device (such as Memory Type = DDR4 and Memory Technology = DRAM), Volatile Size would equal the total physical size of the memory device, with Non-volatile Size = 0 and Cache Size = 0. 
			/// 
			/// For volatile memory device (such as Memory Type = DDR4 and Memory Technology = DRAM), configured for cache, Cache Size would equal the total physical size of the memory device, with Non-volatile Size = 0 and Volatile Size = 0.
			/// 
			/// For single use non-volatile memory device (such as Memory Type = DDR4 and Memory Technology = NVDIMM-N), Non-volatile Size is less than or equal to the total physical size of the memory device, with Volatile Size = 0 and Cache Size = 0. 
			/// 
			/// For multiple use non-volatile memory device (such as Memory Type = DDR4 and Memory Technology = NVDIMM-P), that is configured for non-volatile and volatile usage, Cache Size = 0, with the value of Non-Volatile Size plus Volatile Size less than or equal to the total physical size of the memory device.
			/// 
			/// The total amount of available volatile memory shall be calculated by adding the total of Volatile Size not set to unknown for all memory devices.
			/// The total amount of available non-volatile memory shall be calculated by adding the total of Non-volatile Size not set to unknown for all memory devices.
			/// </example>
			/// <value>v3.2+</value>
			public UInt64 NonVolatileSize;
			/// <summary>Volatile size</summary>
			/// <remarks>
			/// Size of the Volatile portion of the memory device in Bytes, if any.
			/// If the value is 0, there is no Volatile portion.
			/// If the Volatile Size is unknown, the field is set to 0xffffffffffffffff
			/// 
			/// These fields are intended to represent the size of the portions of the memory device used for volatile, non-volatile and cache respectively.
			/// The existing Size and ExtendedSize fields shall continue to report the total physical capacity of the device, except when the Memory Device – Type is set to 1Fh (Logical).
			/// It is not required that the Volatile Size, Non-volatile Size and Cache Size add up to the total physical capacity of the device.
			/// 
			/// If the memory device has any non-volatile capacity, the Non-volatile size field shall be set to a non-zero value or all Fs and Bit 12 (Non-volatile) in the Memory Device – Type Detail field shall be set to 1.
			/// 
			/// If the memory device has no non-volatile capacity, the Non-volatile size field shall be set to 0 or all 0xFs and Bit 12 (Non-volatile) in the Memory Device – Type Detail field shall be set to 0.
			/// </remarks>
			/// <example>
			/// For volatile memory device (such as Memory Type = DDR4 and Memory Technology = DRAM), Volatile Size would equal the total physical size of the memory device, with Non-volatile Size = 0 and Cache Size = 0. 
			/// 
			/// For volatile memory device (such as Memory Type = DDR4 and Memory Technology = DRAM), configured for cache, Cache Size would equal the total physical size of the memory device, with Non-volatile Size = 0 and Volatile Size = 0.
			/// 
			/// For single use non-volatile memory device (such as Memory Type = DDR4 and Memory Technology = NVDIMM-N), Non-volatile Size is less than or equal to the total physical size of the memory device, with Volatile Size = 0 and Cache Size = 0. 
			/// 
			/// For multiple use non-volatile memory device (such as Memory Type = DDR4 and Memory Technology = NVDIMM-P), that is configured for non-volatile and volatile usage, Cache Size = 0, with the value of Non-Volatile Size plus Volatile Size less than or equal to the total physical size of the memory device.
			/// 
			/// The total amount of available volatile memory shall be calculated by adding the total of Volatile Size not set to unknown for all memory devices.
			/// The total amount of available non-volatile memory shall be calculated by adding the total of Non-volatile Size not set to unknown for all memory devices.
			/// </example>
			/// <value>v3.2+</value>
			public UInt64 VolatileSize;
			/// <summary>Cache size</summary>
			/// <remarks>
			/// Size of the Cache portion of the memory device in Bytes, if any.
			/// If the value is 0, there is no Cache portion.
			/// If the Cache Size is unknown, the field is set to 0xffffffffffffffff.
			/// 
			/// These fields are intended to represent the size of the portions of the memory device used for volatile, non-volatile and cache respectively.
			/// The existing Size and ExtendedSize fields shall continue to report the total physical capacity of the device, except when the Memory Device – Type is set to 1Fh (Logical).
			/// It is not required that the Volatile Size, Non-volatile Size and Cache Size add up to the total physical capacity of the device.
			/// 
			/// If the memory device has any non-volatile capacity, the Non-volatile size field shall be set to a non-zero value or all Fs and Bit 12 (Non-volatile) in the Memory Device – Type Detail field shall be set to 1.
			/// 
			/// If the memory device has no non-volatile capacity, the Non-volatile size field shall be set to 0 or all 0xFs and Bit 12 (Non-volatile) in the Memory Device – Type Detail field shall be set to 0.
			/// </remarks>
			/// <example>
			/// For volatile memory device (such as Memory Type = DDR4 and Memory Technology = DRAM), Volatile Size would equal the total physical size of the memory device, with Non-volatile Size = 0 and Cache Size = 0. 
			/// 
			/// For volatile memory device (such as Memory Type = DDR4 and Memory Technology = DRAM), configured for cache, Cache Size would equal the total physical size of the memory device, with Non-volatile Size = 0 and Volatile Size = 0.
			/// 
			/// For single use non-volatile memory device (such as Memory Type = DDR4 and Memory Technology = NVDIMM-N), Non-volatile Size is less than or equal to the total physical size of the memory device, with Volatile Size = 0 and Cache Size = 0. 
			/// 
			/// For multiple use non-volatile memory device (such as Memory Type = DDR4 and Memory Technology = NVDIMM-P), that is configured for non-volatile and volatile usage, Cache Size = 0, with the value of Non-Volatile Size plus Volatile Size less than or equal to the total physical size of the memory device.
			/// 
			/// The total amount of available volatile memory shall be calculated by adding the total of Volatile Size not set to unknown for all memory devices.
			/// The total amount of available non-volatile memory shall be calculated by adding the total of Non-volatile Size not set to unknown for all memory devices.
			/// </example>
			/// <value>v3.2+</value>
			public UInt64 CacheSize;
			/// <summary>Logical size</summary>
			/// <remarks>
			/// Size of the Logical memory device in Bytes.
			/// If the size is unknown, the field is set to 0xffffffffffffffff
			/// 
			/// Logical non-volatile memory devices are not physically installed in the system.
			/// Logical memory devices are created using memory capacity from the installed physical volatile memory devices.
			/// Logical memory devices are not created from installed physical non-volatile memory devices.
			/// 
			/// The size of the Logical memory device is described in the Logical Size field.
			/// Logical Size is valid when Memory Type is Logical.
			/// When Memory Type is not Logical, Logical Size shall be 0.
			/// The total amount of Logical memory from all Logical Size fields shall never by be larger than the total amount of physical volatile memory.
			/// 
			/// Non-volatile Logical devices using Memory Device Type enumeration value 1Fh (Logical) shall set the existing Size field to FFFFh indicating the size is unknown.
			/// The new Non-volatile Size field shall report the size of the Non-volatile Logical device.
			/// </remarks>
			/// <value>v3.2+</value>
			public UInt64 LogicalSize;
			/// <summary>Extended speed</summary>
			/// <remarks>
			/// Extended speed of the memory device (complements the Speed field at offset 15h).
			/// Identifies the maximum capable speed of the device, in megatransfers per second (MT/s)
			/// 
			/// The Extended Speed and Extended Configured Memory Speed fields are intended to represent memory devices that operate faster than 65,535 MT/s,
			/// which cannot be described using the Speed or Configured Memory Speed fields.
			/// These fields are only meaningful if the value in the Speed or Configured Memory Speed fields are FFFFh.
			/// For compatibility with older SMBIOS parsers, memory devices slower than 65,535 MT/s
			/// should represent their speed using the Speed and Configured Memory Speed fields, leaving the Extended Speed and Extended Configured Memory Speed fields set to 0. 
			/// 
			/// Bit 31 is reserved for future use and must be set to 0 
			/// Bits 30:0 represent the speed or configured memory speed of the device in MT/s.
			/// </remarks>
			/// <value>v3.3+</value>
			public UInt32 ExtendedSpeed;
			/// <summary>Extended configured memory speed</summary>
			/// <remarks>
			/// Extended configured memory speed of the memory device (complements the Configured Memory Speed field at offset 20h).
			/// Identifies the configured speed of the memory device, in megatransfers per second (MT/s).
			/// 
			/// The Extended Speed and Extended Configured Memory Speed fields are intended to represent memory devices that operate faster than 65,535 MT/s,
			/// which cannot be described using the Speed or Configured Memory Speed fields.
			/// These fields are only meaningful if the value in the Speed or Configured Memory Speed fields are FFFFh.
			/// For compatibility with older SMBIOS parsers, memory devices slower than 65,535 MT/s
			/// should represent their speed using the Speed and Configured Memory Speed fields, leaving the Extended Speed and Extended Configured Memory Speed fields set to 0. 
			/// 
			/// Bit 31 is reserved for future use and must be set to 0 
			/// Bits 30:0 represent the speed or configured memory speed of the device in MT/s.
			/// </remarks>
			/// <value>v3.3+</value>
			public UInt32 ExtendedConfiguredMemorySpeed;

			/// <summary>True - kilobytes; False - megabytes</summary>
			public Boolean IsSizeInKb { get { return (this._Size >> 15 & 0x01)==0x01; } }
			/// <summary>Attributes Rank</summary>
			public Byte? AttributesRank { get { return this.Attributes > 0 ? (Byte)(this.Attributes >> 0 & 0x07) : (Byte?)null; } }
			/// <summary>Size</summary>
			/// <remarks>Size of the memory device</remarks>
			public UInt32 Size
			{
				get
				{
					switch(this._Size)
					{
					case 0x00:
						return 0;
					case 0xffff:
						return this._ExtendedSize;
					default:
						return this._Size;
					}
				}
			}
		}

		/// <summary>32-Bit Memory Error Information (Type 18)</summary>
		/// <remarks>This structure identifies the specifics of an error that might be detected within a Physical Memory Array.</remarks>
		/// <value>v2.1+</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type18
		{
			/// <summary>Type of error that is associated with the current status reported for the memory array or device</summary>
			public enum ErrorType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>OK</summary>
				OK = 0x03,
				/// <summary>Bad read</summary>
				BadRead = 0x04,
				/// <summary>Parity error</summary>
				Parity = 0x05,
				/// <summary>Single-bit error</summary>
				SingleBit = 0x06,
				/// <summary>Double-bit error</summary>
				DoubleBit = 0x07,
				/// <summary>Multi-bit error</summary>
				MultiBit = 0x08,
				/// <summary>Nibble error</summary>
				Nibble = 0x09,
				/// <summary>Checksum error</summary>
				Checksum = 0x0A,
				/// <summary>CRC error</summary>
				CRC = 0x0B,
				/// <summary>Corrected single-bit error</summary>
				CorrectedSingleBit = 0x0C,
				/// <summary>Corrected error</summary>
				Corrected = 0x0D,
				/// <summary>Uncorrectable error</summary>
				Uncorrectable = 0x0E,
			}

			/// <summary>Type of error that is associated with the current status reported for the memory array or device</summary>
			/// <remarks>Refer to 6.3 for the CIM properties associated with this enumerated value.</remarks>
			public enum ErrorOperationType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Read</summary>
				Read = 0x03,
				/// <summary>Write</summary>
				Write = 0x04,
				/// <summary>Partial write</summary>
				PartialWrite = 0x05,
			}

			/// <summary>Granularity (for example, device versus Partition) to which the error can be resolved</summary>
			public enum ErrorGranularityType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Device level</summary>
				DeviceLevel = 0x03,
				/// <summary>Memory partition level</summary>
				MemoryPartitionLevel = 0x04,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Error</summary>
			/// <remarks>Type of error that is associated with the current status reported for the memory array or device</remarks>
			public ErrorType Error;
			/// <summary>Error granularity</summary>
			/// <remarks>Granularity (for example, device versus Partition) to which the error can be resolved</remarks>
			public ErrorGranularityType ErrorGranularity;
			/// <summary>Error operation</summary>
			/// <remarks>Memory access operation that caused the error</remarks>
			public ErrorOperationType ErrorOperation;
			/// <summary>Vendor syndrome</summary>
			/// <remarks>Vendor-specific ECC syndrome or CRC data associated with the erroneous access If the value is unknown, this field contains 0x0000_0000.</remarks>
			public UInt32 VendorSyndrome;
			/// <summary>Memory array error address</summary>
			/// <remarks>32-bit physical address of the error based on the addressing of the bus to which the memory array is connected If the address is unknown, this field contains 0x8000_0000.</remarks>
			public UInt32 MemoryArrayErrorAddress;
			/// <summary>Device error address</summary>
			/// <remarks>32-bit physical address of the error relative to the start of the failing memory device, in bytes If the address is unknown, this field contains 0x8000_0000.</remarks>
			public UInt32 DeviceErrorAddress;
			/// <summary>Error resolution</summary>
			/// <remarks>Range, in bytes, within which the error can be determined, when an error address is given If the range is unknown, this field contains 0x8000_0000.</remarks>
			public UInt32 ErrorResolution;
		}

		/// <summary>Memory Array Mapped Address (Type 19)</summary>
		/// <value>v2.1+</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type19
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Starting address</summary>
			/// <remarks>
			/// Physical address, in kilobytes, of a range of memory mapped to the specified Physical Memory Array.
			/// 
			/// When the field value is 0xffff_ffff, the actual address is stored in the <see cref="ExtendedStartingAddress"/> field.
			/// When this field contains a valid address, <see cref="EndingAddress"/> must also contain a valid address.
			/// When this field contains 0xffff_ffff, <see cref="EndingAddress"/> must also contain 0xffff_ffff
			/// </remarks>
			public UInt32 StartingAddress;
			/// <summary>Ending address</summary>
			/// <remarks>
			/// Physical ending address of the last kilobyte of a range of addresses mapped to the specified.
			/// 
			/// Physical Memory Array When the field value is 0xffff_ffff and the <see cref="StartingAddress"/> field also contains 0xffff_ffff,
			/// the actual address is stored in the <see cref="ExtendedEndingAddress"/> field.
			/// When this field contains a valid address, <see cref="StartingAddress"/> must also contain a valid address.
			/// </remarks>
			public UInt32 EndingAddress;
			/// <summary>Memory array handle</summary>
			/// <remarks>
			/// Handle, or instance number, associated with the Physical Memory Array to which this address range is mapped
			/// Multiple address ranges can be mapped to a single Physical Memory Array
			/// </remarks>
			public UInt16 MemoryArrayHandle;
			/// <summary>Partition width</summary>
			/// <remarks>Number of Memory Devices that form a single row of memory for the address partition defined by this structure</remarks>
			public Byte PartitionWidth;
			/// <summary>Extended starting address</summary>
			/// <remarks>
			/// Physical address, in bytes, of a range of memory mapped to the specified Physical Memory Array.
			/// 
			/// This field is valid when <see cref="StartingAddress"/> contains the value 0xffff_ffff.
			/// If <see cref="StartingAddress"/> contains a value other than 0xffff_ffff, this field contains zeros.
			/// When this field contains a valid address, <see cref="ExtendedEndingAddress"/> must also contain a valid address.
			/// </remarks>
			/// <value>v2.7+</value>
			public UInt64 ExtendedStartingAddress;
			/// <summary>Extended ending address</summary>
			/// <remarks>
			/// Physical ending address, in bytes, of the last of a range of addresses mapped to the specified Physical Memory Array.
			/// 
			/// This field is valid when both <see cref="StartingAddress"/> and <see cref="EndingAddress"/> contain the value 0xffff_ffff.
			/// If <see cref="EndingAddress"/> contains a value other than 0xffff_ffff, this field contains zeros.
			/// When this field contains a valid address, <see cref="ExtendedStartingAddress"/> must also contain a valid address.
			/// </remarks>
			/// <value>v2.7+</value>
			public UInt64 ExtendedEndingAddress;
		}

		/// <summary>Memory Device Mapped Address (Type 20)</summary>
		/// <value>v2.1+</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type20
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Starting address</summary>
			/// <remarks>
			/// Physical address, in kilobytes, of a range of memory mapped to the referenced Memory Device.
			/// 
			/// When the field value is 0xffff_ffff the actual address is stored in the <see cref="ExtendedStartingAddress"/> field.
			/// When this field contains a valid address, <see cref="EndingAddress"/> must also contain a valid address.
			/// When this field contains 0xffff_ffff, <see cref="EndingAddress"/> must also contain 0xffff_ffff.
			/// </remarks>
			public UInt32 StartingAddress;
			/// <summary>Ending address</summary>
			/// <remarks>
			/// Physical ending address of the last kilobyte of a range of addresses mapped to the referenced Memory Device.
			/// 
			/// When the field value is 0xffff_ffff the actual address is stored in the <see cref="ExtendedEndingAddress"/> field.
			/// When this field contains a valid address, <see cref="StartingAddress"/> must also contain a valid address.
			/// </remarks>
			public UInt32 EndingAddress;
			/// <summary>Memory device handle</summary>
			/// <remarks>
			/// Handle, or instance number, associated with the Memory Device structure to which this address range is mapped.
			/// 
			/// Multiple address ranges can be mapped to a single Memory Device
			/// </remarks>
			public UInt16 MemoryDeviceHandle;
			/// <summary>Memory array mapped address handle</summary>
			/// <remarks>
			/// Handle, or instance number, associated with the Memory Array Mapped Address structure to which this device address range is mapped.
			/// 
			/// Multiple address ranges can be mapped to a single Memory Array Mapped Address.
			/// </remarks>
			public UInt16 MemoryArrayMappedAddressHandle;
			/// <summary>Partition row position</summary>
			/// <remarks>
			/// Position of the referenced Memory Device in a row of the address partition.
			/// The value 0 is reserved.
			/// If the position is unknown, the field contains 0xff
			/// </remarks>
			/// <example>For example, if two 8-bit devices form a 16-bit row, this field’s value is either 1 or 2.</example>
			public Byte PartitionRowPosition;
			/// <summary>Interleave position</summary>
			/// <remarks>
			/// Position of the referenced Memory Device in an interleave.
			///
			/// The value 0 indicates non-interleaved, 1 indicates first interleave position, 2 the second interleave position, and so on.
			/// If the position is unknown, the field contains FFh.
			/// </remarks>
			/// <example>
			/// In a 2:1 interleave, the value 1 indicates the device in the ”even” position.
			/// In a 4:1 interleave, the value 1 indicates the first of four possible positions.
			/// </example>
			public Byte InterleavePosition;
			/// <summary>Interleaved data depth</summary>
			/// <remarks>
			/// Maximum number of consecutive rows from the referenced Memory Device that are accessed in a single interleaved transfer.
			/// If the device is not part of an interleave, the field contains 0; if the interleave configuration is unknown, the value is 0xff.</remarks>
			/// <example>
			/// If a device transfers two rows each time it is read, its Interleaved Data Depth is set to 2.
			/// If that device is 2:1 interleaved and in Interleave Position 1, the rows mapped to that device are 1, 2, 5, 6, 9, 10, etc.
			/// </example>
			public Byte InterleavedDataDepth;
			/// <summary>Extended starting address</summary>
			/// <remarks>
			/// Physical address, in bytes, of a range of memory mapped to the referenced Memory Device.
			/// 
			/// This field is valid when <see cref="StartingAddress"/> contains the value 0xffff_ffff.
			/// If <see cref="StartingAddress"/> contains a value other than 0xffff_ffff, this field contains zeros.
			/// When this field contains a valid address, <see cref="ExtendedEndingAddress"/> must also contain a valid address
			/// </remarks>
			public UInt64 ExtendedStartingAddress;
			/// <summary>Extended ending address</summary>
			/// <remarks>
			/// Physical ending address, in bytes, of the last of a range of addresses mapped to the referenced Memory Device.
			/// 
			/// This field is valid when both <see cref="StartingAddress"/> and <see cref="EndingAddress"/> contain the value 0xffff_ffff.
			/// If <see cref="EndingAddress"/> contains a value other than 0xffff_ffff, this field contains zeros.
			/// When this field contains a valid address, <see cref="ExtendedStartingAddress"/> must also contain a valid address.
			/// </remarks>
			public UInt64 ExtendedEndingAddress;
		}

		/// <summary>Built-in Pointing Device (Type 21)</summary>
		/// <remarks>
		/// This structure describes the attributes of the built-in pointing device for the system.
		/// The presence of this structure does not imply that the built-in pointing device is active for the system’s use.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type21
		{
			/// <summary>Type of pointing device</summary>
			public enum PointingDeviceType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Mouse</summary>
				Mouse = 0x03,
				/// <summary>Track Ball</summary>
				TrackBall = 0x04,
				/// <summary>Track Point</summary>
				TrackPoint = 0x05,
				/// <summary>Glide Point</summary>
				GlidePoint = 0x06,
				/// <summary>Touch Pad</summary>
				TouchPad = 0x07,
				/// <summary>Touch Screen</summary>
				TouchScreen = 0x08,
				/// <summary>Optical Sensor</summary>
				OpticalSensor = 0x09,
			}

			/// <summary>Interface type for the pointing device</summary>
			public enum PointingDeviceInterfaceType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Serial</summary>
				Serial = 0x03,
				/// <summary>PS/2</summary>
				PS_2 = 0x04,
				/// <summary>Infrared</summary>
				Infrared = 0x05,
				/// <summary>HP-HIL</summary>
				HP_HIL = 0x06,
				/// <summary>Bus mouse</summary>
				BusMouse = 0x07,
				/// <summary>ADB (Apple Desktop Bus)</summary>
				ADB = 0x08,
				/// <summary>Bus mouse DB-9</summary>
				BusMouseDB_9 = 0xA0,
				/// <summary>Bus mouse micro-DIN</summary>
				BusMouseMicroDIN = 0xA1,
				/// <summary>USB</summary>
				USB = 0xA2,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Type</summary>
			/// <remarks>Type of pointing device</remarks>
			public PointingDeviceType Type;
			/// <summary>Interface</summary>
			/// <summary>Interface type for the pointing device</summary>
			public PointingDeviceInterfaceType Interface;
			/// <summary>Number of buttons</summary>
			/// <remarks>
			/// Number of buttons on the pointing device.
			/// If the device has three buttons, the field value is 0x03.
			/// </remarks>
			public Byte NumberOfButtons;
		}

		/// <summary>Portable Battery (Type 22)</summary>
		/// <remarks>
		/// This structure describes the attributes of the portable battery or batteries for the system.
		/// The structure contains the static attributes for the group.
		/// Each structure describes a single battery pack’s attributes.
		/// </remarks>
		/// <value>v2.1+</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type22
		{
			/// <summary>Identifies the battery chemistry</summary>
			public enum DeviceChemistryType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Lead Acid</summary>
				LeadAcid = 0x03,
				/// <summary>Nickel Cadmium</summary>
				NickelCadmium = 0x04,
				/// <summary>Nickel metal hydride</summary>
				NickelMetalHydride = 0x05,
				/// <summary>Lithium-ion</summary>
				LithiumIon = 0x06,
				/// <summary>Zinc air</summary>
				ZincAir = 0x07,
				/// <summary>Lithium Polymer</summary>
				LithiumPolymer = 0x08,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Location</summary>
			/// <remarks>Number of the string that identifies the location of the battery</remarks>
			/// <example>“in the back, on the left-hand side”</example>
			public Byte Location;
			/// <summary>Manufacturer</summary>
			/// <remarks>Number of the string that names the company that manufactured the batter</remarks>
			public Byte Manufacturer;
			/// <summary>Manufacture date</summary>
			/// <remarks>
			/// Number of the string that identifies the date on which the battery was manufactured.
			/// Version 2.2+ implementations that use a Smart Battery set this field to 0 (no string) to indicate that the <see cref="SBDSManufactureDate"/> field contains the information
			/// </remarks>
			public Byte ManufactureDate;
			/// <summary>Serial number</summary>
			/// <remarks>
			/// Number of the string that contains the serial number for the batter.
			/// Version 2.2+ implementations that use a Smart Battery set this field to 0 (no string) to indicate that the <see cref="SBDSSerialNumber"/> field contains the information
			/// </remarks>
			public Byte SerialNumber;
			/// <summary>device name</summary>
			/// <remarks>Number of the string that names the battery device</remarks>
			/// <example>"DR-36"</example>
			public Byte DeviceName;
			/// <summary>Device chemistry</summary>
			/// <remarks>
			/// Identifies the battery chemistry.
			/// Version 2.2+ implementations that use a Smart Battery set this field to 02h (Unknown) to indicate that the <see cref="SBDSDeviceChemistry"/> field contains the information
			/// </remarks>
			public DeviceChemistryType DeviceChemistry;
			/// <summary>Design capacity</summary>
			/// <remarks>
			/// Design capacity of the battery in mWatt-hours.
			/// 
			/// If the value is unknown, the field contains 0.
			/// For version 2.2+ implementations, this value is multiplied by the <see cref="DesignCapacityMultiplier"/> to produce the actual value.
			/// </remarks>
			public UInt16 DesignCapacity;

			/// <summary>Design voltage</summary>
			/// <remarks>Design voltage of the battery in mVolts If the value is unknown, the field contains 0.</remarks>
			public UInt16 DesignVoltage;
			/// <summary>SBDS version number</summary>
			/// <remarks>
			/// Number of the string that contains the Smart Battery Data Specification version number supported by this battery.
			/// If the battery does not support the function, no string is supplied.
			/// </remarks>
			public Byte SBDSVersionNumber;
			/// <summary>Maximum error in battery data</summary>
			/// <remarks>
			/// Maximum error (as a percentage in the range 0 to 100) in the Watt-hour data reported by the battery, indicating an upper bound on how much additional energy the battery might have above the energy it reports having.
			/// If the value is unknown, the field contains 0xff
			/// </remarks>
			public Byte MaximumErrorInBatteryData;
			/// <summary>SBDS serial number</summary>
			/// <remarks>
			/// 16-bit value that identifies the battery’s serial number.
			/// 
			/// This value, when combined with the <see cref="Manufacturer"/>, <see cref="DeviceName"/>, and <see cref="ManufactureDate"/>, uniquely identifies the battery.
			/// The <see cref="SerialNumber"/> field must be set to 0 (no string) for this field to be valid. 
			/// </remarks>
			/// <value>v2.2</value>
			public UInt16 SBDSSerialNumber;
			/// <summary>SBDS manufacture date</summary>
			/// <remarks>
			/// The Manufacture Date field must be set to 0 (no string) for this field to be valid
			/// Date the cell pack was manufactured, in packed format: 
			/// Bits 15:9	Year, biased by 1980, in the range 0 to 127 
			/// Bits 8:5	Month, in the range 1 to 12 
			/// Bits 4:0	Date, in the range 1 to 3
			/// </remarks>
			/// <example>01 February 2000 would be identified as 0010 1000 0100 0001b (0x2841)</example>
			/// <value>v2.2</value>
			private UInt16 _SBDSManufactureDate;

			/// <summary>SBDS Device chemistry</summary>
			/// <remarks>
			/// Number of the string that identifies the battery chemistry (for example, "PbAc")
			/// The <see cref="DeviceChemistry"/> field must be set to 0x02 (Unknown) for this field to be valid
			/// </remarks>
			/// <value>v2.2</value>
			public Byte SBDSDeviceChemistry;
			/// <summary>Design capacity multiplier</summary>
			/// <remarks>
			/// Multiplication factor of the <see cref="DesignCapacity"/> value, which assures that the mWatt hours value does not overflow for SBDS implementation.
			/// The multiplier default is 1, SBDS implementations use the value 10 to correspond to the data as returned from the SBDS Function 0x18.
			/// </remarks>
			/// <value>v2.2</value>
			public Byte DesignCapacityMultiplier;
			/// <summary>OEM specific</summary>
			/// <remarks>Contains OEM- or BIOS vendor-specific information</remarks>
			/// <value>v2.2</value>
			public UInt32 OEMSpecific;

			/// <summary>SBDS manufacture date</summary>
			/// <remarks>Date the cell pack was manufactured</remarks>
			public DateTime? SBDSManufactureDate
			{
				get
				{
					if(this._SBDSManufactureDate == 0)
						return null;

					Byte day = (Byte)(this._SBDSManufactureDate >> 0 & 0x1f);
					Byte month = (Byte)(this._SBDSManufactureDate >> 5 & 0x0f);
					Int32 year = ((this._SBDSManufactureDate >> 9 & 0x7f) + 1980);
					return new DateTime(year, month, day);
				}
			}
		}

		/// <summary>System Reset (Type 23)</summary>
		/// <remarks>
		/// This structure describes whether Automatic System Reset functions are enabled (Status).
		/// 
		/// If the system has a watchdog timer and the timer is not reset (Timer Reset) before the Interval elapses, an automatic system reset occurs.
		/// The system re-boots according to the Boot Option.
		/// This function may repeat until the Limit is reached, at which time the system re-boots according to the Boot Option at Limit.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type23
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Capabilities</summary>
			/// <remarks>
			/// Identifies the system-reset capabilities for the system.
			/// Bits 7:6	Reserved for future assignment by this specification; set to 00b 
			/// Bit 5		System contains a watchdog timer; either True (1) or False (0) 
			/// Bits 4:3	Boot Option on Limit. Identifies one of the following   system actions to be taken when the Reset Limit is reached: 
			///		00b		Reserved, do not use. 
			///		01b		Operating system 
			///		10b		System utilities 
			///		11b		Do not reboot 
			/// Bits 2:1	Boot Option. Indicates one of the following actions   to be taken after a watchdog reset:   
			///		00b		Reserved, do not use. 
			///		01b		Operating system 
			///		10b		System utilities 
			///		11b		Do not reboot 
			/// Bit 0		Status. Identifies whether (1) or not (0) the system reset is enabled by the use
			/// </remarks>
			public Byte Capabilities;
			/// <summary>Reset count</summary>
			/// <remarks>
			/// Number of automatic system resets since the last intentional reset.
			/// A value of 0FFFFh indicates unknown
			/// </remarks>
			public UInt16 ResetCount;
			/// <summary>Reset limit</summary>
			/// <remarks>
			/// Number of consecutive times the system reset is attempted.
			/// A value of 0FFFFh indicates unknown.
			/// </remarks>
			public UInt16 ResetLimit;
			/// <summary>Timer interval</summary>
			/// <remarks>
			/// Number of minutes to use for the watchdog timer If the timer is not reset within this interval, the system reset timeout begins.
			/// A value of 0FFFFh indicates unknown.
			/// </remarks>
			public UInt16 TimerInterval;
			/// <summary>Timeout</summary>
			/// <remarks>
			/// Number of minutes before the reboot is initiated It is used after a system power cycle, system reset (local or remote), and automatic system reset.
			/// A value of 0x0ffff indicates unknown
			/// </remarks>
			public UInt16 Timeout;
		}

		/// <summary>Hardware Security (Type 24)</summary>
		/// <remarks>This structure describes the system-wide hardware security settings</remarks>
		/// <value>v2.2</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type24
		{
			/// <summary>Identifies the password and reset status for the system</summary>
			public enum HardwareSecurityStatus : byte
			{
				/// <summary>Disabled</summary>
				Disabled = 0x00,
				/// <summary>Enabled</summary>
				Enabled = 0x01,
				/// <summary>Not implemented</summary>
				NotImplemented = 0x02,
				/// <summary>Unknown</summary>
				Unknown = 0x03,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Hardware security settings</summary>
			/// <remarks>
			/// Identifies the password and reset status for the system.
			/// 
			/// Bits 7:6 Power-on Password Status value: 
			///		00b		Disabled
			///		01b		Enabled
			///		10b		Not Implemented
			///		11b		Unknown
			/// Bits 5:4 Keyboard Password Status value:
			///		00b		Disabled
			///		01b		Enabled
			///		10b		Not Implemented
			///		11b		Unknown
			/// Bits 3:2 Administrator Password Status value:
			///		00b		Disabled
			///		01b		Enabled
			///		10b		Not Implemented
			///		11b		Unknown
			/// Bits 1:0 Front Panel Reset Status value: 
			///		00b		Disabled
			///		01b		Enabled
			///		10b		Not Implemented
			///		11b		Unknown
			/// </remarks>
			private Byte HardwareSecuritySettings;

			/// <summary>Front Panel Reset Status</summary>
			public HardwareSecurityStatus FrontPanelResetStatus { get { return (HardwareSecurityStatus)(this.HardwareSecuritySettings >> 0 & 0x03); } }
			/// <summary>Administrator Password Status</summary>
			public HardwareSecurityStatus AdministratorPasswordStatus { get { return (HardwareSecurityStatus)(this.HardwareSecuritySettings >> 2 & 0x03); } }
			/// <summary>Keyboard Password Status</summary>
			public HardwareSecurityStatus KeyboardPasswordStatus { get { return (HardwareSecurityStatus)(this.HardwareSecuritySettings >> 4 & 0x03); } }
			/// <summary>Power-on Password Status</summary>
			public HardwareSecurityStatus PowerOnPasswordStatus { get { return (HardwareSecurityStatus)(this.HardwareSecuritySettings >> 6 & 0x03); } }
		}

		/// <summary>System Power Controls (Type 25) </summary>
		/// <remarks>
		/// This structure describes the attributes for controlling the main power supply to the system.
		/// 
		/// Software that interprets this structure uses the month, day, hour, minute, and second values to determine the number of seconds until the next power-on of the system.
		/// The presence of this structure implies that a timed power-on facility is available for the system.
		/// </remarks>
		/// <value>v2.2</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type25
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Next scheduled Power-on Month</summary>
			/// <remarks>
			/// BCD value of the month on which the next scheduled power-on is to occur, in the range 0x01 to 0x12;
			/// 
			/// The DMTF System Power Controls group contains a Next Scheduled Power-on Time, specified as the number of seconds until the next scheduled power-on of the system.
			/// Management software uses the date and time information specified in the associated SMBIOS structure to calculate the total number of seconds.
			/// 
			/// Any date or time field in the structure whose value is outside of the field’s specified range does not contribute to the total-seconds count.
			/// For example, if the Month field contains the value 0xFF the next power-on is scheduled to fall within the next month, perhaps on a specific day-of-month and time.
			/// </remarks>
			public Byte NextScheduledPowerOnMonth;
			/// <summary>Next scheduled Power-on Day-of-month</summary>
			/// <remarks>BCD value of the day-of-month on which the next scheduled power-on is to occur, in the range 0x01 to 0x31</remarks>
			public Byte NextScheduledPowerOnDayOfMonth;
			/// <summary>Next scheduled Power-on Hour</summary>
			/// <remarks>BCD value of the hour on which the next scheduled power-on is to occur, in the range 0x00 to 0x23</remarks>
			public Byte NextScheduledPowerOnHour;
			/// <summary>Next scheduled Power-on Minute</summary>
			/// <remarks>BCD value of the minute on which the next scheduled power-on is to occur, in the range 0x00 to 0x59</remarks>
			public Byte NextScheduledPowerOnMinute;
			/// <summary>Next scheduled Power-on Second</summary>
			/// <remarks>BCD value of the second on which the next scheduled power-on is to occur, in the range 0x00 to 0x59</remarks>
			public Byte NextScheduledPowerOnSecond;
		}

		/// <summary>Voltage Probe (Type 26)</summary>
		/// <remarks>
		/// This describes the attributes for a voltage probe in the system.
		/// Each structure describes a single voltage probe.
		/// </remarks>
		/// <value>v2.2</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type26
		{
			/// <summary>Probe's physical location</summary>
			public enum LocationType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Processor</summary>
				Processor = 0x03,
				/// <summary>Disk</summary>
				Disk = 0x04,
				/// <summary>Peripheral Bay</summary>
				PeripheralBay = 0x05,
				/// <summary>System Management Module</summary>
				SystemManagementModule = 0x06,
				/// <summary>Motherboard</summary>
				Motherboard = 0x07,
				/// <summary>Memory Module</summary>
				MemoryModule = 0x08,
				/// <summary>Processor Module</summary>
				ProcessorModule = 0x09,
				/// <summary>Power Unit</summary>
				PowerUnit = 0x0a,
				/// <summary>Add-in Card</summary>
				AddInCard = 0x0b,
			}

			/// <summary>Phrobe's physical status</summary>
			public enum StatusType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>OK</summary>
				OK = 0x03,
				/// <summary>Non-critical</summary>
				NonCritical = 0x04,
				/// <summary>Critical</summary>
				Critical = 0x05,
				/// <summary>Non-recoverable</summary>
				NonRecoverable = 0x06,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Description</summary>
			/// <remarks>Number of the string that contains additional descriptive information about the probe or its location</remarks>
			public Byte Description;
			/// <summary>Location &amp; status</summary>
			/// <remarks>Probe’s physical location and status of the voltage monitored by this voltage probe</remarks>
			private Byte LocationAndStatus;
			/// <summary>Maximum value</summary>
			/// <remarks>
			/// Maximum voltage level readable by this probe, in millivolts.
			/// If the value is unknown, the field is set to 0x800
			/// </remarks>
			public UInt16 MaximumValue;
			/// <summary>Minimum value</summary>
			/// <remarks>
			/// Minimum voltage level readable by this probe, in millivolts.
			/// If the value is unknown, the field is set to 0x8000.
			/// </remarks>
			public UInt16 MinimumValue;
			/// <summary>Resolution</summary>
			/// <remarks>
			/// Resolution for the probe’s reading, in tenths of millivolts.
			/// If the value is unknown, the field is set to 0x8000.
			/// </remarks>
			public UInt16 Resolution;
			/// <summary>Tolerance</summary>
			/// <remarks>
			/// Tolerance for reading from this probe, in plus/minus millivolts.
			/// If the value is unknown, the field is set to 0x8000.
			/// </remarks>
			public UInt16 Tolerance;
			/// <summary>Accuracy</summary>
			/// <remarks>
			/// Accuracy for reading from this probe, in plus/minus 1/100th of a percent.
			/// If the value is unknown, the field is set to 0x8000.
			/// </remarks>
			public UInt16 Accuracy;
			/// <summary>OEM Defined</summary>
			/// <remarks>OEM- or BIOS vendor-specific information</remarks>
			public UInt32 OEMDefined;
			/// <summary>Nominal value</summary>
			/// <remarks>
			/// Nominal value for the probe’s reading in millivolts.
			/// If the value is unknown, the field is set to 0x8000.
			/// This field is present in the structure only if the structure’s length is larger than 0x14
			/// </remarks>
			public UInt16 NominalValue;

			/// <summary>Location</summary>
			/// <reamrks>Probe’s physical location of the voltage monitored by this voltage probe</reamrks>
			public LocationType Location { get { return (LocationType)(this.LocationAndStatus >> 0 & 0x1f); } }

			/// <summary>Status</summary>
			/// <reamrks>Probe’s physical status of the voltage monitored by this voltage probe</reamrks>
			public StatusType Status { get { return (StatusType)(this.LocationAndStatus >> 5 & 0x7); } }
		}

		/// <summary>Cooling Device (Type 27)</summary>
		/// <remarks>
		/// This structure describes the attributes for a cooling device in the system.
		/// Each structure describes a single cooling device.
		/// </remarks>
		/// <value>v2.2</value>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type27
		{
			/// <summary>Cooling device status</summary>
			public enum StatusType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>OK</summary>
				OK = 0x03,
				/// <summary>Non-critical</summary>
				NonCritical = 0x04,
				/// <summary>Critical</summary>
				Critical = 0x05,
				/// <summary>Non-recoverable</summary>
				NonRecoverable = 0x06,
			}

			/// <summary>Cooling device type</summary>
			public enum DeviceType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				///<summary>Fan</summary>
				Fan = 0x03,
				///<summary>Centrifugal Blower</summary>
				CentrifugalBlower = 0x04,
				///<summary>Chip Fan</summary>
				ChipFan = 0x05,
				///<summary>Cabinet Fan</summary>
				CabinetFan = 0x06,
				///<summary>Power Supply Fan</summary>
				PowerSupplyFan = 0x07,
				///<summary>Heat Pipe</summary>
				HeatPipe = 0x08,
				///<summary>Integrated Refrigeration</summary>
				IntegratedRefrigeration = 0x09,
				///<summary>Active Cooling</summary>
				ActiveCooling = 0x0a,
				///<summary>Passive Cooling</summary>
				PassiveCooling = 0x00b,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Temperature Probe Handle</summary>
			/// <remarks>
			/// Handle, or instance number, of the temperature probe monitoring this cooling device.
			/// A value of 0xFFFF indicates that no probe is provided
			/// </remarks>
			public UInt16 TemperatureProbeHandle;
			/// <summary>Device Type &amp; Status</summary>
			/// <remarks>Cooling device type and status</remarks>
			private Byte DeviceTypeAndStatus;
			/// <summary>Cooling unit group</summary>
			/// <remarks>
			/// Cooling unit group to which this cooling device is associated Having multiple cooling devices in the same cooling unit implies a redundant configuration.
			/// The value is 0x00 if the cooling device is not a member of a redundant cooling unit.
			/// Non-zero values imply redundancy and that at least one other cooling device will be enumerated with the same value
			/// </remarks>
			public Byte CoolingUnitGroup;
			/// <summary>OEM defined</summary>
			/// <remarks>OEM- or BIOS vendor-specific information</remarks>
			public UInt32 OEMDefined;
			/// <summary>Nominal speed</summary>
			/// <remarks>
			/// Nominal value for the cooling device’s rotational speed, in revolutions-per-minute (rpm).
			/// If the value is unknown or the cooling device is non-rotating, the field is set to 0x8000.
			/// This field is present in the structure only if the structure’s length is larger than 0x0c
			/// </remarks>
			private UInt16 _NominalSpeed;
			/// <summary>Description</summary>
			/// <remarks>
			/// Number of the string that contains additional descriptive information about the cooling device or it's location.
			/// This field is present in the structure only if the structure’s length is 0x0f or larger.
			/// </remarks>
			/// <value>v2.7</value>
			public Byte Description;

			/// <summary>Device</summary>
			/// <remarks>Cooling device physical location of the voltage monitored by this voltage probe (TODO: Промахнулся?)</remarks>
			public DeviceType Device { get { return (DeviceType)(this.DeviceTypeAndStatus >> 0 & 0x1f); } }
			/// <summary>Status</summary>
			/// <remarks>Cooling device physical status of the voltage monitored by this voltage probe (TODO: Промахнулся?)</remarks>
			public StatusType Status { get { return (StatusType)(this.DeviceTypeAndStatus >> 5 & 0x7); } }
			/// <summary>Nominal speed</summary>
			/// <remarks>Nominal value for the cooling device’s rotational speed, in revolutions-per-minute (rpm)</remarks>
			public UInt16 NominalSpeed { get { return this.Header.Length > 0x0c ? this._NominalSpeed : (UInt16)0x8000; } }
		}

		/// <summary>Temperature Probe (Type 28)</summary>
		/// <remarks>This structure describes the attributes for a temperature probe in the system. Each structure describes a single temperature probe.</remarks>
		/// <value>v2.2</value>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type28
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Description</summary>
			/// <remarks>Number of the string that contains additional descriptive information about the probe or its location</remarks>
			public Byte Description;
			/// <summary>Location &amp; Status</summary>
			/// <remarks>Probe’s physical location and the status of the temperature monitored by this temperature probe (TODO)</remarks>
			public Byte LocationAndStatus;
			/// <summary>Maximum value</summary>
			/// <remarks>
			/// Maximum temperature readable by this probe, in 1/10th degrees C.
			/// If the value is unknown, the field is set to 0x800
			/// </remarks>
			public UInt16 MaximumValue;
			/// <summary>Minimum value</summary>
			/// <remarks>
			/// Minimum temperature readable by this probe, in 1/10th degrees C.
			/// If the value is unknown, the field is set to 0x800
			/// </remarks>
			public UInt16 MinimumValue;
			/// <summary>Resolution</summary>
			/// <remarks>
			/// Resolution for the probe’s reading, in 1/1000th degrees C.
			/// If the value is unknown, the field is set to 0x800
			/// </remarks>
			public UInt16 Resolution;
			/// <summary>Tolerance</summary>
			/// <remarks>
			/// Tolerance for reading from this probe, in plus/minus 1/10th degrees C.
			/// If the value is unknown, the field is set to 0x8000
			/// </remarks>
			public UInt16 Tolerance;
			/// <summary>Accuracy</summary>
			/// <remarks>
			/// Accuracy for reading from this probe, in plus/minus 1/100th of a percent.
			/// If the value is unknown, the field is set to 0x8000.
			/// </remarks>
			public UInt16 Accuracy;
			/// <summary>OEM-defined</summary>
			/// <remarks>OEM- or BIOS vendor-specific information</remarks>
			public UInt32 OEMDefined;
			/// <summary>Nominal value</summary>
			/// <remarks>
			/// Nominal value for the probe’s reading in 1/10th degrees C.
			/// If the value is unknown, the field is set to 0x8000.
			/// This field is present in the structure only if the structure’s Length is larger than 14h
			/// </remarks>
			public UInt16 NominalValue;
		}

		/// <summary>Electrical Current Probe (Type 29)</summary>
		/// <remarks>
		/// This structure describes the attributes for an electrical current probe in the system.
		/// Each structure describes a single electrical current probe.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type29
		{
			/// <summary>Electrical current probe location</summary>
			public enum LocationType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Processor</summary>
				Processor = 0x03,
				/// <summary>Disk</summary>
				Disk = 0x04,
				/// <summary>Peripheral Bay</summary>
				PeripheralBay = 0x05,
				/// <summary>System Management Module</summary>
				SystemManagementModule = 0x06,
				/// <summary>Motherboard</summary>
				Motherboard = 0x07,
				/// <summary>Memory Module</summary>
				MemoryModule = 0x08,
				/// <summary>Processor Module</summary>
				ProcessorModule = 0x09,
				/// <summary>Power Unit</summary>
				PowerUnit = 0x0a,
				/// <summary>Add-in Card</summary>
				AddInCard = 0x0b,
			}
			/// <summary>Electrical current probe status</summary>
			public enum StatusType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>OK</summary>
				OK = 0x03,
				/// <summary>Non-critical</summary>
				NonCritical = 0x04,
				/// <summary>Critical</summary>
				Critical = 0x05,
				/// <summary>Non-recoverable</summary>
				NonRecoverable = 0x06,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Description</summary>
			/// <remarks>Number of the string that contains additional descriptive information about the probe or its location</remarks>
			public Byte Description;
			/// <summary>Location &amp; Status</summary>
			/// <remarks>Defines the probe’s physical location and the status of the current monitored by this current probe</remarks>
			private Byte LocationAndStatus;
			/// <summary>Maximum value</summary>
			/// <remarks>
			/// Minimum current readable by this probe, in milliamps.
			/// If the value is unknown, the field is set to 0x800
			/// </remarks>
			public UInt16 MaximumValue;
			/// <summary>Minimum value</summary>
			/// <remarks>
			/// Minimum current readable by this probe, in milliamps.
			/// If the value is unknown, the field is set to 0x800
			/// </remarks>
			public UInt16 MinimumValue;
			/// <summary>Resolution</summary>
			/// <remarks>
			/// Resolution for the probe’s reading, in tenths of milliamp.
			/// If the value is unknown, the field is set to 0x8000
			/// </remarks>
			public UInt16 Resolution;
			/// <summary>Tolerance</summary>
			/// <remarks>
			/// Tolerance for reading from this probe, in plus/minus milliamps.
			/// If the value is unknown, the field is set to 0x800
			/// </remarks>
			public UInt16 Tolerance;
			/// <summary>Accuracy</summary>
			/// <remarks>
			/// Accuracy for reading from this probe, in plus/minus 1/100th of a percent.
			/// If the value is unknown, the field is set to 0x800
			/// </remarks>
			public UInt16 Accuracy;
			/// <summary>OEM defined</summary>
			/// <remarks>OEM- or BIOS vendor-specific information</remarks>
			public UInt32 OEMDefined;
			/// <summary>Nominal value</summary>
			/// <remarks>
			/// Nominal value for the probe’s reading in milliamp.
			/// 
			/// If the value is unknown, the field is set to 0x8000.
			/// This field is present in the structure only if the structure’s length is larger than 0x14
			/// </remarks>
			private UInt16 _NominalValue;

			/// <summary>Location</summary>
			/// <remarks>Defines the probe’s physical location of the current monitored by this current probe</remarks>
			public LocationType Location { get { return (LocationType)(this.LocationAndStatus >> 0 & 0x1f); } }
			/// <summary>Status</summary>
			/// <remarks>Defines the probe’s status of the current monitored by this current probe</remarks>
			public StatusType Status { get { return (StatusType)(this.LocationAndStatus >> 5 & 0x7); } }
			/// <summary>Nominal value</summary>
			/// <remarks>Nominal value for the probe’s reading in milliamp</remarks>
			public UInt16 NominalValue { get { return this.Header.Length > 0x14 ? this._NominalValue : (UInt16)0x8000; } }
		}

		/// <summary>Out-of-Band Remote Access (Type 30)</summary>
		/// <remarks>
		/// This structure describes the attributes and policy settings of a hardware facility that may be used to gain remote access
		/// to a hardware system when the operating system is not available due to power-down status, hardware failures, or boot failures.
		/// </remarks>
		/// <value>v2.2</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type30
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Manufacturer name</summary>
			/// <remarks>Number of the string that contains the manufacturer of the out-of-band access facility</remarks>
			public Byte ManufacturerName;
			/// <summary>Connections</summary>
			/// <remarks>
			/// Current remote-access connections.
			/// Bits 7:2	Reserved for future definition by this specification; set to all zeros
			/// Bit 1		Outbound Connection Enabled. Identifies whether (1) or not (0)
			///					the facility is allowed to initiate outbound connections to contact an 
			///					alert management facility when critical conditions occur 
			/// Bit 0		Inbound Connection Enabled. Identifies whether (1) or not (0)
			///					the facility is allowed to initiate outbound connections to receive incoming connections
			///					for the purpose of remote operations or problem managemen
			/// </remarks>
			private Byte Connections;
			/// <summary>Inbound Connection Enabled</summary>
			public Boolean InboundConnectionEnabled { get { return (this.Connections >> 0 & 0x01) == 0x01; } }
			/// <summary>Outbound Connection Enabled</summary>
			public Boolean OutboundConnectionEnabled { get { return (this.Connections >> 1 & 0x01) == 0x01; } }
		}

		/// <summary>System Boot Information (Type 32)</summary>
		/// <remarks>
		/// The client system firmware (for example, BIOS) communicates the System Boot Status to the client’s Pre-boot Execution Environment (PXE)
		/// boot image or OS-present management application through this structure.
		/// 
		/// When used in the PXE environment, for example, this code identifies the reason the PXE was initiated
		/// and can be used by boot-image software to further automate an enterprise’s PXE sessions.
		/// For example, an enterprise could choose to automatically download a hardware-diagnostic image to a client whose reason code indicated either a firmware- or an operating system-detected hardware failure.
		/// </remarks>
		/// <value>v2.3</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type32
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Reserved</summary>
			/// <remarks>Reserved for future assignment by this specification; all bytes are set to 0x00</remarks>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public Byte[] Reserved;

			/*/// <summary>Status and Additional Data fields that identify the boot status</summary>
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
			public Byte[] BootStatus;*/
			//TODO: Varable length array follows
		}

		/// <summary>64-Bit Memory Error Information (Type 33)</summary>
		/// <remarks>This structure describes an error within a Physical Memory Array, when the error address is above 4G (0xffffffff).</remarks>
		/// <value>v2.3</value>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type33
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <remarks>Type of error that is associated with the current status reported for the memory array or device</remarks>
			public Type18.ErrorType ErrorType;
			/// <remarks>Granularity (for example, device versus Partition) to which the error can be resolve</remarks>
			public Type18.ErrorGranularityType ErrorGranularity;
			/// <remarks>Memory access operation that caused the error</remarks>
			public Type18.ErrorOperationType ErrorOperation;
			/// <summary>Vendor syndrome</summary>
			/// <remarks>
			/// Vendor-specific ECC syndrome or CRC data associated with the erroneous access.
			/// If the value is unknown, this field contains 0x0000_0000
			/// </remarks>
			public UInt32 VendorSyndrome;
			/// <summary>Memory array Error Address</summary>
			/// <remarks>
			/// 64-bit physical address of the error based on the addressing of the bus to which the memory array is connected.
			/// If the address is unknown, this field contains 0x8000_0000_0000_0000
			/// </remarks>
			public UInt64 MemoryArrayErrorAddress;
			/// <summary>Device Error Address</summary>
			/// <remarks>
			/// 64-bit physical address of the error relative to the start of the failing memory device, in byte.
			/// If the address is unknown, this field contains 0x8000_0000_0000_0000
			/// </remarks>
			public UInt64 DeviceErrorAddress;
			/// <summary>Error Resolution</summary>
			/// <remarks>
			/// Range, in bytes, within which the error can be determined, when an error address is given.
			/// If the range is unknown, this field contains 0x8000_0000
			/// </remarks>
			public UInt32 ErrorResolution;
		}

		/// <summary>Management Device (Type 34)</summary>
		/// <remarks>
		/// he information in this structure defines the attributes of a Management Device.
		/// A Management Device might control one or more fans or voltage, current, or temperature probes as defined by one or more Management Device Component structures. 
		/// </remarks>
		/// <value>v2.3</value>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type34
		{
			/// <summary>Device’s type</summary>
			public enum ManagementDeviceType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>National Semiconductor LM7</summary>
				NS_LM7 = 0x03,
				/// <summary>National Semiconductor LM78</summary>
				NS_LM78 = 0x04,
				/// <summary>National Semiconductor LM79</summary>
				NS_LM79 = 0x05,
				/// <summary>National Semiconductor LM80</summary>
				NS_LM80 = 0x06,
				/// <summary>National Semiconductor LM81</summary>
				NS_LM81 = 0x07,
				/// <summary>Analog Devices ADM9240</summary>
				AD_ADM9240 = 0x08,
				/// <summary>Dallas Semiconductor DS1780</summary>
				DS_DS1780 = 0x09,
				/// <summary>Maxim 1617</summary>
				Maxim_1617 = 0x0A,
				/// <summary>Genesys GL518SM</summary>
				Genesys_GL518SM = 0x0B,
				/// <summary>Winbond W83781D</summary>
				Winbond_W83781D = 0x0C,
				/// <summary>Holtek HT82H79</summary>
				Holtek_HT82H79 = 0x0D,
			}

			/// <summary>Type of addressing used to access the device</summary>
			public enum ManagementDeviceAddressType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>I/O Port</summary>
				IOPort = 0x03,
				/// <summary>Memory</summary>
				Memory = 0x04,
				/// <summary>SM Bus</summary>
				SMBus = 0x05,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Description</summary>
			/// <remarks>Number of the string that contains additional descriptive information about the device or its location</remarks>
			public Byte Description;
			/// <summary>Device’s type</summary>
			public ManagementDeviceType Type;
			/// <summary>Device’s address</summary>
			public UInt32 Address;
			/// <summary>Address type</summary>
			/// <remarks>Type of addressing used to access the device</remarks>
			public ManagementDeviceAddressType AddressType;
		}

		/// <summary>Management Device Component (Type 35) </summary>
		/// <remarks>This structure associates a cooling device or environmental probe with structures that define the controlling hardware device and (optionally) the component’s thresholds.</remarks>
		/// <value>v2.3</value>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type35
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Description</summary>
			/// <remarks>Number of the string that contains additional descriptive information about the component</remarks>
			public Byte Description;
			/// <summary>Management device handle</summary>
			/// <remarks>Handle, or instance number, of the Management Device that contains this component</remarks>
			public UInt16 ManagementDeviceHandle;
			/// <summary>Component handle</summary>
			/// <remarks>Handle, or instance number, of the probe or cooling device that defines this component</remarks>
			public UInt16 ComponentHandle;
			/// <summary>Threshold handle</summary>
			/// <remarks>
			/// Handle, or instance number, associated with the device thresholds.
			/// A value of 0FFFFh indicates that no Threshold Data structure is associated with this component
			/// </remarks>
			public UInt16 ThresholdHandle;
		}

		/// <summary>Management Device Threshold Data (Type 36)</summary>
		/// <remarks>
		/// The information in this structure defines threshold information for a component (probe or cooling-unit) contained within a Management Device.
		/// 
		/// For each threshold field present in the structure: 
		/// • The threshold units (millivolts, milliamps, 1/10th degrees C, or RPMs) are as defined by the associated probe or cooling-unit component structure.
		/// • If the value is unavailable, the field is set to 0x8000.
		/// </remarks>
		/// <value>v2.3</value>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type36
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Lower threshold: Non critical</summary>
			/// <remarks>Lower non-critical threshold for this component</remarks>
			public UInt16 LowerThresholdNonCritical;
			/// <summary>Upper threshold: Non critical</summary>
			/// <remarks>Upper non-critical threshold for this component</remarks>
			public UInt16 UpperThresholdNonCritical;
			/// <summary>Lower threshold: Critical</summary>
			/// <remarks>Lower critical threshold for this component</remarks>
			public UInt16 LowerThresholdCritical;
			/// <summary>Upper threshold: Critical</summary>
			/// <remarks>Upper critical threshold for this component</remarks>
			public UInt16 UpperThresholdCritical;
			/// <summary>Lower threshold: Non recoverable</summary>
			/// <remarks>Lower non-recoverable threshold for this component</remarks>
			public UInt16 LowerThresholdNonRecoverable;
			/// <summary>Upper threshold: Non recoverable</summary>
			/// <remarks>Upper non-recoverable threshold for this component</remarks>
			public UInt16 UpperThresholdNonRecoverable;
		}

		/// <summary>Memory Channel (Type 37)</summary>
		/// <remarks>
		/// The information in this structure provides the correlation between a Memory Channel and its associated Memory Devices.
		/// Each device presents one or more loads to the channel; the sum of all device loads cannot exceed the channel’s defined maximum.
		/// </remarks>
		/// <value>v2.3</value>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type37
		{
			/// <summary>Type of memory associated with the channel</summary>
			public enum ChannelType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>RamBus</summary>
				RamBus = 0x03,
				/// <summary>SyncLink</summary>
				SyncLink = 0x04,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Channel</summary>
			/// <remarks>Type of memory associated with the channel</remarks>
			public ChannelType Channel;
			/// <summary>Maximum Channel Load</summary>
			/// <remarks>Maximum load supported by the channel; the sum of all device loads cannot exceed this value</remarks>
			public Byte MaximumChannelLoad;
			/// <summary>Memory Device Count</summary>
			/// <remarks>
			/// Number of Memory Devices (Type 11h) that are associated with this channel.
			/// This value also defines the number of Load/Handle pairs that follow.
			/// </remarks>
			public Byte MemoryDeviceCount;
			/// <summary>Memory 1 Device Load</summary>
			/// <remarks>Channel load provided by the first Memory Device associated with this channel</remarks>
			public Byte Memory1DeviceLoad;
			/// <summary>Memory Device 1 Handle</summary>
			/// <remarks>Structure handle that identifies the first Memory Device associated with this channel</remarks>
			public UInt16 MemoryDevice1Handle;
			//TODO: Varable length array follows
		}

		/// <summary> IPMI Device Information (Type 38)</summary>
		/// <remarks>
		/// The information in this structure defines the attributes of an Intelligent Platform Management Interface (IPMI) Baseboard Management Controller (BMC).
		/// Refer to the Intelligent Platform Management Interface (IPMI) Interface Specification for full documentation of IPMI and additional information on the use of this structure.
		/// 
		/// The Type 42 structure can also be used to describe a physical management controller host interface and 
		/// one or more protocols that share that interface.
		/// If IPMI is not shared with other protocols, either the Type 38 or the Type 42 structures can be used.
		/// Providing Type 38 is recommended for backward compatibility.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type38
		{
			/// <summary>Baseboard Management Controller (BMC) interface type</summary>
			public enum BmcInterfaceType : byte
			{
				/// <summary>Unknown</summary>
				Unknown = 0x00,
				/// <summary>KCS: Keyboard Controller Style</summary>
				KCS = 0x01,
				/// <summary>SMIC: Server Management Interface Chip</summary>
				SMIC = 0x02,
				/// <summary>BT: Block Transfer</summary>
				BT = 0x03,
				/// <summary>SSIF: SMBus System Interface</summary>
				SSIF = 0x04,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>INterface type</summary>
			/// <remarks>Baseboard Management Controller (BMC) interface type</remarks>
			public BmcInterfaceType InterfaceType;
			/// <summary>IPMI specification revision</summary>
			/// <remarks>
			/// IPMI specification revision, in BCD format, to which the BMC was designed.
			/// Bits 7:4 hold the most significant digit of the revision, while 
			/// Bits 3:0 hold the least significant bits.
			/// </remarks>
			/// <example>A value of 0x10 indicates revision 1.</example>
			public Byte IpmiSpecificationRevision;
			/// <summary>I2C Target Address</summary>
			/// <remarks>Target address on the I2C bus of this BMC</remarks>
			public Byte I2cTargetAddress;
			/// <summary>NV Storage Device Address</summary>
			/// <remarks>
			/// Bus ID of the NV storage device.
			/// If no storage device exists for this BMC, the field is set to 0xff
			/// </remarks>
			public Byte NVStorageDeviceAddress;
			/// <summary>Base Address</summary>
			/// <remarks>
			/// Base address (either memory-mapped or I/O) of the BMC
			/// If the least-significant bit of the field is a 1, the address is in I/O space;
			/// otherwise, the address is memory-mapped.
			/// Refer to the IPMI Interface Specification for usage details
			/// </remarks>
			public UInt64 BaseAddress;
			/// <summary>Base Address Modifier / Interrupt Info</summary>
			/// <remarks>
			/// (This field is unused and set to 0x00 for SSIF.) 
			/// bit 7:6 – Register spacing 
			///		00b = Interface registers are on successive byte  boundaries. 
			///		01b = Interface registers are on 32-bit boundaries. 
			///		10b = Interface registers are on 16-byte boundaries. 
			///		11b = Reserved. 
			/// bit 5 – Reserved. Return as 0b. 
			/// bit 4 – LS-bit for addresses: 
			///		0b = Address bit 0 = 0b 
			///		1b = Address bit 0 = 1b 
			///
			/// Interrupt Info Identifies the type and polarity of the interrupt associated with the IPMI system interface, if any: 
			/// bit 3 – Interrupt Info 
			///		1b = Interrupt information specified 
			///		0b = Interrupt information not specified 
			/// bit 2 – Reserved. Return as 0b 
			/// bit 1 – Interrupt Polarity 
			///		1b = active high 
			///		0b = active low 
			/// bit 0 – Interrupt Trigger Mode 
			///		1b = level 
			///		0b = edge
			/// </remarks>
			public Byte BaseAddressModifier;
			/// <summary>Interrupt number</summary>
			/// <remarks>
			/// Interrupt number for IPMI System Interface 
			/// 0x00 = unspecified/unsupporte
			/// </remarks>
			public Byte InterruptNumber;
		}

		/// <summary>System Power Supply (Type 39)</summary>
		/// <remarks>
		/// This structure identifies attributes of a system power supply.
		/// One instance of this structure is present for each possible power supply in a system.
		/// </remarks>
		/// <value>v2.3.1</value>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type39
		{
			/// <summary>Power supply type</summary>
			public enum DmtfPowerType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Linear</summary>
				Linear = 0x03,
				/// <summary>Switching</summary>
				Switching = 0x04,
				/// <summary>Battery</summary>
				Battery = 0x05,
				/// <summary>UPS</summary>
				UPS = 0x06,
				/// <summary>Converter</summary>
				Converter = 0x07,
				/// <summary>Regulator</summary>
				Regulator = 0x08,
			}
			/// <summary>DMTF Input Voltage Range Switching</summary>
			public enum DmtfInputVoltageType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Manual</summary>
				Manual=0x03,
				/// <summary>Auto-switch</summary>
				AutoSwitch=0x04,
				/// <summary>Wide range</summary>
				WideRange=0x05,
				/// <summary>Not applicable</summary>
				NotApplicable=0x06,
			}

			/// <summary>Power supply status</summary>
			public enum StatusType : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>OK</summary>
				OK = 0x03,
				/// <summary>Non-critical</summary>
				NonCritical = 0x04,
				/// <summary>Critical; power supply has failed and has been taken off-line</summary>
				Critical = 0x05,
			}
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Power unit group</summary>
			/// <remarks>
			/// Power unit group to which this power supply is associated.
			/// Specifying the same Power Unit Group value for more than one System Power Supply structure indicates a redundant power supply configuration.
			/// The field’s value is 0x00 if the power supply is not a member of a redundant power unit.
			/// Non-zero values imply redundancy and that at least one other power supply will be enumerated with the same value.
			/// </remarks>
			public Byte PowerUnitGroup;
			/// <summary>Location</summary>
			/// <example>
			/// Number of the string that identifies the location of the power supply.
			/// “in the back, on the left-hand side” or "Left Supply Bay"
			/// </example>
			public Byte Location;
			/// <summary>Device name</summary>
			/// <remarks>Number of the string that names the power supply device</remarks>
			/// <example>“DR-36”</example>
			public Byte DeviceName;
			/// <summary>Manufacturer</summary>
			/// <remarks>Number of the string that names the company that manufactured the supply</remarks>
			public Byte Manufacturer;
			/// <summary>Serial number</summary>
			/// <remarks>Number of the string that contains the serial number for the power supply</remarks>
			public Byte SerialNumber;
			/// <summary>Asset Tag number</summary>
			/// <remarks>Number of the string that contains the Asset Tag Number</remarks>
			public Byte AssetTagNumber;
			/// <summary>Model Part number</summary>
			/// <remarks>Number of the string that contains the OEM Part Order Number</remarks>
			public Byte ModelPartNumber;
			/// <summary>Revision Level</summary>
			/// <remarks>Power supply Revision String</remarks>
			/// <example>"2.30"</example>
			public Byte RevisionLevel;
			/// <summary>Max Power Capacity</summary>
			/// <remarks>Maximum sustained power output in Watts</remarks>
			/// <remarks>Set to 0x8000 if unknown. Note that the units specified by the DMTF for this field are milliWatts</remarks>
			public UInt16 MaxPowerCapacity;
			/// <summary>Power Supply Characteristics</summary>
			/// <remarks>
			/// Provides information about power supply characteristics
			/// 15 to 14 Reserved; set to 0x00 
			/// 13 to 10 DMTF Power Supply Type 
			///		0001 Other 
			///		0010 Unknown 
			///		0011 Linear 
			///		0100 Switching 
			///		0101 Battery 
			///		0110 UPS 
			///		0111 Converter 
			///		1000 Regulator 
			///		1001 to 1111b — Reserved for future assignment  
			/// 9 to 7 Status 
			///		001 Other 
			///		010 Unknown 
			///		011 OK 
			///		100 Non-critical 
			///		101 Critical; power supply has failed and has been taken off-line. 
			/// 6 to 3 DMTF Input Voltage Range Switching 
			///		0001 Other 
			///		0010 Unknown 
			///		0011 Manual 
			///		0100 Auto-switch 
			///		0101 Wide range 
			///		0110 Not applicable 
			///		0111 to 1111b — Reserved for future assignment  
			/// 2 1b power supply is unplugged from the wall 
			/// 1 1b power supply is present 
			/// 0 1b power supply is hot-replaceable
			/// </remarks>
			private UInt16 PowerSupplyCharacteristics;
			/// <summary>Input Voltage Probe handle</summary>
			/// <remarks>
			/// Handle, or instance number, of a voltage probe (Type 26) monitoring this power supply’s input voltage.
			/// A value of 0xFFFF indicates that no probe is provided.
			/// </remarks>
			public UInt16 InputVoltageProbeHandle;
			/// <summary>Cooling device handle</summary>
			/// <remarks>
			/// Handle, or instance number, of a cooling device (Type 27) associated with this power supply.
			/// A value of 0xFFFF indicates that no cooling device is provided
			/// </remarks>
			public UInt16 CoolingDeviceHandle;
			/// <summary>Input Current Probe handle</summary>
			/// <remarks>
			/// Handle, or instance number, of the electrical current probe (Type 29) monitoring this power supply’s input current.
			/// A value of 0xFFFF indicates that no current probe is provided
			/// </remarks>
			public UInt16 InputCurrentProbeHandle;
			/// <summary>Power supply is hot-replaceable</summary>
			public Boolean PowerSupplyIsHotReplaceable { get { return (this.PowerSupplyCharacteristics >> 0 & 0x01) == 0x01; } }
			/// <summary>Power supply is present</summary>
			public Boolean PowerSupplyIsPresent { get { return (this.PowerSupplyCharacteristics >> 1 & 0x01) == 0x01; } }
			/// <summary>Power supply is unplugged from the wall</summary>
			public Boolean PowerSupplyIsUnplugged { get { return (this.PowerSupplyCharacteristics >> 2 & 0x01) == 0x01; } }
			/// <summary>DMTF Input Voltage Range Switching</summary>
			public DmtfInputVoltageType PowerSupplyVoltageSwitch { get { return (DmtfInputVoltageType)(this.PowerSupplyCharacteristics >> 3 & 0xf); } }
			/// <summary>Power supply status</summary>
			public StatusType PowerSupplyStatus { get { return (StatusType)(this.PowerSupplyCharacteristics >> 7 & 0x7); } }
			/// <summary>Power supply type</summary>
			public DmtfPowerType PowerSupplyType { get { return (DmtfPowerType)(this.PowerSupplyCharacteristics >> 10 & 0xf); } }
		}

		/// <summary>Additional Information (Type 40)</summary>
		/// <remarks>This structure is intended to provide additional information for handling unspecified enumerated values and interim field updates in another structure.</remarks>
		/// <value>v2.6</value>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type40
		{
			/// <summary>Additional Information Entry</summary>
			[StructLayout(LayoutKind.Sequential,Pack=1)]
			public struct Type40_AdditionalInformation
			{
				/// <summary>Length of this Additional Information Entry instance; a minimum of 6</summary>
				public Byte EntryLength;
				/// <summary>Handle, or instance number, associated with the structure for which additional information is provided</summary>
				public UInt16 ReferencedHandle;
				/// <summary>Offset of the field within the structure referenced by the Referenced Handle for which additional information is provided</summary>
				public Byte ReferencedOffset;
				/// <summary>Number of the optional string to be associated with the field referenced by the Referenced Offset</summary>
				public Byte String;

				// <summary>Enumerated value or updated field content that has not yet been approved for publication in this specification and therefore could not be used in the field referenced by Referenced Offset</summary>
				// <remarks>This field is the same type and size as the field being referenced by this Additional Information Entry</remarks>
				//public Byte[] Value;
				//TODO: Varable length array follows
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Additional Information Entries Count</summary>
			/// <remarks>Number of Additional Information Entries that follow</remarks>
			public Byte AdditionalInformationEntriesCount;
		}

		/// <summary>Onboard Devices Extended Information (Type 41)</summary>
		/// <remarks>
		/// The information in this structure defines the attributes of devices that are onboard (soldered onto) a system element, usually the baseboard.
		/// In general, an entry in this table implies that the BIOS has some level of control over the enablement of the associated device for use by the system.
		/// 
		/// NOTE This structure replaces Onboard Device Information (<see cref="Type10"/>) starting with version 2.6 of this specification.
		/// BIOS providers can choose to implement both types to allow existing SMBIOS browsers to properly display the system’s onboard devices information
		/// </remarks>
		/// <value>v2.6</value>
		[StructLayout(LayoutKind.Sequential,Pack=1)]
		public struct Type41
		{
			/// <summary>Device type</summary>
			public enum Device : byte
			{
				/// <summary>Other</summary>
				Other = 0x01,
				/// <summary>Unknown</summary>
				Unknown = 0x02,
				/// <summary>Video</summary>
				Video = 0x03,
				/// <summary>SCSI Controller</summary>
				ScsiController = 0x04,
				/// <summary>Ethernet</summary>
				Ethernet = 0x05,
				/// <summary>Token Ring</summary>
				TokenRing = 0x06,
				/// <summary>Sound</summary>
				Sound = 0x07,
				/// <summary>PATA Controller</summary>
				PataController = 0x08,
				/// <summary>SATA Controller</summary>
				SataController = 0x09,
				/// <summary>SAS Controller</summary>
				SasController = 0x0A,
			}

			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Reference Designation</summary>
			/// <remarks>String number of the onboard device reference designation</remarks>
			public Byte ReferenceDesignation;
			/// <summary>Type</summary>
			/// <remarks>
			/// Type of the device
			/// Bit 7 – Device Status: 
			///		1 – Device Enabled 
			///		0 – Device Disabled 
			/// Bits 6:0 – Type of Device
			/// </remarks>
			private Byte _DeviceType;
			/// <summary>Type instance</summary>
			/// <remarks>
			/// Device Type Instance is a unique value (within a given onboard device type) used to indicate the order the device is designated by the system.
			/// For example, a system with two identical Ethernet NICs may designate one NIC (with higher Bus/Device/Function=15/0/0) as the first onboard NIC (instance 1)
			/// and the other NIC (with lower Bus/Device/Function =3/0/0) as the second onboard NIC (instance 2).
			/// </remarks>
			public Byte DeviceTypeInstance;
			/// <summary>Segment Group number</summary>
			/// <remarks>
			/// For devices that are not of types PCI, AGP, PCI-X, or PCI-Express and that do not have bus/device/function information,
			/// 0xff should be populated in the fields of Segment Group Number, Bus Number, Device/Function Number.
			/// 
			/// Segment Group Number is defined in the PCI Firmware Specification.
			/// The value is 0 for a single-segment topology.
			/// </remarks>
			public UInt16 SegmentGroupNumber;
			/// <summary>Bus number</summary>
			/// <remarks>
			/// For devices that are not of types PCI, AGP, PCI-X, or PCI-Express and that do not have bus/device/function information,
			/// 0xff should be populated in the fields of Segment Group Number, Bus Number, Device/Function Number.
			/// 
			/// Segment Group Number is defined in the PCI Firmware Specification.
			/// The value is 0 for a single-segment topology.
			/// </remarks>
			public Byte BusNumber;
			/// <summary>Device &amp; Function number</summary>
			/// <remarks>
			/// For devices that are not of types PCI, AGP, PCI-X, or PCI-Express and that do not have bus/device/function information,
			/// 0xff should be populated in the fields of Segment Group Number, Bus Number, Device/Function Number.
			/// 
			/// Bits 7:3 – Device number 
			/// Bits 2:0 – Function number
			/// </remarks>
			private Byte DeviceAndFunctionNumber;

			/// <summary>Is Device enabled</summary>
			public Boolean DeviceEnabled { get { return (this._DeviceType >> 7 & 0x01) == 1; } }

			/// <summary>Type of the device</summary>
			public Device DeviceType { get { return (Device)(this._DeviceType >> 0 & 0x3f); } }

			/// <summary>Function number</summary>
			public Byte FunctionNumber { get { return (Byte)(this.DeviceAndFunctionNumber >> 0 & 0x03); } }

			/// <summary>Device number</summary>
			public Byte DeviceNumber { get { return (Byte)(this.DeviceAndFunctionNumber >> 3 & 0x1f); } }
		}

		/// <summary>Management Controller Host Interface (Type 42)</summary>
		/// <remarks>
		/// The information in this structure defines the attributes of a Management Controller Host Interface that is not discoverable by “Plug and Play” mechanisms.
		/// The Type 42 structure can be used to describe a physical management controller host interface and one or more protocols that share that interface.
		/// 
		/// Type 42 should be used for management controller host interfaces that use protocols other than IPMI or that use multiple protocols on a single host interface type.
		/// 
		/// This structure should also be provided if IPMI is shared with other protocols over the same interface hardware.
		/// If IPMI is not shared with other protocols, either the Type 38 or the Type 42 structures can be used.
		/// Providing Type 38 is recommended for backward compatibility.
		/// The structures are not required to be mutually exclusive.
		/// Type 38 and Type 42 structures may be implemented simultaneously to provide backward compatibility with IPMI applications or drivers that do not yet recognize the Type 42 structure. 
		/// Refer to the Intelligent Platform Management Interface (IPMI) Interface Specification for full 2011 documentation of IPMI and additional information on the use of this structure with IPMI.
		/// </remarks>
		/// <see>http://developer.intel.com/design/servers/ipmi/spec.htm</see>
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Type42
		{
			/// <summary>SMBIOS type header</summary>
			public Header Header;
			/// <summary>Interface type</summary>
			/// <remarks>Management Controller Interface Type</remarks>
			public Byte InterfaceType;
			/// <summary>Interface Type specific data length</summary>
			/// <remarks>Management Controller Host Interface Data as specified by the Interface Type</remarks>
			public Byte InterfaceTypeSpecificDataLength;
			//TODO: Varable length array follows
		}
	}
}