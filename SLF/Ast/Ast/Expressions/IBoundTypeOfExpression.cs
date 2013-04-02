using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
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
