using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a logical or expression (LeftSide | RightSide).
    /// </summary>
    /// <remarks>The <see cref="ICSharpBitwiseOrExpression"/> uses/has
    /// <see cref="BinaryOperationAssociativity.Left"/> recursion.</remarks>
    public interface ICSharpBitwiseOrExpression :
        IBinaryOperationExpression<ICSharpBitwiseOrExpression, ICSharpBitwiseExclusiveOrExpression>,
        ICSharpExpression
    {
    }
}
