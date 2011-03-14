using System;
using System.Collections.Generic;
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
    /// Defines properties and methods for working with
    /// a series of assembly references and their potential alias.
    /// </summary>
    public interface IAssemblyReferenceCollection :
        IControlledStateCollection<IAssemblyReference>
    {
        /// <summary>
        /// Adds an <paramref name="assembly"/> to the <see cref="IAssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="IAssembly"/> to add to the
        /// <see cref="IAssemblyReferenceCollection"/>.</param>
        IAssemblyReference Add(IAssembly assembly);
        /// <summary>
        /// Adds an <paramref name="assembly"/> to the <see cref="IAssemblyReferenceCollection"/>
        /// with the <paramref name="aliases"/> provided.
        /// </summary>
        /// <param name="aliases">The series of <see cref="String"/> values used to alias the <paramref name="assembly"/>
        /// provided.</param>
        /// <param name="assembly">The <see cref="IAssembly"/> to add to the
        /// <see cref="IAssemblyReferenceCollection"/>.</param>
        IAssemblyReference Add(IAssembly assembly, params string[] aliases);
        /// <summary>
        /// Adds a series of <paramref name="assemblies"/> to the
        /// <see cref="IAssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assemblies">The <see cref="IAssembly"/> array to add to the 
        /// <see cref="IAssemblyReferenceCollection"/>.</param>
        IAssemblyReference[] AddRange(params IAssembly[] assemblies);

        /// <summary>
        /// Adds a series of <see cref="IAssemblyReference"/> instances
        /// to the <see cref="IAssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="references">The <see cref="IEnumerable{T}"/>
        /// which contains the assembly references to add.</param>
        void AddRange(IEnumerable<IAssemblyReference> references);

        /// <summary>
        /// Removes an <paramref name="assembly"/> from the 
        /// <see cref="IAssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="IAssembly"/> to remove from the
        /// <see cref="IAssemblyReferenceCollection"/>.</param>
        void Remove(IAssembly assembly);
        /// <summary>
        /// Removes a series of <paramref name="assemblies"/>
        /// from the <see cref="IAssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assemblies">The series of <see cref="IAssembly"/> instances
        /// to remove from the <see cref="IAssemblyReferenceCollection"/>.</param>
        void RemoveRange(IAssembly[] assemblies);
        IAssemblyReference this[IAssembly target] { get; }
        /// <summary>
        /// Attempts to obtain a specific <paramref name="target"/> assembly's
        /// <see cref="IAssemblyReference"/>.
        /// </summary>
        /// <param name="target">The target <see cref="IAssembly"/>
        /// to attempt to retrieve the reference of.</param>
        /// <param name="value">The <see cref="IAssemblyReference"/> which denotes the 
        /// assembly and its respective aliases.</param>
        /// <returns>true, if the <paramref name="target"/> <see cref="IAssembly"/>
        /// has an associated <see cref="IAssemblyReference"/>; false, otherwise.</returns>
        bool TryGetValue(IAssembly target, out IAssemblyReference value);
    }
}
