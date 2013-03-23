using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Cst;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines generic properties and methods for working with a high level
    /// intermediate assembly with the <typeparamref name="TLanguage"/>,
    /// <typeparamref name="TRootNode"/> and <typeparamref name="TProvider"/>
    /// types that provide insight into the structure of the versioned
    /// language.
    /// </summary>
    /// <typeparam name="TLanguage">The type of 
    /// <see cref="IVersionedLanguage{TVersion}">language</see>
    /// which yields information and services pertinent to the 
    /// tools which build the assembly.</typeparam>
    /// <typeparam name="TRootNode">The <see cref="IConcreteNode"/>
    /// which represents the top-level node of the concrete syntax tree that results
    /// from a parse.</typeparam>
    /// <typeparam name="TProvider">The language service 
    /// <see cref="IVersionedLanguageProvider{TVersion}">provider</see>
    /// which yields a set of services to aid in compilation
    /// of the <see cref="IVersionedIntermediateAssembly{TLanguage, TProvider, TVersion}">assembly</see>.</typeparam>
    /// <typeparam name="TVersion">The kind of entity used to represent the
    /// <typeparamref name="TLanguage">language</typeparamref>'s various versions.</typeparam>
    public interface IVersionedIntermediateAssembly<TLanguage, TProvider, TVersion> :
        IIntermediateAssembly<TLanguage, TProvider>
        where TLanguage :
            IVersionedLanguage<TVersion>
        where TProvider :
            IVersionedLanguageProvider<TVersion>
    {
    }
}
