# SystemFirmware

[![Nuget](https://img.shields.io/nuget/v/AlphaOmega.SystemFirmware)](https://www.nuget.org/packages/AlphaOmega.SystemFirmware)

Managed wrapper Win32 API functions: [GetSystemFirmwareTable](https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-getsystemfirmwaretable) and [EnumSystemFirmwareTables](https://learn.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-enumsystemfirmwaretables)

Supported SMBIOS Types:
- BIOS Information (Type 0)
- System Information (Type 1)
- Baseboard (or Module) Information (Type 2)
- System Enclosure or Chassis (Type 3)
- Processor Information (Type 4)
- Memory Controller Information (Type 5, Obsolete)
- Memory Module Information (Type 6, Obsolete)
- Cache Information (Type 7)
- Port Connector Information (Type 8)
- System Slots (Type 9)
- On Board Devices Information (Type 10, Obsolete)
- OEM Strings (Type 11)
- System Configuration Options (Type 12)
- BIOS Language Information (Type 13)
- Group Associations (Type 14)
- System Event Log (Type 15) _[Not supported]_
- Physical Memory Array (Type 16) _[Not supported]_
- Memory Device (Type 17)
- 32-Bit Memory Error Information (Type 18) _[Not supported]_
- Memory Array Mapped Address (Type 19) _[Not supported]_
- Memory Device Mapped Address (Type 20) _[Not supported]_
- Built-in Pointing Device (Type 21) _[Not supported]_
- Portable Battery (Type 22) _[Not supported]_
- System Reset (Type 23) _[Not supported]_
- Hardware Security (Type 24) _[Not supported]_
- System Power Controls (Type 25) _[Not supported]_
- Voltage Probe (Type 26)
- Cooling Device (Type 27)
- Temperature Probe (Type 28)
- Electrical Current Probe (Type 29)
- Out-of-Band Remote Access (Type 30) _[Not supported]_
- Boot Integrity Services (BIS) Entry Point (Type 31) _[Not supported]_
- System Boot Information (Type 32) _[Not supported]_
- 64-Bit Memory Error Information (Type 33) _[Not supported]_
- Management Device (Type 34)
- Management Device Component (Type 35)
- Management Device Threshold Data (Type 36) _[Not supported]_
- Memory Channel (Type 37) _[Not supported]_
- IPMI Device Information (Type 38) _[Not supported]_
- System Power Supply (Type 39)
- Additional Information (Type 40)
- Onboard Devices Extended Information (Type 41)
- Management Controller Host Interface (Type 42) _[Not supported]_
- TPM Device (Type 43) _[Not supported]_
- Processor Additional Information (Type 44) _[Not supported]_

Example how to read SMBIOS Type 2 (Baseboard) and Type 8 (Port Connector) information:

    using System;
    using System.Linq;
    using AlphaOmega.Debug;
    using AlphaOmega.Debug.Native;
    using AlphaOmega.Debug.Smb;

    class Program
    {
        static void Main(String[] args)
        {
            FirmwareT<FirmwareSmBios> fw = new FirmwareT<FirmwareSmBios>();
            FirmwareSmBios smbios = fw.GetData().Single();

            //Motherboard vendor and name
            Baseboard baseboard = smbios.GetType<Baseboard>();
            Console.WriteLine("Motherboard Manufacturer: {0} Name: {1}",baseboard.Manufacturer,baseboard.Product);

            //Try to find USB 3.0 (Not tested. It's a sample)
            foreach(TypeBase type in smbios.Types)
                switch(type.Header.Type)
                {
                case SmBios.Type.PortConnector:
                    PortConnector type8 = (PortConnector)type;
                    if(type8.Type.Port == SmBios.Type8.PortType.USB && type8.InternalReferenceDesignator.StartsWith("USB_3"))
                        Console.WriteLine("USB 3.0 port found");
                break;
                }
        }
    }

Example how to save SMBIOS information:

    FirmwareT<FirmwareSmBios> fw = new FirmwareT<FirmwareSmBios>();
    System.IO.File.WriteAllBytes(@"C:\Temp\Firmware\smbios.sfw", fw.Save());

Example how to load SMBIOS infromation from file:

    try{
        FirmwareT<FirmwareSmBios> smbios = new FirmwareT<FirmwareSmBios>(System.IO.File.ReadAllBytes(@"C:\Temp\Firmware\smbios.sfw"));
    } catch(ArgumentNullException)//Empty file
    {
        ...
    } catch(ArgumentException)//Invalid signature or invalid firmware type (Ex. loading ACPI data)
    {
    ...
    }