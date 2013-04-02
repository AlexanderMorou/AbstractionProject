using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    public interface IStructPartials :
        ISegmentableDeclaredTypePartials<IStructType, CodeTypeDeclaration>
    {
    }
}
