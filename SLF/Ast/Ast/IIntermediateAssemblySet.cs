using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with a set of 
    /// intermediate assemblies.
    /// </summary>
    public interface IIntermediateAssemblySet :
        IIntermediateDeclarationDictionary<IAssemblyUniqueIdentifier, IAssembly, IIntermediateAssembly>
    {
        /// <summary>
        /// Adds a <typeparamref name="TAssembly"/> instance
        /// to the <see cref="IIntermediateAssemblySet"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <typeparam name="TAssembly">The type of <see cref="IIntermediateAssembly"/>
        /// to add to the <see cref="IIntermediateAssemblySet"/>.</typeparam>
        /// <param name="name">The <see cref="String"/> name of the <typeparamref name="TAssembly"/>
        /// to insert.</param>
        /// <returns>A new <typeparamref name="TAssembly"/> instance.</returns>
        /// <exception cref="System.ArgumentException">thrown when the
        /// type <typeparamref name="TAssembly"/> does not have a public
        /// constructor which accepts a <see cref="String"/> value.</exception>
        /// <remarks><para>The resulted instance is a part of a new <typeparamref name="TAssembly"/>
        /// instance created.</para><para>Implementors should use 
        /// <see cref="IntermediateGateway.CreateAssembly{T}(string)"/> for 
        /// consistent functionality.</para></remarks>
        TAssembly Add<TAssembly>(string name)
            where TAssembly :
                IIntermediateAssembly;
        IIntermediateAssembly Add(string name);
        void Add(IIntermediateAssembly assembly);
    }
}
