using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a method member.
    /// </summary>
    /// <typeparam name="TMethod">The type of method used in the current implementation.</typeparam>
    /// <typeparam name="TMethodParent">The type of <see cref="IMethodParent"/> that contains the
    /// <typeparamref name="TMethod"/> instances in the current implementation.</typeparam>
    public interface IMethodMember<TMethod, TMethodParent> :
        IMethodSignatureMember<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IMethodMember
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with a method.
    /// </summary>
    public interface IMethodMember :
        IScopedDeclaration,
        IMethodSignatureMember
    {

    }
}
