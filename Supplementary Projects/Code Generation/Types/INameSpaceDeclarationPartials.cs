using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    /// <summary>
    /// Defines properties and methods for working with a single instance of a <see cref="INameSpaceDeclaration"/>.
    /// </summary>
    public interface INameSpaceDeclarationPartials :
        ISegmentableDeclarationTargetPartials<INameSpaceDeclaration>
    {
        INameSpaceDeclaration AddNew(INameSpaceParent parentTarget);
    }
}
