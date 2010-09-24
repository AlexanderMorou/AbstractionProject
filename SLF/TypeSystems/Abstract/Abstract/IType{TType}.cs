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


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines generic properties and methods for working with a the definition of a type.
    /// </summary>
    /// <typeparam name="TType">The current type of <see cref="IType{TType}"/>.</typeparam>
    public interface IType<TType> :
        IType
        where TType :
            IType<TType>
    {
        /// <summary>
        /// Returns the element type of special classification types.
        /// </summary>
        new TType ElementType { get; }
    }
}
