using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base full declaration dictionary.
    /// </summary>
    /// <typeparam name="TIdentifier">The kind of identifier used to differentiate the <typeparamref name="TDeclaration"/>
    /// instances from one another.</typeparam>
    /// <typeparam name="TDeclaration">The type of declaration in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateDeclaration">The type of declaration in the intermediate
    /// abstract syntax tree.</typeparam>
    public abstract partial class IntermediateFullDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> :
        MasterDictionaryBase<TIdentifier, TDeclaration>,
        IIntermediateFullDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration>
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>
        where TDeclaration :
            class,
            IDeclaration
        where TIntermediateDeclaration :
            class,
            TDeclaration,
            IIntermediateDeclaration
    {
        private ValuesCollection values;
        #region IIntermediateFullDeclarationDictionary<TDeclaration,TIntermediateDeclaration> Members

        public new IControlledStateCollection<MasterDictionaryEntry<TIntermediateDeclaration>> Values
        {
            get
            {
                if (this.values == null)
                    this.values = new ValuesCollection(this);
                return this.values;
            }
        }

        public new IEnumerator<KeyValuePair<TIdentifier, MasterDictionaryEntry<TIntermediateDeclaration>>> GetEnumerator()
        {
            foreach (var item in ((IEnumerable<KeyValuePair<TIdentifier, MasterDictionaryEntry<TDeclaration>>>)(this)))
                yield return new KeyValuePair<TIdentifier, MasterDictionaryEntry<TIntermediateDeclaration>>(item.Key, new MasterDictionaryEntry<TIntermediateDeclaration>(item.Value.Subordinate, ((TIntermediateDeclaration)(item.Value.Entry))));
            yield break;
        }

        #endregion

        /// <summary>
        /// Creates a new <see cref="IntermediateFullDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>
        /// initialized to a default state.
        /// </summary>
        public IntermediateFullDeclarationDictionary()
            : base()
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateFullDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/>
        /// with the <paramref name="sibling"/> provided.
        /// </summary>
        /// <param name="sibling">The <see cref="IntermediateFullDeclarationDictionary{TIdentifier, TDeclaration, TIntermediateDeclaration}"/> 
        /// as the sibling of the active instance.</param>
        /// <remarks>Siblings contain the same underlying data 
        /// for their dictionary.</remarks>
        public IntermediateFullDeclarationDictionary(IntermediateFullDeclarationDictionary<TIdentifier, TDeclaration, TIntermediateDeclaration> sibling)
            : base(sibling)
        {
        }


    }
}
