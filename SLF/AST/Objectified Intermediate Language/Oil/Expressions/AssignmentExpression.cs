using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class AssignmentExpression :
        ExpressionBase,
        IAssignmentExpression
    {

        /// <summary>
        /// Data member for <see cref="AssignmentExpression"/>.
        /// </summary>
        private AssignmentOperation operation;
        /// <summary>
        /// Creates a new non-operational <see cref="AssignmentExpression"/> with the <paramref name="term"/>
        /// provided.
        /// </summary>
        /// <param name="term">The term the non-operational <see cref="AssignmentExpression"/>
        /// points to.</param>
        public AssignmentExpression(INaryOperandExpression term)
            : base()
        {
            operation = AssignmentOperation.Term;
            this.LeftSide = term;
        }

        /// <summary>
        /// Creates a new <see cref="AssignmentExpression"/> instance
        /// with the <paramref name="leftSide"/>, <paramref name="operation"/>
        /// and <paramref name="rightSide"/> provided.
        /// </summary>
        /// <param name="leftSide">The <see cref="ICSharpConditionalExpression"/> which makes up the
        /// left operand.</param>
        /// <param name="operation">The <see cref="AssignmentOperation"/> which indicates the operation
        /// the current <see cref="AssignmentExpression"/> represents.</param>
        /// <param name="rightSide">The <see cref="IAssignmentExpression"/> which makes up the 
        /// right operand.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="leftSide"/> or 
        /// <paramref name="rightSide"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="operation"/>
        /// is a value not within the <see cref="AssignmentOperation"/> enumeration; or when
        /// <paramref name="operation"/> is <see cref="AssignmentOperation.Term"/>.</exception>
        public AssignmentExpression(INaryOperandExpression leftSide, AssignmentOperation operation, INaryOperandExpression rightSide)
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
            this.operation = operation;
            this.LeftSide = leftSide;
            this.RightSide = rightSide;
        }

        /// <summary>
        /// Returns the type of expression the <see cref="AssignmentExpression"/> is.
        /// </summary>
        /// <remarks>Returns based upon the <see cref="Operation"/>.</remarks>
        public override ExpressionKinds Type
        {
            get {
                switch (this.Operation)
                {
                    case AssignmentOperation.SimpleAssign:
                        return ExpressionKinds.AssignExpression;
                    case AssignmentOperation.MultiplicationAssign:
                        return ExpressionKinds.AssignMultiplyOperation;
                    case AssignmentOperation.DivisionAssign:
                        return ExpressionKinds.AssignDivideOperation;
                    case AssignmentOperation.ModulusAssign:
                        return ExpressionKinds.AssignModulusOperation;
                    case AssignmentOperation.AddAssign:
                        return ExpressionKinds.AssignAddOperation;
                    case AssignmentOperation.SubtractionAssign:
                        return ExpressionKinds.AssignSubtractOperation;
                    case AssignmentOperation.LeftShiftAssign:
                        return ExpressionKinds.AssignLeftShiftOperation;
                    case AssignmentOperation.RightShiftAssign:
                        return ExpressionKinds.AssignRightShiftOperation;
                    case AssignmentOperation.BitwiseAndAssign:
                        return ExpressionKinds.AssignBitwiseAndOperation;
                    case AssignmentOperation.BitwiseOrAssign:
                        return ExpressionKinds.AssignBitwiseOrOperation;
                    case AssignmentOperation.BitwiseExclusiveOrAssign:
                        return ExpressionKinds.AssignBitwiseExclusiveOrOperation;
                    case AssignmentOperation.Term:
                        return ExpressionKinds.BinaryForwardTerm;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Returns the <see cref="BinaryOperationAssociativity"/> associated to the 
        /// <see cref="AssignmentExpression"/>.
        /// </summary>
        /// <remarks>Returns <see cref="BinaryOperationAssociativity.Right"/>.</remarks>
        public BinaryOperationAssociativity Associativity
        {
            get { return BinaryOperationAssociativity.Right; }
        }

        #region IAssignmentExpression Members

        /// <summary>
        /// Returns/sets the <see cref="AssignmentOperation"/> associated to the <see cref="AssignmentExpression"/>.
        /// </summary>
        public AssignmentOperation Operation
        {
            get
            {
                if (this.RightSide == null)
                    return AssignmentOperation.Term;
                return this.operation;
            }
            set
            {
                switch (value)
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
                        throw new ArgumentOutOfRangeException("value");
                }
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
            visitor.Visit(this);
        }


        #region IBinaryOperationExpression Members

        public INaryOperandExpression LeftSide { get; set; }

        public INaryOperandExpression RightSide { get; set; }

        #endregion


        #region IStatementExpression Members

        public bool ValidAsStatement
        {
            get {
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
                        if (this.LeftSide is IStatementExpression)
                            return ((IStatementExpression)this.LeftSide).ValidAsStatement;
                        return false;
                    default:
                        return false;
                }
            }
        }

        #endregion
    }
}
