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

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal partial class _GenericParameterMethodMemberBase<TGenericParameter> :
        _MethodSignatureMemberBase<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterMethodMember<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        public _GenericParameterMethodMemberBase(IGenericParameterMethodMember<TGenericParameter> original, IControlledTypeCollection genericReplacements)
            : base(original, genericReplacements)
        {
        }

        protected override IGenericParameterMethodMember<TGenericParameter> OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
        {
            throw new InvalidOperationException("Must be generic definition");
        }
    }
}
