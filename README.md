# SystemFirmware

[![Auto build](https://github.com/DKorablin/SystemFirmware/actions/workflows/release.yml/badge.svg)](https://github.com/DKorablin/SystemFirmware/releases/latest)
[![NuGet](https://img.shields.io/nuget/v/AlphaOmega.SystemFirmware)](https://www.nuget.org/packages/AlphaOmega.SystemFirmware)
[![NuGet Downloads](https://img.shields.io/nuget/dt/AlphaOmega.SystemFirmware)](https://www.nuget.org/packages/AlphaOmega.SystemFirmware)

Managed .NET wrapper for the Win32 API functions:
- [GetSystemFirmwareTable](https://learn.microsoft.com/windows/win32/api/sysinfoapi/nf-sysinfoapi-getsystemfirmwaretable)
- [EnumSystemFirmwareTables](https://learn.microsoft.com/windows/win32/api/sysinfoapi/nf-sysinfoapi-enumsystemfirmwaretables)

Provides strongly typed access to SMBIOS firmware structures.

## Features
- Enumerate and parse SMBIOS tables via native Win32 APIs
- Strongly typed model for supported SMBIOS structures
- Save and load serialized firmware snapshots (*.sfw*)
- Multi-targeted library (.NET Framework 3.5, .NET Standard 2.0, .NET 8)
- Simple discovery helpers (generic retrieval per type)

## Installation
NuGet:
```powershell
Install-Package AlphaOmega.SystemFirmware
```
Or with .NET CLI:
```bash
 dotnet add package AlphaOmega.SystemFirmware
```

## Quick Start
Read baseboard (Type 2) and optionally locate USB 3.0 port connectors (Type 8):
```csharp
using System;
using System.Linq;
using AlphaOmega.Debug;
using AlphaOmega.Debug.Native;
using AlphaOmega.Debug.Smb;

class Program
{
    static void Main()
    {
        FirmwareT<FirmwareSmBios> fw = new FirmwareT<FirmwareSmBios>();
        FirmwareSmBios smbios = fw.GetData().Single();

        // Motherboard vendor and name
        Baseboard baseboard = smbios.GetType<Baseboard>();
        Console.WriteLine($"Motherboard Manufacturer: {baseboard.Manufacturer} Name: {baseboard.Product}");

        // Find USB 3.0 ports (example logic)
        foreach (TypeBase type in smbios.Types)
            switch (type.Header.Type)
            {
                case SmBios.Type.PortConnector:
                    PortConnector port = (PortConnector)type;
                    if (port.Type.Port == SmBios.Type8.PortType.USB && port.InternalReferenceDesignator.StartsWith("USB_3", StringComparison.OrdinalIgnoreCase))
                        Console.WriteLine("USB 3.0 port found");
                    break;
            }
    }
}
```

## Enumerating All SMBIOS Structures
```csharp
FirmwareT<FirmwareSmBios> fw = new FirmwareT<FirmwareSmBios>();
FirmwareSmBios smbios = fw.GetData().Single();

foreach (TypeBase type in smbios.Types)
{
    Console.WriteLine($"Type {type.Header.Type} Length: {type.Header.Length}");
    // Cast based on type.Header.Type if further detail is needed
}
```

## Saving & Loading Snapshot
Persist a firmware snapshot to reuse later or for offline analysis.
```csharp
// Save
FirmwareT<FirmwareSmBios> fw = new FirmwareT<FirmwareSmBios>();
byte[] data = fw.Save();
System.IO.File.WriteAllBytes(@"C:\Temp\Firmware\smbios.sfw", data);

// Load
try
{
    FirmwareT<FirmwareSmBios> loaded = new FirmwareT<FirmwareSmBios>(System.IO.File.ReadAllBytes(@"C:\Temp\Firmware\smbios.sfw"));
}
catch (ArgumentNullException)
{
    // Empty file
}
catch (ArgumentException)
{
    // Invalid signature or wrong firmware type (e.g. ACPI data passed)
}
```

## Supported SMBIOS Types
| Type ID | Name | Status |
|---------|------|--------|
| 0 | BIOS Information | Supported |
| 1 | System Information | Supported |
| 2 | Baseboard (Module) Information | Supported |
| 3 | System Enclosure or Chassis | Supported |
| 4 | Processor Information | Supported |
| 5 | Memory Controller Information | Obsolete |
| 6 | Memory Module Information | Obsolete |
| 7 | Cache Information | Supported |
| 8 | Port Connector Information | Supported |
| 9 | System Slots | Supported |
| 10 | On Board Devices Information | Obsolete |
| 11 | OEM Strings | Supported |
| 12 | System Configuration Options | Supported |
| 13 | BIOS Language Information | Supported |
| 14 | Group Associations | Supported |
| 15 | System Event Log | Not supported |
| 16 | Physical Memory Array | Not supported |
| 17 | Memory Device | Supported |
| 18 | 32-Bit Memory Error Information | Not supported |
| 19 | Memory Array Mapped Address | Not supported |
| 20 | Memory Device Mapped Address | Not supported |
| 21 | Built-in Pointing Device | Not supported |
| 22 | Portable Battery | Not supported |
| 23 | System Reset | Not supported |
| 24 | Hardware Security | Not supported |
| 25 | System Power Controls | Not supported |
| 26 | Voltage Probe | Supported |
| 27 | Cooling Device | Supported |
| 28 | Temperature Probe | Supported |
| 29 | Electrical Current Probe | Supported |
| 30 | Out-of-Band Remote Access | Not supported |
| 31 | Boot Integrity Services (BIS) Entry Point | Not supported |
| 32 | System Boot Information | Not supported |
| 33 | 64-Bit Memory Error Information | Not supported |
| 34 | Management Device | Supported |
| 35 | Management Device Component | Supported |
| 36 | Management Device Threshold Data | Not supported |
| 37 | Memory Channel | Not supported |
| 38 | IPMI Device Information | Not supported |
| 39 | System Power Supply | Supported |
| 40 | Additional Information | Supported |
| 41 | Onboard Devices Extended Information | Supported |
| 42 | Management Controller Host Interface | Not supported |
| 43 | TPM Device | Supported |
| 44 | Processor Additional Information | Not supported |

## Notes & Limitations
- Not supported types are skipped but header metadata remains accessible.
- Some obsolete types are retained for backward compatibility; content may be limited.
- Requires Windows for native firmware table enumeration.
- Snapshot (*.sfw*) is a custom binary format (signature validated on load).