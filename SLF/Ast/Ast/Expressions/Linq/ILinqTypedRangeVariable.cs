using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with an
    /// explicitly typed range variable within a language
    /// integrated query.
    /// </summary>
    public interface ILinqTypedRangeVariable :
        ILinqRangeVariable
    {
        /// <summary>
        /// Returns/sets the <see cref="IType"/> which identifies 
        /// the kind of <see cref="ILinqTypedRangeVariable"/>.
        /// </summary>
        IType RangeType { get; set; }
    }
}
