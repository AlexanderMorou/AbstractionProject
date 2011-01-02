using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// The kind of type the generic type is.
    /// </summary>
    public enum TypeKindGeneric
    {
        /// <summary>
        /// The type is a generic class.
        /// </summary>
        Class = TypeKind.Class,
        /// <summary>
        /// The type is a generic delegate.
        /// </summary>
        Delegate = TypeKind.Delegate,
        /// <summary>
        /// The type is a generic interface.
        /// </summary>
        Interface = TypeKind.Interface,
        /// <summary>
        /// The type is a generic struct.
        /// </summary>
        Struct = TypeKind.Struct,
    }
}
