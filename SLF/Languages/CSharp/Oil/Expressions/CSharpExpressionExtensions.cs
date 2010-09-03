using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public static class CSharpExpressionExtensions
    {
        /// <summary>
        /// Returns a <see cref="CSharpUnaryOperationExpression"/> with the <paramref name="target"/>'s
        /// sign inverted.
        /// </summary>
        /// <param name="target">The <see cref="IExpression"/> to negate.</param>
        /// <returns>A new <see cref="UnaryOperationExpression"/> instance with
        /// the <paramref name="target"/> as a <see cref="CSharpOperatorPrecedences.UnaryTerm"/>
        /// with the <see cref="CSharpUnaryOperation.Negate"/> applied.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="target"/> is null.</exception>
        public static CSharpUnaryOperationExpression Negate(this IExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            return new CSharpUnaryOperationExpression((IUnaryOperationPrimaryTerm)target.AffixTo(CSharpOperatorPrecedences.UnaryTerm), CSharpUnaryOperation.SignInversion);
        }

        /// <summary>
        /// Returns a <see cref="CSharpUnaryOperationExpression"/> with the
        /// <paramref name="target"/> to setup to be logically inverted.
        /// </summary>
        /// <param name="target">The <see cref="IExpression"/> to boolInvert.</param>
        /// <returns>A new <see cref="UnaryOperationExpression"/> instance with
        /// the <paramref name="target"/> as a <see cref="CSharpOperatorPrecedences.UnaryTerm"/>
        /// with the <see cref="CSharpUnaryOperation.BooleanInversion"/> applied.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="target"/> is null.</exception>
        public static CSharpUnaryOperationExpression Not(this IExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null.", "target");
            return new CSharpUnaryOperationExpression((IUnaryOperationPrimaryTerm)target.AffixTo(CSharpOperatorPrecedences.UnaryTerm), CSharpUnaryOperation.BooleanInversion);
        }

        /// <summary>
        /// Returns a <see cref="CSharpUnaryOperationExpression"/> with the
        /// <paramref name="target"/> to be setup for the runtime value's
        /// bits to be inverted.
        /// </summary>
        /// <param name="target">The <see cref="Boolean"/> <see cref="IExpression"/> to
        /// invert the bits of.</param>
        /// <returns>A <see cref="CSharpUnaryOperationExpression"/> instance 
        /// with the <paramref name="target"/>'s bits set to be inverted.</returns>
        public static CSharpUnaryOperationExpression Invert(this IExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target cannot be null", "target");
            return new CSharpUnaryOperationExpression(((IUnaryOperationPrimaryTerm)(target.AffixTo(CSharpOperatorPrecedences.UnaryTerm))), CSharpUnaryOperation.BitwiseInversion);
        }

        public static CSharpLogicalAndExpression ConditionalAnd(this IExpression leftSide, IExpression rightSide)
        {
            if (leftSide == null)
                throw new ArgumentNullException("The left side of the operation cannot be null.", "leftSide");
            if (rightSide == null)
                throw new ArgumentNullException("The right side of the operation cannot be null.", "rightSide");
            return new CSharpLogicalAndExpression(
                ((ICSharpLogicalAndExpression)(leftSide .AffixTo(CSharpOperatorPrecedences.LogicalAndOperation))), 
                ((ICSharpBitwiseOrExpression) (rightSide.AffixTo(CSharpOperatorPrecedences.BitwiseOrOperation))));
        }

        public static CSharpLogicalOrExpression ConditionalOr(this IExpression leftSide, IExpression rightSide)
        {
            if (leftSide == null)
                throw new ArgumentNullException("The left side of the operation cannot be null.", "leftSide");
            if (rightSide == null)
                throw new ArgumentNullException("The right side of the operation cannot be null.", "rightSide");
            return new CSharpLogicalOrExpression(((ICSharpLogicalOrExpression)(
                leftSide.AffixTo(CSharpOperatorPrecedences.LogicalOrOperation))), ((ICSharpLogicalAndExpression)(
                rightSide.AffixTo(CSharpOperatorPrecedences.LogicalAndOperation))));
        }

        public static ICSharpRelationalExpression As(this IExpression target, IReferenceType type)
        {
            return new CSharpRelationalExpression(((ICSharpRelationalExpression)target.AffixTo(CSharpOperatorPrecedences.RelationalOperation)), CSharpRelationalOperation.TypeCastOrNull, (ICSharpShiftExpression)type.GetTypeExpression().AffixTo(CSharpOperatorPrecedences.ShiftOperation));
        }

        public static ICSharpRelationalExpression Is(this IExpression target, IType type)
        {
            return new CSharpRelationalExpression(((ICSharpRelationalExpression)target.AffixTo(CSharpOperatorPrecedences.RelationalOperation)), CSharpRelationalOperation.TypeCheck, (ICSharpShiftExpression)type.GetTypeExpression().AffixTo(CSharpOperatorPrecedences.ShiftOperation));
        }

        public static ICSharpRelationalExpression GreaterThan(this IExpression source, IExpression target)
        {
            return new CSharpRelationalExpression((ICSharpRelationalExpression)source.AffixTo(CSharpOperatorPrecedences.RelationalOperation), CSharpRelationalOperation.GreaterThan, (ICSharpShiftExpression)target.AffixTo(CSharpOperatorPrecedences.ShiftOperation));
        }

        internal static CSharpOperatorPrecedences GetPrecedence(this IExpression operation)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");
            ExpressionKind kind = operation.Type;
            ExpressionKind.ActiveSectorFlags flags = kind.ActiveSectors;
            bool binaryOps = ((flags & ExpressionKind.ActiveSectorFlags.BinaryOperationExpression) == ExpressionKind.ActiveSectorFlags.BinaryOperationExpression),
                 unaryOps = ((flags & ExpressionKind.ActiveSectorFlags.UnaryOperationExpression) == ExpressionKind.ActiveSectorFlags.UnaryOperationExpression),
                 prims = (flags & ExpressionKind.ActiveSectorFlags.PrimitiveExpression) == ExpressionKind.ActiveSectorFlags.PrimitiveExpression,
                 refs = (flags & ExpressionKind.ActiveSectorFlags.ReferenceExpression) == ExpressionKind.ActiveSectorFlags.ReferenceExpression,
                 calls = (flags & ExpressionKind.ActiveSectorFlags.InvocationExpression) == ExpressionKind.ActiveSectorFlags.InvocationExpression,
                 expansions = (flags & ExpressionKind.ActiveSectorFlags.ExpansionRequiredExpression) == ExpressionKind.ActiveSectorFlags.ExpansionRequiredExpression,
                 specials = (flags & ExpressionKind.ActiveSectorFlags.SpecialFunctionExpression) == ExpressionKind.ActiveSectorFlags.SpecialFunctionExpression,
                 symbs = (flags & ExpressionKind.ActiveSectorFlags.SymbolExpression) == ExpressionKind.ActiveSectorFlags.SymbolExpression;
            if (binaryOps)
            {
                switch (kind.BinaryOperations)
                {
                    case ExpressionKind.BinaryOperationSector.BinaryForwardTerm:
                        if (operation is ICSharpAssignExpression)
                            return CSharpOperatorPrecedences.AssignmentOperation;
                        else if (operation is ICSharpAddSubtExpression)
                            return CSharpOperatorPrecedences.AddSubtOperation;
                        else if (operation is ICSharpMulDivExpression)
                            return CSharpOperatorPrecedences.MulDivOperation;
                        else if (operation is ICSharpLogicalOrExpression)
                            return CSharpOperatorPrecedences.LogicalOrOperation;
                        else if (operation is ICSharpLogicalAndExpression)
                            return CSharpOperatorPrecedences.LogicalAndOperation;
                        else if (operation is ICSharpBitwiseOrExpression)
                            return CSharpOperatorPrecedences.BitwiseOrOperation;
                        else if (operation is ICSharpBitwiseAndExpression)
                            return CSharpOperatorPrecedences.BitwiseAndOperation;
                        else if (operation is ICSharpBitwiseExclusiveOrExpression)
                            return CSharpOperatorPrecedences.BitwiseExclusiveOrOperation;
                        else if (operation is ICSharpInequalityExpression)
                            return CSharpOperatorPrecedences.InequalityOperation;
                        else if (operation is ICSharpRelationalExpression)
                            return CSharpOperatorPrecedences.RelationalOperation;
                        else if (operation is ICSharpShiftExpression)
                            return CSharpOperatorPrecedences.ShiftOperation;
                        else
                            return CSharpOperatorPrecedences.NoPrecedence;
                    case ExpressionKind.BinaryOperationSector.AssignExpression:
                    case ExpressionKind.BinaryOperationSector.AssignMultiplyOperation:
                    case ExpressionKind.BinaryOperationSector.AssignDivideOperation:
                    case ExpressionKind.BinaryOperationSector.AssignModulusOperation:
                    case ExpressionKind.BinaryOperationSector.AssignAddOperation:
                    case ExpressionKind.BinaryOperationSector.AssignSubtractOperation:
                    case ExpressionKind.BinaryOperationSector.AssignLeftShiftOperation:
                    case ExpressionKind.BinaryOperationSector.AssignRightShiftOperation:
                    case ExpressionKind.BinaryOperationSector.AssignBitwiseAndOperation:
                    case ExpressionKind.BinaryOperationSector.AssignBitwiseOrOperation:
                    case ExpressionKind.BinaryOperationSector.AssignBitwiseExclusiveOrOperation:
                        return CSharpOperatorPrecedences.AssignmentOperation;
                    case ExpressionKind.BinaryOperationSector.LogicalOrOperation:
                        return CSharpOperatorPrecedences.LogicalOrOperation;
                    case ExpressionKind.BinaryOperationSector.LogicalAndOperation:
                        return CSharpOperatorPrecedences.LogicalAndOperation;
                    case ExpressionKind.BinaryOperationSector.BitwiseOrOperation:
                        return CSharpOperatorPrecedences.BitwiseOrOperation;
                    case ExpressionKind.BinaryOperationSector.BitwiseExclusiveOrOperation:
                        return CSharpOperatorPrecedences.BitwiseExclusiveOrOperation;
                    case ExpressionKind.BinaryOperationSector.BitwiseAndOperation:
                        return CSharpOperatorPrecedences.BitwiseAndOperation;
                    case ExpressionKind.BinaryOperationSector.InequalityOperation:
                    case ExpressionKind.BinaryOperationSector.EqualityOperation:
                        return CSharpOperatorPrecedences.InequalityOperation;
                    case ExpressionKind.BinaryOperationSector.LessThanOperation:
                    case ExpressionKind.BinaryOperationSector.LessThanOrEqualToOperation:
                    case ExpressionKind.BinaryOperationSector.GreaterThanOperation:
                    case ExpressionKind.BinaryOperationSector.GreaterThanOrEqualToOperation:
                    case ExpressionKind.BinaryOperationSector.TypeCheckOperation:
                    case ExpressionKind.BinaryOperationSector.TypeCastOrNull:
                        return CSharpOperatorPrecedences.RelationalOperation;
                    case ExpressionKind.BinaryOperationSector.ShiftLeftOperation:
                    case ExpressionKind.BinaryOperationSector.ShiftRightOperation:
                        return CSharpOperatorPrecedences.ShiftOperation;
                    case ExpressionKind.BinaryOperationSector.AddOperation:
                    case ExpressionKind.BinaryOperationSector.SubtractOperation:
                        return CSharpOperatorPrecedences.AddSubtOperation;
                    case ExpressionKind.BinaryOperationSector.MultiplyOperation:
                    case ExpressionKind.BinaryOperationSector.StrictDivisionOperation:
                    case ExpressionKind.BinaryOperationSector.ModulusOperation:
                        return CSharpOperatorPrecedences.MulDivOperation;
                }
            }
            if (unaryOps)
            {
                switch (kind.UnaryOperators)
                {
                    case ExpressionKind.UnaryOperationSector.UnaryForwardTerm:
                    case ExpressionKind.UnaryOperationSector.UnaryPreincrement:
                    case ExpressionKind.UnaryOperationSector.UnaryPostincrement:
                    case ExpressionKind.UnaryOperationSector.UnaryPredecrement:
                    case ExpressionKind.UnaryOperationSector.UnaryPostdecrement:
                    case ExpressionKind.UnaryOperationSector.UnaryBooleanInversion:
                    case ExpressionKind.UnaryOperationSector.UnaryBitwiseInversion:
                    case ExpressionKind.UnaryOperationSector.UnarySignInversionOperation:
                        return CSharpOperatorPrecedences.UnaryOperation;
                }
            }
            if (prims)
            {
                switch (kind.PrimitiveInserts)
                {
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveSByteInsert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveByteInsert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveBooleanInsert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveCharInsert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveUInt16Insert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveInt16Insert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveUInt32Insert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveInt32Insert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveUInt64Insert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveInt64Insert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveSingleInsert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveDoubleInsert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveDecimalInsert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveStringInsert:
                    case ExpressionKind.PrimitiveInsertSector.PrimitiveNullInsert:
                        return CSharpOperatorPrecedences.UnaryTerm;
                }
            }
            else if (refs)
            {
                switch (kind.ReferenceKinds)
                {
                    case ExpressionKind.ReferenceSector.LocalReference:
                    case ExpressionKind.ReferenceSector.EventReference:
                    case ExpressionKind.ReferenceSector.TypeReference:
                    case ExpressionKind.ReferenceSector.MethodReference:
                    case ExpressionKind.ReferenceSector.PropertyReference:
                    case ExpressionKind.ReferenceSector.ThisReference:
                    case ExpressionKind.ReferenceSector.BaseReference:
                    case ExpressionKind.ReferenceSector.SelfReference:
                    case ExpressionKind.ReferenceSector.FieldReference:
                    case ExpressionKind.ReferenceSector.IndexerReference:
                    case ExpressionKind.ReferenceSector.CurrentTypeReference:
                        return CSharpOperatorPrecedences.UnaryTerm;
                }
            }
            else if (calls)
                switch (kind.Invocations)
                {
                    case ExpressionKind.InvocationSector.EventFire:
                    case ExpressionKind.InvocationSector.MethodCall:
                    case ExpressionKind.InvocationSector.MultiCastDelegateCall:
                        return CSharpOperatorPrecedences.UnaryTerm;
                }
            else if (specials)
                switch (kind.SpecialFunctions)
                {
                    case ExpressionKind.SpecialFunctionSector.TypeCast:
                    case ExpressionKind.SpecialFunctionSector.CheckedExpression:
                    case ExpressionKind.SpecialFunctionSector.UncheckedExpression:
                    case ExpressionKind.SpecialFunctionSector.TypeOfExpression:
                        return CSharpOperatorPrecedences.UnaryTerm;
                }
            else if (symbs)
                switch (kind.Symbols)
                {
                    case ExpressionKind.SymbolSector.ExpressionFusion:
                    case ExpressionKind.SymbolSector.ExpressionToCommaFusion:
                    case ExpressionKind.SymbolSector.ExpressionToTypeCollectionFusion:
                    case ExpressionKind.SymbolSector.SymbolExpression:
                    case ExpressionKind.SymbolSector.ParenthesizedExpression:
                        return CSharpOperatorPrecedences.UnaryTerm;
                }
            return CSharpOperatorPrecedences.NoPrecedence;
        }

        /// <summary>
        /// Affixes the given <paramref name="target"/> <see cref="IExpression"/> 
        /// to the <paramref name="targetPrecedence"/>.
        /// </summary>
        /// <param name="target">The <see cref="IExpression"/> to affix to 
        /// <paramref name="targetPrecedence"/>.</param>
        /// <param name="targetPrecedence">The <see cref="CSharpOperatorPrecedences"/> 
        /// that the expression should yield upon return.</param>
        /// <returns>A new <see cref="IExpression"/> affixed to the 
        /// <paramref name="targetPrecedence"/> for use in a binary 
        /// operation.</returns>
        public static IExpression AffixTo(this IExpression target, CSharpOperatorPrecedences targetPrecedence)
        {
            CSharpOperatorPrecedences currentPrecedence = target.GetPrecedence();
            if (currentPrecedence == targetPrecedence)
                return target;
            if ((currentPrecedence.GetIndex() > targetPrecedence.GetIndex()) ||
                (target is IParenthesizedExpression))
                return WrapFull(target, targetPrecedence);
            return WrapFull(new ParenthesizedExpression(target), targetPrecedence);
        }

        internal static int GetIndex(this CSharpOperatorPrecedences target)
        {
            switch (target)
            {
                case CSharpOperatorPrecedences.AssignmentOperation:
                    return 1;
                case CSharpOperatorPrecedences.ConditionalOperation:
                    return 2;
                case CSharpOperatorPrecedences.LogicalOrOperation:
                    return 3;
                case CSharpOperatorPrecedences.LogicalAndOperation:
                    return 4;
                case CSharpOperatorPrecedences.BitwiseOrOperation:
                    return 4;
                case CSharpOperatorPrecedences.BitwiseExclusiveOrOperation:
                    return 5;
                case CSharpOperatorPrecedences.BitwiseAndOperation:
                    return 6;
                case CSharpOperatorPrecedences.InequalityOperation:
                    return 7;
                case CSharpOperatorPrecedences.RelationalOperation:
                    return 8;
                case CSharpOperatorPrecedences.ShiftOperation:
                    return 9;
                case CSharpOperatorPrecedences.AddSubtOperation:
                    return 10;
                case CSharpOperatorPrecedences.MulDivOperation:
                    return 11;
                case CSharpOperatorPrecedences.UnaryOperation:
                    return 12;
                case CSharpOperatorPrecedences.UnaryTerm:
                    return 13;
                case CSharpOperatorPrecedences.NoPrecedence:
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Removes a previous <see cref="AffixTo(IExpression, CSharpOperatorPrecedences)"/> casing on 
        /// a given <paramref name="target"/>.
        /// </summary>
        /// <param name="target">The <see cref="IExpression"/> 
        /// which has had <see cref="AffixTo(IExpression, CSharpOperatorPrecedences)"/>
        /// called on it.</param>
        /// <returns>The <see cref="IExpression"/> instance as it was 
        /// before the <see cref="AffixTo"/> was called on it..</returns>
        public static IExpression Disfix(this ICSharpExpression target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            ExpressionKind targetKind = target.Type;
            var activeSectors = targetKind.ActiveSectors;
            if (((activeSectors & ExpressionKind.ActiveSectorFlags.BinaryOperationExpression) == ExpressionKind.ActiveSectorFlags.BinaryOperationExpression) &&
                ((targetKind.BinaryOperations & ExpressionKind.BinaryOperationSector.BinaryForwardTerm) == ExpressionKind.BinaryOperationSector.BinaryForwardTerm))
            {
                if (target is ICSharpAssignExpression)
                    return ((ICSharpAssignExpression)(target)).LeftSide.Disfix();
                else if (target is ICSharpAddSubtExpression)
                    return ((ICSharpAddSubtExpression)(target)).RightSide.Disfix();
                else if (target is ICSharpMulDivExpression)
                    return ((ICSharpMulDivExpression)(target)).RightSide.Disfix();
                else if (target is ICSharpLogicalOrExpression)
                    return ((ICSharpLogicalOrExpression)(target)).RightSide.Disfix();
                else if (target is ICSharpLogicalAndExpression)
                    return ((ICSharpLogicalAndExpression)(target)).RightSide.Disfix();
                else if (target is ICSharpBitwiseOrExpression)
                    return ((ICSharpBitwiseOrExpression)(target)).RightSide.Disfix();
                else if (target is ICSharpBitwiseAndExpression)
                    return ((ICSharpBitwiseAndExpression)(target)).RightSide.Disfix();
                else if (target is ICSharpBitwiseExclusiveOrExpression)
                    return ((ICSharpBitwiseExclusiveOrExpression)(target)).RightSide.Disfix();
                else if (target is ICSharpInequalityExpression)
                    return ((ICSharpInequalityExpression)(target)).RightSide.Disfix();
                else if (target is ICSharpRelationalExpression)
                    return ((ICSharpRelationalExpression)(target)).RightSide.Disfix();
                else if (target is ICSharpShiftExpression)
                    return ((ICSharpShiftExpression)(target)).RightSide.Disfix();
            }
            else if ((((activeSectors & ExpressionKind.ActiveSectorFlags.UnaryOperationExpression) == ExpressionKind.ActiveSectorFlags.UnaryOperationExpression)) &&
                      ((targetKind.UnaryOperators & ExpressionKind.UnaryOperationSector.UnaryForwardTerm) == ExpressionKind.UnaryOperationSector.UnaryForwardTerm) &&
                       (target is ICSharpUnaryOperationExpression))
                return ((ICSharpUnaryOperationExpression)(target)).Term;
            return target;
        }

        private static IExpression WrapFull(IExpression exprB, CSharpOperatorPrecedences targetPrecedence)
        {
            CSharpOperatorPrecedences currentPrecedence = exprB.GetPrecedence();
            if (currentPrecedence == targetPrecedence)
                return exprB;
            switch (currentPrecedence)
            {
                case CSharpOperatorPrecedences.ConditionalOperation:
                    return new CSharpAssignExpression(((ICSharpConditionalExpression)(exprB)));
                case CSharpOperatorPrecedences.LogicalOrOperation:
                    return new CSharpConditionalExpression((ICSharpLogicalOrExpression)exprB);
                case CSharpOperatorPrecedences.LogicalAndOperation:
                    return WrapFull(new CSharpLogicalOrExpression(((ICSharpLogicalAndExpression)(exprB))), targetPrecedence);
                case CSharpOperatorPrecedences.BitwiseOrOperation:
                    return WrapFull(new CSharpLogicalAndExpression(((ICSharpBitwiseOrExpression)(exprB))), targetPrecedence);
                case CSharpOperatorPrecedences.BitwiseExclusiveOrOperation:
                    return WrapFull(new CSharpBitwiseOrExpression(((ICSharpBitwiseExclusiveOrExpression)(exprB))), targetPrecedence);
                case CSharpOperatorPrecedences.BitwiseAndOperation:
                    return WrapFull(new CSharpBitwiseExclusiveOrExpression(((ICSharpBitwiseAndExpression)(exprB))), targetPrecedence);
                case CSharpOperatorPrecedences.InequalityOperation:
                    return WrapFull(new CSharpBitwiseAndExpression(((ICSharpInequalityExpression)(exprB))), targetPrecedence);
                case CSharpOperatorPrecedences.RelationalOperation:
                    return WrapFull(new CSharpInequalityExpression((ICSharpRelationalExpression)exprB), targetPrecedence);
                case CSharpOperatorPrecedences.ShiftOperation:
                    return WrapFull(new CSharpRelationalExpression((ICSharpShiftExpression)exprB), targetPrecedence);
                case CSharpOperatorPrecedences.AddSubtOperation:
                    return WrapFull(new CSharpShiftExpression((ICSharpAddSubtExpression)exprB), targetPrecedence);
                case CSharpOperatorPrecedences.MulDivOperation:
                    return WrapFull(new CSharpAddSubtExpression((ICSharpMulDivExpression)exprB), targetPrecedence);
                case CSharpOperatorPrecedences.UnaryOperation:
                    return WrapFull(new CSharpMulDivExpression((ICSharpUnaryOperationExpression)exprB), targetPrecedence);
                case CSharpOperatorPrecedences.UnaryTerm:
                    return WrapFull(new CSharpUnaryOperationExpression((IUnaryOperationPrimaryTerm)exprB), targetPrecedence);
                case CSharpOperatorPrecedences.AssignmentOperation:
                default:
                    throw new ArgumentException("Cannot have a precedence level below Assign.", "exprB");
            }
        }

        public static CSharpAddSubtExpression Add(this IExpression target, IExpression operand)
        {
            return new CSharpAddSubtExpression((ICSharpAddSubtExpression)target.AffixTo(CSharpOperatorPrecedences.AddSubtOperation), CSharpAddSubtOperation.Addition, (ICSharpMulDivExpression)operand.AffixTo(CSharpOperatorPrecedences.MulDivOperation));
        }

        public static CSharpAddSubtExpression Subtract(this IExpression target, IExpression operand)
        {
            return new CSharpAddSubtExpression((ICSharpAddSubtExpression)target.AffixTo(CSharpOperatorPrecedences.AddSubtOperation), CSharpAddSubtOperation.Subtraction, (ICSharpMulDivExpression)operand.AffixTo(CSharpOperatorPrecedences.MulDivOperation));
        }

        public static CSharpMulDivExpression Divide(this IExpression target, IExpression operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Division, (ICSharpUnaryOperationExpression)operand.AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Modulus(this IExpression target, IExpression operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Remainder, (ICSharpUnaryOperationExpression)operand.AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Multiply(this IExpression target, IExpression operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Multiplication, (ICSharpUnaryOperationExpression)operand.AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Multiply(this int target, int operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.ToPrimitive().AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Multiplication, (ICSharpUnaryOperationExpression)operand.ToPrimitive().AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Multiply(this IExpression target, int operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Multiplication, (ICSharpUnaryOperationExpression)operand.ToPrimitive().AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Multiply(this int target, IExpression operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.ToPrimitive().AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Multiplication, (ICSharpUnaryOperationExpression)operand.AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Divide(this int target, int operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.ToPrimitive().AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Division, (ICSharpUnaryOperationExpression)operand.ToPrimitive().AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Divide(this IExpression target, int operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Division, (ICSharpUnaryOperationExpression)operand.ToPrimitive().AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Divide(this int target, IExpression operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.ToPrimitive().AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Division, (ICSharpUnaryOperationExpression)operand.AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Modulus(this int target, int operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.ToPrimitive().AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Remainder, (ICSharpUnaryOperationExpression)operand.ToPrimitive().AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Modulus(this IExpression target, int operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Remainder, (ICSharpUnaryOperationExpression)operand.ToPrimitive().AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpMulDivExpression Modulus(this int target, IExpression operand)
        {
            return new CSharpMulDivExpression((ICSharpMulDivExpression)target.ToPrimitive().AffixTo(CSharpOperatorPrecedences.MulDivOperation), CSharpMulDivOperation.Remainder, (ICSharpUnaryOperationExpression)operand.AffixTo(CSharpOperatorPrecedences.UnaryOperation));
        }

        public static CSharpAddSubtExpression Add(this int target, int operand)
        {
            return new CSharpAddSubtExpression((ICSharpAddSubtExpression)target.ToPrimitive().AffixTo(CSharpOperatorPrecedences.AddSubtOperation), CSharpAddSubtOperation.Addition, (ICSharpMulDivExpression)operand.ToPrimitive().AffixTo(CSharpOperatorPrecedences.MulDivOperation));
        }

        public static CSharpAddSubtExpression Add(this IExpression target, int operand)
        {
            return new CSharpAddSubtExpression((ICSharpAddSubtExpression)target.AffixTo(CSharpOperatorPrecedences.AddSubtOperation), CSharpAddSubtOperation.Addition, (ICSharpMulDivExpression)operand.ToPrimitive().AffixTo(CSharpOperatorPrecedences.MulDivOperation));
        }

        public static CSharpAddSubtExpression Add(this int target, IExpression operand)
        {
            return new CSharpAddSubtExpression((ICSharpAddSubtExpression)target.ToPrimitive().AffixTo(CSharpOperatorPrecedences.AddSubtOperation), CSharpAddSubtOperation.Addition, (ICSharpMulDivExpression)operand.AffixTo(CSharpOperatorPrecedences.MulDivOperation));
        }

        public static CSharpAddSubtExpression Subtract(this int target, int operand)
        {
            return new CSharpAddSubtExpression((ICSharpAddSubtExpression)target.ToPrimitive().AffixTo(CSharpOperatorPrecedences.AddSubtOperation), CSharpAddSubtOperation.Subtraction, (ICSharpMulDivExpression)operand.ToPrimitive().AffixTo(CSharpOperatorPrecedences.MulDivOperation));
        }

        public static CSharpAddSubtExpression Subtract(this IExpression target, int operand)
        {
            return new CSharpAddSubtExpression((ICSharpAddSubtExpression)target.AffixTo(CSharpOperatorPrecedences.AddSubtOperation), CSharpAddSubtOperation.Subtraction, (ICSharpMulDivExpression)operand.ToPrimitive().AffixTo(CSharpOperatorPrecedences.MulDivOperation));
        }

        public static CSharpAddSubtExpression Subtract(this int target, IExpression operand)
        {
            return new CSharpAddSubtExpression((ICSharpAddSubtExpression)target.ToPrimitive().AffixTo(CSharpOperatorPrecedences.AddSubtOperation), CSharpAddSubtOperation.Subtraction, (ICSharpMulDivExpression)operand.AffixTo(CSharpOperatorPrecedences.MulDivOperation));
        }
        public static CSharpInequalityExpression EqualTo(this IExpression target, IExpression operand)
        {
            return new CSharpInequalityExpression((ICSharpInequalityExpression)target.AffixTo(CSharpOperatorPrecedences.InequalityOperation), true, (ICSharpRelationalExpression)operand.AffixTo(CSharpOperatorPrecedences.RelationalOperation));
        }
        public static CSharpInequalityExpression InequalTo(this IExpression target, IExpression operand)
        {
            return new CSharpInequalityExpression((ICSharpInequalityExpression)target.AffixTo(CSharpOperatorPrecedences.InequalityOperation), false, (ICSharpRelationalExpression)operand.AffixTo(CSharpOperatorPrecedences.RelationalOperation));
        }

        public static CSharpConditionalExpression IIf(this IExpression check, IExpression truePart, IExpression falsePart)
        {
            return new CSharpConditionalExpression((ICSharpLogicalOrExpression)check.AffixTo(CSharpOperatorPrecedences.LogicalOrOperation), (ICSharpConditionalExpression)truePart.AffixTo(CSharpOperatorPrecedences.ConditionalOperation), (ICSharpConditionalExpression)falsePart.AffixTo(CSharpOperatorPrecedences.ConditionalOperation));
        }
    }
}
