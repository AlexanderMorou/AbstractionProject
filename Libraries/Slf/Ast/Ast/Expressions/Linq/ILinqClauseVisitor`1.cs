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
  /// Represents a basic visitor for linq clauses which yields a result of <typeparamref name="TResult"/>.
  /// </summary>
  /// <typeparam name="TResult">
  /// Denotes the type of result the members of the <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqClauseVisitor{TResult}"/>
  /// should yield.
  /// </typeparam>
  public interface ILinqClauseVisitor<TResult>
  {
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqFromClause"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqFromClause linqClause);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqJoinClause"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqJoinClause linqClause);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqLetClause"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqLetClause linqClause);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqOrderByClause"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqOrderByClause linqClause);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqTypedFromClause"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqTypedFromClause linqClause);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqTypedJoinClause"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqTypedJoinClause linqClause);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="linqClause"/>
    /// provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqWhereClause"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqWhereClause linqClause);
  };
};