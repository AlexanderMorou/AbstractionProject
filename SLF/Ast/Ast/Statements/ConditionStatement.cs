using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Provides a base implementation of a condition block statement.
    /// </summary>
    public class ConditionBlockStatement :
        ConditionContinuationStatement,
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

        public void CreateNext(IExpression condition)
        {
            this.Next = new ConditionBlockStatement(this.Parent) { Condition = condition };
        }

        public void CreateNext()
        {
            this.Next = new ConditionContinuationStatement(this.Parent);
        }

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
