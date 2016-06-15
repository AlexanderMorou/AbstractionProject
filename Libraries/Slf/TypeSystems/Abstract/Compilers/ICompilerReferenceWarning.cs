using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen Copeland Jr.                             |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

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
