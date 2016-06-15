using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public static class StatementExtensions
    {
        public static ILockStatement Lock(this IBlockStatementParent parent, IExpression monitorLock)
        {
            var lockStatement = new LockStatement(parent, monitorLock);
            parent.Add(lockStatement);
            return lockStatement;
        }

        public static ILockStatement Lock(this IBlockStatementParent parent, ILocalMember localMember)
        {
            return parent.Lock(localMember.GetReference());
        }

        public static ILockStatement Lock(this IBlockStatementParent parent, IClassFieldMember fieldMember)
        {
            return parent.Lock(fieldMember.GetReference());
        }
        public static ILockStatement Lock(this IBlockStatementParent parent, IStructFieldMember fieldMember)
        {
            return parent.Lock(fieldMember.GetReference());
        }


        public static ITryStatement TryCatch(this IBlockStatementParent parent)
        {
            var result = new TryStatement(parent);
            result.HasCatchAll = true;
            parent.Add(result);
            return result;
        }

        public static ITryStatement TryCatch(this IBlockStatementParent parent, TypedName exceptionType)
        {
            var result = new TryStatement(parent);
            result.Catch(exceptionType);
            parent.Add(result);
            return result;
        }

        public static ITryStatement TryFinally(this IBlockStatementParent parent)
        {
            var result = new TryStatement(parent);
            result.HasFinally = true;
            parent.Add(result);
            return result;
        }


        public static IThrowStatement Throw(this IBlockStatementParent parent, IExpression throwTarget)
        {
            var throwStatement = new ThrowStatement(parent, throwTarget);
            parent.Add(throwStatement);
            return throwStatement;
        }

        public static IThrowStatement Throw(this IBlockStatementParent parent)
        {
            return parent.Throw(null);
        }

        public static IUsingExpressionBlockStatement Using(this IBlockStatementParent parent, IExpression resourceAcquisition)
        {
            var usingStatement = new UsingExpressionBlockStatement(parent, resourceAcquisition);
            parent.Add(usingStatement);
            return usingStatement;
        }

        public static IUsingBlockStatement Using(this IBlockStatementParent parent, ILocalDeclarationsStatement resourceAcquisition)
        {
            var usingStatement = new UsingBlockStatement(parent, resourceAcquisition);
            parent.Add(usingStatement);
            return usingStatement;
        }

        public static IUsingBlockStatement Using(this IBlockStatementParent parent, ILocalMember resourceAcquisition, params ILocalMember[] siblings)
        {
            var decl = resourceAcquisition.GetDeclarationStatement(siblings);
            var usingStatement = new UsingBlockStatement(parent, decl);
            parent.Add(usingStatement);
            return usingStatement;
        }
        #region Return Extensions
        public static IReturnStatement Return(this IBlockStatementParent blockParent, ILocalMember returnValue)
        {
            return blockParent.Return(returnValue.GetReference());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, IClassFieldMember returnValue)
        {
            return blockParent.Return(returnValue.GetReference());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, IClassPropertyMember returnValue)
        {
            return blockParent.Return(returnValue.GetReference());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, IEnumFieldMember returnValue)
        {
            return blockParent.Return(returnValue.GetReference());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, IInterfacePropertyMember returnValue)
        {
            return blockParent.Return(returnValue.GetReference());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, IStructFieldMember returnValue)
        {
            return blockParent.Return(returnValue.GetReference());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, IStructPropertyMember returnValue)
        {
            return blockParent.Return(returnValue.GetReference());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, IFieldReferenceExpression returnValue)
        {
            return blockParent.Return(returnValue);
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, IPropertyReferenceExpression returnValue)
        {
            return blockParent.Return(returnValue);
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, ILocalReferenceExpression returnValue)
        {
            return blockParent.Return(returnValue);
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, IParameterMember returnValue)
        {
            return blockParent.Return(returnValue.GetReference());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, IParameterReferenceExpression returnValue)
        {
            return blockParent.Return(returnValue);
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, INaryOperandExpression returnValue)
        {
            return blockParent.Return(returnValue);
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, sbyte returnValue)
        {
            return blockParent.Return(returnValue.ToPrimitive());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, byte returnValue)
        {
            return blockParent.Return(returnValue.ToPrimitive());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, short returnValue)
        {
            return blockParent.Return(returnValue.ToPrimitive());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, ushort returnValue)
        {
            return blockParent.Return(returnValue.ToPrimitive());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, int returnValue)
        {
            return blockParent.Return(returnValue.ToPrimitive());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, uint returnValue)
        {
            return blockParent.Return(returnValue.ToPrimitive());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, long returnValue)
        {
            return blockParent.Return(returnValue.ToPrimitive());
        }
        public static IReturnStatement Return(this IBlockStatementParent blockParent, ulong returnValue)
        {
            return blockParent.Return(returnValue.ToPrimitive());
        }

        public static IExplicitStringLiteralStatement Literal(this IBlockStatementParent block, string literal)
        {
            IExplicitStringLiteralStatement result = new ExplicitStringLiteralStatement(block) { Literal = literal };
            block.Add(result);
            return result;
        }
        #endregion

        #region YieldReturn Extensions
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, ILocalMember returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.GetReference());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IClassFieldMember returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.GetReference());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IClassPropertyMember returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.GetReference());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IEnumFieldMember returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.GetReference());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IInterfacePropertyMember returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.GetReference());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IStructFieldMember returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.GetReference());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IStructPropertyMember returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.GetReference());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IFieldReferenceExpression returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue);
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IPropertyReferenceExpression returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue);
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, ILocalReferenceExpression returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue);
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IParameterMember returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.GetReference());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IParameterReferenceExpression returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue);
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, INaryOperandExpression returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue);
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, sbyte returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.ToPrimitive());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, byte returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.ToPrimitive());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, short returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.ToPrimitive());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, ushort returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.ToPrimitive());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, int returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.ToPrimitive());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, uint returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.ToPrimitive());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, long returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.ToPrimitive());
        }
        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, ulong returnValue)
        {
            return blockParent.YieldReturn((IExpression)returnValue.ToPrimitive());
        }

        public static IYieldReturnStatement YieldReturn(this IBlockStatementParent blockParent, IExpression returnValue)
        {
            var yieldResult = new YieldReturnStatement(blockParent, returnValue);
            blockParent.Add(yieldResult);
            return yieldResult;
        }
        #endregion

        public static IYieldBreakStatement YieldBreak(this IBlockStatementParent blockParent)
        {
            var yieldResult = new YieldBreakStatement(blockParent);
            blockParent.Add(yieldResult);
            return yieldResult;
        }

        public static IBlockStatementParent FollowedBy(this IStatement statement)
        {
            if (!(statement.Parent is IBlockStatementParent))
                throw new ArgumentException("statement");
            return (IBlockStatementParent)statement.Parent;
        }
    }
}
