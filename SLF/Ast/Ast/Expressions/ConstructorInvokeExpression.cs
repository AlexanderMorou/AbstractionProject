using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class ConstructorInvokeExpression :
        ExpressionBase,
        IConstructorInvokeExpression
    {

        public ConstructorInvokeExpression(IConstructorPointerReferenceExpression reference)
        {
            this.Reference = reference;
            this.Parameters = new CallParameterSet();
        }

        public ConstructorInvokeExpression(IConstructorPointerReferenceExpression reference, IExpression[] parameters)
            : this(reference)
        {
            this.Parameters.AddRange(parameters);
        }

        public ConstructorInvokeExpression(IConstructorPointerReferenceExpression reference, IExpressionCollection parameters)
            : this(reference)
        {
            this.Parameters.AddRange(parameters);
        }

        public ConstructorInvokeExpression(IConstructorPointerReferenceExpression reference, IEnumerable<IExpression> parameters)
            : this(reference)
        {
        }


        public override ExpressionKind Type
        {
            get { return ExpressionKind.ConstructorInvoke; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        #region IConstructorInvokeExpression Members

        public IConstructorPointerReferenceExpression Reference { get; private set; }

        public ICallParameterSet Parameters { get; private set; }

        #endregion

        #region IStatementExpression Members

        public bool ValidAsStatement
        {
            get { return true; }
        }

        #endregion
    }
}