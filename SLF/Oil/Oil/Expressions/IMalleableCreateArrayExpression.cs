using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with
    /// a malleable array creation expression.
    /// </summary>
    public interface IMalleableCreateArrayExpression :
        ICreateArrayExpression
    {
        /// <summary>
        /// Returns the type of array to create.
        /// </summary>
        new IType ArrayType { get; set; }
        /// <summary>
        /// Returns the <see cref="Rank"/> of the array
        /// to create.
        /// </summary>
        new int Rank { get; set; }
        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> used 
        /// to denote the size of the <see cref="ICreateArrayExpression"/>
        /// </summary>
        new IMalleableExpressionCollection Sizes { get; }
    }
}
