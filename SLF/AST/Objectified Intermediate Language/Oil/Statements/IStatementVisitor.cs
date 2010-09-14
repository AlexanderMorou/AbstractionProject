using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public interface IStatementVisitor
    {
        void Visit(IBlockStatement statement);
        void Visit(IBreakStatement statement);
        void Visit(ICallMethodStatement statement);
        void Visit(IConditionBlockStatement statement);
        void Visit(ICallFusionStatement statement);
        void Visit(IConditionContinuationStatement statement);
        void Visit(IEnumerationBlockStatement statement);
        void Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement);
        void Visit(IExpressionStatement statement);
        void Visit(IGoToStatement statement);
        void Visit(IJumpTarget statement);
        void Visit(IIterationBlockStatement statement);
        void Visit(IJumpStatement statement);
        void Visit(ILabelStatement statement);
        void Visit(IReturnStatement statement);
        void Visit(ISimpleIterationBlockStatement statement);
        void Visit(ISwitchCaseBlockStatement statement);
        void Visit(ISwitchStatement statement);
        void Visit(ITryCatchStatement statement);
        void Visit(ITryStatement statement);
        void Visit(ILocalDeclarationStatement statement);
    }
}
