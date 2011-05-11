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
        IVersionedHighLevelLanguage<TVersion, TRootNode> Language { get; }

        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> associated to the current
        /// versioned high level language provider instance.
        /// </summary>
        /// <remarks>Depending on implementation, the version can denote
        /// specific functional aspects of a given langauge implementation.</remarks>
        TVersion Version { get; }
    }
}
