using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
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
    /// Provides a base implementation of generic <see cref="IDeclarationDictionary{TItem}"/> for working with a series of <typeparamref name="TItem"/> 
    /// instances.
    /// </summary>
    /// <typeparam name="TItem">The type of <see cref="IDeclaration"/> in the current
    /// implementation.</typeparam>
    /// <typeparam name="TMItem">The base type of <typeparamref name="TItem"/>.</typeparam>
    internal partial class GroupedDeclarationDictionaryBase<TItem, TMItem> :
        SubordinateDictionary<string, TItem, TMItem>,
        IGroupedDeclarationDictionary<TItem>,
        IGroupedDeclarationDictionary
        where TItem :
            TMItem
        where TMItem :
            class,
            IDeclaration
    {
        /// <summary>
        /// Creates a new <see cref="GroupedDeclarationDictionaryBase{TItem, TMItem}"/> 
        /// with the <paramref name="master"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="FullDeclarationDictionaryBase{TMItem}"/>
        /// in which the <see cref="GroupedDeclarationDictionaryBase{TItem, TMItem}"/> 
        /// synchronizes with.</param>
        public GroupedDeclarationDictionaryBase(FullDeclarationDictionaryBase<TMItem> master)
            : base(master)
        {

        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            base._Clear();
        }

        #endregion

        #region IDeclarationDictionary Members

        int IDeclarationDictionary.IndexOf(IDeclaration decl)
        {
            if (decl is TItem)
                return this.IndexOf(((TItem)(decl)));
            return -1;
        }

        #endregion

        public int IndexOf(TItem decl)
        {
            int index =0;
            foreach (var item in this.Values)
                if (object.ReferenceEquals(item, decl))
                    return index;
                else
                    index++;
            return -1;
        }
    }
}
