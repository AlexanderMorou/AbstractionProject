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
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    
    // Module: RootModule
    /// <summary>
    /// Defines properties and methods for a table which defines the structure
    /// of a type reference, which identifies how to resolve the reference,
    /// its name, and namespace.
    /// </summary>
    /// <summary>
    /// ResolutionScope shall be exactly one of:<list type="table">|-null -|- in this case
    /// there shall be a row in <see cref="CliMetadataExportedTypeTable"/> which should
    /// identify where the type is now defined.-|</list>
    /// </summary>
    public interface ICliMetadataTypeRefTable :
        IControlledCollection<ICliMetadataTypeRefTableRow>
    {
        long Length { get; }
        void Read();
    }
}
