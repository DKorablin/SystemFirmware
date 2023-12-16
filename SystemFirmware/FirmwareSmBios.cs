using System;
using System.Collections.Generic;
using System.Text;
using AlphaOmega.Debug.Native;
using AlphaOmega.Debug.Smb;

namespace AlphaOmega.Debug
{
	/// <summary>SMBIOS facade</summary>
	public class FirmwareSmBios : FirmwareTable
	{
		private SmBios.RawSMBIOSHeader? _header;
		private List<TypeBase> _types;

		/// <summary>SMBIOS header</summary>
		public SmBios.RawSMBIOSHeader Header
		{
			get
			{
				if(this._header == null)
					this.Parse();
				return this._header.Value;
			}
		}

		/// <summary>Array of all loaded SMBIOS Types</summary>
		public IEnumerable<TypeBase> Types
		{
			get
			{
				if(this._types == null)
					this.Parse();
				return this._types;
			}
		}

		/// <summary>Gets specified type from parsed SMBIOS memory</summary>
		/// <typeparam name="T">SMBIOS type facade</typeparam>
		/// <returns>First found type</returns>
		public T GetType<T>() where T : TypeBase
		{
			foreach(T item in this.GetTypes<T>())
				return item;
			return null;
		}

		/// <summary>Get all specified types from parsed SMBIOS memory</summary>
		/// <typeparam name="T">SMBIOS type facade</typeparam>
		/// <returns>All found types</returns>
		public IEnumerable<T> GetTypes<T>() where T : TypeBase
		{
			Type typeT = typeof(T);
			foreach(TypeBase type in this.Types)
				if(type.GetType() == typeT)
					yield return (T)type;
		}

		private static Dictionary<SmBios.Type, TypeBase.TypeMapping> StructMapping = new Dictionary<SmBios.Type, TypeBase.TypeMapping>
		{
			{ SmBios.Type.Bios, new TypeBase.TypeMapping(typeof(Bios)) },
			{ SmBios.Type.System, new TypeBase.TypeMapping(typeof(SystemType)) },
			{ SmBios.Type.Baseboard, new TypeBase.TypeMapping(typeof(Baseboard)) },
			{ SmBios.Type.Chassis, new TypeBase.TypeMapping(typeof(Chassis)) },
			{ SmBios.Type.Processor, new TypeBase.TypeMapping(typeof(Processor)) },
			{ SmBios.Type.MemoryController, new TypeBase.TypeMapping(typeof(MemoryController)) },
			{ SmBios.Type.MemoryModule, new TypeBase.TypeMapping(typeof(MemoryModule)) },
			{ SmBios.Type.Cache, new TypeBase.TypeMapping(typeof(Cache)) },
			{ SmBios.Type.PortConnector, new TypeBase.TypeMapping(typeof(PortConnector)) },
			{ SmBios.Type.SystemSlots, new TypeBase.TypeMapping(typeof(SystemSlots)) },
			{ SmBios.Type.OnBoardDevices, new TypeBase.TypeMapping(typeof(OnBoardDevices)) },
			{ SmBios.Type.OEMStrings, new TypeBase.TypeMapping(typeof(OEMStrings)) },
			{ SmBios.Type.SystemConfigurationOptions, new TypeBase.TypeMapping(typeof(SystemConfigurationOptions)) },
			{ SmBios.Type.BiosLanguage, new TypeBase.TypeMapping(typeof(BiosLanguage)) },
			{ SmBios.Type.GroupAssociations, new TypeBase.TypeMapping(typeof(GroupAssociations)) },

			{ SmBios.Type.MemoryDevice, new TypeBase.TypeMapping(typeof(MemoryDevice)) },

			{ SmBios.Type.PortableBattery, new TypeBase.TypeMapping(typeof(PortableBattery)) },

			{ SmBios.Type.VoltageProbe, new TypeBase.TypeMapping(typeof(VoltageProbe)) },
			{ SmBios.Type.CoolingDevice, new TypeBase.TypeMapping(typeof(CoolingDevice)) },
			{ SmBios.Type.TemperatureProbe, new TypeBase.TypeMapping(typeof(TemperatureProbe)) },
			{ SmBios.Type.ElectricalCurrentProbe, new TypeBase.TypeMapping(typeof(ElectricalCurrentProbe)) },

			{ SmBios.Type.ManagementDevice, new TypeBase.TypeMapping(typeof(ManagementDevice)) },
			{ SmBios.Type.ManagementDeviceComponent, new TypeBase.TypeMapping(typeof(ManagementDeviceComponent)) },

			{ SmBios.Type.SystemPowerSupply, new TypeBase.TypeMapping(typeof(SystemPowerSupply)) },
			{ SmBios.Type.AdditionalInformation, new TypeBase.TypeMapping(typeof(AdditionalInformation)) },
			{ SmBios.Type.OnboardDevicesExtended, new TypeBase.TypeMapping(typeof(OnboardDevicesExtended)) },
		};

		private void Parse()
		{
			this._types = new List<TypeBase>();

			using(PinnedBufferReader reader = new PinnedBufferReader(base.Data))
			{
				UInt32 padding = 0;
				this._header = reader.BytesToStructure<SmBios.RawSMBIOSHeader>(ref padding);

				while(padding < base.Data.Length)
				{
					UInt32 structPadding = padding;
					SmBios.Header header = reader.BytesToStructure<SmBios.Header>(padding);
					TypeBase table = null;

					TypeBase.TypeMapping mapping;
					if(StructMapping.TryGetValue(header.Type, out mapping))
					{
						Byte[] exBytes;
						Object typeN = reader.BytesToStructure2(mapping.StructType, padding, header.Length, out exBytes);
						table = (TypeBase)mapping.Ctor.Invoke(new Object[] { typeN });
						table.ExData = exBytes;

						//Двигаемся в начало массива строк
						padding = padding + header.Length;
					} else
					{
						switch(header.Type)
						{
						case SmBios.Type.SystemEventLog:
							SmBios.Type15 type15 = reader.BytesToStructure<SmBios.Type15>(ref structPadding);
							table = new TypeBaseT<SmBios.Type15>(header, type15);
							break;
						case SmBios.Type.PhysicalMemoryArray:
							SmBios.Type16 type16 = reader.BytesToStructure<SmBios.Type16>(ref structPadding);
							table = new TypeBaseT<SmBios.Type16>(header, type16);
							break;
						case SmBios.Type.MemoryError32Bit:
							SmBios.Type18 type18 = reader.BytesToStructure<SmBios.Type18>(ref structPadding);
							table = new TypeBaseT<SmBios.Type18>(header, type18);
							break;
						case SmBios.Type.MemoryArrayMappedAddress:
							SmBios.Type19 type19 = reader.BytesToStructure<SmBios.Type19>(ref structPadding);
							table = new TypeBaseT<SmBios.Type19>(header, type19);
							break;
						case SmBios.Type.MemoryDeviceMappedAddress:
							SmBios.Type20 type20 = reader.BytesToStructure<SmBios.Type20>(ref structPadding);
							table = new TypeBaseT<SmBios.Type20>(header, type20);
							break;
						case SmBios.Type.BuiltInPointingDevice:
							SmBios.Type21 type21 = reader.BytesToStructure<SmBios.Type21>(ref structPadding);
							table = new TypeBaseT<SmBios.Type21>(header, type21);
							break;
						case SmBios.Type.SystemReset:
							SmBios.Type23 type23 = reader.BytesToStructure<SmBios.Type23>(ref structPadding);
							table = new TypeBaseT<SmBios.Type23>(header, type23);
							break;
						case SmBios.Type.HardwareSecurity:
							SmBios.Type24 type24 = reader.BytesToStructure<SmBios.Type24>(ref structPadding);
							table = new TypeBaseT<SmBios.Type24>(header, type24);
							break;
						case SmBios.Type.SystemPowerControls:
							SmBios.Type25 type25 = reader.BytesToStructure<SmBios.Type25>(ref structPadding);
							table = new TypeBaseT<SmBios.Type25>(header, type25);
							break;
						case SmBios.Type.OobRemoteAccess:
							SmBios.Type30 type30 = reader.BytesToStructure<SmBios.Type30>(ref structPadding);
							table = new TypeBaseT<SmBios.Type30>(header, type30);
							break;
						case SmBios.Type.SystemBootInformation:
							SmBios.Type32 type32 = reader.BytesToStructure<SmBios.Type32>(ref structPadding);
							table = new TypeBaseT<SmBios.Type32>(header, type32);
							break;
						case SmBios.Type.MemoryError64Bit:
							SmBios.Type33 type33 = reader.BytesToStructure<SmBios.Type33>(ref structPadding);
							table = new TypeBaseT<SmBios.Type33>(header, type33);
							break;
						case SmBios.Type.ManagementDeviceThresholdData:
							SmBios.Type36 type36 = reader.BytesToStructure<SmBios.Type36>(ref structPadding);
							table = new TypeBaseT<SmBios.Type36>(header, type36);
							break;
						case SmBios.Type.MemoryChannel:
							SmBios.Type37 type37 = reader.BytesToStructure<SmBios.Type37>(ref structPadding);
							table = new TypeBaseT<SmBios.Type37>(header, type37);
							break;
						case SmBios.Type.IPMIDeviceInformation:
							SmBios.Type38 type38 = reader.BytesToStructure<SmBios.Type38>(ref structPadding);
							table = new TypeBaseT<SmBios.Type38>(header, type38);
							break;
						case SmBios.Type.ManagementControllerHostInterface:
							SmBios.Type42 type42 = reader.BytesToStructure<SmBios.Type42>(ref structPadding);
							table = new TypeBaseT<SmBios.Type42>(header, type42);
							break;
						case SmBios.Type.EoT:
							if(header.Length == 4)
								return;
							else
								throw new NotSupportedException();
						default:
#if DEBUG
							/*if(!Enum.IsDefined(typeof(SmBios.Type), header.Type))//Нам может быть неизвестен заголовок
							throw new NotImplementedException();*/
#endif
							table = new TypeBase(header);
							break;
						}

						//Двигаемся в начало массива строк
						padding += header.Length;

						//В разных версиях - разный размер структур
						if(structPadding < padding)
						{//Стуктура меньше чем данные. Необходимо скопировать недостающие
							table.ExData = new Byte[padding - structPadding];
							Array.Copy(base.Data, structPadding, table.ExData, 0, table.ExData.Length);
						}
					}

					List<String> strings = new List<String>();
					UInt32 stringStart = padding;
					for(UInt32 loop = padding; loop < base.Data.Length; loop++)
					{
						if(base.Data[loop] == 0x00)
						{
							if(loop == stringStart)
							{//0x00, 0x00 <- End of array
								while(base.Data[loop] == 0x00)
									loop++;//0x00, 0x00, 0x00, 0x00, .... <- Может закончится массивом из нулей
								padding = loop;
								break;
							} else
							{//0x00 <- End of string
								String str = Encoding.ASCII.GetString(base.Data, (Int32)stringStart, (Int32)(loop - stringStart));
								strings.Add(str);
								stringStart = loop + 1;
							}
						}
					}

					table.Strings = strings.ToArray();
					this._types.Add(table);
				}
			}
		}
	}
}