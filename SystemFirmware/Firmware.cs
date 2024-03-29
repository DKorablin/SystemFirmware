﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug
{
	/// <summary>Base Firmware facade</summary>
	public class Firmware
	{
		/// <summary>Identifier of the firmware table provider</summary>
		public Methods.FirmwareTableType TableType { get ; }

		/// <summary>Crate instanceof base firmware table type with requred data</summary>
		/// <param name="tableType">Required firmware table</param>
		public Firmware(Methods.FirmwareTableType tableType)
			=> this.TableType = tableType;

		/// <summary>Loads all firmware tables from Win32 API</summary>
		/// <exception cref="Win32Exception">Failed to retrieve firmware information</exception>
		/// <returns>ID's of known firmware tables</returns>
		public virtual IEnumerable<UInt32> EnumFirmwareTables()
		{
			UInt32 dataSize = Methods.EnumSystemFirmwareTables(this.TableType, IntPtr.Zero, 0);
			if(dataSize == 0)
			{
				switch((UInt32)Marshal.GetHRForLastWin32Error())
				{
				case 0x80070001://Incorrect function
					if(this.TableType == Methods.FirmwareTableType.Firm)
						throw new NotSupportedException("Not supported for UEFI systems; use 'RSMB' instead.");
					else goto default;
				case 0x80070000://The operation completed succesfully
					yield break;
				default:
					throw new Win32Exception();
				}
			}

			Byte[] payload = new Byte[dataSize];
			IntPtr hBuffer = Marshal.AllocHGlobal((Int32)dataSize);
			try
			{
				dataSize = Methods.EnumSystemFirmwareTables(this.TableType, hBuffer, dataSize);
				if(dataSize == 0)
					throw new Win32Exception();

				Marshal.Copy(hBuffer, payload, 0, (Int32)dataSize);
			} finally
			{
				Marshal.FreeHGlobal(hBuffer);
			}

			for(Int32 loop = 0; loop < payload.Length; loop += sizeof(UInt32))
				yield return BitConverter.ToUInt32(payload, loop);
		}

		/// <summary>Gets all data from firmware table by ID</summary>
		/// <param name="firmwareTableID">Firmware table ID</param>
		/// <returns>Payload</returns>
		public virtual Byte[] GetSystemFirmwareTable(UInt32 firmwareTableID = 0)
		{
			UInt32 dataSize = Methods.GetSystemFirmwareTable(this.TableType, firmwareTableID, IntPtr.Zero, 0);
			if(dataSize == 0)
				throw new Win32Exception();

			Byte[] payload = new Byte[dataSize];
			IntPtr hBuffer = Marshal.AllocHGlobal((Int32)dataSize);
			try
			{
				dataSize = Methods.GetSystemFirmwareTable(this.TableType, firmwareTableID, hBuffer, dataSize);
				if(dataSize == 0)
					throw new Win32Exception();

				Marshal.Copy(hBuffer, payload, 0, (Int32)dataSize);
			} finally
			{
				Marshal.FreeHGlobal(hBuffer);
			}

			return payload;
		}
	}
}