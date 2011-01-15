using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// compiler message
    /// </summary>
    public interface ICompilerSourceMessage :
        ICompilerMessage,
        ISourceRelatedMessage
    {

    }
}
