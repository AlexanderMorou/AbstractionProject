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
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    /// <summary>
    /// Provides a locked row class for a locked table which defines information about the
    /// image's methods.
    /// </summary>
    internal class CliMetadataMethodDefinitionLockedTableRow :
        ICliMetadataMethodDefinitionTableRow
    {
        /// <summary>
        /// Data member for <see cref="Index"/>.
        /// </summary>
        private uint index;
        private ICliMetadataRoot metadataRoot;
        /// <summary>
        /// Data member for <see cref="RVA"/>.
        /// </summary>
        private uint rva;
        /// <summary>
        /// Data member for <see cref="ImplementationDetails"/>.
        /// </summary>
        private MethodImplementationDetails implementationDetails;
        /// <summary>
        /// Data member for <see cref="UsageDetails"/>.
        /// </summary>
        private MethodUseDetails usageDetails;
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private uint nameIndex;
        /// <summary>
        /// Data member for <see cref="Signature"/>.
        /// </summary>
        private uint signatureIndex;
        /// <summary>
        /// Data member for <see cref="ParameterStart"/>.
        /// </summary>
        private uint parameterStartIndex;
        /// <summary>
        /// Data member which denotes the state of the row, used to calculate the size of the
        /// <see cref="AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables.CliMetadataMethodDefinitionTableReader"/>
        /// </summary>
        private byte state;
        /// <summary>
        /// Data member for <see cref="CustomAttributes"/>.
        /// </summary>
        private IControlledCollection<ICliMetadataCustomAttributeTableRow> customAttributes;
        /// <summary>
        /// Data member for <see cref="TypeParameters"/>.
        /// </summary>
        private IControlledCollection<ICliMetadataGenericParameterTableRow> typeParameters;
        /// <summary>
        /// Data member for <see cref="Parameters"/>
        /// </summary>
        private IControlledCollection<ICliMetadataParameterTableRow> parameters;
        ICliMetadataMethodBody body;
        bool checkedBody;
        /// <summary>
        /// Returns the index of the row within the <see cref="CliMetadataMethodDefinitionTableReader"/>
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
        /// Returns the root of the metadata from which the current <see cref="CliMetadataMethodDefinitionLockedTableRow"/>
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
        /// Returns the relative virtual address of the method's body.
        /// </summary>
        public uint RVA
        {
            get
            {
                return this.rva;
            }
        }
        /// <summary>
        /// Returns the conditional information about the method's implementation.
        /// </summary>
        public MethodImplementationDetails ImplementationDetails
        {
            get
            {
                return this.implementationDetails;
            }
        }
        /// <summary>
        /// Returns conditional information about the method, its accessibility, and vtable information.
        /// </summary>
        public MethodUseDetails UsageDetails
        {
            get
            {
                return this.usageDetails;
            }
        }
        /// <summary>
        /// Returns the name of the method.
        /// </summary>
        public string Name
        {
            get
            {
                return this.metadataRoot.StringsHeap[this.nameIndex];
            }
        }
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.StringsHeap"/> from which <see cref="Name"/>
        /// is derived.
        /// </summary>
        public uint NameIndex
        {
            get
            {
                return this.nameIndex;
            }
        }
        /// <summary>
        /// Returns the signature of the method, that is: it's return type, parameter types, and
        /// potential generic calling convention.
        /// </summary>
        public ICliMetadataMethodSignature Signature
        {
            get
            {
                return this.metadataRoot.BlobHeap.GetSignature<ICliMetadataMethodSignature>(SignatureKinds.MethodDefSig, this.signatureIndex);
            }
        }
        /// <summary>
        /// Returns the index onto the <see cref="CliMetadataRoot.BlobHeap"/> from which <see cref="Signature"/>
        /// is derived.
        /// </summary>
        public uint SignatureIndex
        {
            get
            {
                return this.signatureIndex;
            }
        }
        /// <summary>
        /// Returns the <see cref="UInt32"/> value which determines the index of the first <see cref="AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataParameterTableRow"/>
        /// within <see cref="Parameters"/>
        /// </summary>
        public uint ParameterStartIndex
        {
            get
            {
                return this.parameterStartIndex;
            }
        }
        public int Size
        {
            get
            {
                switch (this.state)
                {
                    case 1: case 2: case 4:
                        return CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_2__;
                    case 3: case 5: case 6:
                        return CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_3__;
                    case 7:
                        return CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_4__;
                }
                return CliMetadataMethodDefinitionTableReader.__COR_METHODDEFINITION_CALC_SIZE_1__;
            }
        }
        /// <summary>
        /// Returns the set of custom metadata elements applied to the member.
        /// </summary>
        /// <remarks>
        /// Created through references from the <see cref="ICliMetadataCustomAttributeTable"/>.
        /// </remarks>
        public IControlledCollection<ICliMetadataCustomAttributeTableRow> CustomAttributes
        {
            get
            {
                if (this.customAttributes == null)
                {
                    var customAttributesTable = this.metadataRoot.TableStream.CustomAttributeTable;
                    if (customAttributesTable != null)
                    {
                        List<uint> customAttributes = new List<uint>();
                        foreach (var customAttribute in customAttributesTable)
                            if (this.ParentSource == CliMetadataHasCustomAttributeTag.MethodDefinition && customAttribute.ParentIndex == this.Index)
                                customAttributes.Add(customAttribute.Index);
                            else if (customAttribute.ParentIndex > this.Index)
                                break;
                        this.customAttributes = new CliMetadataLazySet<ICliMetadataCustomAttributeTableRow>(customAttributes.ToArray(), this.metadataRoot.TableStream.CustomAttributeTable);
                    }
                }
                return this.customAttributes;
            }
        }
        /// <summary>
        /// Returns the type-parameters relative to the current row.
        /// </summary>
        /// <remarks>
        /// Created through references from the <see cref="ICliMetadataGenericParameterTable"/>.
        /// </remarks>
        public IControlledCollection<ICliMetadataGenericParameterTableRow> TypeParameters
        {
            get
            {
                if (this.typeParameters == null)
                {
                    var typeParametersTable = this.metadataRoot.TableStream.GenericParameterTable;
                    if (typeParametersTable != null)
                    {
                        List<uint> typeParameters = new List<uint>();
                        foreach (var genericParameter in typeParametersTable)
                            if (this.OwnerSource == CliMetadataTypeOrMethodDef.MethodDefinition && genericParameter.OwnerIndex == this.Index)
                                typeParameters.Add(genericParameter.Index);
                            else if (genericParameter.OwnerIndex > this.Index)
                                break;
                        this.typeParameters = new CliMetadataLazySet<ICliMetadataGenericParameterTableRow>(typeParameters.ToArray(), this.metadataRoot.TableStream.GenericParameterTable);
                    }
                }
                return this.typeParameters;
            }
        }
        /// <summary>
        /// Returns returns the parameters for the current method.
        /// </summary>
        public IControlledCollection<ICliMetadataParameterTableRow> Parameters
        {
            get
            {
                if (this.parameters == null)
                {
                    ICliMetadataMethodDefinitionTableRow nextMethodDefinition = this.metadataRoot.TableStream.MethodDefinitionTable[((int)(this.Index + 1))];
                    uint lastParameterStartIndex;
                    ICliMetadataParameterTable parameterTable = this.metadataRoot.TableStream.ParameterTable;
                    if (nextMethodDefinition != null)
                        lastParameterStartIndex = nextMethodDefinition.ParameterStartIndex;
                    else if (parameterTable != null)
                        lastParameterStartIndex = ((uint)(parameterTable.Count + 1));
                    else
                        lastParameterStartIndex = this.ParameterStartIndex;
                    this.parameters = new CliMetadataLazySequentialSet<ICliMetadataParameterTableRow>(this.ParameterStartIndex, lastParameterStartIndex, parameterTable);
                }
                return this.parameters;
            }
        }
        public ICliMetadataMethodBody Body
        {
            get
            {
                if (!this.checkedBody)
                {
                    if (this.rva != 0)
                        this.body = new CliMetadataMethodBody(this.metadataRoot, this.rva);
                    this.checkedBody = true;
                }
                return this.body;
            }
        }
        CliMetadataHasCustomAttributeTag ICliMetadataHasCustomAttributeRow.HasCustomAttributeEncoding
        {
            get
            {
                return CliMetadataHasCustomAttributeTag.MethodDefinition;
            }
        }
        CliMetadataHasDeclSecurityTag ICliMetadataHasDeclSecurityRow.HasDeclSecurityEncoding
        {
            get
            {
                return CliMetadataHasDeclSecurityTag.MethodDefinition;
            }
        }
        CliMetadataMemberRefParentTag ICliMetadataMemberRefParentRow.MemberRefParentEncoding
        {
            get
            {
                return CliMetadataMemberRefParentTag.MethodDefinition;
            }
        }
        CliMetadataMethodDefOrRefTag ICliMetadataMethodDefOrRefRow.MethodDefOrRefEncoding
        {
            get
            {
                return CliMetadataMethodDefOrRefTag.MethodDefinition;
            }
        }
        CliMetadataMemberForwardedTag ICliMetadataMemberForwardedRow.MemberForwardedEncoding
        {
            get
            {
                return CliMetadataMemberForwardedTag.MethodDefinition;
            }
        }
        CliMetadataCustomAttributeTypeTag ICliMetadataCustomAttributeTypeRow.CustomAttributeTypeEncoding
        {
            get
            {
                return CliMetadataCustomAttributeTypeTag.MethodDefinition;
            }
        }
        CliMetadataTypeOrMethodDef ICliMetadataTypeOrMethodDefRow.TypeOrMethodDefEncoding
        {
            get
            {
                return CliMetadataTypeOrMethodDef.MethodDefinition;
            }
        }
        public override string ToString()
        {
            return string.Format("MethodDefinition: RVA = {0}, Name = {1}, Signature = {2}", this.RVA, this.Name, this.Signature);
        }
        internal CliMetadataMethodDefinitionLockedTableRow(uint index, byte state, ICliMetadataRoot metadataRoot, uint rva, MethodImplementationDetails implementationDetails, MethodUseDetails usageDetails, uint nameIndex, uint signatureIndex, uint parameterStartIndex)
        {
            this.index = index;
            this.metadataRoot = metadataRoot;
            this.state = state;
            this.rva = rva;
            this.implementationDetails = implementationDetails;
            this.usageDetails = usageDetails;
            this.nameIndex = nameIndex;
            this.signatureIndex = signatureIndex;
            this.parameterStartIndex = parameterStartIndex;
        }
    };
};
