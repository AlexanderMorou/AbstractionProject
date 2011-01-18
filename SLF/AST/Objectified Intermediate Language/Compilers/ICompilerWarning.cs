using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilerWarning :
        ICompilerMessage
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value representing the warning 
        /// level or severity of the warning.
        /// </summary>
        int Level { get; }
    }
}
