using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Denotes the side of a <see cref="ICommentExpression.InnerExpression"/>
    /// a <see cref="ICommentExpression.Comment"/> will appear on.
    /// </summary>
    public enum DecorationDisplaySide
    {
        /// <summary>
        /// The decoration will appear on the 
        /// <see cref="IDecoratingExpression.ContainedExpression"/>'s left side.
        /// </summary>
        LeftSide,
        /// <summary>
        /// The decoration will appear on the 
        /// <see cref="IDecoratingExpression.ContainedExpression"/>'s right side.
        /// </summary>
        RightSide,
    };
    /// <summary>
    /// Denotes a decorative expression used to add context
    /// </summary>
    public interface IDecorationExpression :
        IExpression
    {
        /// <summary>
        /// Returns the <see cref="DecorationDisplaySide"/> which denotes which
        /// side of the <see cref="IDecoratingExpression.ContainedExpression"/>
        /// the decoration appears on.
        /// </summary>
        DecorationDisplaySide Side { get; set; }
    }
}
