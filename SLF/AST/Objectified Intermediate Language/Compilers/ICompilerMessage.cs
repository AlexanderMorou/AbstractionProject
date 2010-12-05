using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods associated to a message
    /// from a compiler.
    /// </summary>
    public interface ICompilerMessage :
        ISourceRelatedMessage
    {
    }
}
