# SystemFirmware
Managed wrapper Win32 API functions: GetSystemFirmwareTable and EnumSystemFirmwareTables

Usage:
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
