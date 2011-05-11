using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;

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
    }
}
