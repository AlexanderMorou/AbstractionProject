using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a
    /// typed catch exception block statement.
    /// </summary>
    public interface ITypedCatchExceptionBlockStatement :
        IBlockStatement
    {
        /// <summary>
        /// Returns/sets the type of exception caught by the <see cref="ITypedCatchExceptionBlockStatement"/>.
        /// </summary>
        IType CaughtException { get; set; }
    }
}
