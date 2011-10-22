using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a high level language
    /// which yields constructs with functional parity with the
    /// static language framework.
    /// </summary>
    /// <typeparam name="TRootNode">The type concrete node in the syntax tree
    /// of the language which denotes the root entry for a given parse.</typeparam>
    public interface IHighLevelLanguage<TRootNode> :
        ILanguage
        where TRootNode :
            IConcreteNode
    {
        /// <summary>
        /// Returns a new <see cref="IHighLevelLanguageProvider{TRootNode}"/> associated to the current
        /// <see cref="IHighLevelLanguage{TRootNode}"/>.
        /// </summary>
        /// <returns>A new <see cref="IHighLevelLanguageProvider{TRootNode}"/> for the current
        /// <see cref="IHighLevelLanguage{TRootNode}"/>.</returns>
        new IHighLevelLanguageProvider<TRootNode> GetProvider();
    }
}
