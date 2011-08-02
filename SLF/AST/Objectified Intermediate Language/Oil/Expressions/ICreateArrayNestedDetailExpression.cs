using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface ICreateArrayNestedDetailExpression :
        IExpression
    {
        /// <summary>
        /// Returns the <see cref="IExpressionCollection"/> used
        /// to instantiate the array.
        /// </summary>
        IExpressionCollection Details { get; }
    }
}
