using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
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
        IType ArrayType { get; }
        /// <summary>
        /// Returns the <see cref="Rank"/> of the array
        /// to create.
        /// </summary>
        int Rank { get; }
        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> used 
        /// to denote the size of the <see cref="ICreateArrayExpression"/>
        /// </summary>
        IExpressionCollection Sizes { get; }
    }
}
