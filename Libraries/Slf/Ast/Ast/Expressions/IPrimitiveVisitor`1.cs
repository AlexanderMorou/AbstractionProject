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
  /// Represents a basic visitor for primitives which yields a result of <typeparamref name="TResult"/>.
  /// </summary>
  /// <typeparam name="TResult">
  /// Denotes the type of result the members of the <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveVisitor{TResult}"/>
  /// should yield.
  /// </typeparam>
  public interface IPrimitiveVisitor<TResult>
  {
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="AllenCopeland.Abstraction.Slf.Abstract.IType"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<IType> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Boolean"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<bool> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Byte"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<byte> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Char"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<char> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Decimal"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<decimal> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Double"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<double> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Int16"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<short> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Int32"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<int> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Int64"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<long> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.SByte"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<sbyte> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Single"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<float> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.String"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<string> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.UInt16"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<ushort> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.UInt32"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<uint> primitive);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="primitive"/>
    /// provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.UInt64"/> relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IPrimitiveExpression<ulong> primitive);
  };
};
