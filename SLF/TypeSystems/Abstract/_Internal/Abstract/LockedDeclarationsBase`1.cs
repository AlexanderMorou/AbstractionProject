using System;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


/* *
 * The reason for creating a duplicate hierarcy of 
 * DeclarationBase derived classes is to ensure that 
 * there's little chance for someone to accidentally
 * enter a member that doesn't exist on a compiled type.  
 * This is important for the linking the system 
 * provides.
 * */
namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    /// <summary>
    /// Defines properties and methods for an <see cref="IDeclarationDictionary{TItem}"/> that is locked.
    /// </summary>
    /// <typeparam name="TItem">The type of <see cref="IDeclaration"/> contained by the 
    /// <see cref="LockedDeclarationsBase{TItem}"/>.</typeparam>
    internal partial class LockedDeclarationsBase<TItem> :
        ControlledStateDictionary<string, TItem>,
        IDeclarationDictionary<TItem>,
        IDeclarationDictionary
        where TItem :
            IDeclaration
    {
        /// <summary>
        /// Creates a new <see cref="LockedDeclarationsBase{TItem}"/> with the <paramref name="items"/>
        /// to contain.
        /// </summary>
        /// <param name="items">The <see cref="IDictionary{TKey, TValue}"/> 
        /// to encapsulate.</param>
        internal LockedDeclarationsBase(LockedDeclarationsBase<TItem> sibling)
            : base(sibling)
        {
        }

        /// <summary>
        /// Creates a new <see cref="LockedDeclarationsBase{TItem}"/> 
        /// initialized to a default state.
        /// </summary>
        internal LockedDeclarationsBase()
            : base()
        {
        }

        /// <summary>
        /// Creates a new <see cref="LockedDeclarationsBase{TItem}"/> with the <paramref name="target"/>
        /// to contain.
        /// </summary>
        /// <param name="target">The <see cref="IEnumerable{T}"/> of <typeparamref name="TItem"/> instances
        /// the <see cref="LockedDeclarationsBase{TItem}"/> will contain.</param>
        internal LockedDeclarationsBase(IEnumerable<TItem> target)
            : base()
        {
            foreach (TItem ti in target)
                base._Add(ti.UniqueIdentifier, ti);
        }

        #region IDictionary<string,TItem> Members

        /// <summary>
        /// Adds an element of the provided <paramref name="key"/> and <paramref name="value"/> to the
        /// <see cref="LockedDeclarationsBase{TItem}"/>.
        /// </summary>
        /// <param name="key">The <see cref="IDeclaration.UniqueIdentifier"/> of the current <paramref name="value"/></param>
        /// <param name="value">The <typeparamref name="TItem"/> to insert.</param>
        /// <exception cref="System.NotSupportedException">
        /// The <see cref="LockedDeclarationsBase{TItem}"/> does not
        /// support modification.</exception>
        protected internal override void _Add(string key, TItem value)
        {
            throw new NotSupportedException("Declarations locked.");
        }

        internal void _AddInternal(string key, TItem value)
        {
            base._Add(key, value);
        }

        /// <summary>
        /// Removes an element with the specified <paramref name="index"/> from the 
        /// <see cref="LockedDeclarationsBase{TItem}"/>.
        /// </summary>
        /// <param name="key">The <see cref="Int32"/> ordinal index of 
        /// the <typeparamref name="TItem"/> to remove.</param>
        /// <returns>true if the element was successfully removed; false otherwise.</returns>
        /// <exception cref="System.NotSupportedException">
        /// The <see cref="LockedDeclarationsBase{TItem}"/> does 
        /// not support modification.</exception>
        protected internal override bool _Remove(int index)
        {
            throw new NotSupportedException("Declarations locked.");
        }


        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            this.Values.OnAll(f => f.Dispose());
            this._Clear();
        }

        #endregion

        #region IDeclarationDictionary Members

        int IDeclarationDictionary.IndexOf(IDeclaration decl)
        {
            if (!(decl is TItem))
                return -1;
            return this.IndexOf(((TItem)(decl)));
        }

        #endregion

        public int IndexOf(TItem decl)
        {
            int index = 0;
            foreach (var item in this.Values)
                if (object.ReferenceEquals(decl, item))
                    return index;
                else
                    index++;
            return -1;
        }
    }
}
