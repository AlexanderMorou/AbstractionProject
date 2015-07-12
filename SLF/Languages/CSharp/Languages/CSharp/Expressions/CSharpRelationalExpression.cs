using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    /// <summary>
    /// Provides a base implementation of <see cref="ICSharpRelationalExpression"/>, a relational check operation
    /// expression.
    /// </summary>
    public partial class CSharpRelationalExpression :
        CSharpBinaryOperationExpressionBase<ICSharpRelationalExpression, ICSharpShiftExpression>,
        ICSharpRelationalExpression
    {
        /// <summary>
        /// Data member for <see cref="Operation"/>.
        /// </summary>
        private CSharpRelationalOperation operation;

        /// <summary>
        /// Creates a new <see cref="CSharpRelationalExpression"/> instance with the 
        /// <paramref name="leftSide"/>, <paramref name="operation"/>, and <paramref name="rightSide"/>
        /// provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpRelationalExpression"/>
        /// which sits on the left side of <paramref name="operation"/>.</param>
        /// <param name="operation">The <see cref="CSharpRelationalOperation"/> to be performed
        /// with regards to <paramref name="leftSide"/> and <paramref name="rightSide"/>.</param>
        /// <param name="rightSide">The <see cref="ICSharpShiftExpression"/>
        /// which sits on the right-side of <paramref name="operation"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="rightSide"/> is null, or when
        /// <paramref name="leftSide"/> is null and <paramref name="operation"/> is not 
        /// <see cref="CSharpRelationalOperation.Term"/>.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="operation"/> is
        /// <see cref="CSharpRelationalOperation.TypeCastOrNull"/> or <see cref="CSharpRelationalOperation.TypeCheck"/>
        /// and <paramref name="rightSide"/> does not <see cref="CSharpExpressionExtensions.Detach(IExpression)"/>
        /// to an <see cref="ITypeReferenceExpression"/>.</exception>
        public CSharpRelationalExpression(ICSharpRelationalExpression leftSide, CSharpRelationalOperation operation, ICSharpShiftExpression rightSide)
            : base(leftSide, rightSide)
        {
            if (rightSide == null)
                throw new ArgumentNullException("rightSide");
            if (leftSide == null && operation != CSharpRelationalOperation.Term)
                throw new ArgumentNullException("leftSide", "Only nullable on term pass-throughs.");
            this.operation = operation;
            switch (operation)
            {
                case CSharpRelationalOperation.LessThan:
                case CSharpRelationalOperation.LessThanOrEqualTo:
                case CSharpRelationalOperation.GreaterThan:
                case CSharpRelationalOperation.GreaterThanOrEqualTo:
                    {
                        IExpression detached = rightSide.Detach();
                        if (detached is ITypeReferenceExpression)
                            throw ThrowHelper.ObtainArgumentException(ArgumentWithException.rightSide, ExceptionMessageId.RelationalInvalidOnExpression);
                        break;
                    }
                case CSharpRelationalOperation.TypeCheck:
                case CSharpRelationalOperation.TypeCastOrNull:
                    {
                        IExpression detached = rightSide.Detach();
                        if (!(detached is ITypeReferenceExpression))
                            throw ThrowHelper.ObtainArgumentException(ArgumentWithException.rightSide, ExceptionMessageId.TypeRelationalCheckRequiresType, ThrowHelper.GetArgumentName(ArgumentWithException.rightSide));
                        ITypeReferenceExpression itre = ((ITypeReferenceExpression)(detached));
                        if (itre.ReferenceType == null)
                            throw ThrowHelper.ObtainArgumentException(ArgumentWithException.rightSide, ExceptionMessageId.TypeRelationalTypeCannotBeNull);
                        if ((!(itre.ReferenceType is IReferenceType)) && (operation == CSharpRelationalOperation.TypeCastOrNull))
                            throw ThrowHelper.ObtainArgumentException(ArgumentWithException.rightSide, ExceptionMessageId.TypeRelationalOrNullCastMustBeReference);
                        break;
                    }
                case CSharpRelationalOperation.Term:
                    if (leftSide != null)
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.operation, ExceptionMessageId.CannotTermBinaryOperation);
                    break;
                default:
                    break;
            }
        }

        public CSharpRelationalExpression(ICSharpShiftExpression term)
            : this(null, CSharpRelationalOperation.Term, term)
        {
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpRelationalExpression"/>.
        /// </summary>
        /// <remarks>Returns <see cref="BinaryOperationAssociativity.Left"/>.</remarks>
        public override BinaryOperationAssociativity Associativity
        {
            get { return BinaryOperationAssociativity.Left; }
        }

        /// <summary>
        /// Returns the type of expression the <see cref="CSharpRelationalExpression"/> is.
        /// </summary>
        /// <remarks>Returns the appropriate <see cref="ExpressionKind"/> value
        /// relative to the <see cref="CSharpRelationalOperation"/>.</remarks>
        public override ExpressionKind Type
        {
            get {
                switch (this.Operation)
                {
                    case CSharpRelationalOperation.LessThan:
                        return ExpressionKind.LessThanOperation;
                    case CSharpRelationalOperation.LessThanOrEqualTo:
                        return ExpressionKind.LessThanOrEqualToOperation;
                    case CSharpRelationalOperation.GreaterThan:
                        return ExpressionKind.GreaterThanOperation;
                    case CSharpRelationalOperation.GreaterThanOrEqualTo:
                        return ExpressionKind.GreaterThanOrEqualToOperation;
                    case CSharpRelationalOperation.TypeCheck:
                        return ExpressionKind.TypeCheckOperation;
                    case CSharpRelationalOperation.TypeCastOrNull:
                        return ExpressionKind.TypeCastOrNull;
                    case CSharpRelationalOperation.Term:
                        return ExpressionKind.BinaryForwardTerm;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        #region ICSharpRelationalExpression Members

        /// <summary>
        /// Returns the type of <see cref="CSharpRelationalOperation"/> the
        /// <see cref="CSharpRelationalExpression"/>
        /// is.
        /// </summary>
        public CSharpRelationalOperation Operation
        {
            get
            {
                return this.operation;
            }
            set
            {
                this.operation = value;
            }
        }

        #endregion

        public override string ToString()
        {
            string op = string.Empty;
            switch (this.Operation)
            {
                case CSharpRelationalOperation.LessThan:
                    op = "<";
                    break;
                case CSharpRelationalOperation.LessThanOrEqualTo:
                    op = "<=";
                    break;
                case CSharpRelationalOperation.GreaterThan:
                    op = ">";
                    break;
                case CSharpRelationalOperation.GreaterThanOrEqualTo:
                    op = ">=";
                    break;
                case CSharpRelationalOperation.TypeCheck:
                    op = "is";
                    break;
                case CSharpRelationalOperation.TypeCastOrNull:
                    op = "as";
                    break;
                case CSharpRelationalOperation.Term:
                    if (this.RightSide == null)
                        return null;
                    return this.RightSide.ToString();
                default:
                    return null;
            }
            //Invalid state, can't really display the format if the object is invalid.
            if (RightSide == null || LeftSide == null)
                return null;
            return string.Format(CultureInfo.CurrentCulture, "{0} {1} {2}", LeftSide.ToString(), op, RightSide.ToString());
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }


        public override BinaryOperationKind OperationKind
        {
            get {
                switch (this.Operation)
                {
                    case CSharpRelationalOperation.LessThan:
                        return BinaryOperationKind.LessThan;
                    case CSharpRelationalOperation.LessThanOrEqualTo:
                        return BinaryOperationKind.LessThanOrEqualTo;
                    case CSharpRelationalOperation.GreaterThan:
                        return BinaryOperationKind.GreaterThan;
                    case CSharpRelationalOperation.GreaterThanOrEqualTo:
                        return BinaryOperationKind.GreaterThanOrEqualTo;
                    case CSharpRelationalOperation.TypeCheck:
                        return BinaryOperationKind.TypeCheck;
                    case CSharpRelationalOperation.TypeCastOrNull:
                        return BinaryOperationKind.TypeCastOrNull;
                    case CSharpRelationalOperation.Term:
                    default:
                        return BinaryOperationKind.Term;
                }
            }
        }
    }
}
