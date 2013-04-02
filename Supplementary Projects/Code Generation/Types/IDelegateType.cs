using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    /// <summary>
    /// Defines properties and methods for working with a delegate type declaration.
    /// </summary>
    public interface IDelegateType :
        IDeclaredType<CodeTypeDelegate>,
        IParameteredDeclaredType<CodeTypeDelegate>,
        IParameteredDeclaration<IDelegateTypeParameterMember, CodeTypeDelegate, ITypeParent>
    {
        /// <summary>
        /// Returns the parameters of the <see cref="IDelegateType"/>.
        /// </summary>
        new IDelegateTypeParameterMembers Parameters { get; }
        /// <summary>
        /// Returns the type that the delegate method instances return.
        /// </summary>
        ITypeReference ReturnType { get; set; }

    }
}
