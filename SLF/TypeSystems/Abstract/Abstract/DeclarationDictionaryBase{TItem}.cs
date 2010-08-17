using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
    public class DeclarationDictionaryBase<TItem> :
        ControlledStateDictionary<string, TItem>,
        IDeclarationDictionary<TItem>,
        IDeclarationDictionary
        where TItem :
            IDeclaration
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
            foreach (var key in this.dictionaryCopy.Keys)
                this.dictionaryCopy[key].Dispose();
            this.dictionaryCopy.Clear();
            this.dictionaryCopy = null;
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
            if (this.valuesCollection == null)
                return -1;
            foreach (var item in this.Values)
                if (object.ReferenceEquals(item, decl))
                    return index;
                else
                    index++;
            return -1;
        }
    }
}
