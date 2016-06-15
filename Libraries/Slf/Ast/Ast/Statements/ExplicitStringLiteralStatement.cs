using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class ExplicitStringLiteralStatement :
        StatementBase,
        IExplicitStringLiteralStatement
    {
        /// <summary>Returns/sets the <see cref="String"/> value emitted explicitly as is in the result code.</summary>
        public string Literal { get; set; }

        public ExplicitStringLiteralStatement(IStatementParent parent) : base(parent) { }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
    }
}
