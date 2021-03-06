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
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    
    // Module: RootModule
    /// <summary>
    /// Defines properties and methods for row in a table which defines information
    /// about the image's fields.
    /// </summary>
    public interface ICliMetadataFieldTableRow :
        ICliMetadataTableRow,
        ICliMetadataHasConstantRow,
        ICliMetadataHasCustomAttributeRow,
        ICliMetadataHasFieldMarshalRow,
        ICliMetadataMemberForwardedRow,
        ICliMetadataIndexedRow
    {
        /// <summary>
        /// Returns conditional information about the field and its accessibility.
        /// </summary>
        FieldAttributes FieldAttributes { get; }
        
        /// <summary>
        /// Returns the type of the field, in signature form.
        /// </summary>
        ICliMetadataFieldSignature FieldType { get; }
        
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.BlobHeap"/> from which
        /// <see cref="FieldType"/> is derived.
        /// </summary>
        uint FieldTypeIndex { get; }
        
        /// <summary>
        /// Returns the layout of the field which determines the byte offset of
        /// the field relative to the structure which contains it.
        /// </summary>
        /// <remarks>
        /// Can be null.
        /// </remarks>
        ICliMetadataFieldLayoutTableRow Layout { get; }
        
        /// <summary>
        /// Returns the relative virtual address for the field.
        /// </summary>
        /// <remarks>
        /// Usually null except for initialized and uninitialized '.data' fields
        /// which store sequential bytes of data within the application's memory
        /// space.  The data-types of such fields must have no private fields of
        /// their own and contain no reference type fields as they point into the
        /// GC Heap.
        /// </remarks>
        ICliMetadataFieldRVATableRow RVA { get; }
        
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="ICliMetadataFieldTableRow"/>
        /// was derived.
        /// </summary>
        new ICliMetadataRoot MetadataRoot { get; }
        
        /// <summary>
        /// Returns the name of the field.
        /// </summary>
        new string Name { get; }
        
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.StringsHeap"/> from which
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
