using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with an array creation expression.
    /// </summary>
    public interface ICreateArrayExpression :
        IMemberParentReferenceExpression
    {
        /// <summary>
        /// Returns the type of array to create.
        /// </summary>
        IType ArrayType { get; set; }
        /// <summary>
        /// Returns the <see cref="Rank"/> of the array
        /// to create.
        /// </summary>
        int Rank { get; set; }
        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> used 
        /// to denote the size of the <see cref="ICreateArrayExpression"/>
        /// </summary>
        IMalleableExpressionCollection Sizes { get; }
    }
}
