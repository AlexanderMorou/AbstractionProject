using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
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

        public override ExpressionKind Type
        {
            get { return ExpressionKinds.ConstructorInvoke; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
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