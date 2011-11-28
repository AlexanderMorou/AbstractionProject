using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Languages;
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
    /// types that provide insight into the structure of the language.
    /// </summary>
    /// <typeparam name="TLanguage">The type of 
    /// <see cref="IHighLevelLanguage{TRootNode}">language</see>
    /// which yields information and services pertinent to the 
    /// tools which build the assembly.</typeparam>
    /// <typeparam name="TRootNode"></typeparam>
    /// <typeparam name="TProvider">The language service 
    /// <see cref="IHighLevelLanguageProvider{TRootNode}">provider</see>
    /// which yields a set of services to aid in compilation
    /// of the <see cref="IHighLevelIntermediateAssembly{TLanguage, TRootNode, TProvider}">assembly</see>.</typeparam>
    public interface IHighLevelIntermediateAssembly<TLanguage, TRootNode, TProvider> :
        IIntermediateAssembly<TLanguage, TProvider>
        where TLanguage :
            IHighLevelLanguage<TRootNode>
        where TRootNode :
            IConcreteNode
        where TProvider :
            IHighLevelLanguageProvider<TRootNode>
    {
    }
}
