using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    /// <summary>
    /// Defines properties and methods for working with a provider
    /// for the <see cref="IVisualBasicLanguage">Visual Basic.NET language</see>.
    /// </summary>
    public interface IVisualBasicProvider<TAssembly, TProvider> :
        IVersionedHighLevelLanguageProvider<VisualBasicVersion, IVisualBasicStart>,
        IIntermediateLanguageTypeProvider
        where TAssembly :
            IVisualBasicAssembly<TAssembly, TProvider>
        where TProvider :
            IVisualBasicProvider<TAssembly, TProvider>
    {
        /// <summary>
        /// Returns the <see cref="IVisualBasicLanguage">Visual Basic.NET
        /// language</see>
        /// associated to the <see cref="IVisualBasicProvider"/>.
        /// </summary>
        new IVisualBasicLanguage Language { get; }
        /// <summary>
        /// Creates a new <typeparamref name="TAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="TAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        new TAssembly CreateAssembly(string name);
    }
}
