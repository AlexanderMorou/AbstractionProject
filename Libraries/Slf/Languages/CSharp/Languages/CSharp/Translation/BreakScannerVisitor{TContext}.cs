using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation
{
    internal class BreakScannerVisitor<TContext> :
        IStatementVisitor<bool, TContext>
    {
        private bool isHardVisitor = false;
        private static BreakScannerVisitor<TContext> hardVisitor = new BreakScannerVisitor<TContext>() { isHardVisitor = true };
        #region IStatementVisitor<bool,TContext> Members

        public bool Visit(IBlockStatement statement, TContext context)
        {
            return IsBrokeFull(statement, context);
        }

        public bool IsBrokeFull(IControlledCollection<IStatement> series, TContext context)
        {
            foreach (var blockStatement in series)
                if (blockStatement.Accept(this, context))
                    return true;
            return false;
        }


        public bool Visit(IBreakStatement statement, TContext context)
        {
            return !isHardVisitor;
        }

        public bool Visit(ICallMethodStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit(IConditionBlockStatement statement, TContext context)
        {
            return IsBrokeFull(statement, context) && (statement.HasNext && statement.Next.Accept(this, context));
        }

        public bool Visit(ICallFusionStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit(IConditionContinuationStatement statement, TContext context)
        {
            return IsBrokeFull(statement, context);
        }

        public bool Visit(IEnumerateSetBreakableBlockStatement statement, TContext context)
        {
            return IsBrokeFull(statement, context);
        }

        public bool Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit(IExpressionStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit(IGoToStatement statement, TContext context)
        {
            return true;
        }

        public bool Visit(IJumpTarget statement, TContext context)
        {
            return false;
        }

        public bool Visit(IIterationBlockStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit(IJumpStatement statement, TContext context)
        {
            return true;
        }

        public bool Visit(ILabelStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit(IReturnStatement statement, TContext context)
        {
            return true;
        }

        public bool Visit(ISimpleIterationBlockStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit(ISwitchCaseBlockStatement statement, TContext context)
        {
            return IsBrokeFull(statement, context);
        }

        public bool Visit(ISwitchStatement statement, TContext context)
        {
            return statement.All(c => c.Accept(hardVisitor, context)) && statement.DefaultBlock != null;
        }

        public bool Visit(ITryStatement statement, TContext context)
        {
            return IsBrokeFull(statement, context) && statement.Values.All(clause => clause.Accept(this, context)) && (statement.HasCatchAll && statement.CatchAll.Accept(this, context) || !statement.HasCatchAll) && (statement.HasFinally && statement.Finally.Accept(this, context) || !statement.HasFinally);
        }

        public bool Visit(ILocalDeclarationsStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit(IChangeEventHandlerStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement, TContext context)
            where TEvent : Abstract.Members.IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParameter : Abstract.Members.IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TEventParent : Abstract.IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter : Abstract.Members.IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature : Abstract.Members.IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent : Abstract.ISignatureParent<Abstract.Members.IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        {
            return false;
        }

        public bool Visit(ICommentStatement statement, TContext context)
        {
            return false;
        }


        public bool Visit(IUsingBlockStatement statement, TContext context)
        {
            return IsBrokeFull(statement, context);
        }

        public bool Visit(IUsingExpressionBlockStatement statement, TContext context)
        {
            return IsBrokeFull(statement, context);
        }

        public bool Visit(IThrowStatement statement, TContext context)
        {
            return true;
        }

        public bool Visit(ILockStatement statement, TContext context)
        {
            return IsBrokeFull(statement, context);
        }


        public bool Visit(IYieldReturnStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit(IYieldBreakStatement statement, TContext context)
        {
            return false;
        }

        public bool Visit(IWhileStatement whileStatement, TContext context)
        {
            return IsBrokeFull(whileStatement, context);
        }


        public bool Visit(IGoToCaseStatement statement, TContext context)
        {
            return true;
        }

        public bool Visit(IExplicitStringLiteralStatement statement, TContext context)
        {
            return false;
        }

        #endregion

        public bool Visit(IIterationDeclarationBlockStatement statement, TContext context)
        {
            return false;
        }
    }
}
