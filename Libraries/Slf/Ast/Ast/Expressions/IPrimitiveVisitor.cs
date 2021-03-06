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
  /// Represents a basic visitor for primitives.
  /// </summary>
  public interface IPrimitiveVisitor
  {
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="AllenCopeland.Abstraction.Slf.Abstract.IType"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<IType> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Boolean"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<bool> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Byte"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<byte> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Char"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<char> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Decimal"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<decimal> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Double"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<double> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Int16"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<short> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Int32"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<int> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Int64"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<long> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.SByte"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<sbyte> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.Single"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<float> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.String"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<string> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.UInt16"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<ushort> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.UInt32"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<uint> primitive);
    /// <summary>
    /// Visits the <paramref name="primitive"/> provided.
    /// </summary>
    /// <param name="primitive">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.IPrimitiveExpression{T}"/>
    /// of <see cref="System.UInt64"/> relevant to the visit.
    /// </param>
    void Visit(IPrimitiveExpression<ulong> primitive);
    void VisitNull();
  };
};
