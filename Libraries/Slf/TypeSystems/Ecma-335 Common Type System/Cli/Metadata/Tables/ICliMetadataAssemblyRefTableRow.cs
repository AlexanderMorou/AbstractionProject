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
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    
    // Module: RootModule
    /// <summary>
    /// Defines properties and methods for row in a table which defines the
    /// assembly references of a module through its manifest.
    /// </summary>
    public interface ICliMetadataAssemblyRefTableRow :
        ICliMetadataTableRow,
        ICliMetadataHasCustomAttributeRow,
        ICliMetadataImplementationRow,
        ICliMetadataResolutionScopeRow,
        ICliMetadataIndexedRow
    {
        QWordLongVersion Version { get; }
        
        CliMetadataAssemblyFlags Flags { get; }
        
        byte[] PublicKeyOrToken { get; }
        
        /// <summary>
        /// Returns the index onto the <see cref="ICliMetadataRoot.BlobHeap"/> from which
        /// <see cref="PublicKeyOrToken"/> is derived.
        /// </summary>
        uint PublicKeyOrTokenIndex { get; }
        
        string Culture { get; }
        
        /// <summary>
        /// Returns the index onto the <see cref="ICliMetadataRoot.StringsHeap"/> from which
        /// <see cref="Culture"/> is derived.
        /// </summary>
        uint CultureIndex { get; }
        
        byte[] HashValue { get; }
        
        /// <summary>
        /// Returns the index onto the <see cref="ICliMetadataRoot.BlobHeap"/> from which
        /// <see cref="HashValue"/> is derived.
        /// </summary>
        uint HashValueIndex { get; }
        
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="ICliMetadataAssemblyRefTableRow"/>
        /// was derived.
        /// </summary>
        new ICliMetadataRoot MetadataRoot { get; }
        
        new string Name { get; }
        
        /// <summary>
        /// Returns the index onto the <see cref="ICliMetadataRoot.StringsHeap"/> from which
        /// <see cref="Name"/> is derived.
        /// </summary>
        new uint NameIndex { get; }
        
        /// <summary>
        /// Returns the set of custom metadata elements applied to the member.
        /// </summary>
        /// <remarks>
        /// Created through references from the <see cref="ICliMetadataCustomAttributeTable"/>.
        /// </remarks>
        new IControlledCollection<ICliMetadataCustomAttributeTableRow> CustomAttributes { get; }
    }
}
