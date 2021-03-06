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
    /// <summary>
    /// Defines properties and methods for a locked row in a table which defines the layout
    /// of the fields on an <see cref="LayoutKind.Explicit"/> layout type.
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
        /// Returns the <see cref="UInt32"/> value which represents the field for which the layout
        /// exists.
        /// </summary>
        uint FieldIndex { get; }
    };
};
