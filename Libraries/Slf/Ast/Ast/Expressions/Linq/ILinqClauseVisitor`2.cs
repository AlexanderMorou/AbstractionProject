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
namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
  /// <summary>
  /// Represents a basic visitor for linq clauses which yields a result of <typeparamref name="TResult"/>
  /// and also has a <typeparamref name="TContext"/> relevant to the visit.
  /// </summary>
  /// <typeparam name="TResult">
  /// Denotes the type of result the members of the <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqClauseVisitor{TResult,TContext}"/>
  /// should yield.
  /// </typeparam>
  /// <typeparam name="TContext">
  /// Denotes the type of context the members of the <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqClauseVisitor{TResult,TContext}"/>
  /// should receive along with the types that accept the visitor.
  /// </typeparam>
  public interface ILinqClauseVisitor<TResult, TContext>
  {
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqFromClause"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqFromClause linqClause, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqJoinClause"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqJoinClause linqClause, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqLetClause"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqLetClause linqClause, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqOrderByClause"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqOrderByClause linqClause, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqTypedFromClause"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqTypedFromClause linqClause, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqTypedJoinClause"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqTypedJoinClause linqClause, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqWhereClause"/>
    /// relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqWhereClause linqClause, TContext context);
  };
};
