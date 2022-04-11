# SystemFirmware
Managed wrapper Win32 API functions: GetSystemFirmwareTable and EnumSystemFirmwareTables
Supported SMBIOS Types:
<ul>
	<li>BIOS Information (Type 0)</li>
	<li>System Information (Type 1)</li>
	<li>Baseboard (or Module) Information (Type 2)</li>
	<li>System Enclosure or Chassis (Type 3)</li>
	<li>Processor Information (Type 4)</li>
	<li>Memory Controller Information (Type 5, Obsolete)</li>
	<li>Memory Module Information (Type 6, Obsolete)</li>
	<li>Cache Information (Type 7)</li>
	<li>Port Connector Information (Type 8)</li>
	<li>System Slots (Type 9)</li>
	<li>On Board Devices Information (Type 10, Obsolete)</li>
	<li>OEM Strings (Type 11)</li>
	<li>System Configuration Options (Type 12)</li>
	<li>BIOS Language Information (Type 13)</li>
	<li>Group Associations (Type 14)</li>
	<li>System Event Log (Type 15) <i>[Not supported]</i></li>
	<li>Physical Memory Array (Type 16) <i>[Not supported]</i></li>
	<li>Memory Device (Type 17)</li>
	<li>32-Bit Memory Error Information (Type 18) <i>[Not supported]</i></li>
	<li>Memory Array Mapped Address (Type 19) <i>[Not supported]</i></li>
	<li>Memory Device Mapped Address (Type 20) <i>[Not supported]</i></li>
	<li>Built-in Pointing Device (Type 21) <i>[Not supported]</i></li>
	<li>Portable Battery (Type 22) <i>[Not supported]</i></li>
	<li>System Reset (Type 23) <i>[Not supported]</i></li>
	<li>Hardware Security (Type 24) <i>[Not supported]</i></li>
	<li>System Power Controls (Type 25) <i>[Not supported]</i></li>
	<li>Voltage Probe (Type 26)</li>
	<li>Cooling Device (Type 27)</li>
	<li>Temperature Probe (Type 28)</li>
	<li>Electrical Current Probe (Type 29)</li>
	<li> Out-of-Band Remote Access (Type 30) <i>[Not supported]</i></li>
	<li>Boot Integrity Services (BIS) Entry Point (Type 31) <i>[Not supported]</i></li>
	<li>System Boot Information (Type 32) <i>[Not supported]</i></li>
	<li>64-Bit Memory Error Information (Type 33) <i>[Not supported]</i></li>
	<li>Management Device (Type 34)</li>
	<li>Management Device Component (Type 35)</li>
	<li>Management Device Threshold Data (Type 36) <i>[Not supported]</i></li>
	<li>Memory Channel (Type 37) <i>[Not supported]</i></li>
	<li>IPMI Device Information (Type 38) <i>[Not supported]</i></li>
	<li>System Power Supply (Type 39)</li>
	<li>Additional Information (Type 40)</li>
	<li>Onboard Devices Extended Information (Type 41)</li>
	<li>Management Controller Host Interface (Type 42) <i>[Not supported]</i></li>
	<li>TPM Device (Type 43) <i>[Not supported]</i></li>
	<li>Processor Additional Information (Type 44) <i>[Not supported]</i></li>
</ul>

Example how to read SMBIOS Type 2 (Baseboard) and Type 8 (Port Connector) information:
<pre>
using System;
using System.Linq;
using using AlphaOmega.Debug;
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
</pre>

Example how to save SMBIOS information:
<pre>
FirmwareT<FirmwareSmBios> fw = new FirmwareT<FirmwareSmBios>();
System.IO.File.WriteAllBytes(@"C:\Temp\Firmware\smbios.sfw", fw.Save());
</pre>

Example how to load SMBIOS infromation from file:
<pre>
try{
	FirmwareT<FirmwareSmBios> smbios = new FirmwareT<FirmwareSmBios>(System.IO.File.ReadAllBytes(@"C:\Temp\Firmware\smbios.sfw"));
} catch(ArgumentNullException)//Empty file
{
...
} catch(ArgumentException)//Invalid signature or invalid firmware type (Ex. loading ACPI data)
{
...
}
</pre>
