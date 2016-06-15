using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// language provider which is versioned.
    /// </summary>
    /// <typeparam name="TVersion">The kind of entity used to represent the
    /// language's various versions.</typeparam>
    public interface IVersionedLanguageProvider<TVersion> :
        ILanguageProvider
    {
        /// <summary>
        /// Returns the <typeparamref name="TVersion"/>
        /// of the current <see cref="IVersionedLanguageProvider{TVersion}"/>
        /// which denotes the specific levels of functional support provided
        /// by the language.
        /// </summary>
        TVersion Version { get; }
    }
}
