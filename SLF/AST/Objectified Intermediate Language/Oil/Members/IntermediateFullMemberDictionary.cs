using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base full intermediate members dictionary
    /// that merges a series of grouped member dictionaries
    /// to provide a verbatim look at the members as they were created
    /// irregardless of type.
    /// </summary>
    [DebuggerDisplay("Members: {Count}")]
    public class IntermediateFullMemberDictionary :
        IntermediateFullDeclarationDictionary<IGeneralMemberUniqueIdentifier, IMember, IIntermediateMember>,
        IIntermediateFullMemberDictionary
    {
        /// <summary>
        /// Initializes a <see cref="IntermediateFullMemberDictionary"/> to its default state.
        /// </summary>
        public IntermediateFullMemberDictionary()
            : base()
        {
        }
        /// <summary>
        /// Initializes a <see cref="IntermediateFullMemberDictionary"/> to its default state.
        /// </summary>
        /// <param name="target">The <see cref="IDictionary{TKey, TValue}"/> which contains the target
        /// dictonary the <see cref="IntermediateFullMemberDictionary"/>
        /// encapsulates</param>
        public IntermediateFullMemberDictionary(IntermediateFullMemberDictionary target)
            : base(target)
        {
        }
        #region IDisposable Members

        public void Dispose()
        {
            this._Clear();
        }

        #endregion

        internal void ConditionalRemove(IIntermediateMemberParent parent)
        {
            this._RemoveSet(from element in this
                            where element.Value.Entry.Parent == parent
                            select element.Key);
        }
    }
}
