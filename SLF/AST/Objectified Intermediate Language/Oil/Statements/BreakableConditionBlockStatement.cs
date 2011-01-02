using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Provides a condition statement which alters the flow of 
    /// execution based upon a boolean condition  within a 
    /// breakable section of code.
    /// </summary>
    public class BreakableConditionBlockStatement :
        BreakableConditionContinuationStatement,
        IBreakableConditionBlockStatement
    {
        private IBreakableConditionContinuationStatement next;
        /// <summary>
        /// Creates a new <see cref="BreakableConditionBlockStatement"/> with the
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IBreakableBlockStatement"/></param>
        public BreakableConditionBlockStatement(IBreakableBlockStatement parent)
            : base(parent)
        {
        }

        #region IBreakableConditionBlockStatement Members

        /// <summary>
        /// Returns/sets the <see cref="IBreakableConditionContinuationStatement"/> which continues the
        /// code flow control conditioning.
        /// </summary>
        public IBreakableConditionContinuationStatement Next
        {
            get
            {
                if (this.next == null)
                    this.CreateNext();
                return this.next;
            }
            set
            {
                this.next = value;
            }
        }

        #endregion
        #region IConditionBlockStatement Members

        /// <summary>
        /// Returns/sets the condition for the <see cref="ConditionBlockStatement"/> to execute.
        /// </summary>
        public IExpression Condition { get; set; }

        IConditionContinuationStatement IConditionBlockStatement.Next
        {
            get
            {
                return this.Next;
            }
            set
            {
                if (value is IBreakableConditionContinuationStatement)
                    this.Next = (IBreakableConditionContinuationStatement)value;
                else
                    throw new ArgumentException("invalid value", "value");
            }
        }
        #endregion

        #region IConditionBlockStatement Members

        public void CreateNext()
        {
            this.Next = new BreakableConditionContinuationStatement(this.Parent);
        }

        public void CreateNext(IExpression condition)
        {
            this.Next = new BreakableConditionBlockStatement(this.Parent) { Condition = condition };
        }

        #endregion

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IStatementVisitor.Visit(IConditionBlockStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public override void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit((IConditionBlockStatement)this);
        }

        public override string ToString()
        {
            return string.Format("if ({0}) {{ ...", this.Condition);
        }
    }
}
