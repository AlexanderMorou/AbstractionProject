using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateConstructorSignatureMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableType<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableSignatureType<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
        protected class Parameter :
            IntermediateSignatureParameterMemberBase<TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>, TType, TIntermediateType>,
            IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        {
            /// <summary>
            /// Creates a new <see cref="Parameter"/> with the <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateCtor"/> which
            /// contains the <see cref="Parameter"/>.</param>
            public Parameter(TIntermediateCtor parent)
                : base(parent)
            {
            }

            
        }
    }
}
