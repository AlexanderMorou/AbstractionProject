 /* ----------------------------------------------------------------\
 |  This code was generated by Allen Copeland's Abstraction.        |
 |  Version: 0.5.0.0                                                |
 |------------------------------------------------------------------|
 |  To ensure the code works properly,                              |
 |  please do not make any changes to the file.                     |
 |------------------------------------------------------------------|
 |  The specific language is C♯ (Runtime Version: 4.0.30319.42000)  |
 |  Sub-tool Name: C♯ Code Translator                               |
 |  Sub-tool Version: 1.0.0.0                                       |
 \---------------------------------------------------------------- */
using System;
namespace AllenCopeland.Abstraction.Slf.Ast
{
  /// <summary>
  /// Represents a basic visitor for inclusions which has a <typeparamref name="TContext"/>
  /// relevant to the visit.
  /// </summary>
  /// <typeparam name="TContext">
  /// Denotes the type of context the members of the <see cref="AllenCopeland.Abstraction.Slf.Ast.IInclusionContextualVisitor{TContext}"/>
  /// should receive along with the types that accept the visitor.
  /// </typeparam>
  public interface IInclusionContextualVisitor<TContext>
  {
    /// <summary>
    /// Visits the <paramref name="inclusion"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="inclusion">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.INamedInclusionRenameScopeCoercion"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(INamedInclusionRenameScopeCoercion inclusion, TContext context);
    /// <summary>
    /// Visits the <paramref name="inclusion"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="inclusion">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.INamedInclusionScopeCoercion"/> relevant
    /// to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(INamedInclusionScopeCoercion inclusion, TContext context);
    /// <summary>
    /// Visits the <paramref name="inclusion"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="inclusion">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.INamespaceInclusionRenameScopeCoercion"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(INamespaceInclusionRenameScopeCoercion inclusion, TContext context);
    /// <summary>
    /// Visits the <paramref name="inclusion"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="inclusion">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.INamespaceInclusionScopeCoercion"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(INamespaceInclusionScopeCoercion inclusion, TContext context);
    /// <summary>
    /// Visits the <paramref name="inclusion"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="inclusion">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.IStaticInclusionScopeCoercion"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(IStaticInclusionScopeCoercion inclusion, TContext context);
    /// <summary>
    /// Visits the <paramref name="inclusion"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="inclusion">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.ITypeInclusionRenameScopeCoercion"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ITypeInclusionRenameScopeCoercion inclusion, TContext context);
    /// <summary>
    /// Visits the <paramref name="inclusion"/> provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="inclusion">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.ITypeInclusionScopeCoercion"/> relevant
    /// to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    void Visit(ITypeInclusionScopeCoercion inclusion, TContext context);
  };
};