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
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    
    // Module: RootModule
    /// <summary>
    /// Defines the umbrella interface for reference indexes encoded with <see cref="CliMetadataHasDeclSecurityTag"/>.
    /// </summary>
    public interface ICliMetadataHasDeclSecurityRow
    {
        /// <summary>
        /// Returns the <see cref="ICliMetadataRoot"/> metadataRoot associated to the row
        /// encoded with <see cref="CliMetadataHasDeclSecurityTag"/>.
        /// </summary>
        ICliMetadataRoot MetadataRoot { get; }
        
        /// <summary>
        /// Returns the <see cref="System.String"/> name associated to the row encoded with
        /// <see cref="CliMetadataHasDeclSecurityTag"/>.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Returns the <see cref="System.UInt32"/> nameIndex associated to the row encoded
        /// with <see cref="CliMetadataHasDeclSecurityTag"/>.
        /// </summary>
        uint NameIndex { get; }
        
        /// <summary>
        /// Returns the <see cref="IControlledCollection{T}"/> customAttributes associated
        /// to the row encoded with <see cref="CliMetadataHasDeclSecurityTag"/>.
        /// </summary>
        IControlledCollection<ICliMetadataCustomAttributeTableRow> CustomAttributes { get; }
        
        /// <summary>
        /// Returns the source table from which the current <see cref="ICliMetadataHasDeclSecurityRow"/>
        /// is derived.
        /// </summary>
        CliMetadataHasDeclSecurityTag HasDeclSecurityEncoding { get; }
    }
}
