using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a primitve expression that contains a specific type of
    /// <see cref="IPrimitiveExpression{T}.Value"/>.
    /// </summary>
    /// <typeparam name="T">The type of value contained by the primitive expression.</typeparam>
    public interface IPrimitiveExpression<T> :
        IPrimitiveExpression
    {
        /// <summary>
        /// Returns/sets the value represented by the <see cref="IPrimitiveExpression{T}"/>.
        /// </summary>
        new T Value { get; set; }
    }
}
