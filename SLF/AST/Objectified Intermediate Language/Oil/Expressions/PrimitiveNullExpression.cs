using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a null value as an expression.
    /// </summary>
    public class PrimitiveNullExpression :
        ExpressionBase,
        IPrimitiveExpression
    {
        internal PrimitiveNullExpression()
        {

        }
        #region IPrimitiveExpression Members

        public object Value
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotSupportedException("Single-ton null value is null only.");
            }
        }

        public PrimitiveType PrimitiveType
        {
            get { return PrimitiveType.Null; }
        }

        #endregion

        #region IMemberParentReferenceExpression Members

        public override IMethodReferenceStub GetMethod(string name)
        {
            throw new NotSupportedException();
        }

        public override IMethodReferenceStub GetMethod(string name, ITypeCollection genericParameters)
        {
            throw new NotSupportedException();
        }

        public override IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature)
        {
            throw new NotSupportedException();
        }

        public override IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature, AllenCopeland.Abstraction.Slf.Abstract.ITypeCollection genericParameters)
        {
            throw new NotSupportedException();
        }

        public override IIndexerReferenceExpression GetIndexer(string name, params IExpression[] parameters)
        {
            throw new NotSupportedException();
        }

        public override IPropertyReferenceExpression GetProperty(string name)
        {
            throw new NotSupportedException();
        }

        public override IIndexerReferenceExpression GetIndexer(params IExpression[] parameters)
        {
            throw new NotSupportedException();
        }

        public override IFieldReferenceExpression GetField(string name)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IExpression Members

        public override ExpressionKinds Type
        {
            get { return ExpressionKinds.PrimitiveNullInsert; }
        }

        #endregion

        public override string ToString()
        {
            return "null";
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.VisitNull();
        }

        public void Visit(IIntermediatePrimitiveVisitor visitor)
        {
            visitor.VisitNull();
        }
    }
}
