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
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    // Module: RootModule
    /// <summary>
    /// Provides a row class for a table which defines the event mapping of
    /// a type defined within the module.
    /// </summary>
    internal class CliMetadataEventMapLockedTableRow :
        ICliMetadataEventMapTableRow
    {
        private uint index;
        
        private ICliMetadataRoot metadataRoot;
        
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private uint parentIndex;
        
        /// <summary>
        /// Data member for <see cref="EventList"/>.
        /// </summary>
        private uint eventListIndex;
        
        /// <summary>
        /// Data member which denotes the state of the row, used to calculate the size of the
        /// <see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables.CliMetadataEventMapTableReader"/>
        /// </summary>
        private byte state;
        /// <summary>
        /// Returns the index of the row within the <see cref="CliMetadataEventMapTableReader"/>
        /// since the rows from the containing table are referenced by other tables.
        /// </summary>
        public uint Index
        {
            get
            {
                return this.index;
            }
        }
        
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="CliMetadataEventMapLockedTableRow"/>
        /// was derived.
        /// </summary>
        public ICliMetadataRoot MetadataRoot
        {
            get
            {
                return this.metadataRoot;
            }
        }
        
        /// <summary>
        /// Returns the <see cref="UInt32"/> value which determines the index of the first
        /// <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataTypeDefinitionTableRow"/>
        /// within <see cref="Events"/>
        /// </summary>
        public uint ParentIndex
        {
            get
            {
                return this.parentIndex;
            }
        }
        
        public uint EventListIndex
        {
            get
            {
                return this.eventListIndex;
            }
        }
        
        public int Size
        {
            get
            {
                switch (this.state)
                {
                    case 1:
                    case 2:
                        return CliMetadataEventMapTableReader.__COR_EVENTMAP_CALC_SIZE_2__;
                    case 3:
                        return CliMetadataEventMapTableReader.__COR_EVENTMAP_CALC_SIZE_3__;
                }
                return CliMetadataEventMapTableReader.__COR_EVENTMAP_CALC_SIZE_1__;
            }
        }
        internal CliMetadataEventMapLockedTableRow(uint index, byte state, ICliMetadataRoot metadataRoot, uint parentIndex, uint eventListIndex)
        {
            this.index = index;
            this.metadataRoot = metadataRoot;
            this.state = state;
            this.parentIndex = parentIndex;
            this.eventListIndex = eventListIndex;
        }
    }
}
