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
    /// Defines properties and methods for working with a member that coerces the
    /// target's evaluation in an expression.
    /// </summary>
    /// <typeparam name="TCoercion">
    /// The type of <see cref="ICoercionMember"/> used 
    /// in the current implementation.</typeparam>
    /// <typeparam name="TCoercionParent">
    /// The type of coercible <see cref="IType{TTypeIdentifier, TType}"/> 
    /// that contains <typeparamref name="TCoercion"/> 
    /// members in the current implementation.</typeparam>
    public interface ICoercionMember<TCoercionIdentifier, TCoercion, TCoercionParent> :
        IMember<TCoercionIdentifier, TCoercionParent>,
        ICoercionMember
        where TCoercionIdentifier :
            IMemberUniqueIdentifier
        where TCoercion :
            ICoercionMember<TCoercionIdentifier, TCoercion, TCoercionParent>
        where TCoercionParent :
            ICoercibleType<TCoercionIdentifier, TCoercion, TCoercionParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with a member that coerces the
    /// target's evaluation in an expression.
    /// </summary>
    public interface ICoercionMember :
        IMember,
        IScopedDeclaration
    {
        /// <summary>
        /// Returns the parent of the <see cref="ICoercionMember"/>.
        /// </summary>
        new ICoercibleType Parent { get; }
    }
}
