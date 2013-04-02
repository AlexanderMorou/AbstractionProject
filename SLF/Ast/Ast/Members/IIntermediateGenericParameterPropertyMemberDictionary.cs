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
    /// Defines generic properties and methods for working with a series of properties defined
    /// on an intermediate generic parameter.
    /// </summary>
    /// <typeparam name="TGenericParameter">The type of generic parameter in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateGenericParameter">The type of generic parameter in
    /// the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateGenericParameterPropertyMemberDictionary<TGenericParameter, TIntermediateGenericParameter> :
        IIntermediatePropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
        IGenericParameterPropertyMemberDictionary<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
        where TIntermediateGenericParameter :
            TGenericParameter,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with a series of properties defined
    /// on an intermediate generic parameter.
    /// </summary>
    public interface IIntermediateGenericParameterPropertyMemberDictionary :
        IIntermediatePropertySignatureMemberDictionary,
        IGenericParameterPropertyMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateGenericParameter"/> which contains
        /// the <see cref="IIntermediateGenericParameterPropertyMemberDictionary"/>.
        /// </summary>
        new IIntermediateGenericParameter Parent { get; }
    }
}
