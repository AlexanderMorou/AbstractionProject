using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface IIntermediateAssemblyCtorLanguageService<TProvider, TLanguage, TAssembly> :
        ILanguageService<TLanguage, TProvider>,
        IIntermediateAssemblyCtorLanguageService
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>
        where TLanguage :
            ILanguage<TLanguage, TProvider>
        where TAssembly :
            IIntermediateAssembly<TLanguage, TProvider>
    {
        /// <summary>
        /// Creates a new <typeparamref name="TAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value which
        /// differentiates the <typeparamref name="TAssembly"/> from
        /// others.</param>
        /// <returns>A new <typeparamref name="TAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        new TAssembly New(string name);
    }

    public interface IIntermediateAssemblyCtorLanguageService<TLanguage, TProvider> :
        ILanguageService<TLanguage, TProvider>,
        IIntermediateAssemblyCtorLanguageService
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>
        where TLanguage :
            ILanguage<TLanguage, TProvider>
    {
    }

    public interface IIntermediateAssemblyCtorLanguageService :
        ILanguageService
    {
        /// <summary>
        /// Creates a new <see cref="IIntermediateAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value which
        /// differentiates the <see cref="IIntermediateAssembly"/> from
        /// others.</param>
        /// <returns>A new <see cref="IIntermediateAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        IIntermediateAssembly New(string name);
    }
}
