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
    /// Provides a locked row class for a locked table which defines information about the
    /// constants within the image.
    /// </summary>
    internal class CliMetadataConstantLockedTableRow :
        ICliMetadataConstantTableRow
    {
        private ICliMetadataRoot metadataRoot;
        /// <summary>
        /// Data member for <see cref="Type"/>.
        /// </summary>
        private CliMetadataNativeTypes @type;
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private uint parentIndex;
        private CliMetadataHasConstantTag parentSource;
        /// <summary>
        /// Data member for <see cref="Value"/>.
        /// </summary>
        private uint valueIndex;
        /// <summary>
        /// Data member which denotes the state of the row, used to calculate the size of the
        /// <see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables.CliMetadataConstantTableReader"/>
        /// </summary>
        private byte state;
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="CliMetadataConstantLockedTableRow"/>
        /// was derived.
        /// </summary>
        public ICliMetadataRoot MetadataRoot
        {
            get
            {
                return this.metadataRoot;
            }
        }
        public CliMetadataNativeTypes Type
        {
            get
            {
                return this.@type;
            }
        }
        public ICliMetadataHasConstantRow Parent
        {
            get
            {
                if (this.parentIndex == 0)
                    return null;
                switch (this.parentSource)
                {
                    case CliMetadataHasConstantTag.Field:
                        return this.metadataRoot.TableStream.FieldTable[((int)(this.parentIndex))];
                    case CliMetadataHasConstantTag.Param:
                        return this.metadataRoot.TableStream.ParameterTable[((int)(this.parentIndex))];
                    case CliMetadataHasConstantTag.Property:
                        return this.metadataRoot.TableStream.PropertyTable[((int)(this.parentIndex))];
                }
                return null;
            }
        }
        /// <summary>
        /// Returns the decoded index of the <see cref="Parent"/> relative to the appropriate
        /// table.
        /// </summary>
        /// <remarks>
        /// Refer to <see cref="ParentSource"/> to discern the proper table for <see cref="ParentIndex"/>
        /// </remarks>
        public uint ParentIndex
        {
            get
            {
                return this.parentIndex;
            }
        }
        /// <summary>
        /// Returns the <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataHasConstantTag"/>
        /// which determines the table that <see cref="ParentIndex"/> refers to.
        /// </summary>
        /// <remarks>
        /// <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataHasConstantTag"/>
        /// encoding <see cref="CliMetadataTableStreamAndHeader"/> tables:<list type="table"><listheader><term>Encoding</term><description>TableStream
        /// Property</description></listheader><item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataHasConstantTag.Field"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.FieldTable"/></description></item>
        /// <item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataHasConstantTag.Param"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.ParameterTable"/></description></item>
        /// <item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataHasConstantTag.Property"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.PropertyTable"/></description></item>
        /// </list>
        /// </remarks>
        public CliMetadataHasConstantTag ParentSource
        {
            get
            {
                return this.parentSource;
            }
        }
        public byte[] Value
        {
            get
            {
                return this.metadataRoot.BlobHeap[this.valueIndex];
            }
        }
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.BlobHeap"/> from which <see cref="Value"/>
        /// is derived.
        /// </summary>
        public uint ValueIndex
        {
            get
            {
                return this.valueIndex;
            }
        }
        public int Size
        {
            get
            {
                switch (this.state)
                {
                    case 1: case 2:
                        return CliMetadataConstantTableReader.__COR_CONSTANT_CALC_SIZE_2__;
                    case 3:
                        return CliMetadataConstantTableReader.__COR_CONSTANT_CALC_SIZE_3__;
                }
                return CliMetadataConstantTableReader.__COR_CONSTANT_CALC_SIZE_1__;
            }
        }
        public override string ToString()
        {
            return string.Format("Constant: Value = {0}", this.Value);
        }
        internal CliMetadataConstantLockedTableRow(byte state, ICliMetadataRoot metadataRoot, CliMetadataNativeTypes @type, CliMetadataHasConstantTag parentSource, uint parentIndex, uint valueIndex)
        {
            this.metadataRoot = metadataRoot;
            this.state = state;
            this.@type = @type;
            this.parentSource = parentSource;
            this.parentIndex = parentIndex;
            this.valueIndex = valueIndex;
        }
    };
};