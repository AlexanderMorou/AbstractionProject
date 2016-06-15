using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    public enum PathSetKind
    {
        Expression,
        Member,
        Declaration,
        Statement,
    }
    public interface IExpressionPath :
        IIntermediatePath<IExpression>
    {
    }
    public interface IMemberPath :
        IIntermediatePath<IIntermediateMember>
    {
    }
    public interface IDeclarationPath :
        IIntermediatePath<IIntermediateDeclaration>
    {
    }
    public interface IStatementPath :
        IIntermediatePath<IStatement>
    {
    }
    public interface IIntermediatePath<T> :
        ILockedLinkedListNode<T>,
        IIntermediatePath
    {
    }
    public interface IIntermediatePath
    {
    }
    public interface ITransformationPathStack :
        ILockedLinkedListNode<IIntermediatePath>
    {
    }
}
