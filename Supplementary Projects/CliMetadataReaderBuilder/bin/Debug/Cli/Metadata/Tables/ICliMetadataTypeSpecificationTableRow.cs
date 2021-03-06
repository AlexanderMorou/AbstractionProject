 /* ----------------------------------------------------------\
 |  This code was generated by Allen Copeland's Abstraction.  |
 |  Version: 0.5.0.0                                          |
 |------------------------------------------------------------|
 |  To ensure the code works properly,                        |
 |  please do not make any changes to the file.               |
 |------------------------------------------------------------|
 |  The specific language is C♯                               |
 |  Sub-tool Name: C♯ Code Translator                         |
 |  Sub-tool Version: 1.0.0.0                                 |
 \---------------------------------------------------------- */
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    /// <summary>
    /// Defines properties and methods for a locked row in a table which defines information
    /// about a type specification.
    /// </summary>
    public interface ICliMetadataTypeSpecificationTableRow :
        ICliMetadataTableRow, 
        ICliMetadataTypeDefOrRefRow, 
        ICliMetadataHasCustomAttributeRow, 
        ICliMetadataMemberRefParentRow, 
        ICliMetadataIndexedRow
    {
        /// <summary>
        /// Returns the signature of the type specification.
        /// </summary>
        ICliMetadataTypeSpecSignature Signature { get; }
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.BlobHeap"/> from which <see cref="Signature"/>
        /// is derived.
        /// </summary>
        uint SignatureIndex { get; }
        ICliMetadataRoot MetadataRoot { get; }
        IControlledCollection<ICliMetadataCustomAttributeTableRow> CustomAttributes { get; }
    };
};
