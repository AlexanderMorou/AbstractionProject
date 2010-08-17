using System;
using System.Collections.Generic;
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
    /// Defines properties and methods for working with
    /// a series of assembly references.
    /// </summary>
    public interface IAssemblyReferenceCollection :
        IControlledStateCollection<IAssembly>
    {
        /// <summary>
        /// Adds an <paramref name="assembly"/> to the <see cref="IAssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="IAssembly"/> to add to the
        /// <see cref="IAssemblyReferenceCollection"/>.</param>
        void Add(IAssembly assembly);
        /// <summary>
        /// Adds a series of <paramref name="assemblies"/> to the
        /// <see cref="IAssemblyReferenceCollection"/>.
        /// </summary>
        /// <param name="assemblies">The <see cref="IAssembly"/> array to add to the 
        /// <see cref="IAssemblyReferenceCollection"/>.</param>
        void AddRange(params IAssembly[] assemblies);
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
    }
}
