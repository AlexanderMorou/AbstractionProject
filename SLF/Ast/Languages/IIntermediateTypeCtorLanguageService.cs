using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cst;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface IIntermediateTypeCtorLanguageService<TProvider, TLanguage, TType> :
        ILanguageService<TLanguage, TProvider>,
        IIntermediateTypeCtorLanguageService<TType>
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>
        where TLanguage :
            ILanguage<TLanguage, TProvider>
        where TType :
            IIntermediateType
    {
    }

    public interface IIntermediateTypeCtorLanguageService<TType> :
        ILanguageService
        where TType :
            IIntermediateType
    {
        /// <summary>
        /// Creates a new <typeparamref name="TType"/> with the
        /// <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value which denotes the
        /// unique identifier relative to the type and its siblings.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contians the <typeparamref name="TType"/>.</param>
        /// <returns>A new <typeparamref name="TType"/> bound to the <paramref name="parent"/>
        /// provided.</returns>
        TType GetNew(string name, IIntermediateTypeParent parent);
    }

}
