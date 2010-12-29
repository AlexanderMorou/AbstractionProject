using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilerWarning :
        ICompilerMessage,
        ISourceRelatedMessage
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value representing the warning 
        /// level or severity of the warning.
        /// </summary>
        int Level { get; }
    }
}
