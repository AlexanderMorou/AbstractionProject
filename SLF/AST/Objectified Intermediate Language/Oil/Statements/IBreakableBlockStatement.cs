using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a statement with which
    /// break statements are allowed.
    /// </summary>
    public interface IBreakableBlockStatement :
        IBlockStatement
    {
        /// <summary>
        /// Returns the <see cref="IBreakExit"/> for the <see cref="IBreakableBlockStatement"/>.
        /// </summary>
        /// <remarks>In languages that natively support the break statement
        /// this is unnecessary; however in using this in the code, 
        /// the label will be emitted in the associated supporting 
        /// language as well.</remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IBreakExit AssociatedJumpLabel { get; }

        /// <summary>
        /// Breaks the execution from its current point elsewhere.
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
