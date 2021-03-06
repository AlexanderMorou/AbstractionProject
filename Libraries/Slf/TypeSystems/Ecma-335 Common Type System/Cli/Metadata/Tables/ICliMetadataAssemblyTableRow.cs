 /* -----------------------------------------------------------\
 |  This code was generated by Allen Copeland's Abstraction.   |
 |  Version: 1.0.0.14276                                       |
 |-------------------------------------------------------------|
 |  To ensure the code works properly,                         |
 |  please do not make any changes to the file.                |
 |-------------------------------------------------------------|
 |  The specific language is C# (Runtime version: v4.0.30319)  |
 |  Sub-tool Name: Abstraction's Old C♯ Code Translator        |
 |  Sub-tool Version: 1.0.0.14276                              |
 \----------------------------------------------------------- */
using System;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    
    // Module: RootModule
    /// <summary>
    /// Defines properties and methods for row in a table which defines the
    /// manifest of an assembly.
    /// </summary>
    public interface ICliMetadataAssemblyTableRow :
        ICliMetadataTableRow,
        ICliMetadataHasCustomAttributeRow,
        ICliMetadataHasDeclSecurityRow,
        ICliMetadataIndexedRow
    {
        AssemblyHashAlgorithm HashAlgorithmId { get; }
        
        QWordLongVersion Version { get; }
        
        CliMetadataAssemblyFlags Flags { get; }
        
        byte[] PublicKey { get; }
        
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.BlobHeap"/> from which
        /// <see cref="PublicKey"/> is derived.
        /// </summary>
        uint PublicKeyIndex { get; }
        
        string Culture { get; }
        
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.StringsHeap"/> from which
        /// <see cref="Culture"/> is derived.
        /// </summary>
        uint CultureIndex { get; }
    }
}
