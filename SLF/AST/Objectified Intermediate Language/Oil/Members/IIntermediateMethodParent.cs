using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with a parent to intermediate
    /// method instances.
    /// </summary>
    /// <typeparam name="TMethod">The type of methods in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMethod">The type of methods in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TParent">The type of parent that contains the <typeparamref name="TMethod"/> instances
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type of the parent that contains the <see cref="IIntermediateMethodMember{TMethod, TIntermediateMethod, TParent, TIntermediateParent}"/>.</typeparam>
    public interface IIntermediateMethodParent<TMethod, TIntermediateMethod, TParent, TIntermediateParent> :
        IIntermediateSignatureParent<TMethod, TIntermediateMethod, IMethodParameterMember<TMethod, TParent>, IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TParent, TIntermediateParent>, TParent, TIntermediateParent>,
        IIntermediateMethodParent,
        IMethodParent<TMethod, TParent>
        where TMethod :
            IMethodMember<TMethod, TParent>
        where TIntermediateMethod :
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TParent, TIntermediateParent>
        where TParent :
            IMethodParent<TMethod, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TParent, TIntermediateParent>
    {
        /// <summary>
        /// Returns the methods defined on the <typeparamref name="TIntermediateParent"/>.
        /// </summary>
        new IIntermediateMethodMemberDictionary<TMethod, TIntermediateMethod, TParent, TIntermediateParent> Methods { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a parent 
    /// to intermediate method members.
    /// </summary>
    public interface IIntermediateMethodParent :
        IIntermediateMemberParent,
        IMethodParent
    {
        /// <summary>
        /// Returns the intermediate methods defined on 
        /// the <see cref="IIntermediateMethodParent"/>.
        /// </summary>
        new IIntermediateMethodMemberDictionary Methods { get; }
    }
}
