﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2008-2011 Allen Copeland Jr.                             |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IAssemblyReference 
    {
        /// <summary>
        /// Returns the <see cref="IAssembly"/> associated
        /// to the <see cref="IAssemblyReference"/>.
        /// </summary>
        IAssembly Reference { get; }
        /// <summary>
        /// Returns the <see cref="IList{T}"/> of the
        /// aliases associated to the <see cref="Reference"/>.
        /// </summary>
        IList<string> Aliases { get; }
    }
}