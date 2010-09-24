using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with a method parent.
    /// </summary>
    /// <typeparam name="TMethod">The type of <see cref="IMethodMember{TMethod, TMethodParent}"/> in the 
    /// current implementation</typeparam>
    /// <typeparam name="TParent">The type of <see cref="IMethodParent{TMethod, TParent}"/> that contains
    /// the <typeparamref name="TMethod"/> instances in the current implementation.</typeparam>
    public interface IMethodParent<TMethod, TParent> :
        ISignatureParent<TMethod, IMethodParameterMember<TMethod, TParent>, TParent>,
        IMethodParent
        where TMethod :
            IMethodMember<TMethod, TParent>
        where TParent :
            IMethodParent<TMethod, TParent>
    {
        /// <summary>
        /// Returns the methods defined on the <typeparamref name="TParent"/>.
        /// </summary>
        new IMethodMemberDictionary<TMethod, TParent> Methods { get; }
    }
}
