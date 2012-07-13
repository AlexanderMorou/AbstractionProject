using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent> :
        CliMetadataDrivenDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow, TSignature>,
        IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        private ICliMetadataTypeDefinitionTableRow ownerInfo;

        internal CliMethodSignatureMemberDictionary(ICliMetadataTypeDefinitionTableRow ownerInfo)
            : base(ExtractOwnerCount(ownerInfo))
        {
            this.ownerInfo = ownerInfo;
        }

        private static int ExtractOwnerCount(ICliMetadataTypeDefinitionTableRow ownerInfo)
        {
            return ownerInfo.Methods.Count;
        }

        protected override ICliMetadataMethodDefinitionTableRow GetMetadataAt(int index)
        {
            return this.ownerInfo.Methods[index];
        }

        protected override TSignature CreateElementFrom(ICliMetadataMethodDefinitionTableRow metadata)
        {
            throw new NotImplementedException();
        }

        #region IMethodSignatureMemberDictionary<TSignatureParameter,TSignature,TSignatureParent> Members

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(string name, bool strict, ITypeCollection search)
        {
            throw new NotImplementedException();
        }

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection search)
        {
            throw new NotImplementedException();
        }

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(string name, bool strict, params IType[] search)
        {
            throw new NotImplementedException();
        }

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(string name, params IType[] search)
        {
            throw new NotImplementedException();
        }

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, bool strict, ITypeCollection search)
        {
            throw new NotImplementedException();
        }

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, ITypeCollection search)
        {
            throw new NotImplementedException();
        }

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, bool strict, params IType[] search)
        {
            throw new NotImplementedException();
        }

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, params IType[] search)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier,TSignature,TSignatureParameter,TSignatureParent> Members

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, ITypeCollectionBase search)
        {
            throw new NotImplementedException();
        }

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(ITypeCollectionBase search)
        {
            throw new NotImplementedException();
        }

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, params IType[] search)
        {
            throw new NotImplementedException();
        }

        public IFilteredSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(params IType[] search)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMemberDictionary<TSignatureParent,IGeneralGenericSignatureMemberUniqueIdentifier,TSignature> Members

        public TSignatureParent Parent
        {
            get { return (TSignatureParent) (object) ownerInfo; }
        }

        #endregion
    }
}
