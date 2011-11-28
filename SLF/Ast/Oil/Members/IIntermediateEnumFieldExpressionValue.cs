using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    public interface IIntermediateEnumFieldExpressionValue :
        IIntermediateEnumFieldValue
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> the
        /// <see cref="IIntermediateEnumFieldExpressionValue"/> represents.
        /// </summary>
        IExpression Value { get; set; }
    }
}
