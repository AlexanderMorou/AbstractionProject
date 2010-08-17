using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
        IntermediateFullDeclarationDictionary<IMember, IIntermediateMember>,
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
        public IntermediateFullMemberDictionary(IDictionary<string, MasterDictionaryEntry<IMember>> target)
            : base(target)
        {
        }
        #region IDisposable Members

        public void Dispose()
        {
            this.backup.Clear();
        }

        #endregion
    }
}
