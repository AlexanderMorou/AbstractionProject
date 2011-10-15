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
    /// Provides a base declaration dictionary base class.
    /// </summary>
    /// <typeparam name="TItem">The type of <see cref="IDeclaration"/> the
    /// <see cref="DeclarationDictionaryBase{TItem}"/> is used for.</typeparam>
    public class DeclarationDictionaryBase<TItemIdentifier, TItem> :
        ControlledStateDictionary<TItemIdentifier, TItem>,
        IDeclarationDictionary<TItemIdentifier, TItem>,
        IDeclarationDictionary
        where TItemIdentifier :
            IDeclarationUniqueIdentifier<TItemIdentifier>
        where TItem :
            IDeclaration<TItemIdentifier>
    {
        /// <summary>
        /// Creates a new <see cref="DeclarationDictionaryBase{TItem}"/> 
        /// initialized to a default state.
        /// </summary>
        public DeclarationDictionaryBase()
            : base()
        {

        }

        #region IDisposable Members

        /// <summary>
        /// Disposes the current <see cref="DeclarationDictionaryBase{TITem}"/>
        /// </summary>
        public virtual void Dispose()
        {
            var values = this.Values.ToArray();
            foreach (var value in values)
                value.Dispose();
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

        /// <summary>
        /// Returns the <see cref="Int32"/> value of the zero-based
        /// index of the <paramref name="decl"/> contained
        /// within the <see cref="DeclarationDictionaryBase{TItem}"/>.
        /// </summary>
        /// <param name="decl">The <typeparamref name="TItem"/>
        /// element to return the zero-based index of.</param>
        /// <returns>An <see cref="Int32"/> value of the zero-based
        /// index of the <paramref name="decl"/> provided; or -1
        /// if the <paramref name="decl"/> does not exist
        /// within the <see cref="DeclarationDictionaryBase{TItem}"/>.</returns>
        public int IndexOf(TItem decl)
        {
            int index = 0;
            if (this.valuesInstance == null)
                return -1;
            return this.valuesInstance.IndexOf(decl);
            //foreach (var item in this.Values)
            //    if (object.ReferenceEquals(item, decl))
            //        return index;
            //    else
            //        index++;
            //return -1;
        }
    }
}
