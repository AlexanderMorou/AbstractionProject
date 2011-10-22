using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with the parameter of a method member.
    /// </summary>
    /// <typeparam name="TMethod">The type of method member in the current implementation.</typeparam>
    /// <typeparam name="TMethodParent">The type of method parent in the current implementation.</typeparam>
    public interface IMethodParameterMember<TMethod, TMethodParent> :
        IMethodSignatureParameterMember<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IMethodParameterMember
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
    }

    /// <summary>
    /// Defines properties and methods for working with the parameter of a method member.
    /// </summary>
    public interface IMethodParameterMember :
        IMethodSignatureParameterMember
    {
        /// <summary>
        /// Returns the parent of the <see cref="IMethodParameterMember"/>.
        /// </summary>
        new IMethodMember Parent { get; }
    }
}
