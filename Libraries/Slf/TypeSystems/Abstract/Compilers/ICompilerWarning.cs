﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
