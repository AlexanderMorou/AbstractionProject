using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Provides a base implementation of a call to a
    /// fused expression wherein the results are disposed
    /// (popped off of the stack).
    /// </summary>
    public class CallFusionStatement :
        StatementBase,
        ICallFusionStatement
    {
        /// <summary>
        /// Creates a new <see cref="CallFusionStatement"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IStatementParent"/> which
        /// contains the <see cref="CallFusionStatement"/>.</param>
        public CallFusionStatement(IStatementParent parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IStatement"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IStatementVisitor.Visit(ICallFusionStatement)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public override void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        #region ICallFusionStatement Members

        /// <summary>
        /// Returns/sets the <see cref="IExpressionToCommaFusionExpression"/> which
        /// is invoked by the <see cref="CallFusionStatement"/>.
        /// </summary>
        public IExpressionToCommaFusionExpression Target { get; set; }

        #endregion

        public override string ToString()
        {
            return Target.ToString();
        }

        public override TResult Visit<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

    }
}
