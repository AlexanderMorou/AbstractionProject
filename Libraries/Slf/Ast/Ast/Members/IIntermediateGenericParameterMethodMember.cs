using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a generic parameter's
    /// method member.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateGenericParameter">The type of generic parameter in
    /// the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter> :
        IIntermediateMethodSignatureMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IGenericParameterMethodMember<TGenericParameter>,
        IIntermediateGenericParameterMethodMember
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
        where TIntermediateGenericParameter :
            TGenericParameter,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a generic parameter's
    /// method member.
    /// </summary>
    public interface IIntermediateGenericParameterMethodMember :
        IIntermediateMethodSignatureMember,
        IGenericParameterMethodMember
    {
    }
}
