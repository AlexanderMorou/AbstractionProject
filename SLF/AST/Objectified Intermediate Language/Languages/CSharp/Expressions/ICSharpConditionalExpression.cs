using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a ternary operator
    /// conditional expression.
    /// </summary>
    public interface ICSharpConditionalExpression :
        ICSharpExpression,
        IConditionalExpression
    {

        /* *
         * The check part of the conditional must be 
         * a precedence level higher than the conditional.
         * 
         * This ensures that they must properly affix the 
         * expression to the right precedence level
         * to enable proper execution order.
         * */
        /// <summary>
        /// Returns/sets the check part of the conditional.
        /// </summary>
        new ICSharpLogicalOrExpression CheckPart { get; set; }
        /// <summary>
        /// Returns/sets the true part of the conditional.
        /// </summary>
        new ICSharpConditionalExpression TruePart { get; set; }
        /// <summary>
        /// Returns/sets the false part of the conditional.
        /// </summary>
        new ICSharpConditionalExpression FalsePart { get; set; }
    }
}
