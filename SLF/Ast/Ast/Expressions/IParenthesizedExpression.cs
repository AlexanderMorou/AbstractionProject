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
    public interface IParenthesizedExpression :
        IUnaryOperationPrimaryTerm,
        IFusionTermExpression,
        IFusionCommaTargetExpression
    {
        /// <summary>
        /// Returns/sets the expression that's surrounded by parenthesis.
        /// </summary>
        IExpression Parenthesized { get; set; }
    }
}
