using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public interface IMalleableTypeOfExpression :
        ITypeOfExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="IType"/> which is represented
        /// under the typeof expression.
        /// </summary>
        new IType ReferenceType { get; set; }
    }
}
