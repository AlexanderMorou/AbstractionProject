using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    /// <summary>
    /// Defines properties and methods for working with an <see cref="IInterfaceType"/> that
    /// represents an interface declaration.
    /// </summary>
    public interface IInterfaceType :
        ISegmentableDeclaredType<IInterfaceType, CodeTypeDeclaration>,
        IParameteredDeclaredType<CodeTypeDeclaration>,
        ISignatureMemberParentType
    {
        /// <summary>
        /// Returns the partial elements of the <see cref="IInterfaceType"/>.
        /// </summary>
        new IInterfacePartials Partials { get; }
        /// <summary>
        /// Returns the implementation listing which relate to the series of interfaces
        /// the implementation of the <see cref="IInterfaceType"/> will implement as well.
        /// </summary>
        ITypeReferenceCollection ImplementsList { get; }
    }
}
