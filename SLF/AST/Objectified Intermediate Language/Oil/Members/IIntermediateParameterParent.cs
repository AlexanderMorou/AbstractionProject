using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with the parent
    /// of a series of parameters.
    /// </summary>
    /// <typeparam name="TParent">The type which parents the <typeparamref name="TParameter"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type which parents the <typeparamref name="TIntermediateParameter"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TParameter">The type of parameter in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParameter">The type of parameter in the intermediate
    /// abstract syntax tree.</typeparam>
    public interface IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter> :
        IIntermediateParameterParent,
        IParameterParent<TParent, TParameter>
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
        /// Returns the dictionary of <typeparamref name="TIntermediateParameter"/> instances for the current <see cref="IIntermediateParameterParent{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>
        /// implementation.
        /// </summary>
        new IIntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> Parameters { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with the parent
    /// of a series of parameters.
    /// </summary>
    public interface IIntermediateParameterParent :
        IIntermediateMemberParent,
        IParameterParent
    {
        /// <summary>
        /// Returns the dictionary of <see cref="IParameterMember"/> instances for the current <see cref="IParameterParent"/>
        /// implementation.
        /// </summary>
        new IIntermediateParameterMemberDictionary Parameters { get; }
    }
}
