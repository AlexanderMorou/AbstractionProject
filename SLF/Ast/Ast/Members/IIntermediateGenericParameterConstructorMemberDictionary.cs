using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of
    /// constructor members for an intermediate generic parameter.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateGenericParameter">The type of generic parameter
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGenericParameterConstructorMemberDictionary<TGenericParameter, TIntermediateGenericParameter> :
        IIntermediateConstructorSignatureMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IGenericParameterConstructorMemberDictionary<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
        where TIntermediateGenericParameter :
            TGenericParameter,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>
    {
        /// <summary>
        /// Returns the <typeparamref name="TIntermediateGenericParameter"/> which owns the <see cref="IIntermediateGenericParameterConstructorMember{TGenericParameter, TIntermediateGenericParameter}"/> series.
        /// </summary>
        new TIntermediateGenericParameter Parent { get; }
    }
    /// <summary>
    /// Defines generic properties and methods for working with a series of
    /// constructor members for an intermediate generic parameter.
    /// </summary>
    public interface IIntermediateGenericParameterConstructorMemberDictionary :
        IIntermediateConstructorSignatureMemberDictionary,
        IGenericParameterConstructorMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateGenericParameter"/> which owns the <see cref="IIntermediateGenericParameterConstructorMember"/> series.
        /// </summary>
        new IIntermediateGenericParameter Parent { get; }
    }
}
