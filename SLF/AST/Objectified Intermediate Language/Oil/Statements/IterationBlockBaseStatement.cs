using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class IterationBlockBaseStatement :
        BlockStatementBase,
        IIterationBlockBaseStatement
    {
        private IBreakExit exitPoint;

        public IterationBlockBaseStatement(IBlockStatementParent parent, IExpression condition, IEnumerable<IStatementExpression> iterations)
            : base(parent)
        {
            this.Condition = condition;
            this.Iterations = new MalleableStatementExpressionCollection(iterations);
        }

        #region IBreakableBlockStatement Members

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
        /// Breaks the execution from its current point elsewhere.
        /// </summary>
        /// <returns>A <see cref="IBreakStatement"/> which designates the <see cref="IJumpStatement.Target"/>
        /// as necessary.</returns>
        public IBreakStatement Break()
        {
            var b = new BreakStatement(this, this.AssociatedJumpLabel);
            this.baseCollection.Add(b);
            return b;
        }

        public new IBreakableConditionBlockStatement If(IExpression condition)
        {
            return (IBreakableConditionBlockStatement)(base.If(condition));
        }

        #endregion

        internal override IConditionBlockStatement OnIf(IExpression condition)
        {
            var result = new BreakableConditionBlockStatement(this)
            {
                Condition = condition
            };
            this.baseCollection.Add(result);
            return result;
        }


        #region IIterationBlockBaseStatement Members

        public IExpression Condition { get; set; }

        public IMalleableStatementExpressionCollection Iterations { get; private set; }

        #endregion

    }
}
