using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguageProvider
    {
        /// <summary>
        /// Returns the <see cref="ILanguage"/>
        /// of the current language provider instance.
        /// </summary>
        ILanguage Language { get; }
        /// <summary>
        /// Creates a new <see cref="IIntermediateAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="IIntermediateAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        IIntermediateAssembly CreateAssembly(string name);
    }
}
