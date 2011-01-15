using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Abstract Syntax Tree core for the Scripting Language Foundation")]
[assembly: AssemblyDescription("Provides an Abstract Syntax Tree which defines the core functionality of the type system represented by the Scripting Language Foundation.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("None")]
[assembly: AssemblyProduct("Abstraction - Scripting Language Foundation - Abstract Syntax Tree")]
[assembly: AssemblyCopyright("Copyright © Allen Copeland Jr. 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

//[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("AED22292-368F-4a72-AD63-198801C985AE")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: InternalsVisibleTo("_abs.slf.transformation.abstract, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
   [assembly: InternalsVisibleTo("_abs.slf.translation.abstract, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
                [assembly: InternalsVisibleTo("_abs.slf.ast.oil, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
        [assembly: InternalsVisibleTo("_abs.slf.typesystems.cli, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
     [assembly: InternalsVisibleTo("_abs.slf.compilers.abstract, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
     [assembly: InternalsVisibleTo("_abs.slf.languages.abstract, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
       [assembly: InternalsVisibleTo("_abs.slf.languages.csharp, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
      [assembly: InternalsVisibleTo("_abs.slf.languages.oilexer, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
          [assembly: InternalsVisibleTo("_abs.slf.languages.cil, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
  [assembly: InternalsVisibleTo("_abs.slf.languages.visualbasic, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
             [assembly: InternalsVisibleTo("Abstraction.BugTest, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
                         [assembly: InternalsVisibleTo("Oilexer, PublicKey=002400000480000094000000060200000024000052534131000400000100010009e0756ea3c80f41287e7a2b53118c84bd8c573361b8a085268d639dbb675c68adec8c1f5d750cbf75635bdae6a15635762b759daba1cdcc1439a8ab5288a0d204483205a214786c5a23e68c1f0ad76efc12061a3715a280d1ae5ee1e732a9064e826e6396fe4ba5ad62f44500dab91ef574b9e51ebe8fdd04cd356658d69cbe")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Scope = "namespace", Target = "AllenCopeland.Abstraction.Slf", MessageId = "Slf")]
[module: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "typesystems")]
[assembly: ComVisibleAttribute(false)]
