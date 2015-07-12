using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an expression which embeds a 
    /// comment to the side of an expression.
    /// </summary>
    public interface ICommentExpression :
        IDecorationExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/> which denotes the comment.
        /// </summary>
        string Comment { get; set; }
    };
}
