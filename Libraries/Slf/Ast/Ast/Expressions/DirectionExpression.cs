using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class DirectionExpression :
      IDirectionExpression
    {
        /// <summary>
        /// Creates a new <see cref="DirectionExpression"/> with the <paramref name="directed"/> to coerce in the
        /// <paramref name="direction"/> provided.
        /// </summary>
        /// <param name="directed">The <see cref="IExpression"/> to direct.</param>
        /// <param name="direction">The <see cref="ParameterCoercionDirection"/> to direct the <paramref name="directed"/>.</param>
        public DirectionExpression(IExpression directed, ParameterCoercionDirection direction)
        {
            this.Directed = directed;
            this.Direction = direction;
        }

        public ParameterCoercionDirection Direction { get; set; }

        public IExpression Directed { get; set; }

        public ExpressionKind Type
        {
            get { return ExpressionKind.DirectionExpression; }
        }

        public void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public Uri Location { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }
    }
}
