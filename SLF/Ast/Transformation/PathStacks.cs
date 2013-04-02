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
    /// <summary>
    /// Defines properties and methods for working with a locked linked
    /// list node.
    /// </summary>
    /// <typeparam name="T">The type of element represented by the
    /// locked linked list node.</typeparam>
    public interface ILockedLinkedListNode<T> :
        ICountableEnumerable<T>
    {
        /// <summary>
        /// Returns the first element within the locked linked list.
        /// </summary>
        ILockedLinkedListNode<T> First { get; }
        /// <summary>
        /// Returns the last element within the locked linked list.
        /// </summary>
        ILockedLinkedListNode<T> Last { get; }
        /// <summary>
        /// Returns the previous element within the locked linked list.
        /// </summary>
        ILockedLinkedListNode<T> Previous { get; }
        /// <summary>
        /// Returns the next element within the locked linked list.
        /// </summary>
        ILockedLinkedListNode<T> Next { get; }
        /// <summary>
        /// Returns the <typeparamref name="T"/> instance which
        /// is represented by the <see cref="ILockedLinkedListNode{T}"/>.
        /// </summary>
        T Element { get; }
        /// <summary>
        /// Returns the <see cref="Int32"/> index within the set 
        /// the <see cref="ILockedLinkedListNode{T}"/> is at.
        /// </summary>
        int Index { get; }
    }
}
