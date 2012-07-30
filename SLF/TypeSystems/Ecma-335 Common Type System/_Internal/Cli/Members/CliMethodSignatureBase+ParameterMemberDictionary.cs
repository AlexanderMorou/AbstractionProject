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
        CliMethodSignatureBase<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent>,
        _ICliParameterParent
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
        private class ParameterMemberDictionary :
            CliParameterMemberDictionary<TSignature, IMethodSignatureParameterMember<TSignature, TSignatureParent>>
        {
            private new CliMethodSignatureBase<TSignature, TSignatureParent> Parent { get { return ((CliMethodSignatureBase<TSignature, TSignatureParent>) (object) base.Parent); } }

            public ParameterMemberDictionary(CliMethodSignatureBase<TSignature, TSignatureParent> signature)
                : base(signature.Manager, (int) signature.Metadata.Index, signature.Metadata.MetadataRoot)
            {
            }

            protected override IMethodSignatureParameterMember<TSignature, TSignatureParent> CreateElementFrom(ICliMetadataParameterTableRow metadata, int index)
            {
                return new ParameterMember(metadata, this.Parent, index);
            }
        }

        private class ParameterMember :
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
        }


        #region _ICliParameterParent Members

        _ICliManager _ICliParameterParent.Manager
        {
            get { return this.Manager; }
        }

        public ICliMetadataMethodSignature Signature
        {
            get { return this.Metadata.Signature; }
        }

        #endregion

        #region ICliDeclaration Members

        ICliMetadataTableRow ICliDeclaration.Metadata
        {
            get { return this.Metadata; }
        }

        #endregion

    }
}
