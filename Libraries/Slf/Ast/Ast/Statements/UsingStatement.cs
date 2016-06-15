using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public abstract class UsingStatement<T> :
        BlockStatementBase
    {
        protected UsingStatement(IBlockStatementParent parent)
            : base(parent)
        {

        }

        protected UsingStatement(IBlockStatementParent parent, T resourceAcquisition)
            : this(parent)
        {
            this.ResourceAcquisition = resourceAcquisition;
        }

        public T ResourceAcquisition { get; set; }
        
    }

    public class UsingBlockStatement :
        UsingStatement<ILocalDeclarationsStatement>,
        IUsingBlockStatement
    {
        protected UsingBlockStatement(IBlockStatementParent parent)
            : base(parent)
        {

        }

        public UsingBlockStatement(IBlockStatementParent parent, ILocalDeclarationsStatement resourceAcquisition)
            : base(parent, resourceAcquisition)
        {
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
    }

    public class UsingExpressionBlockStatement :
        UsingStatement<IExpression>,
        IUsingExpressionBlockStatement
    {
        protected UsingExpressionBlockStatement(IBlockStatementParent parent)
            : base(parent)
        {

        }

        public UsingExpressionBlockStatement(IBlockStatementParent parent, IExpression resourceAcquisition)
            : base(parent, resourceAcquisition)
        {
        }

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
