using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public interface IVersionedHighLevelLanguageProvider<TVersion, TRootNode> :
        IHighLevelLanguageProvider<TRootNode>,
        IVersionedLanguageProvider<TVersion>
        where TRootNode :
            IConcreteNode
    {
        /// <summary>
        /// Returns the <see cref="IVersionedHighLevelLanguage{TVersion, TRootNode}"/>
        /// associated to the current verisoned high level
        /// language provider instance.
        /// </summary>
        new IVersionedHighLevelLanguage<TVersion, TRootNode> Language { get; }
    }
}
