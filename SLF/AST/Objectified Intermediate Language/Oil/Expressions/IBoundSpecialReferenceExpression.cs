using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
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
