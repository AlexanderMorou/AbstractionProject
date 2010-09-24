using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IConditionalExpression :
        INaryOperandExpression
    {
        /// <summary>
        /// Returns/sets the check part of the conditional.
        /// </summary>
        IExpression CheckPart { get; set; }
        /// <summary>
        /// Returns/sets the true part of the conditional.
        /// </summary>
        IExpression TruePart { get; set; }
        /// <summary>
        /// Returns/sets the false part of the conditional.
        /// </summary>
        IExpression FalsePart { get; set; }
    }
}
