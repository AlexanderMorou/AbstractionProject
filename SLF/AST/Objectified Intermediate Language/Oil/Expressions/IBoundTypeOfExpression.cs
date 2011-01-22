using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a type of expression 
    /// which loads the meta-data token of the type in question onto the
    /// stack.
    /// </summary>
    /// <remarks>Difference between a bound and normal typeof expression
    /// is a bound typeof expression is guaranteed not to be a symbol.
    /// </remarks>
    public interface IBoundTypeOfExpression :
        ITypeOfExpression
    {
    }
}
