using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignatureParameter
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignature
        where TParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
        where TIntermediateParent :
            IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
            TParent
    {
        public partial class TypeParameterDictionary :
            IntermediateGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IIntermediateMethodSignatureGenericTypeParameterMember, IMethodSignatureMember, IIntermediateMethodSignatureMember>,
            IIntermediateGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IIntermediateMethodSignatureGenericTypeParameterMember, IMethodSignatureMember, IIntermediateMethodSignatureMember>,
            IIntermediateGenericParameterDictionary
        {
            protected new IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> Parent
            {
                get
                {
                    return ((IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>)(base.Parent));
                }
            }

            internal protected TypeParameterDictionary(IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> parent)
                : base(parent)
            {
            }
            protected internal override void _Add(IGenericParameterUniqueIdentifier key, IMethodSignatureGenericTypeParameterMember value)
            {
                base._Add(key, value);
                Parent.OnTypeParameterAdded((IIntermediateMethodSignatureGenericTypeParameterMember)value);
            }

            protected internal override bool _Remove(int index)
            {
                if (index < 0 || index >= this.Count)
                    return false;
                var result = base._Remove(index);
                if (result)
                    Parent.OnTypeParameterRemoved(base[index].Value);
                return result;
            }


            protected override void OnRearranged(GenericParameterMovedEventArgs e)
            {
                this.Parent.OnRearranged(e);
                base.OnRearranged(e);
            }

            #region IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember> Members

            IMethodSignatureMember IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember>.Parent
            {
                get { return this.Parent; }
            }

            #endregion

            protected override sealed IIntermediateMethodSignatureGenericTypeParameterMember GetNew(string name)
            {
                return new TypeParameter(name, this.Parent);
            }

            internal override IType Disambiguate(IType ambiguousType)
            {
                var parentType = this.Parent.Parent as IGenericType;
                var typeGenericParameters = parentType == null ? (IControlledTypeCollection)null : (IControlledTypeCollection)parentType.GenericParameters;
                return ambiguousType.Disambiguify(typeGenericParameters, this.Parent.GenericParameters, typeGenericParameters != null ? TypeParameterSources.Both : TypeParameterSources.Method);
            }
        }
    }
}
