using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    /// <summary>
    /// Defines properties and methods for a logical and binary operation (LeftSide &amp; RightSide).
    /// </summary>
    /// <remarks>The <see cref="ICSharpBitwiseAndExpression"/> uses/has
    /// <see cref="BinaryOperationAssociativity.Left"/> recursion.</remarks>
    public interface ICSharpBitwiseAndExpression :
        IBinaryOperationExpression<ICSharpBitwiseAndExpression, ICSharpInequalityExpression>,
        ICSharpExpression
    {
    }
}
