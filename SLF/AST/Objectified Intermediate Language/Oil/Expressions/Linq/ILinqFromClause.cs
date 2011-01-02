using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a language integrated
    /// query expression from clause which includes a data source for
    /// iteration into the series.
    /// </summary>
    public interface ILinqFromClause  :
        ILinqClause
    {
        /// <summary>
        /// Returns/sets the name of the range variable
        /// defined by the <see cref="ILinqFromClause"/>.
        /// </summary>
        string RangeVariableName { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="IExpression"/> from which
        /// the langauge integrated query obtains its data from
        /// for the current clause.
        /// </summary>
        IExpression RangeSource { get; set; }
    }
}
