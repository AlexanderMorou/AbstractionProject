using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Statements;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguage
    {
        /// <summary>
        /// Returns the <see cref="String"/> value representing
        /// the unique name of the language.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns a new <see cref="ILanguageProvider"/> associated to the current
        /// <see cref="ILanguage"/>.
        /// </summary>
        /// <returns>A new <see cref="ILanguageProvider"/> for the current
        /// <see cref="ILangauge"/>.</returns>
        ILanguageProvider GetProvider();
        /// <summary>
        /// Returns the level of functionality support the 
        /// compiler contains.
        /// </summary>
        CompilerSupport CompilerSupport { get; }
        /// <summary>
        /// Returns the <see cref="ExpressionKind"/> representing the
        /// kinds of expressions supported by the language.
        /// </summary>
        ExpressionKind SupportedExpressions { get; }
        /// <summary>
        /// Returns the <see cref="StatementKinds"/> representing the 
        /// kinds of statements supported by the language.
        /// </summary>
        StatementKinds SupportedStatements { get; }
    }
}
