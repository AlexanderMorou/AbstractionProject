using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class BreakableConditionContinuationStatement :
        ConditionContinuationStatement,
        IBreakableConditionContinuationStatement
    {
        public BreakableConditionContinuationStatement(IBlockStatementParent parent)
            :base(parent)
        {
        }

        /// <summary>
        /// Returns the <see cref="IBreakableBlockStatement"/> which contains the 
        /// <see cref="IBreakableConditionBlockStatement"/>.
        /// </summary>
        public new IBreakableBlockStatement Parent
        {
            get { return (IBreakableBlockStatement)base.Parent; }
        }

        #region IBreakableBlockStatement Members

        public IBreakExit AssociatedJumpLabel
        {
            get
            {
                return this.Parent.AssociatedJumpLabel;
            }
        }

        /// <summary>
        /// Breaks the execution from its current point elsewhere.
        /// </summary>
        /// <returns>A <see cref="IBreakStatement"/> which designates the <see cref="IJumpStatement.Target"/>
        /// as necessary.</returns>
        public IBreakStatement Break()
        {
            var b = new BreakStatement(this, this.AssociatedJumpLabel);
            this.baseList.Add(b);
            return b;
        }

        public new IBreakableConditionBlockStatement If(IExpression condition)
        {
            return (IBreakableConditionBlockStatement)(base.If(condition));
        }

        #endregion

        internal override IConditionBlockStatement OnIf(IExpression condition)
        {
            var result = new BreakableConditionBlockStatement(this) { Condition = condition };
            return result;
        }

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IStatementVisitor.Visit(IConditionContinuationStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public override void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit((IConditionContinuationStatement)this);
        }


    }
}
