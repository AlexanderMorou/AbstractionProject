using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
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
                return this.Parent.AssociatedJumpLabel;
            }
        }

        /// <summary>
        /// Breaks the execution from its current point to somewhere else.
        /// </summary>
        /// <returns>A <see cref="IBreakStatement"/> which designates the <see cref="IJumpStatement.Target"/>
        /// as necessary.</returns>
        public IBreakStatement Break()
        {
            var b = new BreakStatement(this, this.AssociatedJumpLabel);
            this.baseList.Add(b);
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
