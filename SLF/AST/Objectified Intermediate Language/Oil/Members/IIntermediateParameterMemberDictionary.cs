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
        IIntermediateMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter>,
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
        TIntermediateParameter Add(string name, IType parameterType, ParameterDirection direction);

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
        IIntermediateParameterMember Add(string name, IType parameterType, ParameterDirection direction);

    }
}
