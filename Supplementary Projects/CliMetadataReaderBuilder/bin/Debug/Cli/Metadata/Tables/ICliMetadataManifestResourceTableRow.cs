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
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    public interface ICliMetadataManifestResourceTableRow :
        ICliMetadataTableRow, 
        ICliMetadataHasCustomAttributeRow, 
        ICliMetadataIndexedRow
    {
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="ICliMetadataManifestResourceTableRow"/>
        /// was derived.
        /// </summary>
        ICliMetadataRoot MetadataRoot { get; }
        uint Offset { get; }
        uint Flags { get; }
        string Name { get; }
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.StringsHeap"/> from which <see cref="Name"/>
        /// is derived.
        /// </summary>
        uint NameIndex { get; }
        ICliMetadataImplementationRow Implementation { get; }
        /// <summary>
        /// Returns the decoded index of the <see cref="Implementation"/> relative to the appropriate
        /// table.
        /// </summary>
        /// <remarks>
        /// Refer to <see cref="ImplementationSource"/> to discern the proper table for <see cref="ImplementationIndex"/>
        /// </remarks>
        uint ImplementationIndex { get; }
        /// <summary>
        /// Returns the <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataImplementationTag"/>
        /// which determines the table that <see cref="ImplementationIndex"/> refers to.
        /// </summary>
        /// <remarks>
        /// <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataImplementationTag"/>
        /// encoding <see cref="CliMetadataTableStreamAndHeader"/> tables:<list type="table"><listheader><term>Encoding</term><description>TableStream
        /// Property</description></listheader><item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataImplementationTag.AssemblyReference"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.AssemblyRefTable"/></description></item>
        /// <item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataImplementationTag.ExportedType"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.ExportedTypeTable"/></description></item>
        /// <item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataImplementationTag.File"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.FileTable"/></description></item>
        /// </list>
        /// </remarks>
        CliMetadataImplementationTag ImplementationSource { get; }
        /// <summary>
        /// Returns the set of custom metadata elements applied to the member.
        /// </summary>
        /// <remarks>
        /// Created through references from the <see cref="ICliMetadataCustomAttributeTable"/>.
        /// </remarks>
        IControlledCollection<ICliMetadataCustomAttributeTableRow> CustomAttributes { get; }
    };
};