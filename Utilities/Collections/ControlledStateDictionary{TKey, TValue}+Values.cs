using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright � 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Utilities.Collections
{
    partial class ControlledStateDictionary<TKey, TValue>
    {
        /// <summary>
        /// Represents the collection of values in a <see cref="ControlledStateDictionary{TKey,TValue}"/>.
        /// </summary>
        protected internal class ValuesCollection :
            ControlledStateCollection<TValue>
        {
            /// <summary>
            /// Creates a new <see cref="ValuesCollection"/> instance
            /// with the <paramref name="baseCollection"/> to wrap
            /// provided.
            /// </summary>
            /// <param name="baseCollection">The <see cref="ICollection{T}"/>
            /// to wrap that contains the real data.</param>
            internal protected ValuesCollection(ICollection<TValue> baseCollection)
                : base(baseCollection)
            {
            }
        }
    }
}