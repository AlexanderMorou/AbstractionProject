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
using System;
namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    public interface ICliMetadataGenericParamConstraintTableRow :
        ICliMetadataTableRow, 
        ICliMetadataIndexedRow
    {
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="ICliMetadataGenericParamConstraintTableRow"/>
        /// was derived.
        /// </summary>
        ICliMetadataRoot MetadataRoot { get; }
        uint OwnerIndex { get; }
        ICliMetadataGenericParameterTableRow Owner { get; }
        ICliMetadataTypeDefOrRefRow Constraint { get; }
        /// <summary>
        /// Returns the decoded index of the <see cref="Constraint"/> relative to the appropriate
        /// table.
        /// </summary>
        /// <remarks>
        /// Refer to <see cref="ConstraintSource"/> to discern the proper table for <see cref="ConstraintIndex"/>
        /// </remarks>
        uint ConstraintIndex { get; }
        /// <summary>
        /// Returns the <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag"/>
        /// which determines the table that <see cref="ConstraintIndex"/> refers to.
        /// </summary>
        /// <remarks>
        /// <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag"/>
        /// encoding <see cref="CliMetadataTableStreamAndHeader"/> tables:<list type="table"><listheader><term>Encoding</term><description>TableStream
        /// Property</description></listheader><item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag.TypeDefinition"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.TypeDefinitionTable"/></description></item>
        /// <item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag.TypeReference"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.TypeRefTable"/></description></item>
        /// <item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag.TypeSpecification"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.TypeSpecificationTable"/></description></item>
        /// </list>
        /// </remarks>
        CliMetadataTypeDefOrRefTag ConstraintSource { get; }
    };
};
