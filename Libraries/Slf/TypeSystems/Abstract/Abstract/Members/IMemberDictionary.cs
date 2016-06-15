using System;
using System.Collections;
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
    /// Defines generic properties and methods for working with a series of <typeparamref name="TItem"/> 
    /// instances as a member of a <typeparamref name="TParent"/> instance.
    /// </summary>
    /// <typeparam name="TParent">The type of the parent that contains the <typeparamref name="TItem"/>
    /// instances in the current implementation.</typeparam>
    /// <typeparam name="TItem">The type of <see cref="IMember{TParent}"/> contained within the 
    /// current <see cref="IMemberDictionary{TParent, TItem}"/> implementation.</typeparam>
    public interface IMemberDictionary<TParent, TItemIdentifier, TItem> :
        IDeclarationDictionary<TItemIdentifier, TItem>
        where TItemIdentifier :
            IMemberUniqueIdentifier
        where TParent :
            IMemberParent
        where TItem :
            IMember<TItemIdentifier, TParent>
    {
        /// <summary>
        /// Returns the <typeparamref name="TParent"/> which contains the <see cref="IMemberDictionary{TParent, TItem}"/>.
        /// </summary>
        TParent Parent { get; }
    }
    /// <summary>
    /// Defines properties and methods for working with a series of <see cref="IMember"/> instances.
    /// </summary>
    public interface IMemberDictionary :
        IDeclarationDictionary
    {
        /// <summary>
        /// Returns the <see cref="IMemberParent"/> which owns the <see cref="IMember"/> series.
        /// </summary>
        IMemberParent Parent { get; }

        /// <summary>
        /// Returns the index of the <paramref name="member"/> provided.
        /// </summary>
        /// <param name="member">The <see cref="IDeclaration"/> in the <see cref="IMemberDictionary"/> to return
        /// the index of.</param>
        /// <returns>An <see cref="Int32"/> value representing the index of the <paramref name="member"/> in the
        /// <see cref="IMemberDictionary"/>, if present; -1, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="member"/> is null.</exception>
        int IndexOf(IMember member);
    }
}
