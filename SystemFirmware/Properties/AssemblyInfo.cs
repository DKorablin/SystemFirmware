using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("SystemFirmware")]
[assembly: AssemblyDescription("Native GetSystemFirmware wrapper assembly")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Danila Korablin")]
[assembly: AssemblyProduct("SystemFirmware")]
[assembly: AssemblyCopyright("Copyright © Danila Korablin 2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("1748f224-a790-45df-8fd8-416a4c78244f")]

[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: System.CLSCompliant(false)]
[assembly: InternalsVisibleTo("Demo")]