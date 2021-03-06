﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _MethodSignatureMembersBase<TSignature, TSignatureParent> :
        _MethodSignatureMembersBase<IMethodSignatureParameterMember<TSignature, TSignatureParent>, TSignature, TSignatureParent, IMethodSignatureMemberDictionary<TSignature, TSignatureParent>>,
        IMethodSignatureMemberDictionary<TSignature, TSignatureParent>,
        IMethodSignatureMemberDictionary
        where TSignature :
            class,
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
    {
        protected _MethodSignatureMembersBase(_FullMembersBase master, IMethodSignatureMemberDictionary<TSignature, TSignatureParent> originalSet, TSignatureParent parent)
            : base(master, originalSet, parent)
        {
        }
    }
}
