using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public interface IBoundSpecialReferenceExpression :
        ISpecialReferenceExpression,
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IType"/> associated to the special reference used for further
        /// member binding.
        /// </summary>
        IType ReferenceType { get; }
    }
}
