using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a language
    /// integrated query join clause with an explicit type assigned to the
    /// range variable.
    /// </summary>
    public interface ILinqTypedJoinClause :
        ILinqJoinClause
    {
        /// <summary>
        /// Returns the <see cref="ILinqTypedRangeVariable"/> associated
        /// to the <see cref="ILinqTypedJoinClause"/>.
        /// </summary>
        new ILinqTypedRangeVariable RangeVariable { get; }
    }
}