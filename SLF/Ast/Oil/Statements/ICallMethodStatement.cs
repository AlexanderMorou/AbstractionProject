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

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a statement which
    /// calls a method, wherein the result of the method is disposed.
    /// </summary>
    public interface ICallMethodStatement :
        IStatement
    {
        /// <summary>
        /// Returns/sets the <see cref="IMethodInvokeExpression"/> called by the
        /// current <see cref="ICallMethodStatement"/>.
        /// </summary>
        IMethodInvokeExpression Target { get; set; }
    }
}
