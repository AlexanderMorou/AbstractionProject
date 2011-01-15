using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.VisualBasic.My
{
    public interface IMyNamespaceDeclaration : 
        IIntermediateNamespaceDeclaration
    {
        IMyApplicationClass MyApplication { get; }
    }
}
