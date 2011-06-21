using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Common;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Linkers
{
    /// <summary>
    /// Defines a series of <see cref="String"/>
    /// aliases for a <see cref="IAssemblyReference"/>
    /// </summary>
    public interface IAssemblyReferenceAliasCollection :
        IControlledStateCollection<string>
    {
        /// <summary>
        /// Adds an <paramref name="alias"/> to the <see cref="IAssemblyReferenceAliasCollection"/>.
        /// </summary>
        /// <param name="alias">The <see cref="String"/> value representing the
        /// alias to inject.</param>
        void Add(string alias);
        /// <summary>
        /// Removes a <see cref="String"/> <paramref name="alias"/> from the 
        /// <see cref="IAssemblyReferenceAliasCollection"/>.
        /// </summary>
        /// <param name="alias">The <see cref="String"/> value representing the
        /// alias to remove.</param>
        /// <returns>whether the <paramref name="alias"/>
        /// was removed.</returns>
        bool Remove(string alias);
        /// <summary>
        /// Adds a series of <see cref="String"/> <paramref name="aliases"/> 
        /// to the <see cref="IAssemblyReferenceAliasCollection"/>
        /// </summary>
        /// <param name="aliases">A series of <see cref="String"/>
        /// values representing the <paramref name="aliases"/>
        /// to insert.</param>
        void AddRange(params string[] aliases);
        /// <summary>
        /// Removes a series of <paramref name="aliases"/> from the 
        /// <see cref="IAssemblyReferenceAliasCollection"/>.
        /// </summary>
        /// <param name="aliases">The series of <see cref="String"/>
        /// <paramref name="aliases"/> to remove from the 
        /// <see cref="IAssemblyReferenceAliasCollection"/>.
        /// </param>
        /// <returns>A series of <see cref="Boolean"/> values
        /// which relate to which <paramref name="aliases"/>
        /// were removed.</returns>
        bool[] RemoveRange(params string[] aliases);
        /// <summary>
        /// Clears the <see cref="IAssemblyReferenceAliasCollection"/>.
        /// </summary>
        void Clear();
        /// <summary>
        /// Occurs when an alias is added.
        /// </summary>
        event EventHandler<EventArgsR1<string>> AliasAdded;
        /// <summary>
        /// Occurs when an alias is removed.
        /// </summary>
        event EventHandler<EventArgsR1<string>> AliasRemoved;
    }
}
