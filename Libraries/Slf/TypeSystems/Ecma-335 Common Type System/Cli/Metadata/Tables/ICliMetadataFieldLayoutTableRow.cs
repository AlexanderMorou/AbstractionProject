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

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    
    // Module: RootModule
    /// <summary>
    /// Defines properties and methods for row in a table which defines the
    /// layout of the fields on an <see cref="LayoutKind.Explicit"/> layout type.
    /// </summary>
    public interface ICliMetadataFieldLayoutTableRow :
        ICliMetadataTableRow
    {
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="ICliMetadataFieldLayoutTableRow"/>
        /// was derived.
        /// </summary>
        ICliMetadataRoot MetadataRoot { get; }
        
        uint Offset { get; }
        
        /// <summary>
        /// Returns the <see cref="UInt32"/> value which represents the field for which the
        /// layout exists.
        /// </summary>
        uint FieldIndex { get; }
    }
}
