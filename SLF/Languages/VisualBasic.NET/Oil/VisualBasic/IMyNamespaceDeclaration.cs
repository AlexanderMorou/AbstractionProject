using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic.My;

namespace AllenCopeland.Abstraction.Slf.Oil.VisualBasic
{
    public interface IMyNamespaceDeclaration : 
        IIntermediateNamespaceDeclaration
    {
        IMyApplicationClass MyApplication { get; }
    }
}
