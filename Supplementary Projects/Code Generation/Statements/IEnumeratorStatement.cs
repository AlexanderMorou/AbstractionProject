using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public interface IEnumeratorStatement :
        IBlockedStatement<CodeIterationStatement>,
        IBreakTargetStatement
    {
        /// <summary>
        /// Returns/sets the member reference which contains <see cref="IEnumerable.GetEnumerator()"/>.
        /// </summary>
        IMemberParentExpression EnumeratorSource { get; set; }
        /// <summary>
        /// Returns/sets the type of the items in the enumerator.
        /// </summary>
        ITypeReference ItemType { get; set; }
        /// <summary>
        /// The declared local relative to the enumeration.
        /// </summary>
        IStatementBlockLocalMember CurrentMember { get; }
    }
}
