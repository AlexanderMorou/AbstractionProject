using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("AllenCopeland.Abstraction.Utilities")]
[assembly: AssemblyDescription("The Abstraction Project Common Utilities")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("AllenCopeland.Abstraction")]
[assembly: AssemblyCopyright("Copyright © Allen C. Copeland Jr. 2008-2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(false)]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("5fdb79a9-d0a7-4ea9-92bd-5570cb0d5621")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
#if DEBUG
        [assembly: InternalsVisibleTo("_abs.slf.typesystems.cli")]
   [assembly: InternalsVisibleTo("_abs.slf.typesystems.abstract")]
                    [assembly: InternalsVisibleTo("_abs.slf.ast")]
                [assembly: InternalsVisibleTo("_abs.slf.cli.ast")]
      [assembly: InternalsVisibleTo("_abs.slf.languages.oilexer")]
       [assembly: InternalsVisibleTo("_abs.slf.languages.vb.net")]
             [assembly: InternalsVisibleTo("Abstraction.BugTest")]
              [assembly: InternalsVisibleTo("_abs.util.automata")]
                         [assembly: InternalsVisibleTo("Oilexer")]
        [assembly: InternalsVisibleTo("CliMetadataReaderBuilder")]
     [assembly: InternalsVisibleTo("_abs.supplementary.cts.test")]
       [assembly: InternalsVisibleTo("_abs.slf.languages.csharp")]
        [assembly: InternalsVisibleTo("_abs.slf.typesystems.mva")]
#else
[assembly: InternalsVisibleTo("_abs.slf.typesystems.cli, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
   [assembly: InternalsVisibleTo("_abs.slf.typesystems.abstract, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
                [assembly: InternalsVisibleTo("_abs.slf.cli.ast, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
                    [assembly: InternalsVisibleTo("_abs.slf.ast, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
      [assembly: InternalsVisibleTo("_abs.slf.languages.oilexer, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
       [assembly: InternalsVisibleTo("_abs.slf.languages.vb.net, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
             [assembly: InternalsVisibleTo("Abstraction.BugTest, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
              [assembly: InternalsVisibleTo("_abs.util.automata, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
                         [assembly: InternalsVisibleTo("Oilexer, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
        [assembly: InternalsVisibleTo("CliMetadataReaderBuilder, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
     [assembly: InternalsVisibleTo("_abs.supplementary.cts.test, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
       [assembly: InternalsVisibleTo("_abs.slf.languages.csharp, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
        [assembly: InternalsVisibleTo("_abs.slf.typesystems.mva, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
#endif
