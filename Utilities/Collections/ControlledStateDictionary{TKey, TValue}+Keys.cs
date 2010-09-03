using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class ControlledStateDictionary<TKey, TValue>
    {
        /// <summary>
        /// Represents the collection of keys in a <see cref="ControlledStateDictionary{TKey,TKey}"/>.
        /// </summary>
        protected internal class KeysCollection :
            ControlledStateCollection<TKey>
        {
            /// <summary>
            /// Creates a new <see cref="KeysCollection"/> instance
            /// with the <paramref name="baseCollection"/> to wrap
            /// provided.
            /// </summary>
            /// <param name="baseCollection">The <see cref="ICollection{T}"/>
            /// to wrap that contains the real data.</param>
            internal protected KeysCollection(ICollection<TKey> baseCollection)
                : base(baseCollection)
            {
            }
        }

    }
}
