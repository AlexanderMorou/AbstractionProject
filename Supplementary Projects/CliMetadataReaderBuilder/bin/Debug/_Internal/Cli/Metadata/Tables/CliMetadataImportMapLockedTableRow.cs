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
    internal class CliMetadataImportMapLockedTableRow :
        ICliMetadataImportMapTableRow
    {
        private ICliMetadataRoot metadataRoot;
        /// <summary>
        /// Data member for <see cref="MappingCharset"/>.
        /// </summary>
        private PlatformInvokeCharacterSet mappingCharset;
        /// <summary>
        /// Data member for <see cref="MappingCallingConvention"/>.
        /// </summary>
        private PlatformInvokeCallingConvention mappingCallingConvention;
        /// <summary>
        /// Data member for <see cref="MemberForwarded"/>.
        /// </summary>
        private uint memberForwardedIndex;
        private CliMetadataMemberForwardedTag memberForwardedSource;
        /// <summary>
        /// Data member for <see cref="ImportName"/>.
        /// </summary>
        private uint importNameIndex;
        /// <summary>
        /// Data member for <see cref="ImportScope"/>.
        /// </summary>
        private uint importScopeIndex;
        /// <summary>
        /// Data member which denotes the state of the row, used to calculate the size of the
        /// <see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables.CliMetadataImportMapTableReader"/>
        /// </summary>
        private byte state;
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="CliMetadataImportMapLockedTableRow"/>
        /// was derived.
        /// </summary>
        public ICliMetadataRoot MetadataRoot
        {
            get
            {
                return this.metadataRoot;
            }
        }
        public PlatformInvokeCharacterSet MappingCharset
        {
            get
            {
                return this.mappingCharset;
            }
        }
        public PlatformInvokeCallingConvention MappingCallingConvention
        {
            get
            {
                return this.mappingCallingConvention;
            }
        }
        public ICliMetadataMemberForwardedRow MemberForwarded
        {
            get
            {
                if (this.memberForwardedIndex == 0)
                    return null;
                switch (this.memberForwardedSource)
                {
                    case CliMetadataMemberForwardedTag.Field:
                        return this.metadataRoot.TableStream.FieldTable[((int)(this.memberForwardedIndex))];
                    case CliMetadataMemberForwardedTag.MethodDefinition:
                        return this.metadataRoot.TableStream.MethodDefinitionTable[((int)(this.memberForwardedIndex))];
                }
                return null;
            }
        }
        /// <summary>
        /// Returns the decoded index of the <see cref="MemberForwarded"/> relative to the appropriate
        /// table.
        /// </summary>
        /// <remarks>
        /// Refer to <see cref="MemberForwardedSource"/> to discern the proper table for <see cref="MemberForwardedIndex"/>
        /// </remarks>
        public uint MemberForwardedIndex
        {
            get
            {
                return this.memberForwardedIndex;
            }
        }
        /// <summary>
        /// Returns the <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataMemberForwardedTag"/>
        /// which determines the table that <see cref="MemberForwardedIndex"/> refers to.
        /// </summary>
        /// <remarks>
        /// <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataMemberForwardedTag"/>
        /// encoding <see cref="CliMetadataTableStreamAndHeader"/> tables:<list type="table"><listheader><term>Encoding</term><description>TableStream
        /// Property</description></listheader><item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataMemberForwardedTag.Field"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.FieldTable"/></description></item>
        /// <item><term><see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.CliMetadataMemberForwardedTag.MethodDefinition"/></term><description><see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.CliMetadataTableStreamAndHeader.MethodDefinitionTable"/></description></item>
        /// </list>
        /// </remarks>
        public CliMetadataMemberForwardedTag MemberForwardedSource
        {
            get
            {
                return this.memberForwardedSource;
            }
        }
        public string ImportName
        {
            get
            {
                return this.metadataRoot.StringsHeap[this.importNameIndex];
            }
        }
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.StringsHeap"/> from which <see cref="ImportName"/>
        /// is derived.
        /// </summary>
        public uint ImportNameIndex
        {
            get
            {
                return this.importNameIndex;
            }
        }
        public uint ImportScopeIndex
        {
            get
            {
                return this.importScopeIndex;
            }
        }
        public ICliMetadataModuleReferenceTableRow ImportScope
        {
            get
            {
                return this.metadataRoot.TableStream.ModuleReferenceTable[((int)(this.importScopeIndex))];
            }
        }
        public int Size
        {
            get
            {
                switch (this.state)
                {
                    case 1: case 2: case 4:
                        return CliMetadataImportMapTableReader.__COR_IMPORTMAP_CALC_SIZE_2__;
                    case 3: case 5: case 6:
                        return CliMetadataImportMapTableReader.__COR_IMPORTMAP_CALC_SIZE_3__;
                    case 7:
                        return CliMetadataImportMapTableReader.__COR_IMPORTMAP_CALC_SIZE_4__;
                }
                return CliMetadataImportMapTableReader.__COR_IMPORTMAP_CALC_SIZE_1__;
            }
        }
        public override string ToString()
        {
            return string.Format("ImportMap: ImportName = {0}", this.ImportName);
        }
        internal CliMetadataImportMapLockedTableRow(byte state, ICliMetadataRoot metadataRoot, PlatformInvokeCharacterSet mappingCharset, PlatformInvokeCallingConvention mappingCallingConvention, CliMetadataMemberForwardedTag memberForwardedSource, uint memberForwardedIndex, uint importNameIndex, uint importScopeIndex)
        {
            this.metadataRoot = metadataRoot;
            this.state = state;
            this.mappingCharset = mappingCharset;
            this.mappingCallingConvention = mappingCallingConvention;
            this.memberForwardedSource = memberForwardedSource;
            this.memberForwardedIndex = memberForwardedIndex;
            this.importNameIndex = importNameIndex;
            this.importScopeIndex = importScopeIndex;
        }
    };
};