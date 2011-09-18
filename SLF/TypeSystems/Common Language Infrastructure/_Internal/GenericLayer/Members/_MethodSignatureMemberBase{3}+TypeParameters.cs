using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _MethodSignatureMemberBase<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParameter :
            class,
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
    {
        private class TypeParameterDictionary :
            GenericParameterDictionaryBase<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>
        {
            internal TypeParameterDictionary(_MethodSignatureMemberBase<TSignatureParameter, TSignature, TSignatureParent> parent)
                : base(parent)
            {

            }
        }
    }
}
