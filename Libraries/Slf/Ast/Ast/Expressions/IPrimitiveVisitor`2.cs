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
using AllenCopeland.Abstraction.Slf.Abstract;
using System;
namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
  /// <summary>
  /// Represents a basic visitor for primitives which yields a result of <typeparamref name="TResult"/>
  /// and also has a <typeparamref name="TContext"/> relevant to the visit.
  /// </summary>
  /// <typeparam name="TResult">
  /// Denotes the type of result the members of the <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveVisitor{TResult,TContext}"/>
  /// should yield.
  /// </typeparam>
  /// <typeparam name="TContext">
  /// Denotes the type of context the members of the <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveVisitor{TResult,TContext}"/>
  /// should receive along with the types that accept the visitor.
  /// </typeparam>
  public interface IPrimitiveVisitor<TResult, TContext>
  {
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="AllenCopeland.Abstraction.Slf.Abstract.IType"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<IType> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Boolean"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<bool> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Byte"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<byte> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Char"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<char> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Decimal"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<decimal> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Double"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<double> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Int16"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<short> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Int32"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<int> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Int64"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<long> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.SByte"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<sbyte> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Single"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<float> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.String"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<string> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.UInt16"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<ushort> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.UInt32"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<uint> primitive, TContext context);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided with the given <paramref name="context"/>.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.UInt64"/> relevant to the visit.
    /// </param>
    /// <param name="context">
    /// The <typeparamref name="TContext"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<ulong> primitive, TContext context);
    TResult VisitNull(TContext context);
  };
};
