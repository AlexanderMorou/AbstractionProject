using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a dictionary of
    /// <typeparamref name="TIntermediateParameter"/> instances.
    /// </summary>
    /// <typeparam name="TParent">The type which parents the <typeparamref name="TParameter"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type which parents the <typeparamref name="TIntermediateParameter"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TParameter">The type of parameter in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParameter">The type of parameter in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> :
        IIntermediateMemberDictionary<TParent, TIntermediateParent, IGeneralMemberUniqueIdentifier, TParameter, TIntermediateParameter>,
        IParameterMemberDictionary<TParent, TParameter>
        where TParent :
            IParameterParent<TParent, TParameter>
        where TIntermediateParent :
            TParent,
            IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>
        where TParameter :
            IParameterMember<TParent>
        where TIntermediateParameter :
            TParameter,
            IIntermediateParameterMember<TParent, TIntermediateParent>
    {
        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateParameter"/> instance
        /// with the <paramref name="name"/> and <paramref name="parameterType"/>
        /// provided.
        /// </summary>
        /// <param name="name">The name of the parameter to add.</param>
        /// <param name="parameterType">The type of the parameter to add.</param>
        /// <returns>A new <typeparamref name="TIntermediateParameter"/>
        /// as it exists in the <see cref="IIntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</returns>
        TIntermediateParameter Add(string name, IType parameterType);

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateParameter"/> instance
        /// with the <paramref name="name"/>, <paramref name="parameterType"/> and
        /// <paramref name="direction"/>
        /// provided.
        /// </summary>
        /// <param name="name">The name of the parameter to add.</param>
        /// <param name="parameterType">The type of the parameter to add.</param>
        /// <param name="direction">The direction in which the <typeparamref name="TIntermediateParameter"/>
        /// is coerced.</param>
        /// <returns>A new <typeparamref name="TIntermediateParameter"/>
        /// as it exists in the <see cref="IIntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</returns>
        TIntermediateParameter Add(string name, IType parameterType, ParameterCoercionDirection direction);

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateParameter"/> instance
        /// with the <paramref name="parameterInfo"/> provided.
        /// </summary>
        /// <param name="parameterInfo">The <see cref="TypedName"/>
        /// which denotes the name, type and direction of the parameter.</param>
        /// <returns>The <typeparamref name="TIntermediateParameter"/> instance
        /// resulted from the add operation.</returns>
        TIntermediateParameter Add(TypedName parameterInfo);

        /// <summary>
        /// Adds a series of <typeparamref name="TIntermediateParameter"/>
        /// instances from the <paramref name="parameterInfo"/> provided.
        /// </summary>
        /// <param name="parameterInfo">The <see cref="TypedName"/>
        /// series which denotes the name and direction of each element to
        /// insert.</param>
        /// <returns>A series of <typeparamref name="TIntermediateParameter"/>
        /// instances created from the <paramref name="parameterInfo"/>
        /// provided.</returns>
        TIntermediateParameter[] AddRange(params TypedName[] parameterInfo);
        /// <summary>
        /// Removes the <paramref name="parameter"/> provided from the 
        /// <see cref="IIntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.
        /// </summary>
        /// <param name="parameter">The <typeparamref name="TIntermediateParameter"/>
        /// to remove from the 
        /// <see cref="IIntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</param>
        /// <returns></returns>
        bool Remove(TIntermediateParameter parameter);
        /// <summary>
        /// Obtains the <typeparamref name="TIntermediateParameter"/> by the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the 
        /// name of the parameter to retrieve.</param>
        /// <returns>A <typeparamref name="TIntermediateParameter"/> by the
        /// <paramref name="name"/> provided.</returns>
        TIntermediateParameter this[string name] { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a dictionary of
    /// <see cref="IIntermediateParameterMember"/> instances.
    /// </summary>
    public interface IIntermediateParameterMemberDictionary :
        IIntermediateMemberDictionary,
        IParameterMemberDictionary
    {
        /// <summary>
        /// Returns the <see cref="IIntermediateParameterParent"/> which owns 
        /// the <see cref="IIntermediateParameterMemberDictionary"/>.
        /// </summary>
        new IIntermediateParameterParent Parent { get; }

        /// <summary>
        /// Adds a new <see cref="IIntermediateParameterMember"/> instance
        /// with the <paramref name="name"/> and <paramref name="parameterType"/>
        /// provided.
        /// </summary>
        /// <param name="name">The name of the parameter to add.</param>
        /// <param name="parameterType">The type of the parameter to add.</param>
        /// <returns>A new <see cref="IIntermediateParameterMember"/>
        /// as it exists in the <see cref="IIntermediateParameterMemberDictionary"/>.</returns>
        IIntermediateParameterMember Add(string name, IType parameterType);

        /// <summary>
        /// Adds a new <see cref="IIntermediateParameterMember"/> instance
        /// with the <paramref name="name"/>, <paramref name="parameterType"/> and 
        /// <paramref name="direction"/>
        /// </summary>
        /// <param name="name">The name of the parameter to add.</param>
        /// <param name="parameterType">The type of the parameter to add.</param>
        /// <param name="direction">The direction in which the <see cref="IIntermediateParameterMember"/>
        /// is coerced.</param>
        /// <returns>A new <see cref="IIntermediateParameterMember"/>
        /// as it exists in the <see cref="IIntermediateParameterMemberDictionary"/>.</returns>
        IIntermediateParameterMember Add(string name, IType parameterType, ParameterCoercionDirection direction);
        /// <summary>
        /// Adds a series of <see cref="IIntermediateParameterMember"/>
        /// instances from the <paramref name="parameterInfo"/> provided.
        /// </summary>
        /// <param name="parameterInfo">The <see cref="TypedName"/>
        /// series which denotes the name and direction of each element to
        /// insert.</param>
        /// <returns>A series of <see cref="IIntermediateParameterMember"/>
        /// instances created from the <paramref name="parameterInfo"/>
        /// provided.</returns>
        IIntermediateParameterMember[] AddRange(params TypedName[] parameterInfo);

        /// <summary>
        /// Adds a new <see cref="IIntermediateParameterMember"/> instance
        /// with the <paramref name="parameterInfo"/> provided.
        /// </summary>
        /// <param name="parameterInfo">The <see cref="TypedName"/>
        /// which denotes the name, type and direction of the parameter.</param>
        /// <returns>The <see cref="IIntermediateParameterMember"/> instance
        /// resulted from the add operation.</returns>
        IIntermediateParameterMember Add(TypedName parameterInfo);

        bool Remove(IIntermediateParameterMember parameter);

        /// <summary>
        /// Obtains the <see cref="IIntermediateParameterMember"/> by the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the 
        /// name of the parameter to retrieve.</param>
        /// <returns>A <see cref="IIntermediateParameterMember"/> by the
        /// <paramref name="name"/> provided.</returns>
        IIntermediateParameterMember this[string name] { get; }

    }
}
