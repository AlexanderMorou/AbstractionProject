using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides an expression which references a specific type wherein 
    /// the <see cref="RuntimeTypeHandle"/> is pushed onto the stack.
    /// </summary>
    public class TypeOfExpression :
        ExpressionBase,
        IUnaryOperationPrimaryTerm,
        ITypeOfExpression
    {
        public TypeOfExpression(IType referenceType)
        {
            this.ReferenceType = referenceType;
        }

        public override ExpressionKind Type
        {
            get { return ExpressionKinds.TypeOfExpression; }
        }

        #region ITypeOfExpression Members

        public IType ReferenceType { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("typeof({0})", this.ReferenceType.CSharpToString());
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
