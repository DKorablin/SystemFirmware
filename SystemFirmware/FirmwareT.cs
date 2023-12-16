using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlphaOmega.Debug.Native;

namespace AlphaOmega.Debug
{
	/// <summary>Base firmware typed facade</summary>
	/// <typeparam name="T">Type of required firmware</typeparam>
	public class FirmwareT<T> : Firmware where T : FirmwareTable, new()
	{
		private readonly Dictionary<UInt32, Byte[]> _storage;

		/// <summary>Creates instance of base firmware with previosly collected data</summary>
		/// <param name="data">Full payload with all tables</param>
		/// <exception cref="ArgumentNullException"><c>data</c> is empty or null</exception>
		/// <exception cref="InvalidOperationException">Invalid signature or invalid type</exception>
		public FirmwareT(Byte[] data)
			: base(GetTable<T>())
		{
			if(data == null || data.Length == 0)
				throw new ArgumentNullException(nameof(data), "Binary data is required");

			using(PinnedBufferReader reader = new PinnedBufferReader(data))
			{
				UInt32 padding = 0;
				FWStructs.Header header = reader.BytesToStructure<FWStructs.Header>(ref padding);
				if(!header.IsValid)
				{
					Exception exc = new InvalidOperationException("Invalid signature");
					exc.Data.Add("Header", header.SignatureStr);
					throw exc;
				}
				if(header.Type != base.TableType)
				{
					Exception exc = new InvalidOperationException("Invalid type");
					exc.Data.Add("Type", header.Type);
					throw exc;
				}
				if(header.Count == 0)
					throw new InvalidOperationException("No data");

				this._storage = new Dictionary<UInt32, Byte[]>();
				for(UInt32 loop = 0; loop < header.Count; loop++)
				{
					FWStructs.Table table = reader.BytesToStructure<FWStructs.Table>(ref padding);
					Byte[] tableData = reader.GetBytes(padding, table.Size);
					padding += table.Size;

					this._storage.Add(table.TableId, tableData);
				}
			}
		}

		/// <summary>Creates instance of base firmware typed facade</summary>
		public FirmwareT()
			: base(GetTable<T>())
		{ }

		/// <summary>Save loaded data to Byte[] for future examination</summary>
		/// <returns>Byte[] with custom header</returns>
		public Byte[] Save()
		{
			using(MemoryStream stream = new MemoryStream())
			{
				UInt32[] tableIDs = this.EnumFirmwareTables()
					.Distinct()//TODO: Win32 API функция возвращает одинаковые ID'шники для ACPI????
					.ToArray();

				FWStructs.Header header = new FWStructs.Header(base.TableType, (UInt16)tableIDs.Length);
				Byte[] bHeader = Utils.ConvertToBytes(header);
				stream.Write(bHeader, 0, bHeader.Length);

				foreach(UInt32 tableId in tableIDs)
				{
					Byte[] data = this.GetSystemFirmwareTable(tableId);
					FWStructs.Table table = new FWStructs.Table(tableId, (UInt32)data.Length);
					Byte[] bTable = Utils.ConvertToBytes(table);
					stream.Write(bTable, 0, bTable.Length);
					stream.Write(data, 0, data.Length);
				}
				return stream.ToArray();
			}
		}

		/// <summary>Loads all firmware tables from Win32 API or loads from local storage</summary>
		/// <returns>ID's of known firmware tables</returns>
		public override IEnumerable<UInt32> EnumFirmwareTables()
			=> this._storage == null
				? base.EnumFirmwareTables()
				: this._storage.Keys;

		/// <summary>Gets all data from Win32 API or from local storage table by ID</summary>
		/// <param name="firmwareTableID">Firmware table ID</param>
		/// <returns>Payload</returns>
		public override Byte[] GetSystemFirmwareTable(UInt32 firmwareTableID = 0)
			=> this._storage == null
				? base.GetSystemFirmwareTable(firmwareTableID)
				: this._storage[firmwareTableID];

		/// <summary>Load firmware data from memory or from Win32 API function(s)</summary>
		/// <returns>Firmware tables</returns>
		public IEnumerable<T> GetData()
		{
			foreach(UInt32 tableId in this.EnumFirmwareTables())
			{
				Byte[] data = this.GetSystemFirmwareTable(tableId);
				yield return new T() { TableId = tableId, Data = data, };
			}
		}

		private static Methods.FirmwareTableType GetTable<F>()
		{
			if(typeof(F) == typeof(FirmwareAcpi))
				return Methods.FirmwareTableType.Acpi;
			else if(typeof(F) == typeof(FirmwareFirm))
				return Methods.FirmwareTableType.Firm;
			else if(typeof(F) == typeof(FirmwareSmBios))
				return Methods.FirmwareTableType.Rsmb;
			else throw new NotImplementedException();
		}
	}
}