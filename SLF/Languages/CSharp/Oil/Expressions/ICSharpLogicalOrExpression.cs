using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a condition or expression (LeftSide || RightSide)
    /// </summary>
    public interface ICSharpLogicalOrExpression :
        IBinaryOperationExpression<ICSharpLogicalOrExpression, ICSharpLogicalAndExpression>,
        ICSharpExpression
    {
    }
}
