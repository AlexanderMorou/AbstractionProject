using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a <see cref="IMember"/> which contains a 
    /// specific type of <see cref="IMember{TParent}.Parent"/> (<typeparamref name="TParent"/>).
    /// </summary>
    /// <typeparam name="TParent">The type of <see cref="IMemberParent"/>, in the current implementation, which contains the <see cref="IMember{TParent}"/>.</typeparam>
    public interface IMember<TIdentifier, TParent> :
        IDeclaration<TIdentifier>,
        IMember
        where TIdentifier :
            IMemberUniqueIdentifier
        where TParent :
            IMemberParent
    {
        /// <summary>
        /// Returns the parent of the <see cref="IMember{TParent}"/>.
        /// </summary>
        new TParent Parent { get; }
        new TIdentifier UniqueIdentifier { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a general case member.
    /// </summary>
    public interface IMember :
        IDeclaration
    {
        /// <summary>
        /// Returns the parent of the <see cref="IMember"/>.
        /// </summary>
        IMemberParent Parent { get; }
        /// <summary>
        /// Returns the unique identifier for the current
        /// <see cref="IMember"/> in its general case form.
        /// </summary>
        IMemberUniqueIdentifier UniqueIdentifier { get; }
    }
}
