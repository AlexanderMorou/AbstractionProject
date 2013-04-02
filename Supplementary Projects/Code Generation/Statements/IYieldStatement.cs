using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
/* * 
 * Oilexer is an open-source project and must be released
 * as per the license associated to the project.
 * */
namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    /// <summary>
    /// Defines properties and methods for working with a yield statement
    /// for creating an enumerator state machine.
    /// </summary>
    public interface IYieldStatement :
        IStatement<CodeSnippetStatement>
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> yielded by the
        /// <see cref="IYieldStatement"/>.
        /// </summary>
        IExpression Result { get; set; }
    }
}
