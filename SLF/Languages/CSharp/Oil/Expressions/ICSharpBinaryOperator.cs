using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface ICSharpBinaryOperator :
        IBinaryOperationExpression
    {
        /// <summary>
        /// Returns the <see cref="CSharpOperatorPrecedences"/> 
        /// <see cref="IExpression"/> to determine execution
        /// order of certain expression <see cref="Type"/>s.
        /// </summary>
        CSharpOperatorPrecedences Precedence { get; }
    }
}
