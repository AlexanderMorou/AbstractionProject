using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliMethodSignatureBase<TSignatureParameter, TSignature, TSignatureParent> :
        IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        private ICliMetadataMethodDefinitionTableRow metadata;
        #region IMethodSignatureMember<TSignatureParameter,TSignature,TSignatureParent> Members

        public TSignature MakeGenericClosure(ITypeCollectionBase genericReplacements)
        {
            throw new NotImplementedException();
        }

        public TSignature GetGenericDefinition()
        {
            throw new NotImplementedException();
        }

        public TSignature MakeGenericClosure(params IType[] typeParameters)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IParameterParent<TSignature,TSignatureParameter> Members

        public IParameterMemberDictionary<TSignature, TSignatureParameter> Parameters
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IParameterParent Members

        IParameterMemberDictionary IParameterParent.Parameters
        {
            get { return (IParameterMemberDictionary) this.Parameters; }
        }

        public bool LastIsParams
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMember<IGeneralGenericSignatureMemberUniqueIdentifier,TSignatureParent> Members

        public TSignatureParent Parent
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier> Members

        public IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IDeclaration Members

        public string Name
        {
            get { return this.metadata.Name; }
        }

        IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
        {
            get { return this.UniqueIdentifier; }
        }

        public event EventHandler Disposed;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMember Members

        IMemberParent IMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IMethodSignatureMember Members

        public IType ReturnType
        {
            get { throw new NotImplementedException(); }
        }

        public IModifiersAndAttributesMetadata ReturnTypeMetadata
        {
            get { throw new NotImplementedException(); }
        }

        IMethodSignatureMember IMethodSignatureMember.GetGenericDefinition()
        {
            throw new NotImplementedException();
        }

        public ILockedTypeCollection GenericParameters
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMetadataEntity Members

        public IMetadataCollection CustomAttributes
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsDefined(IType metadatumType)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IGenericParamParent<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember> Members

        public IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> TypeParameters
        {
            get { throw new NotImplementedException(); }
        }

        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>.MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            throw new NotImplementedException();
        }

        IMethodSignatureMember IGenericParamParent<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>.MakeGenericClosure(params IType[] typeParameters)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IGenericParamParent Members

        public bool IsGenericDefinition
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsGenericConstruct
        {
            get { return this.metadata.TypeParameters.Count > 0; }
        }

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { return (IGenericParameterDictionary) this.TypeParameters; }
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        public bool ContainsGenericParameters
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
