using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using AllenCopeland.Abstraction.Slf.Cli;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliMethodSignatureBase<TSignature, TSignatureParent> :
        CliMethodSignatureBase<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {

        protected CliMethodSignatureBase(ICliMetadataMethodDefinitionTableRow metadata, _ICliAssembly assembly, TSignatureParent parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(metadata, assembly, parent, uniqueIdentifier)
        {
        }

        protected override CliParameterMemberDictionary<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>> InitializeParameters()
        {
            return new ParameterMemberDictionary(this);
        }

        protected abstract IMethodSignatureParameterMember<TSignature, TSignatureParent> CreateParameter(int index, ICliMetadataParameterTableRow metadataEntry);

        internal class ParameterMemberDictionary :
            CliParameterMemberDictionary<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>>
        {
            private new CliMethodSignatureBase<TSignature, TSignatureParent> Parent { get { return ((CliMethodSignatureBase<TSignature, TSignatureParent>) (object) base.Parent); } }

            public ParameterMemberDictionary(CliMethodSignatureBase<TSignature, TSignatureParent> signature)
                : base(signature.IdentityManager, signature.MetadataEntry.Index, signature.MetadataEntry.MetadataRoot, (TSignature)(object)signature)
            {
            }

            protected override IMethodSignatureParameterMember<TSignature, TSignatureParent> CreateElementFrom(int index, ICliMetadataParameterTableRow metadata)
            {
                return this.Parent.CreateParameter(index, metadata);
            }
        }

        internal abstract class ParameterMember :
            CliParameterMember<TSignature, CliMethodSignatureBase<TSignature, TSignatureParent>>,
            IMethodSignatureParameterMember<TSignature, TSignatureParent>
        {
            internal ParameterMember(ICliMetadataParameterTableRow metadata, CliMethodSignatureBase<TSignature, TSignatureParent> parent, int index)
                : base(metadata, parent, index)
            {
            }

            #region ISignatureParameterMember Members

            ISignatureMember ISignatureParameterMember.Parent
            {
                get { return this.Parent; }
            }

            #endregion

            #region IMethodSignatureParameterMember Members

            IMethodSignatureMember IMethodSignatureParameterMember.Parent
            {
                get { return this.Parent; }
            }

            #endregion

            protected override IMethodSignatureMember ActiveMethod
            {
                get { return this.Parent; }
            }
            
            public override string ToString()
            {
                return this.UniqueIdentifier.ToString();
            }
        }
    }

    internal partial class CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent>
    {

        internal class TypeParameterDictionary :
            CliGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>
        {
            public TypeParameterDictionary(CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent> parent)
                : base(parent.MetadataEntry.TypeParameters, parent) { }

            public new CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent> Parent { get { return (CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent>)base.Parent; } }

            protected override IGenericParameterUniqueIdentifier GetIdentifierFrom(int index, ICliMetadataGenericParameterTableRow metadata) { return AstIdentifier.GetGenericParameterIdentifier(index, false); }

            protected override IMethodSignatureGenericTypeParameterMember CreateElementFrom(int index, ICliMetadataGenericParameterTableRow metadataEntry)
            {
                return this.Parent.GetTypeParameter(index, metadataEntry);
            }
        }

        internal abstract class TypeParameter :
            CliGenericParameterMember<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>,
            IMethodSignatureGenericTypeParameterMember
        {
            internal TypeParameter(IMethodSignatureMember parent, ICliMetadataGenericParameterTableRow metadataEntry, int position)
                : base(parent, metadataEntry, position)
            {
            }

            protected override IGenericParameterUniqueIdentifier OnGetUniqueIdentifier()
            {
                return AstIdentifier.GetGenericParameterIdentifier((int)this.Position, false);
            }

        }
    }
}
