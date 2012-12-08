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

        protected CliMethodSignatureBase(ICliMetadataMethodDefinitionTableRow metadata, _ICliAssembly assembly, TSignatureParent parent)
            : base(metadata, assembly, parent)
        {
        }

        protected override CliParameterMemberDictionary<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>> InitializeParameters()
        {
            return new ParameterMemberDictionary(this);
        }

        protected abstract IMethodSignatureParameterMember<TSignature, TSignatureParent> CreateParameter(int index, ICliMetadataParameterTableRow metadataEntry);

        private class ParameterMemberDictionary :
            CliParameterMemberDictionary<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>>
        {
            private new CliMethodSignatureBase<TSignature, TSignatureParent> Parent { get { return ((CliMethodSignatureBase<TSignature, TSignatureParent>) (object) base.Parent); } }

            public ParameterMemberDictionary(CliMethodSignatureBase<TSignature, TSignatureParent> signature)
                : base(signature.IdentityManager, signature.MetadataEntry.Index, signature.MetadataEntry.MetadataRoot)
            {
            }

            protected override IMethodSignatureParameterMember<TSignature, TSignatureParent> CreateElementFrom(int index, ICliMetadataParameterTableRow metadata)
            {
                return this.Parent.CreateParameter(index, metadata);
            }
        }

        private abstract class ParameterMember :
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

        }


    }
}
