using AllenCopeland.Abstraction.Slf.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>Defines properties and methods for working with a language service which creates custom namespace declarations.</summary>
    public interface IIntermediateNamespaceCtorLanguageService :
        ILanguageService
    {
        /// <summary>
        /// Creates a new instance that implements <see cref="IIntermediateNamespaceDeclaration"/> and specifies its <paramref name="parent"/> and <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The <see cref="string"/> value unqiuely identifying the new <see cref="IIntermediateNamespaceDeclaration"/> instance from its siblings.</param>
        /// <param name="parent">The <see cref="IIntermediateNamespaceParent"/> instance which is the parent of the new <see cref="IIntermediateNamespaceDeclaration"/>.</param>
        /// <returns>A new instance that implements <see cref="IIntermediateNamespaceDeclaration"/> and specifies its <paramref name="parent"/> and <paramref name="name"/>.</returns>
        IIntermediateNamespaceDeclaration New(string name, IIntermediateNamespaceParent parent);
        /// <summary>Returns/sets the <see cref="String"/> value used to separate namespace identities.</summary>
        string IdentitySeparator { get; }
    }
}
