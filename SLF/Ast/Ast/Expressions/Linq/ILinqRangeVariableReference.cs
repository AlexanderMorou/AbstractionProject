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

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    public interface ILinqRangeVariableReference :
        IMemberReferenceExpression
    {
        /// <summary>
        /// Returns the <see cref="ILinqRangeVariable"/> which the
        /// <see cref="ILinqRangeVariableReference"/> refers to.
        /// </summary>
        ILinqRangeVariable Target { get; }
    }
}
