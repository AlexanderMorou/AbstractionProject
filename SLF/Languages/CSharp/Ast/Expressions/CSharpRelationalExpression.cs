using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
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
        /// <param name="operation">The <see cref="RelationalOperation"/> to be performed
        /// with regards to <paramref name="leftSide"/> and <paramref name="rightSide"/>.</param>
        /// <param name="rightSide">The <see cref="ICSharpShiftExpression"/>
        /// which sits on the right-side of <paramref name="operation"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="rightSide"/> is null, or when
        /// <paramref name="leftSide"/> is null and <paramref name="operation"/> is not 
        /// <see cref="RelationalOperation.Term"/>.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="operation"/> is
        /// <see cref="RelationalOperation.TypeCastOrNull"/> and <paramref name="rightSide"/> has a 
        /// <see cref="ICSharpMemberParentReferenceExpression.ForwardType"/> 
        /// that is not a reference type or is null or <paramref name="operation"/> is
        /// <see cref="RelationalOperation.TypeCastOrNull"/> or <see cref="RelationalOperation.TypeCheck"/>
        /// and <paramref name="rightSide"/> does not <see cref="ExpressionExtensions.Disfix(IExpression)"/>
        /// to an <see cref="ICSharpTypeReferenceExpression"/>.</exception>
        public CSharpRelationalExpression(ICSharpRelationalExpression leftSide, CSharpRelationalOperation operation, ICSharpShiftExpression rightSide)
            : base(leftSide, rightSide)
        {
            if (rightSide == null)
                throw new ArgumentNullException("rightSide");
            if (leftSide == null && operation != CSharpRelationalOperation.Term)
                throw new ArgumentNullException("Only nullable on term pass-throughs.", "leftSide");
            this.operation = operation;
            switch (operation)
            {
                case CSharpRelationalOperation.LessThan:
                case CSharpRelationalOperation.LessThanOrEqualTo:
                case CSharpRelationalOperation.GreaterThan:
                case CSharpRelationalOperation.GreaterThanOrEqualTo:
                {
                    IExpression disfix = rightSide.Disfix();
                    if (disfix is ITypeReferenceExpression)
                        throw new ArgumentException("rightSide", "Cannot relationally check a type against another expression.");
                    break;
                }
                case CSharpRelationalOperation.TypeCheck:
                case CSharpRelationalOperation.TypeCastOrNull:
                {
                    IExpression disfix = rightSide.Disfix();
                    if (!(disfix is ITypeReferenceExpression))
                        throw new ArgumentException("rightSide", "Type-relational checks require a type on the rightSide.");
                    ITypeReferenceExpression itre = ((ITypeReferenceExpression)(disfix));
                    if (itre.ReferenceType == null)
                        throw new ArgumentException("rightSide", "Type-relational check invalid.  Target type cannot be null.");
                    if ((!(itre.ReferenceType is IReferenceType)) && (operation == CSharpRelationalOperation.TypeCastOrNull))
                        throw new ArgumentException("rightSide", "Type-Cast (or null) expression requires a reference type to work.");
                    break;
                }
                case CSharpRelationalOperation.Term:
                    if (leftSide != null)
                        throw new ArgumentException("Term is used when leftSide is null","operation");
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
        /// <remarks>Returns <see cref="ExpressionType.RelationalOperation"/>.</remarks>
        public override ExpressionKind Type
        {
            get {
                switch (this.Operation)
                {
                    case CSharpRelationalOperation.LessThan:
                        return ExpressionKinds.LessThanOperation;
                    case CSharpRelationalOperation.LessThanOrEqualTo:
                        return ExpressionKinds.LessThanOrEqualToOperation;
                    case CSharpRelationalOperation.GreaterThan:
                        return ExpressionKinds.GreaterThanOperation;
                    case CSharpRelationalOperation.GreaterThanOrEqualTo:
                        return ExpressionKinds.GreaterThanOrEqualToOperation;
                    case CSharpRelationalOperation.TypeCheck:
                        return ExpressionKinds.TypeCheckOperation;
                    case CSharpRelationalOperation.TypeCastOrNull:
                        return ExpressionKinds.TypeCastOrNull;
                    case CSharpRelationalOperation.Term:
                        return ExpressionKinds.BinaryForwardTerm;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        #region ICSharpRelationalExpression Members

        /// <summary>
        /// Returns the type of <see cref="RelationalOperation"/> the <see cref="CSharpRelationalExpression"/>
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

        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
