using System;
using System.Runtime.InteropServices;

namespace AlphaOmega.Debug.Native
{
	/// <summary>Win32 API wrapper</summary>
	public static class Methods
	{
		/// <summary>Identifier of the firmware table provider</summary>
		public enum FirmwareTableType : uint
		{
			/// <summary>The ACPI firmware table provider</summary>
			Acpi = 0x41435049,
			/// <summary>The raw firmware table provider</summary>
			Firm = 0x4649524D,
			/// <summary>The raw SMBIOS firmware table provider</summary>
			Rsmb = 0x52534D42,
		}

		/// <summary>Retrieves the specified firmware table from the firmware table provider.</summary>
		/// <param name="FirmwareTableProviderSignature">
		/// The identifier of the firmware table provider to which the query is to be directed. This parameter can be one of the following values:
		/// <list type="bullet">
		///		<item>
		///			<term>ACPI</term>
		///			<description>The ACPI firmware table provider</description>
		///		</item>
		///		<item>
		///			<term>FIRM</term>
		///			<description>The raw firmware table provider</description>
		///		</item>
		///		<item>
		///			<term>RSMB</term>
		///			<description>The raw SMBIOS firmware table provider</description>
		///		</item>
		/// </list>
		/// </param>
		/// <param name="FirmwareTableID">
		/// The identifier of the firmware table. This identifier is little endian, you must reverse the characters in the string.
		/// For example, FACP is an ACPI provider, as described in the Signature field of the DESCRIPTION_HEADER structure in the ACPI specification (see http://www.acpi.info).
		/// 
		/// Therefore, use 'PCAF' to specify the FACP table, as shown in the following example: retVal = GetSystemFirmwareTable('ACPI', 'PCAF', pBuffer, BUFSIZE);
		/// </param>
		/// <param name="pFirmwareTableBuffer">A pointer to a buffer that receives the requested firmware table. If this parameter is NULL, the return value is the required buffer size.</param>
		/// <param name="BufferSize">The size of the pFirmwareTableBuffer buffer, in bytes.</param>
		/// <remarks>
		/// Starting with Windows 10, version 1803, Universal Windows apps can access the System Management BIOS (SMBIOS) information by declaring the smbios restricted capability in the app manifest.
		/// See Access SMBIOS information from a Universal Windows App for details.
		/// Only raw SMBIOS (RSMB) firmware tables can be accessed from a Universal Windows app.
		/// 
		/// As of Windows Server 2003 with Service Pack 1 (SP1), applications cannot access the \Device\PhysicalMemory object.
		/// Access to this object is limited to kernel-mode drivers.
		/// This change affects applications read System Management BIOS (SMBIOS) or other BIOS data stored in the lowest 1MB of physical memory. Applications have the following alternatives to read data from low physical memory:
		/// 1) Retrieve the SMBIOS properties using WMI.
		/// 2) Use the GetSystemFirmwareTable function to read the raw SMBIOS firmware table.
		/// 
		/// The raw SMBIOS table provider ('RSMB') retrieves the contents of the raw SMBIOS firmware table.
		/// </remarks>
		/// <returns>
		/// If the function succeeds, the return value is the number of bytes written to the buffer. This value will always be less than or equal to BufferSize.
		/// If the function fails because the buffer is not large enough, the return value is the required buffer size, in bytes. This value is always greater than BufferSize.
		/// If the function fails for any other reason, the return value is zero. To get extended error information, call GetLastError.
		/// </returns>
		[DllImport("kernel32.dll", EntryPoint = "GetSystemFirmwareTable", SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern UInt32 GetSystemFirmwareTable(
			FirmwareTableType FirmwareTableProviderSignature,
			UInt32 FirmwareTableID,
			IntPtr pFirmwareTableBuffer,
			UInt32 BufferSize);

		/// <summary>Enumerates all system firmware tables of the specified type</summary>
		/// <param name="FirmwareTableProviderSignature">
		/// The identifier of the firmware table provider to which the query is to be directed.
		/// This parameter can be one of the following values:
		/// <list type="bullet">
		///		<item>
		///			<term>ACPI</term>
		///			<description>The ACPI firmware table provider</description>
		///		</item>
		///		<item>
		///			<term>FIRM</term>
		///			<description>The raw firmware table provider. Not supported for UEFI systems; use 'RSMB' instead.</description>
		///		</item>
		///		<item>
		///			<term>RSMB</term>
		///			<description>The raw SMBIOS firmware table provider</description>
		///		</item>
		/// </list>
		/// </param>
		/// <param name="pFirmwareTableEnumBuffer">
		/// A pointer to a buffer that receives the list of firmware tables.
		/// If this parameter is NULL, the return value is the required buffer size.
		/// </param>
		/// <param name="BufferSize">The size of the pFirmwareTableBuffer buffer, in bytes.</param>
		/// <remarks>
		/// Starting with Windows 10, version 1803, Universal Windows apps can access the System Management BIOS (SMBIOS) information by declaring the smbios restricted capability in the app manifest.
		/// See Access SMBIOS information from a Universal Windows App for details.
		/// Only raw SMBIOS (RSMB) firmware tables can be accessed from a Universal Windows app.
		/// 
		/// As of Windows Server 2003 with Service Pack 1 (SP1), applications cannot access the \Device\PhysicalMemory object.
		/// Access to this object is limited to kernel-mode drivers.
		/// This change affects applications read System Management BIOS (SMBIOS) or other BIOS data stored in the lowest 1MB of physical memory. Applications have the following alternatives to read data from low physical memory:
		/// 1) Retrieve the SMBIOS properties using WMI.
		/// 2) Use the GetSystemFirmwareTable function to read the raw SMBIOS firmware table.
		/// 
		/// The raw SMBIOS table provider ('RSMB') retrieves the contents of the raw SMBIOS firmware table.
		/// </remarks>
		/// <returns>
		/// If the function succeeds, the return value is the number of bytes written to the buffer. This value will always be less than or equal to BufferSize.
		/// If the function fails because the buffer is not large enough, the return value is the required buffer size, in bytes. This value is always greater than BufferSize.
		/// If the function fails for any other reason, the return value is zero. To get extended error information, call GetLastError.
		/// </returns>
		[DllImport("kernel32.dll", EntryPoint = "EnumSystemFirmwareTables", SetLastError = true)]
		internal static extern UInt32 EnumSystemFirmwareTables(
			FirmwareTableType FirmwareTableProviderSignature,
			IntPtr pFirmwareTableEnumBuffer,
			UInt32 BufferSize);
	}
}