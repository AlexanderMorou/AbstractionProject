using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base full declaration dictionary.
    /// </summary>
    /// <typeparam name="TDeclaration">The type of declaration in the abstract
    /// type system.</typeparam>
    /// <typeparam name="TIntermediateDeclaration">The type of declaration in the intermediate
    /// abstract syntax tree.</typeparam>
    public abstract partial class IntermediateFullDeclarationDictionary<TDeclaration, TIntermediateDeclaration> :
        MasterDictionaryBase<string, TDeclaration>,
        IIntermediateFullDeclarationDictionary<TDeclaration, TIntermediateDeclaration>
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

        public new IEnumerator<KeyValuePair<string, MasterDictionaryEntry<TIntermediateDeclaration>>> GetEnumerator()
        {
            foreach (var item in ((IEnumerable<KeyValuePair<string, MasterDictionaryEntry<TDeclaration>>>)(this)))
                yield return new KeyValuePair<string, MasterDictionaryEntry<TIntermediateDeclaration>>(item.Key, new MasterDictionaryEntry<TIntermediateDeclaration>(item.Value.Subordinate, ((TIntermediateDeclaration)(item.Value.Entry))));
            yield break;
        }

        #endregion

        /// <summary>
        /// Creates a new <see cref="IntermediateFullDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/>
        /// initialized to a default state.
        /// </summary>
        public IntermediateFullDeclarationDictionary()
            : base()
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateFullDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/>
        /// initialized to ecapsulate the <paramref name="target"/> provided.
        /// </summary>
        /// <param name="sibling">The <see cref="IntermediateFullDeclarationDictionary{TDeclaration, TIntermediateDeclaration}"/> 
        /// as the sibling of the active instance.</param>
        public IntermediateFullDeclarationDictionary(IntermediateFullDeclarationDictionary<TDeclaration, TIntermediateDeclaration> sibling)
            : base(sibling)
        {
        }


    }
}
