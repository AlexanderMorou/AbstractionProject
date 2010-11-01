using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;

namespace AllenCopeland.Abstraction.Slf.ModelViewer
{
    /// <summary>
    /// Provides a means to obtain a model viewer.
    /// </summary>
    public static class ModelAid
    {
        public static CodeModelViewer GetModelViewer(this IIntermediateAssembly target)
        {
            return new CodeModelViewer { Assembly = target };
        }
    }
}
