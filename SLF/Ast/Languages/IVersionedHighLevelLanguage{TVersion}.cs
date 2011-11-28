using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a versioned high level
    /// language.
    /// </summary>
    /// <typeparam name="TVersion">The kind of entity used to represent the
    /// language's various versions.</typeparam>
    /// <typeparam name="TRootNode">The type concrete node in the syntax tree
    /// of the language which denotes the root entry for a given parse.</typeparam>
    public interface IVersionedHighLevelLanguage<TVersion, TRootNode> :
        IHighLevelLanguage<TRootNode>,
        IVersionedLanguage<TVersion>
        where TRootNode :
            IConcreteNode
    {
        /// <summary>
        /// Returns a new <see cref="IHighLevelLanguageProvider{TRootNode}"/> associated to the current
        /// <see cref="IHighLevelLanguage{TRootNode}"/>.
        /// </summary>
        /// <param name="version">The <typeparamref name="TVersion"/>
        /// value which denotes what version of the language to return 
        /// the provider for.</param>
        /// <returns>A new <see cref="IHighLevelLanguageProvider{TRootNode}"/> for the current
        /// <see cref="IHighLevelLanguage{TRootNode}"/>.</returns>
        new IVersionedHighLevelLanguageProvider<TVersion, TRootNode> GetProvider(TVersion version);
        /// <summary>
        /// Creates a new <see cref="IIntermediateAssembly"/>
        /// with the <paramref name="name"/> and 
        /// <paramref name="version"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <param name="version">The <typeparamref name="TVersion"/>
        /// of the language to which the <see cref="IIntermediateAssembly"/>
        /// is built against.</param>
        /// <returns>A new <see cref="IIntermediateAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>
        /// or <paramref name="version"/> is out of the values allowed.</exception>
        IIntermediateAssembly CreateAssembly(string name, TVersion version);
    }
}
