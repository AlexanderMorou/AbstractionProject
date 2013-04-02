using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    /// <summary>
    /// Defines properties and methods for a <see cref="IDeclarationTarget"/> which
    /// has attributes declared on it.
    /// </summary>
    public interface IAttributeDeclarationTarget :
        IDeclarationTarget
    {
        /// <summary>
        /// Returns the <see cref="IAttributeDeclarations"/> defined on the <see cref="IAttributeDeclarationTarget"/>
        /// </summary>
        IAttributeDeclarations Attributes { get; }
    }
}
