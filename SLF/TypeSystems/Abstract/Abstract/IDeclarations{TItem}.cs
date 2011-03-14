using System;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of <typeparamref name="TItem"/> 
    /// instances.
    /// </summary>
    /// <typeparam name="TItem">The type of <see cref="IDeclaration"/> in the current
    /// implementation.</typeparam>
    public interface IDeclarationDictionary<TItem> :
        IControlledStateDictionary<string, TItem>,
        IDisposable
        where TItem :
            IDeclaration
    {
        /// <summary>
        /// Returns the index of the <paramref name="decl"/> provided.
        /// </summary>
        /// <param name="decl">The <typeparamref name="TItem"/> in the <see cref="IDeclarationDictionary{TItem}"/> to return
        /// the index of.</param>
        /// <returns>An <see cref="Int32"/> value representing the index of the <paramref name="decl"/> in the
        /// <see cref="IDeclarationDictionary{TItem}"/>, if present; -1, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="decl"/> is null.</exception>
        int IndexOf(TItem decl);
    }
}
