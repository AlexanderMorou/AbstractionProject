using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

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
