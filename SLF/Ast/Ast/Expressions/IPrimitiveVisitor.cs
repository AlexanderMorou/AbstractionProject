﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a visitor that
    /// observes primitive expressions.
    /// </summary>
    public interface IPrimitiveVisitor
    {
        /// <summary>
        /// Visits a boolean primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<bool> expression);
        /// <summary>
        /// Visits a character primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<char> expression);
        /// <summary>
        /// Visits a string primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<string> expression);
        /// <summary>
        /// Visits a byte primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<byte> expression);
        /// <summary>
        /// Visits a sbyte primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<sbyte> expression);
        /// <summary>
        /// Visits an unsigned 16-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<ushort> expression);
        /// <summary>
        /// Visits a 16-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<short> expression);
        /// <summary>
        /// Visits an unsigned 32-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<uint> expression);
        /// <summary>
        /// Visits a 32-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<int> expression);
        /// <summary>
        /// Visits an unsigned 64-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<ulong> expression);
        /// <summary>
        /// Visits a 64-bit primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<long> expression);
        /// <summary>
        /// Visits a single precision floating point primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<float> expression);
        /// <summary>
        /// Visits a double precision floating point primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<double> expression);
        /// <summary>
        /// Visits a decimal primitive expression.
        /// </summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> to visit.</param>
        void Visit(IPrimitiveExpression<decimal> expression);
        /// <summary>
        /// Visits a null primitive expression.
        /// </summary>
        void VisitNull();
    }
}