using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections;

namespace AllenCopeland.Abstraction.OldCodeGen
{
    /// <summary>
    /// Defines properties and methods for working with the partial namespace containers
    /// of an <see cref="IIntermediateProject"/>.
    /// </summary>
    public interface IIntermediateProjectPartials :
        ISegmentableDeclarationTargetPartials<IIntermediateProject>
    {
        
    }
}
