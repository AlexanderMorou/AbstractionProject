using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// condition and expression (LeftSide &amp;&amp;
    /// RightSide)
    /// </summary>
    public interface ICSharpLogicalAndExpression :
        IBinaryOperationExpression<ICSharpLogicalAndExpression, ICSharpBitwiseOrExpression>,
        ICSharpExpression
    {
    }
}
