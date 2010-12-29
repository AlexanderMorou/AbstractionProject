using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public interface ICompilerReferenceWarning :
        ICompilerReferenceMessage
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value indicating how 
        /// severe the warning is.
        /// </summary>
        /// <remarks><see cref="WarningLevel"/>'s meaning is language-specific.</remarks>
        int WarningLevel { get; }
    }
}
