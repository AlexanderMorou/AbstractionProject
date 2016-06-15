using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Languages.Expressions;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    partial class CSharpProvider
    {
        internal class ExpressionService :
            IExpressionService<ICSharpProvider, ICSharpLanguage>
        {
            public ExpressionService(CSharpProvider provider)
            {
                this.Provider = provider;
            }

            public ICSharpProvider Provider { get; private set; }

            public ICSharpLanguage Language
            {
                get { return CSharpLanguage.Singleton; }
            }

            ILanguageProvider ILanguageService.Provider
            {
                get { return this.Provider; }
            }

            ILanguage ILanguageService.Language
            {
                get { return this.Language; }
            }

            IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
            {
                get
                {
                    return this.Provider;
                }
            }

            public Guid ServiceGuid
            {
                get { return LanguageGuids.Services.ExpressionService; }
            }

            public IExpression BinaryOperation(INaryOperandExpression left, BinaryOperationKind op, INaryOperandExpression right)
            {
                switch (op)
                {
                    case BinaryOperationKind.Assign:
                        return left.Add(right);
                    case BinaryOperationKind.AssignMultiply:
                        return new AssignmentExpression(left, AssignmentOperation.MultiplicationAssign, right);
                    case BinaryOperationKind.AssignModulus:
                        return new AssignmentExpression(left, AssignmentOperation.ModulusAssign, right);
                    case BinaryOperationKind.AssignDivide:
                        return new AssignmentExpression(left, AssignmentOperation.DivisionAssign, right);
                    case BinaryOperationKind.AssignAdd:
                        return new AssignmentExpression(left, AssignmentOperation.AddAssign, right);
                    case BinaryOperationKind.AssignSubtract:
                        return new AssignmentExpression(left, AssignmentOperation.SubtractionAssign, right);
                    case BinaryOperationKind.AssignLeftShift:
                        return new AssignmentExpression(left, AssignmentOperation.LeftShiftAssign, right);
                    case BinaryOperationKind.AssignRightShift:
                        return new AssignmentExpression(left, AssignmentOperation.RightShiftAssign, right);
                    case BinaryOperationKind.AssignBitwiseAnd:
                        return new AssignmentExpression(left, AssignmentOperation.BitwiseAndAssign, right);
                    case BinaryOperationKind.AssignBitwiseOr:
                        return new AssignmentExpression(left, AssignmentOperation.BitwiseOrAssign, right);
                    case BinaryOperationKind.AssignBitwiseExclusiveOr:
                        return new AssignmentExpression(left, AssignmentOperation.BitwiseExclusiveOrAssign, right);
                    case BinaryOperationKind.LogicalOr:
                        return left.LogicalOr(right);
                    case BinaryOperationKind.LogicalAnd:
                        return left.LogicalAnd(right);
                    case BinaryOperationKind.BitwiseOr:
                        return left.BitwiseOr(right);
                    case BinaryOperationKind.BitwiseExclusiveOr:
                        return left.BitwiseXOr(right);
                    case BinaryOperationKind.BitwiseAnd:
                        return left.BitwiseAnd(right);
                    case BinaryOperationKind.Inequality:
                        return left.InequalTo(right);
                    case BinaryOperationKind.Equality:
                        return left.EqualTo(right);
                    case BinaryOperationKind.LessThan:
                        return left.LessThan(right);
                    case BinaryOperationKind.LessThanOrEqualTo:
                        return left.LessThanOrEqualTo(right);
                    case BinaryOperationKind.GreaterThan:
                        return left.GreaterThan(right);
                    case BinaryOperationKind.GreaterThanOrEqualTo:
                        return left.GreaterThanOrEqualTo(right);
                    case BinaryOperationKind.LeftShift:
                        return left.Shift(CSharpShiftOperation.LeftShift, right);
                    case BinaryOperationKind.RightShift:
                        return left.Shift(CSharpShiftOperation.RightShift, right);
                    case BinaryOperationKind.Add:
                        return left.Add(right);
                    case BinaryOperationKind.Subtract:
                        return left.Subtract(right);
                    case BinaryOperationKind.Multiply:
                        return left.Multiply(right);
                    case BinaryOperationKind.Modulus:
                        return left.Modulus(right);
                    case BinaryOperationKind.StrictDivision:
                        return left.Division(right);
                    case BinaryOperationKind.IntegerDivision:
                    case BinaryOperationKind.FlexibleDivision:
                        throw new NotSupportedException();
                    default:
                        throw new ArgumentOutOfRangeException("op");
                }
            }
        }
    }
}
