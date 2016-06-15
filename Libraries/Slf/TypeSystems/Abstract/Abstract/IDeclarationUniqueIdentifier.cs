using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines the unique identifier associated to a standard
    /// <see cref="IDeclaration"/>.
    /// </summary>
    /// <remarks>Umbrella interface for all identifiers.</remarks>
    public interface IDeclarationUniqueIdentifier
    {
        /// <summary>
        /// Returns the name of the <see cref="IDeclaration"/> the
        /// <see cref="IDeclarationUniqueIdentifier"/> refers to.
        /// </summary>
        string Name { get; }
        string LanguageSpecificQualifier { get; }
    }
}
