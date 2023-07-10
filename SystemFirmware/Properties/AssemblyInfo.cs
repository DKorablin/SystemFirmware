﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: Guid("1748f224-a790-45df-8fd8-416a4c78244f")]

[assembly: System.CLSCompliant(false)]

#if !NETSTANDARD
[assembly: AssemblyProduct("SystemFirmware")]
[assembly: AssemblyTitle("SystemFirmware")]
[assembly: AssemblyDescription("Native GetSystemFirmware wrapper assembly")]
[assembly: AssemblyCompany("Danila Korablin")]
[assembly: AssemblyCopyright("Copyright © Danila Korablin 2021")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
#endif
[assembly: InternalsVisibleTo("Demo, PublicKey=00240000048000009400000006020000002400005253413100040000010001007f874ea8cb98c26edd475387c0d4cbe7cab7a29881ef155e739f5978320165dc9049f45345f471bf340b9abe38510cb3624cd371e50c573424ed2b8f723b2ad2a1ae86b2817cbcec6716c38fc0117bf90e5ff4d28c79e73887f6b5f9aafe6a5a1e12b655e0d57e2b3cee5050e99c71737f8975ae1cbfb1b34aed4644c398789b")]