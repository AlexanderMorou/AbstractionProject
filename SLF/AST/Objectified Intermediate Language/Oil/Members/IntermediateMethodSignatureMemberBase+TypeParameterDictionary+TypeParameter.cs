using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
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
        partial class TypeParameterDictionary
        {
            private class TypeParameter :
                IntermediateGenericParameterBase<IMethodSignatureGenericTypeParameterMember, IIntermediateMethodSignatureGenericTypeParameterMember, IMethodSignatureMember, IIntermediateMethodSignatureMember>,
                IIntermediateMethodSignatureGenericTypeParameterMember
            {
                public TypeParameter(string name, IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> parent)
                    : base(name, parent)
                {
                }

                protected override bool Equals(IMethodSignatureGenericTypeParameterMember other)
                {
                    return object.ReferenceEquals(other, this);
                }

            }
        }
    }
}
