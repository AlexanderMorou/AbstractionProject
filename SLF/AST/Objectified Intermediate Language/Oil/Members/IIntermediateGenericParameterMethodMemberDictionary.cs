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
    public interface IIntermediateGenericParameterMethodMemberDictionary<TGenericParameter, TIntermediateGenericParameter> :
        IIntermediateMethodSignatureMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IGenericParameterMethodMemberDictionary<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
        where TIntermediateGenericParameter :
            TGenericParameter,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with the method members defined
    /// on an intermediate generic parameter.
    /// </summary>
    public interface IIntermediateGenericParameterMethodMemberDictionary :
        IIntermediateMethodSignatureMemberDictionary,
        IGenericParameterMethodMemberDictionary
    {
    }
}