using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    /// <summary>
    /// Defines properties and methods for working with a structure.
    /// </summary>
    public interface IStructType :
        IDeclaredType<CodeTypeDeclaration>,
        ISegmentableDeclaredType<IStructType, CodeTypeDeclaration>,
        IParameteredParentType<CodeTypeDeclaration>,
        IInterfaceImplementableType
    {
        new IStructPartials Partials { get; }
    }
}
