using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Defines properties and methods for working with a simple 
    /// iteration statement.
    /// </summary>
    /// <remarks>
    /// <para>Used in Visual Basic for 
    ///     'For TARGET = START To END [Step INCREMENTAL]'.</para>
    /// <para>Simplest implementation in C&#9839;: 
    ///     'for (TYPEREF TARGET = START; TARGET &lt;= START; TARGET+[+ | =INCREMENTAL])'.</para>
    /// </remarks>
    public interface ISimpleIterationBlockStatement :
        IBreakableBlockStatement
    {
        ILocalVariableDeclarationStatement Target { get; set; }

        IExpression Start { get; set; }

        IExpression End { get; set; }

        IExpression Incremental { get; set; }
    }
}
