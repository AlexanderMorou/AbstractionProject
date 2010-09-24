using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// The kind of typing used on a series of locals in
    /// a <see cref="ILocalDeclarationStatement"/>.
    /// </summary>
    public enum LocalTypingKind
    {
        /// <summary>
        /// The type of the locals declared is dynamic,
        /// the type can change throughout the runtime of the
        /// program, all calls are late-bound.
        /// </summary>
        Dynamic,
        /// <summary>
        /// The type of the locals declared is impliict,
        /// the <see cref="IType"/> associated is inferred
        /// by the initialization expression.
        /// </summary>
        Implicit,
        /// <summary>
        /// The type of the locals declared is explicit, 
        /// an <see cref="IType"/> is provided.
        /// </summary>
        Explicit,
    }

    /// <summary>
    /// Defines properties and methods for working with a local 
    /// member.
    /// </summary>
    public interface ILocalMember :
        IIntermediateMember<IBlockStatementParent, IBlockStatementParent>
    {
        /// <summary>
        /// Returns the means to which the local's type is discovered.
        /// </summary>
        LocalTypingKind TypingMethod { get; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> which
        /// initializes the <see cref="ILocalMember"/>.
        /// </summary>
        IExpression InitializationExpression { get; set; }
        /// <summary>
        /// Returns a <see cref="ILocalReferenceExpression"/> which refers
        /// directly to the <see cref="ILocalMember"/>.
        /// </summary>
        /// <returns></returns>
        ILocalReferenceExpression GetReference();
        /// <summary>
        /// Returns/sets whether the <see cref="ILocalMember"/> should be automatically declared.
        /// </summary>
        bool AutoDeclare { get; set; }
        /// <summary>
        /// Returns a <see cref="ILocalDeclarationStatement"/> relative to the
        /// current <see cref="ILocalMember"/>.
        /// </summary>
        /// <remarks>The instance returned is a single-ton
        /// object per local.  Subsequent calls to this method
        /// yield the same instance.</remarks>
        /// <returns>A <see cref="ILocalDeclarationStatement"/>
        /// relative to the current <see cref="ILocalMember"/>.</returns>
        ILocalDeclarationStatement GetDeclarationStatement();
    }
    /// <summary>
    /// Defines properties and methods for working with a local whose type is
    /// explicitly declared.
    /// </summary>
    public interface ITypedLocalMember :
        ILocalMember
    {
        /// <summary>
        /// Returns/sets the type of the <see cref="ITypedLocalMember"/>.
        /// </summary>
        IType LocalType { get; set; }
    }
}
