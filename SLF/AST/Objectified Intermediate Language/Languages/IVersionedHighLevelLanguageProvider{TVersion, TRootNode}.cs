using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;

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
