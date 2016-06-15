using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a primitve expression that contains a specific type of
    /// <see cref="IPrimitiveExpression{T}.Value"/>.
    /// </summary>
    /// <typeparam name="T">The type of value contained by the primitive expression.</typeparam>
#if DEBUG

    [VisitorTargetAttribute("PrimitiveVisitor", typeof(bool), typeof(char), typeof(string), typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double), typeof(decimal), typeof(IType),
                                                ContextualVisitor  = true,
                                                ParameterlessName  = "Null")]
    [VisitorTargetAttribute("PrimitiveVisitor", typeof(bool), typeof(char), typeof(string), typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double), typeof(decimal), typeof(IType),
                                                YieldingVisitor    = true,
                                                ParameterlessName  = "Null")]
    [VisitorTargetAttribute("PrimitiveVisitor", typeof(bool), typeof(char), typeof(string), typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double), typeof(decimal), typeof(IType),
                                                ContextualVisitor  = true,
                                                YieldingVisitor    = true,
                                                ParameterlessName  = "Null")]
    [VisitorTargetAttribute("PrimitiveVisitor", typeof(bool), typeof(char), typeof(string), typeof(byte), typeof(sbyte), typeof(ushort), typeof(short), typeof(uint), typeof(int), typeof(ulong), typeof(long), typeof(float), typeof(double), typeof(decimal), typeof(IType))]
    [VisitorTargetAttribute("CommonExpressionVisitor", DerivedThroughInheriting = "PrimitiveVisitor", YieldingVisitor = true, ContextualVisitor = true)]
    [VisitorTargetAttribute("ExpressionVisitor", DerivedThroughInheriting = "PrimitiveVisitor")]
    [VisitorTargetAttribute("IntermediateCodeVisitor", DerivedThroughInheriting = "ExpressionVisitor")]
    [VisitorTargetAttribute("IntermediateTreeVisitor", DerivedThroughInheriting = "IntermediateCodeVisitor")]
#endif
    public interface IPrimitiveExpression<T> :
        IPrimitiveExpression
    {
        /// <summary>
        /// Returns/sets the value represented by the <see cref="IPrimitiveExpression{T}"/>.
        /// </summary>
        new T Value { get; set; }
    }
}
