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
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    /// <summary>
    /// Provides a locked row class for a locked table which defines the property mapping
    /// of a type defined within the module.
    /// </summary>
    internal class CliMetadataPropertyMapLockedTableRow :
        ICliMetadataPropertyMapTableRow
    {
        /// <summary>
        /// Data member for <see cref="Index"/>.
        /// </summary>
        private uint index;
        private ICliMetadataRoot metadataRoot;
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private uint parentIndex;
        /// <summary>
        /// Data member for <see cref="PropertyList"/>.
        /// </summary>
        private uint propertyListIndex;
        /// <summary>
        /// Data member which denotes the state of the row, used to calculate the size of the
        /// <see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables.CliMetadataPropertyMapTableReader"/>
        /// </summary>
        private byte state;
        /// <summary>
        /// Returns the index of the row within the <see cref="CliMetadataPropertyMapTableReader"/>
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
        /// Returns the root of the metadata from which the current <see cref="CliMetadataPropertyMapLockedTableRow"/>
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
        /// Returns the <see cref="UInt32"/> value which determines the index of the first <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataTypeDefinitionTableRow"/>
        /// within <see cref="Properties"/>
        /// </summary>
        public uint ParentIndex
        {
            get
            {
                return this.parentIndex;
            }
        }
        public uint PropertyListIndex
        {
            get
            {
                return this.propertyListIndex;
            }
        }
        public int Size
        {
            get
            {
                switch (this.state)
                {
                    case 1: case 2:
                        return CliMetadataPropertyMapTableReader.__COR_PROPERTYMAP_CALC_SIZE_2__;
                    case 3:
                        return CliMetadataPropertyMapTableReader.__COR_PROPERTYMAP_CALC_SIZE_3__;
                }
                return CliMetadataPropertyMapTableReader.__COR_PROPERTYMAP_CALC_SIZE_1__;
            }
        }
        internal CliMetadataPropertyMapLockedTableRow(uint index, byte state, ICliMetadataRoot metadataRoot, uint parentIndex, uint propertyListIndex)
        {
            this.index = index;
            this.metadataRoot = metadataRoot;
            this.state = state;
            this.parentIndex = parentIndex;
            this.propertyListIndex = propertyListIndex;
        }
    };
};