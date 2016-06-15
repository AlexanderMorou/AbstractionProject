using AllenCopeland.Abstraction.Slf.Ast.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    //ToDo: Remove this once Lambdas are implemented.
    public class BlockExpression :
        ExpressionBase,
        IBlockExpression
    {
        /// <summary>
        /// Returns/sets the <see cref="IBlockStatement"/> to represent as an expression.
        /// </summary>
        public IBlockStatement Block { get; set; }

        public override ExpressionKind Type
        {
            get { return ExpressionKind.BlockExpression; }
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
    }
}
