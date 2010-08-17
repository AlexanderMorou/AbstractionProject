using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Provides a base implementation of a condition block statement.
    /// </summary>
    public class ConditionBlockStatement :
        BlockStatementBase,
        IConditionBlockStatement
    {
        private IConditionContinuationStatement next;
        /// <summary>
        /// Creates a new <see cref="ConditionBlockStatement"/> with
        /// the <paramref name="parent"/> which contains it.
        /// </summary>
        /// <param name="parent">The <see cref="IBlockStatementParent"/> which
        /// contains the <see cref="ConditionBlockStatement"/>.</param>
        public ConditionBlockStatement(IBlockStatementParent parent)
            : base(parent)
        {
            
        }

        #region IConditionBlockStatement Members

        /// <summary>
        /// Returns/sets the condition for the <see cref="ConditionBlockStatement"/> to execute.
        /// </summary>
        public IExpression Condition { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="IConditionContinuationStatement"/> which continues the
        /// code flow control conditioning.
        /// </summary>
        public IConditionContinuationStatement Next
        {
            get
            {
                return this.GetNext();
            }
            set
            {
                SetNext(value);
            }
        }

        #endregion

        internal virtual IConditionContinuationStatement GetNext()
        {
            return this.next;
        }

        internal virtual void SetNext(IConditionContinuationStatement value)
        {
            this.next = value;
        }
        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IIntermediateCodeVisitor.Visit(IConditionBlockStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit((IConditionBlockStatement)this);
        }

    }
}
