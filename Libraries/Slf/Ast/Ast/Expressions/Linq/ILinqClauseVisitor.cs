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
namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
  /// <summary>
  /// Represents a basic visitor for linq clauses.
  /// </summary>
  public interface ILinqClauseVisitor
  {
    /// <summary>
    /// Visits the <paramref name="linqClause"/> provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqFromClause"/>
    /// relevant to the visit.
    /// </param>
    void Visit(ILinqFromClause linqClause);
    /// <summary>
    /// Visits the <paramref name="linqClause"/> provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqJoinClause"/>
    /// relevant to the visit.
    /// </param>
    void Visit(ILinqJoinClause linqClause);
    /// <summary>
    /// Visits the <paramref name="linqClause"/> provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqLetClause"/>
    /// relevant to the visit.
    /// </param>
    void Visit(ILinqLetClause linqClause);
    /// <summary>
    /// Visits the <paramref name="linqClause"/> provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqOrderByClause"/>
    /// relevant to the visit.
    /// </param>
    void Visit(ILinqOrderByClause linqClause);
    /// <summary>
    /// Visits the <paramref name="linqClause"/> provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqTypedFromClause"/>
    /// relevant to the visit.
    /// </param>
    void Visit(ILinqTypedFromClause linqClause);
    /// <summary>
    /// Visits the <paramref name="linqClause"/> provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqTypedJoinClause"/>
    /// relevant to the visit.
    /// </param>
    void Visit(ILinqTypedJoinClause linqClause);
    /// <summary>
    /// Visits the <paramref name="linqClause"/> provided.
    /// </summary>
    /// <param name="linqClause">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqWhereClause"/>
    /// relevant to the visit.
    /// </param>
    void Visit(ILinqWhereClause linqClause);
  };
};