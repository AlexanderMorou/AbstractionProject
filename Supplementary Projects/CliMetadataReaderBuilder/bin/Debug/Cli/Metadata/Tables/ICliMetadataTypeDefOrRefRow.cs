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
namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    /// <summary>
    /// Defines the umbrella interface for reference indexes encoded with <see cref="CliMetadataTypeDefOrRefTag"/>.
    /// </summary>
    public interface ICliMetadataTypeDefOrRefRow
    {
        /// <summary>
        /// Returns the <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.ICliMetadataRoot"/>
        /// metadataRoot associated to the row encoded with <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag"/>.
        /// </summary>
        ICliMetadataRoot MetadataRoot { get; }
        /// <summary>
        /// Returns the @s:AllenCopeland.Abstraction.Utilities.Collections.IControlledCollection{[AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataCustomAttributeTableRow,
        /// CliMetadataReader, Version=1.0.*, Culture=neutral, PublicKeyToken=null]}; of customAttributes
        /// associated to the row encoded with <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataTypeDefOrRefTag"/>.
        /// </summary>
        IControlledCollection<ICliMetadataCustomAttributeTableRow> CustomAttributes { get; }
        /// <summary>
        /// Returns the source table from which the current <see cref="ICliMetadataTypeDefOrRefRow"/>
        /// is derived.
        /// </summary>
        CliMetadataTypeDefOrRefTag TypeDefOrRefEncoding { get; }
    };
};