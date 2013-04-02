using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Provides an expression which references a specific type wherein 
    /// the <see cref="RuntimeTypeHandle"/> is pushed onto the stack.
    /// </summary>
    public class TypeOfExpression :
        ExpressionBase,
        IUnaryOperationPrimaryTerm,
        IMalleableTypeOfExpression
    {
        public TypeOfExpression(IType referenceType)
        {
            this.ReferenceType = referenceType;
        }

        public override ExpressionKind Type
        {
            get { return ExpressionKind.TypeOfExpression; }
        }

        #region IMalleableTypeOfExpression Members

        public IType ReferenceType { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("typeof({0})", this.ReferenceType.BuildTypeName(true, false, TypeParameterDisplayMode.DebuggerStandard));
        }

        public override TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }
        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
