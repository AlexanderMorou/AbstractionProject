using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// <summary>
    /// Defines properties and methods for working with a statement with which
    /// break statements are allowed.
    /// </summary>
    public interface IBreakableBlockStatement :
        IBlockStatement,
        IBreakableStatement
    {

        /// <summary>
        /// Breaks the execution from its current point to somewhere else.
        /// </summary>
        /// <returns>A <see cref="IBreakStatement"/> which designates the <see cref="IJumpStatement.Target"/>
        /// as necessary.</returns>
        IBreakStatement Break();

        /// <summary>
        /// Inserts and returns a new <see cref="IBreakableConditionBlockStatement"/> instance
        /// which relates to the <paramref name="condition"/> provided that can contain a 
        /// break statement.
        /// </summary>
        /// <param name="condition">The <see cref="IExpression"/> to evaluate
        /// before executing the <see cref="IBreakableConditionBlockStatement"/>'s
        /// statements.</param>
        /// <returns>A new <see cref="IBreakableConditionBlockStatement"/> with the
        /// <see cref="IExpression"/> <paramref name="condition"/> provided
        /// that can contain a break statement.</returns>
        new IBreakableConditionBlockStatement If(IExpression condition);
    }
}
