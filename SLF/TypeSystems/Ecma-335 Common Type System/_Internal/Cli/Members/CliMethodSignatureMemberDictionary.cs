using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent> :
        CliMetadataDrivenDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow, TSignature>,
        IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>,
        IMethodSignatureMemberDictionary
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        private TSignatureParent parent;

        internal CliMethodSignatureMemberDictionary(TSignatureParent parent, IReadOnlyCollection<ICliMetadataMethodDefinitionTableRow> methods)
            : base(methods)
        {
            this.parent = parent;
        }

        protected override TSignature CreateElementFrom(int index, ICliMetadataMethodDefinitionTableRow metadata)
        {
            throw new NotImplementedException();
        }


        //#region IMemberDictionary<TSignatureParent,IGeneralGenericSignatureMemberUniqueIdentifier,TSignature> Members

        public TSignatureParent Parent
        {
            get { return parent; }
        }

        //#endregion

        protected override IGeneralGenericSignatureMemberUniqueIdentifier GetIdentifierFrom(int index, ICliMetadataMethodDefinitionTableRow metadata)
        {
            throw new NotImplementedException();
        }

        //#region IMethodSignatureMemberDictionary Members

        int IMethodSignatureMemberDictionary.IndexOf(IMethodSignatureMember method)
        {
            if (method is TSignature)
                return this.IndexOf((TSignature) method);
            return -1;
        }

        //#endregion


        //#region IMemberDictionary Members

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        public int IndexOf(IMember member)
        {
            if (member is TSignature)
                return this.IndexOf((TSignature) member);
            return -1;
        }

        //#endregion
    }
}
