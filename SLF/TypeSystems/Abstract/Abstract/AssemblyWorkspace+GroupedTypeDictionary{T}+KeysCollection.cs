using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class AssemblyWorkspace
    {
        partial class GroupedTypeDictionary<TType>
            where TType :
                class,
                IType<TType>
        {
            /// <summary>
            /// Provides a keys collection for a series of grouped types.
            /// </summary>
            protected internal new class KeysCollection :
                SubordinateDictionary<string, TType, IType>.KeysCollection,
                IControlledStateCollection<string>,
                IControlledStateCollection
            {
                /// <summary>
                /// Creates a new <see cref="KeysCollection"/> associated
                /// to the <paramref name="owner"/> provided.
                /// </summary>
                /// <param name="owner">The <see cref="GroupedTypeDictionary{TType}"/>
                /// which owns the current <see cref="KeysCollection"/>.</param>
                protected internal KeysCollection(GroupedTypeDictionary<TType> owner)
                    : base(owner.groups.Keys)
                {
                }
            }
        }
    }
}
