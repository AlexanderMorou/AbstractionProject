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

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public interface IReturnStatement :
        IStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which represents the <see cref="IReturnStatement"/>
        /// returns.
        /// </summary>
        IExpression ReturnValue { get; set; }
    }
}
