using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a language
    /// integrated query where clause which further refines a 
    /// data series by providing conditional logic to element
    /// selection.
    /// </summary>
    public interface ILinqWhereClause :
        ILinqClause
    {
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/>
        /// which helps filter the series.
        /// </summary>
        IExpression Condition { get; set; }
    }
}
