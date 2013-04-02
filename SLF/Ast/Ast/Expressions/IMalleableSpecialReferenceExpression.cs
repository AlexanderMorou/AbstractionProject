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
    public interface IMalleableSpecialReferenceExpression : 
        ISpecialReferenceExpression
    {
        /// <summary>
        /// Returns the kind of special reference the reference is.
        /// </summary>
        new SpecialReferenceKind Kind { get; set; }
    }
}
