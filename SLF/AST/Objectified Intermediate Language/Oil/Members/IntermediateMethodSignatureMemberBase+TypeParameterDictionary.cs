using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TParent :
            ISignatureParent<TSignature, TSignatureParameter, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>
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

            protected override void Add(string key, IMethodSignatureGenericTypeParameterMember value)
            {
                Parent.OnTypeParameterAdded((IIntermediateMethodSignatureGenericTypeParameterMember)value);
                base.Add(key, value);
            }

            protected override bool RemoveImpl(string key)
            {
                if (base.ContainsKey(key))
                    Parent.OnTypeParameterRemoved(base[key]);
                return base.RemoveImpl(key);
            }


            #region IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember> Members

            IMethodSignatureMember IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember,IMethodSignatureMember>.Parent
            {
                get { return this.Parent; }
            }

            #endregion

            protected override IIntermediateMethodSignatureGenericTypeParameterMember GetNew(string name)
            {
                return new TypeParameter(name, this.Parent);
            }
        }
    }
}