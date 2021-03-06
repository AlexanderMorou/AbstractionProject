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
using System;
namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    public interface ICliMetadataMethodSpecificationTableRow :
        ICliMetadataTableRow
    {
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="ICliMetadataMethodSpecificationTableRow"/>
        /// was derived.
        /// </summary>
        ICliMetadataRoot MetadataRoot { get; }
        ICliMetadataMethodDefOrRefRow Method { get; }
        /// <summary>
        /// Returns the decoded index of the <see cref="Method"/> relative to the appropriate
        /// table.
        /// </summary>
        /// <remarks>
        /// Refer to <see cref="MethodBodySource"/> to discern the proper table for <see cref="MethodIndex"/>
        /// </remarks>
        uint MethodIndex { get; }
        /// <summary>
        /// Returns the <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataMethodDefOrRefTag"/>
        /// which determines the table that <see cref="MethodIndex"/> refers to.
        /// </summary>
        /// <remarks>
        /// <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataMethodDefOrRefTag"/>
        /// encoding <see cref="CliMetadataTableStreamAndHeader"/> tables:<list type="table"><listheader><term>Encoding</term><description>TableStream
        /// Property</description></listheader><item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataMethodDefOrRefTag.MemberRef"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.MemberReferenceTable"/></description></item>
        /// <item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataMethodDefOrRefTag.MethodDefinition"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.MethodDefinitionTable"/></description></item>
        /// </list>
        /// </remarks>
        CliMetadataMethodDefOrRefTag MethodBodySource { get; }
        ICliMetadataMethodSpecSignature Instantiation { get; }
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.BlobHeap"/> from which <see cref="Instantiation"/>
        /// is derived.
        /// </summary>
        uint InstantiationIndex { get; }
    };
};
