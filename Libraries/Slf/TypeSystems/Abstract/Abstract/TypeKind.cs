using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{

    /// <summary>
    /// The kind of type an <see cref="IType"/> is.
    /// </summary>
    public enum TypeKind
    {
        /// <summary>
        /// The <see cref="IType"/> is a class.
        /// </summary>
        Class,
        /// <summary>
        /// The <see cref="IType"/> is a delegate.
        /// </summary>
        Delegate,
        /// <summary>
        /// The <see cref="IType"/> is an enumeration.
        /// </summary>
        Enumeration,
        /// <summary>
        /// The <see cref="IType"/> is an interface.
        /// </summary>
        Interface,
        /// <summary>
        /// The <see cref="IType"/> is a data structure.
        /// </summary>
        Struct,
        /// <summary>
        /// The <see cref="IType"/> is another, unknown, kind of type.
        /// </summary>
        Other,
        /// <summary>
        /// The <see cref="IType"/> is a modified type which contains
        /// optional and required modifiers.
        /// </summary>
        Modified,
        /// <summary>
        /// The <see cref="IType"/> is a part of an assembly workspace, and two or more types
        /// are in conflict for the same type name.
        /// </summary>
        Ambiguity,
        /// <summary>
        /// The <see cref="IType"/> is a dynamic type where member resolution
        /// is handled at runtime.
        /// </summary>
        Dynamic,
        /// <summary>
        /// Denotes the last element of <see cref="TypeKind"/>.
        /// </summary>
        Last = Dynamic
    }
}
