using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Provides a base implementation of the <see cref="IWhileStatement"/> which defines
    /// properties and methods for working with an iterative loop which repeates so long
    /// as its <see cref="IWhileStatement.Condition"/> is true.
    /// </summary>
    public class WhileStatement :
        BlockStatementBase,
        IWhileStatement
    {
        private IBreakExit exitPoint;
        /// <summary>
        /// Creates a new <see cref="WhileStatement"/> with the <paramref name="parent"/> and
        /// <paramref name="condition"/> provided.
        /// </summary>
        /// <param name="parent">
        /// The <see cref="IBlockStatementParent"/> which contains the <see cref="WhileStatement"/>.
        /// </param>
        /// <param name="condition">
        /// The <see cref="IExpression"/> which must evaluate to true for the block of the
        /// <see cref="WhileStatement"/> to be evaluated.
        /// </param>
        public WhileStatement(IBlockStatementParent parent, IExpression condition)
            : base(parent)
        {
            this.Condition = condition;
        }

        #region IBreakableBlockStatement Members

        /// <summary>
        /// Returns the <see cref="IBreakExit"/> for the <see cref="IBreakableStatement"/>.
        /// </summary>
        /// <remarks>In languages that natively support the break statement
        /// this is unnecessary; however in using this in the code, 
        /// the label will be emitted in the associated supporting 
        /// language as well.</remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public IBreakExit AssociatedJumpLabel
        {
            get
            {
                if (this.exitPoint == null)
                    this.exitPoint = this.InitializeJumpPoint();
                return this.exitPoint;
            }
        }

        private IBreakExit InitializeJumpPoint()
        {
            return new BreakExit(this.Parent);
        }

        /// <summary>
        /// Breaks the execution from its current point to somewhere else.
        /// </summary>
        /// <returns>A <see cref="IBreakStatement"/> which designates the <see cref="IJumpStatement.Target"/>
        /// as necessary.</returns>
        public IBreakStatement Break()
        {
            var b = new BreakStatement(this, this.AssociatedJumpLabel);
            base.baseList.Add(b);
            return b;
        }

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
        public new IBreakableConditionBlockStatement If(IExpression condition)
        {
            return (IBreakableConditionBlockStatement)(base.If(condition));
        }

        #endregion

        internal override IConditionBlockStatement OnIf(IExpression condition)
        {
            return new BreakableConditionBlockStatement(this) { Condition = condition };
        }

        #region IEnumerateSetBreakableBlockStatement Members
        /// <summary>
        /// Returns the <see cref="ILocalMember"/> to
        /// utilize within the scope of the enumeration.
        /// </summary>
        public ILocalMember Local { get; private set; }

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which provides the
        /// source set for the enumeration.
        /// </summary>
        public IExpression Source { get; set; }

        #endregion

        public override void Visit(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Visit<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which must evaluate to true for the
        /// block of the <see cref="WhileStatement"/> to be evaluated.
        /// </summary>
        public IExpression Condition { get; set; }
    }
}
