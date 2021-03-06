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
    internal class CliMetadataNestedClassLockedTableRow :
        ICliMetadataNestedClassTableRow
    {
        private ICliMetadataRoot metadataRoot;
        /// <summary>
        /// Data member for <see cref="NestedClass"/>.
        /// </summary>
        private uint nestedClassIndex;
        /// <summary>
        /// Data member for <see cref="EnclosingClass"/>.
        /// </summary>
        private uint enclosingClassIndex;
        /// <summary>
        /// Data member which denotes the state of the row, used to calculate the size of the
        /// <see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables.CliMetadataNestedClassTableReader"/>
        /// </summary>
        private byte state;
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="CliMetadataNestedClassLockedTableRow"/>
        /// was derived.
        /// </summary>
        public ICliMetadataRoot MetadataRoot
        {
            get
            {
                return this.metadataRoot;
            }
        }
        public uint NestedClassIndex
        {
            get
            {
                return this.nestedClassIndex;
            }
        }
        public uint EnclosingClassIndex
        {
            get
            {
                return this.enclosingClassIndex;
            }
        }
        public ICliMetadataTypeDefinitionTableRow EnclosingClass
        {
            get
            {
                return this.metadataRoot.TableStream.TypeDefinitionTable[((int)(this.enclosingClassIndex))];
            }
        }
        public int Size
        {
            get
            {
                switch (this.state)
                {
                    case 1:
                        return CliMetadataNestedClassTableReader.__COR_NESTEDCLASS_CALC_SIZE_2__;
                }
                return CliMetadataNestedClassTableReader.__COR_NESTEDCLASS_CALC_SIZE_1__;
            }
        }
        internal CliMetadataNestedClassLockedTableRow(byte state, ICliMetadataRoot metadataRoot, uint nestedClassIndex, uint enclosingClassIndex)
        {
            this.metadataRoot = metadataRoot;
            this.state = state;
            this.nestedClassIndex = nestedClassIndex;
            this.enclosingClassIndex = enclosingClassIndex;
        }
    };
};
