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
        /// <summary>
        /// Returns the <see cref="IIntermediateGenericParameter"/> 
        /// which contains the 
        /// <see cref="IIntermediateGenericParameterMethodMemberDictionary"/>.
        /// </summary>
        new IIntermediateGenericParameter Parent { get; }

    }
}
