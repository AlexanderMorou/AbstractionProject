using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
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
        protected class ParameterDictionary :
            IntermediateParameterMemberDictionary<TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>>
        {
            
            public ParameterDictionary(TIntermediateCtor parent)
                : base(parent)
            {
            }

            protected override IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType> GetNewParameter(string name, IType parameterType, ParameterDirection direction)
            {
                Parameter p = new Parameter(Parent);
                p.Name = name;
                p.Direction = direction;
                p.ParameterType = parameterType;
                return p;
            }
        }
    }
}
