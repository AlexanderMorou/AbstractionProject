using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.CSharp;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides an expression representing an assignment operation.
    /// </summary>
    public class CSharpAssignExpression :
        CSharpBinaryOperationExpressionBase<ICSharpConditionalExpression, ICSharpAssignExpression>,
        ICSharpAssignExpression
    {
        /// <summary>
        /// Data member for <see cref="CSharpAssignExpression"/>.
        /// </summary>
        private AssignmentOperation operation;
        /// <summary>
        /// Creates a new non-operational <see cref="CSharpAssignExpression"/> with the <paramref name="term"/>
        /// provided.
        /// </summary>
        /// <param name="term">The term the non-operational <see cref="CSharpAssignExpression"/>
        /// points to.</param>
        public CSharpAssignExpression(ICSharpConditionalExpression term)
            : base(term)
        {
            operation = AssignmentOperation.Term;
        }

        /// <summary>
        /// Creates a new <see cref="CSharpAssignExpression"/> instance
        /// with the <paramref name="leftSide"/>, <paramref name="operation"/>
        /// and <paramref name="rightSide"/> provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpConditionalExpression"/> which makes up the
        /// left operand.</param>
        /// <param name="operation">The <see cref="AssignmentOperation"/> which indicates the operation
        /// the current <see cref="CSharpAssignExpression"/> represents.</param>
        /// <param name="rightSide">The <see cref="ICSharpAssignExpression"/> which makes up the 
        /// right operand.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="leftSide"/> or 
        /// <paramref name="rightSide"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="leftSide"/> is not
        /// an <see cref="IAssignTargetExpression"/> when deaffixed.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="operation"/>
        /// is a value not within the <see cref="AssignmentOperation"/> enumeration; or when
        /// <paramref name="operation"/> is <see cref="AssignmentOperation.Term"/>.</exception>
        public CSharpAssignExpression(ICSharpConditionalExpression leftSide, AssignmentOperation operation, ICSharpAssignExpression rightSide)
            : base(leftSide, rightSide)
        {
            switch (operation)
            {
                case AssignmentOperation.SimpleAssign:
                case AssignmentOperation.MultiplicationAssign:
                case AssignmentOperation.DivisionAssign:
                case AssignmentOperation.ModulusAssign:
                case AssignmentOperation.AddAssign:
                case AssignmentOperation.SubtractionAssign:
                case AssignmentOperation.LeftShiftAssign:
                case AssignmentOperation.RightShiftAssign:
                case AssignmentOperation.BitwiseAndAssign:
                case AssignmentOperation.BitwiseOrAssign:
                case AssignmentOperation.BitwiseExclusiveOrAssign:
                    break;
                case AssignmentOperation.Term:
                default:
                    throw new ArgumentOutOfRangeException("operation");
            }
            IExpression dixfixedLeft = leftSide.Disfix();
            if (!(dixfixedLeft is IBinaryOperationExpression) && !(dixfixedLeft is IAssignTargetExpression))
                throw new ArgumentException("leftSide");
            this.operation = operation;
        }

        /// <summary>
        /// Returns the type of expression the <see cref="CSharpAssignExpression"/> is.
        /// </summary>
        /// <remarks>Returns based upon the <see cref="Operation"/>.</remarks>
        public override ExpressionKind Type
        {
            get {
                switch (this.Operation)
                {
                    case AssignmentOperation.SimpleAssign:
                        return ExpressionKind.AssignExpression;
                    case AssignmentOperation.MultiplicationAssign:
                        return ExpressionKind.AssignMultiplyOperation;
                    case AssignmentOperation.DivisionAssign:
                        return ExpressionKind.AssignDivideOperation;
                    case AssignmentOperation.ModulusAssign:
                        return ExpressionKind.AssignModulusOperation;
                    case AssignmentOperation.AddAssign:
                        return ExpressionKind.AssignAddOperation;
                    case AssignmentOperation.SubtractionAssign:
                        return ExpressionKind.AssignSubtractOperation;
                    case AssignmentOperation.LeftShiftAssign:
                        return ExpressionKind.AssignLeftShiftOperation;
                    case AssignmentOperation.RightShiftAssign:
                        return ExpressionKind.AssignRightShiftOperation;
                    case AssignmentOperation.BitwiseAndAssign:
                        return ExpressionKind.AssignBitwiseAndOperation;
                    case AssignmentOperation.BitwiseOrAssign:
                        return ExpressionKind.AssignBitwiseOrOperation;
                    case AssignmentOperation.BitwiseExclusiveOrAssign:
                        return ExpressionKind.AssignBitwiseExclusiveOrOperation;
                    case AssignmentOperation.Term:
                        return ExpressionKind.BinaryForwardTerm;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="CSharpAssignExpression"/>.
        /// </summary>
        /// <remarks>Returns <see cref="BinaryOperationAssociativity.Right"/>.</remarks>
        public override BinaryOperationAssociativity Associativity
        {
            get { return BinaryOperationAssociativity.Right; }
        }

        #region ICSharpAssignExpression Members

        /// <summary>
        /// Returns/sets the <see cref="AssignmentOperation"/> associated to the <see cref="CSharpAssignExpression"/>.
        /// </summary>
        public AssignmentOperation Operation
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
                case AssignmentOperation.SimpleAssign:
                    op = "=";
                    break;
                case AssignmentOperation.MultiplicationAssign:
                    op = "*=";
                    break;
                case AssignmentOperation.DivisionAssign:
                    op = "/=";
                    break;
                case AssignmentOperation.ModulusAssign:
                    op = "%=";
                    break;
                case AssignmentOperation.AddAssign:
                    op = "+=";
                    break;
                case AssignmentOperation.SubtractionAssign:
                    op = "-=";
                    break;
                case AssignmentOperation.LeftShiftAssign:
                    op = "<<=";
                    break;
                case AssignmentOperation.RightShiftAssign:
                    op = ">>=";
                    break;
                case AssignmentOperation.BitwiseAndAssign:
                    op = "&=";
                    break;
                case AssignmentOperation.BitwiseOrAssign:
                    op = "|=";
                    break;
                case AssignmentOperation.Term:
                    return this.LeftSide.ToString();
                default:
                    return null;
            } 
            return string.Format(CultureInfo.CurrentCulture, "{0} {1} {2}", LeftSide.ToString(), op, RightSide.ToString());
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit<ICSharpConditionalExpression, ICSharpAssignExpression>(this);
        }

        #region IStatementExpression Members

        public bool ValidAsStatement
        {
            get
            {
                switch (this.operation)
                {
                    case AssignmentOperation.SimpleAssign:
                    case AssignmentOperation.MultiplicationAssign:
                    case AssignmentOperation.DivisionAssign:
                    case AssignmentOperation.ModulusAssign:
                    case AssignmentOperation.AddAssign:
                    case AssignmentOperation.SubtractionAssign:
                    case AssignmentOperation.LeftShiftAssign:
                    case AssignmentOperation.RightShiftAssign:
                    case AssignmentOperation.BitwiseAndAssign:
                    case AssignmentOperation.BitwiseOrAssign:
                    case AssignmentOperation.BitwiseExclusiveOrAssign:
                        return true;
                    case AssignmentOperation.Term:
                        var leftDisfixed = this.LeftSide.Disfix();
                        return (leftDisfixed is IStatementExpression) &&  ((IStatementExpression)leftDisfixed).ValidAsStatement;
                    default:
                        return false;
                }
            }
        }

        #endregion

        public override BinaryOperationKind OperationKind
        {
            get {
                switch (this.Operation)
                {
                    case AssignmentOperation.SimpleAssign:
                        return BinaryOperationKind.Assign;
                    case AssignmentOperation.MultiplicationAssign:
                        return BinaryOperationKind.AssignMultiply;
                    case AssignmentOperation.DivisionAssign:
                        return BinaryOperationKind.AssignDivide;
                    case AssignmentOperation.ModulusAssign:
                        return BinaryOperationKind.AssignModulus;
                    case AssignmentOperation.AddAssign:
                        return BinaryOperationKind.AssignAdd;
                    case AssignmentOperation.SubtractionAssign:
                        return BinaryOperationKind.AssignSubtract;
                    case AssignmentOperation.LeftShiftAssign:
                        return BinaryOperationKind.AssignLeftShift;
                    case AssignmentOperation.RightShiftAssign:
                        return BinaryOperationKind.AssignRightShift;
                    case AssignmentOperation.BitwiseAndAssign:
                        return BinaryOperationKind.AssignBitwiseAnd;
                    case AssignmentOperation.BitwiseOrAssign:
                        return BinaryOperationKind.AssignBitwiseOr;
                    case AssignmentOperation.BitwiseExclusiveOrAssign:
                        return BinaryOperationKind.AssignBitwiseExclusiveOr;
                }
                return BinaryOperationKind.Term;
            }

        }
    }
}
