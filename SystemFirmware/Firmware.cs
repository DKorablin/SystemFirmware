using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug
{
	/// <summary>Base Firmware facade</summary>
	public class Firmware
	{
		private readonly Methods.FirmwareTableType _tableType;

		/// <summary>Identifier of the firmware table provider</summary>
		public Methods.FirmwareTableType TableType { get { return this._tableType; } }

		/// <summary>Crate instanceof base firmware table type with requred data</summary>
		/// <param name="tableType">Required firmware table</param>
		public Firmware(Methods.FirmwareTableType tableType)
		{
			this._tableType = tableType;
		}

		/// <summary>Loads all firmware tables from Win32 API</summary>
		/// <returns>ID's of known firmware tables</returns>
		public virtual IEnumerable<UInt32> EnumFirmwareTables()
		{
			Int32 size = Marshal.SizeOf(typeof(UInt64));
			UInt32 dataSize = Methods.EnumSystemFirmwareTables(this._tableType, IntPtr.Zero, 0);
			if(dataSize == 0)
				throw new Win32Exception();

			Byte[] payload = new Byte[dataSize];
			IntPtr hBuffer = Marshal.AllocHGlobal((Int32)dataSize);
			try
			{
				dataSize = Methods.EnumSystemFirmwareTables(this._tableType, hBuffer, dataSize);
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
			UInt32 dataSize = AlphaOmega.Debug.Native.Methods.GetSystemFirmwareTable(this._tableType, firmwareTableID, IntPtr.Zero, 0);
			if(dataSize == 0)
				throw new Win32Exception();

			Byte[] payload = new Byte[dataSize];
			IntPtr hBuffer = Marshal.AllocHGlobal((Int32)dataSize);
			try
			{
				dataSize = AlphaOmega.Debug.Native.Methods.GetSystemFirmwareTable(this._tableType, firmwareTableID, hBuffer, dataSize);
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