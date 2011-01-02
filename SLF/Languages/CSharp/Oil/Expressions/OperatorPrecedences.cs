using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// The mathematical (or evaluational) precedence 
    /// yielded by a given <see cref="IExpression"/>.
    /// </summary>
    public enum CSharpOperatorPrecedences : 
        ulong
    {
        /// <summary>
        /// The <see cref="IExpression"/> cannot be
        /// used in a mathematical expression.
        /// </summary>
        NoPrecedence = ulong.MinValue,
        /// <summary>
        /// The <see cref="IExpression"/> is an assignment
        /// expression.
        /// </summary>
        AssignmentOperation =  (ulong)(ExpressionKind.BinaryOperationSector.AssignAddOperation | ExpressionKind.BinaryOperationSector.AssignSubtractOperation |
                                       ExpressionKind.BinaryOperationSector.AssignBitwiseAndOperation | ExpressionKind.BinaryOperationSector.AssignBitwiseOrOperation |
                                       ExpressionKind.BinaryOperationSector.AssignBitwiseExclusiveOrOperation |
                                       ExpressionKind.BinaryOperationSector.AssignDivideOperation | ExpressionKind.BinaryOperationSector.AssignMultiplyOperation |
                                       ExpressionKind.BinaryOperationSector.AssignLeftShiftOperation | ExpressionKind.BinaryOperationSector.AssignRightShiftOperation |
                                       ExpressionKind.BinaryOperationSector.AssignModulusOperation),
        /// <summary>
        /// The <see cref="IExpression"/> is a ternary conditional
        /// expression.
        /// </summary>
        ConditionalOperation = ExpressionKind.ExpansionRequiredSector.ConditionalOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a logical or
        /// expression.
        /// </summary>
        LogicalOrOperation = ExpressionKind.BinaryOperationSector.LogicalOrOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a logical
        /// and expression.
        /// </summary>
        LogicalAndOperation = ExpressionKind.BinaryOperationSector.LogicalAndOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a bitwise
        /// or expression.
        /// </summary>
        BitwiseOrOperation = ExpressionKind.BinaryOperationSector.BitwiseOrOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a bitwise
        /// exclusive expression.
        /// </summary>
        BitwiseExclusiveOrOperation = ExpressionKind.BinaryOperationSector.BitwiseExclusiveOrOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a bitwise
        /// and expression.
        /// </summary>
        BitwiseAndOperation = ExpressionKind.BinaryOperationSector.BitwiseAndOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is an [in]equality
        /// expression.
        /// </summary>
        InequalityOperation = ExpressionKind.BinaryOperationSector.InequalityOperation |
                              ExpressionKind.BinaryOperationSector.EqualityOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a relational
        /// check expression.
        /// </summary>
        RelationalOperation = ExpressionKind.BinaryOperationSector.LessThanOperation    | ExpressionKind.BinaryOperationSector.LessThanOrEqualToOperation    |
                              ExpressionKind.BinaryOperationSector.GreaterThanOperation | ExpressionKind.BinaryOperationSector.GreaterThanOrEqualToOperation |
                              ExpressionKind.BinaryOperationSector.TypeCheckOperation   | ExpressionKind.BinaryOperationSector.TypeCastOrNull,
        /// <summary>
        /// The <see cref="IExpression"/> is a bit-shift
        /// expression.
        /// </summary>
        ShiftOperation = ExpressionKind.BinaryOperationSector.ShiftLeftOperation | ExpressionKind.BinaryOperationSector.ShiftRightOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is an 
        /// addition or subtraction expression.
        /// </summary>
        AddSubtOperation = ExpressionKind.BinaryOperationSector.AddOperation | ExpressionKind.BinaryOperationSector.SubtractOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a multiplication,
        /// division, or modulus (remainder) expression.
        /// </summary>
        MulDivOperation = ExpressionKind.BinaryOperationSector.MultiplyOperation | ExpressionKind.BinaryOperationSector.StrictDivisionOperation | ExpressionKind.BinaryOperationSector.ModulusOperation,
        /// <summary>
        /// The <see cref="IExpression"/> is a unary expression.
        /// </summary>
        UnaryOperation = ExpressionKind.UnaryOperationSector.UnarySignInversionOperation | ExpressionKind.UnaryOperationSector.UnaryPreincrement | ExpressionKind.UnaryOperationSector.UnaryPredecrement | ExpressionKind.UnaryOperationSector.UnaryPostincrement | ExpressionKind.UnaryOperationSector.UnaryPostdecrement | ExpressionKind.UnaryOperationSector.UnaryBooleanInversion | ExpressionKind.UnaryOperationSector.UnaryForwardTerm,
        /// <summary>
        /// The <see cref="IExpression"/> is the term of a
        /// <see cref="UnaryOperation"/>.
        /// </summary>
        UnaryTerm = ExpressionKind.UnaryOperationSector.UnaryForwardTerm,
    }
}
