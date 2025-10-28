using System;

namespace AlphaOmega.Debug
{
	/// <summary>Base firmware information storage</summary>
	public class FirmwareTable
	{
		/// <summary>ID of firmware table</summary>
		public UInt32 TableId { get; internal set; }

		/// <summary>Payload</summary>
		public Byte[] Data { get; internal set; }

		/// <summary>Creates instance of base firmware information</summary>
		protected internal FirmwareTable()
		{ }

		/// <summary>Creates instance of base firmware information with external data</summary>
		/// <param name="tableId">ID of firmware table</param>
		/// <param name="data">Payload</param>
		/// <exception cref="ArgumentNullException"><paramref name="data"/> is null or empty</exception>
		public FirmwareTable(UInt32 tableId, Byte[] data)
		{
			if(data == null || data.Length == 0)
				throw new ArgumentNullException(nameof(data), "Payload is empty");

			this.TableId = tableId;
			this.Data = data;
		}
	}
}