using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CliGenericParameterMember<TGenericParameter, TParent> 
    {
        private class _DefaultCtorEntry :
            ICliMetadataMethodDefinitionTableRow
        {
            private static readonly _Signature _signature = new _Signature();
            private class _Signature :
                ICliMetadataMethodSignature
            {
                private static readonly ICliMetadataReturnTypeSignature returnType = new CliMetadataReturnTypeSignature(new CliMetadataNativeTypeSignature(CliMetadataNativeTypes.Void), new ICliMetadataCustomModifierSignature[0]);
                public ICliMetadataReturnTypeSignature ReturnType
                {
                    get { return returnType; }
                }

                public IControlledCollection<ICliMetadataParamSignature> Parameters
                {
                    get { return ArrayReadOnlyCollection<ICliMetadataParamSignature>.Empty; }
                }

                public CliMetadataMethodSigConventions CallingConvention
                {
                    get { return CliMetadataMethodSigConventions.Default; }
                }

                public CliMetadataMethodSigFlags Flags
                {
                    get { return CliMetadataMethodSigFlags.HasThis; }
                }

                #region ICliMetadataSignature Members

                public SignatureKinds SignatureKind
                {
                    get { return SignatureKinds.MethodDefSig; }
                }

                #endregion
            }
            public uint RVA
            {
                get { return 0; }
            }

            public MethodImplementationDetails ImplementationDetails
            {
                get { return new MethodImplementationDetails(((ushort)(MethodImplementationFlags.NoInlining)) | (ushort)MethodCodeType.NativeCode); }
            }

            public MethodUseDetails UsageDetails
            {
                get { return new MethodUseDetails((ushort)(MethodUseFlags.RTSpecialName | MethodUseFlags.SpecialName) | (ushort)(MethodMemberAccessibility.Public)); }
            }

            public ICliMetadataMethodSignature Signature
            {
                get { return _signature; }
            }

            public uint ParameterStartIndex
            {
                get { return 0; }
            }

            public IControlledCollection<ICliMetadataParameterTableRow> Parameters
            {
                get { return null; }
            }

            public ICliMetadataMethodBody Body
            {
                get { return null; }
            }

            public ICliMetadataRoot MetadataRoot
            {
                get { return null; }
            }

            public IControlledCollection<ICliMetadataCustomAttributeTableRow> CustomAttributes
            {
                get { return null; }
            }

            public string Name
            {
                get { return ".ctor"; }
            }

            public uint NameIndex
            {
                get { return 0; }
            }

            public int Size
            {
                get { return 0; }
            }

            public CliMetadataHasCustomAttributeTag HasCustomAttributeEncoding
            {
                get { return CliMetadataHasCustomAttributeTag.MethodDefinition; }
            }


            public CliMetadataHasDeclSecurityTag HasDeclSecurityEncoding
            {
                get { return CliMetadataHasDeclSecurityTag.MethodDefinition; }
            }


            public CliMetadataMemberRefParentTag MemberRefParentEncoding
            {
                get { return CliMetadataMemberRefParentTag.MethodDefinition; }
            }


            public uint SignatureIndex
            {
                get { return 0; }
            }

            public CliMetadataMethodDefOrRefTag MethodDefOrRefEncoding
            {
                get { return CliMetadataMethodDefOrRefTag.MethodDefinition; }
            }


            public CliMetadataMemberForwardedTag MemberForwardedEncoding
            {
                get { return CliMetadataMemberForwardedTag.MethodDefinition; }
            }

            public CliMetadataCustomAttributeTypeTag CustomAttributeTypeEncoding
            {
                get { return CliMetadataCustomAttributeTypeTag.MethodDefinition; }
            }


            public IControlledCollection<ICliMetadataGenericParameterTableRow> TypeParameters
            {
                get { return null; }
            }

            public CliMetadataTypeOrMethodDef TypeOrMethodDefEncoding
            {
                get { return CliMetadataTypeOrMethodDef.MethodDefinition; }
            }

            public uint Index
            {
                get { return 0; }
            }
        }
    }
}
