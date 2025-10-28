using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AlphaOmega.Debug;
using AlphaOmega.Debug.Native;
using AlphaOmega.Debug.Smb;

namespace Demo
{
	class Program
	{
		static void Main(String[] args)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			FirmwareT<FirmwareSmBios> fw = new FirmwareT<FirmwareSmBios>();
			Baseboard baseboard1 = fw.GetData().Single().GetType<Baseboard>();
			String fileName = Path.Combine(Environment.CurrentDirectory, baseboard1.Product + "_" + baseboard1.SerialNumber + "_smb.sfw");
			if(!File.Exists(fileName))
				File.WriteAllBytes(fileName, fw.Save());

			FirmwareT<FirmwareAcpi> acpi = new FirmwareT<FirmwareAcpi>();
			fileName = Path.Combine(Environment.CurrentDirectory, baseboard1.Product + "_" + baseboard1.SerialNumber + "_acpi.sfw");
			if(!File.Exists(fileName))
				File.WriteAllBytes(fileName, acpi.Save());

			acpi = new FirmwareT<FirmwareAcpi>(File.ReadAllBytes(fileName));
			foreach(FirmwareAcpi table in acpi.GetData())
			{
				if(!Enum.IsDefined(typeof(Acpi.Table), table.Header.Signature))
					Console.WriteLine("Undefined table: {0}", table.Header.SignatureStr);
			}

			FirmwareT<FirmwareFirm> firm = new FirmwareT<FirmwareFirm>();
				fileName = Path.Combine(Environment.CurrentDirectory, baseboard1.Product + "_" + baseboard1.SerialNumber + "_firm.sfw");
			if(!File.Exists(fileName))
			{
				try
				{
					Byte[] payload = firm.Save();
					if(payload.Length > 0)
						File.WriteAllBytes(fileName, firm.Save());
				}catch(NotSupportedException exc)
				{
					Console.WriteLine(exc.Message);
				}
			}

			if(File.Exists(fileName))
			{
				firm = new FirmwareT<FirmwareFirm>(File.ReadAllBytes(fileName));
				foreach(FirmwareFirm table in firm.GetData())
				{
					#region Firmware parsing test (Failed)
					if(table.TableId == 786432 && baseboard1.Product == "P8H61-M PRO")
					{
						using(PinnedBufferReader reader = new PinnedBufferReader(table.Data))
						{
							UInt32 padding = 0;
							FirmwareImage.FFS_FILE_HEADER vesaHdr = reader.BytesToStructure<FirmwareImage.FFS_FILE_HEADER>(ref padding);

							const UInt32 pceStartOffset = 0x00000040;
							padding = pceStartOffset;
							FirmwareImage.Pcir_header pceExp = reader.BytesToStructure<FirmwareImage.Pcir_header>(ref padding);

							const UInt32 vbtStartOffset = 0x00000ac0;
							padding = vbtStartOffset;
							FirmwareImage.vbt_header vtbHeader = reader.BytesToStructure<FirmwareImage.vbt_header>(ref padding);
							padding = vbtStartOffset + vtbHeader.bdb_offset;
							FirmwareImage.bdb_header bdbHeader = reader.BytesToStructure<FirmwareImage.bdb_header>(ref padding);
						}
					}
					#endregion Firmware parsing test (Failed)
				}
			}

			Console.WriteLine(">>>Dump path: {0}", Environment.CurrentDirectory);

			foreach(String filePath in Directory.GetFiles(Environment.CurrentDirectory, "*_smb.sfw", SearchOption.TopDirectoryOnly))
			{
				Console.WriteLine("Reading SMBIOS: {0}", filePath);
				FirmwareT<FirmwareSmBios> smbios1 = new FirmwareT<FirmwareSmBios>(File.ReadAllBytes(filePath));
				FirmwareSmBios smbios2 = smbios1.GetData().Single();

				foreach(TypeBase type in smbios2.Types)
				{
					switch(type.Header.Type)
					{
					case SmBios.Type.Bios:
						Bios bios = (Bios)type;
						Console.WriteLine("BIOS Vendor:\t{0}", bios.Vendor);
						Console.WriteLine("BIOS Version:\t{0}", bios.Version);
						Console.WriteLine("Release Date:\t{0}", bios.ReleaseDate);
						Console.WriteLine("ROM Size:\t{0}", bios.RomSize);
						Console.WriteLine("Characteristics:\t{0}", bios.Type.Characteristics);
						foreach(SmBios.Type0.BiosCharacteristics flag in Enum.GetValues(typeof(SmBios.Type0.BiosCharacteristics)))
						{
							Console.WriteLine("\t{0}:\t{1}", flag, Utils.IsFlagSet((Int64)bios.Type.Characteristics, (Int64)flag) ? "YES" : "NO");
						}
						break;
					case SmBios.Type.System:
						SystemType system = (SystemType)type;
						Console.WriteLine("ProductName: {0} {1} (v{2})", system.Manufacturer, system.ProductName, system.Version);
						Console.WriteLine("SerialNumber: {0}", system.SerialNumber);
						Console.WriteLine("SKU Number: {0}", system.SKUNumber);
						Console.WriteLine("Family: {0}", system.Family);
						break;
					case SmBios.Type.Baseboard:
						Baseboard baseboard = (Baseboard)type;
						Console.WriteLine("ProductName: {0} {1} (v{2})", baseboard.Manufacturer, baseboard.Product, baseboard.Version);
						Console.WriteLine("SerialNumber: {0}", baseboard.SerialNumber);
						Console.WriteLine("Assert Tag: {0}", baseboard.AssetTag);
						Console.WriteLine("Location in Chassis: {0}", baseboard.LocationInChassis);
						break;
					case SmBios.Type.Chassis:
						Chassis type3 = (Chassis)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type);
						Utils.ConsoleWriteMembers(type3.Type);
						break;
					case SmBios.Type.Processor:
						Processor type4 = (Processor)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type);
						Utils.ConsoleWriteMembers(type4.Type);
						break;
					case SmBios.Type.MemoryController:
						MemoryController type5 = (MemoryController)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type);
						Utils.ConsoleWriteMembers(type5.Type);
						break;
					case SmBios.Type.Cache:
						Cache type7 = (Cache)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type);
						Utils.ConsoleWriteMembers(type7.Type);
						break;
					case SmBios.Type.PortConnector:
						PortConnector type8 = (PortConnector)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type);
						Utils.ConsoleWriteMembers(type8.Type);
						if(type8.Type.Port == SmBios.Type8.PortType.USB && type8.InternalReferenceDesignator.StartsWith("USB_3"))
							Console.WriteLine("USB 3.0 port found");
						break;
					case SmBios.Type.SystemSlots:
						SystemSlots type9 = (SystemSlots)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type9);
						Utils.ConsoleWriteMembers(type9.Type);
						break;
					case SmBios.Type.OnBoardDevices:
						OnBoardDevices type10 = (OnBoardDevices)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type10);
						foreach(var deviceInfo in type10.GetDevices())
							Utils.ConsoleWriteMembers(deviceInfo);

						Utils.ConsoleWriteMembers(type10.Type);
						break;
					case SmBios.Type.OEMStrings:
						OEMStrings type11 = (OEMStrings)type;
						Console.WriteLine(String.Join("; ", type11.OEM));
						break;
					case SmBios.Type.SystemConfigurationOptions:
						SystemConfigurationOptions type12 = (SystemConfigurationOptions)type;
						Console.WriteLine(String.Join("; ", type12.Options));
						break;
					case SmBios.Type.BiosLanguage:
						BiosLanguage type13 = (BiosLanguage)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type13);
						Utils.ConsoleWriteMembers(type13.Type);
						break;
					case SmBios.Type.GroupAssociations:
						GroupAssociations type14 = (GroupAssociations)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type14);
						Utils.ConsoleWriteMembers(type14.Type);
						break;
					case SmBios.Type.MemoryDevice:
						MemoryDevice type17 = (MemoryDevice)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type17);
						Utils.ConsoleWriteMembers(type17.Type);
						break;
					case SmBios.Type.PortableBattery:
						PortableBattery type22 = (PortableBattery)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type22);
						Utils.ConsoleWriteMembers(type22.Type);
						break;
					case SmBios.Type.VoltageProbe:
						VoltageProbe type26 = (VoltageProbe)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type26);
						Utils.ConsoleWriteMembers(type26.Type);
						break;
					case SmBios.Type.CoolingDevice:
						CoolingDevice type27 = (CoolingDevice)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type27);
						Utils.ConsoleWriteMembers(type27.Type);
						break;
					case SmBios.Type.TemperatureProbe:
						TemperatureProbe type28 = (TemperatureProbe)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type28);
						Utils.ConsoleWriteMembers(type28.Type);
						break;
					case SmBios.Type.ElectricalCurrentProbe:
						ElectricalCurrentProbe type29 = (ElectricalCurrentProbe)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type29);
						Utils.ConsoleWriteMembers(type29.Type);
						break;
					case SmBios.Type.ManagementDevice:
						ManagementDevice type34 = (ManagementDevice)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type34);
						Utils.ConsoleWriteMembers(type34.Type);
						break;
					case SmBios.Type.ManagementDeviceComponent:
						ManagementDeviceComponent type35 = (ManagementDeviceComponent)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type35);
						Utils.ConsoleWriteMembers(type35.Type);
						break;
					case SmBios.Type.SystemPowerSupply:
						SystemPowerSupply type39 = (SystemPowerSupply)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type39);
						Utils.ConsoleWriteMembers(type39.Type);
						break;
					case SmBios.Type.AdditionalInformation:
						AdditionalInformation type40 = (AdditionalInformation)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type40);
						foreach(var information in type40.GetAdditionalInformation())
							Utils.ConsoleWriteMembers(information.String, information.Type);
						break;
					case SmBios.Type.OnboardDevicesExtended:
						OnboardDevicesExtended type41 = (OnboardDevicesExtended)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type41);
						Utils.ConsoleWriteMembers(type41.Type);
						break;
					case SmBios.Type.TpmDevice:
						TpmDevice type43 = (TpmDevice)type;
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type43);
						Utils.ConsoleWriteMembers(type43.Type);
						break;
					default:
#if DEBUG
						if(type.Strings != null && type.Strings.Length > 0)
						{
							if(type.Header.Type == SmBios.Type.HardwareSecurity && type.Strings[0] == "?3")
							{//Неизвестная хрень...

							} else
							{
								if(Enum.IsDefined(typeof(SmBios.Type), type.Header.Type))
									throw new NotImplementedException();
							}
						}
#endif
						Utils.ConsoleWriteMembers(type.Header.Type.ToString(), type);
						Console.WriteLine();
						break;

					}
				}
			}

			sw.Stop();
			Console.WriteLine("Elapsed: {0}",sw.Elapsed);
#if !DEBUG
			Console.ReadKey();
#endif
			//Int32 z= 32 >> 4 & 3;
		}
	}
}