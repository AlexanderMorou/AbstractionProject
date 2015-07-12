using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
/*---------------------------------------------------------------------\
| Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a collection of
    /// language integrated query ordering pairs for a directed order 
    /// by clause which has multiple ordering keys and directions.
    /// </summary>
    public interface ILinqOrderingPairCollection :
        IControlledCollection<LinqOrderingPair>
    {
        /// <summary>
        /// Adds an ordering pair to the 
        /// <see cref="ILinqOrderingPairCollection"/>
        /// with the default <see cref="LinqOrderingPair.Direction"/>
        /// of <see cref="LinqOrderByDirection.Unspecified"/> and the
        /// <paramref name="orderingKey"/> provided.
        /// </summary>
        /// <param name="orderingKey">The <see cref="IExpression"/>
        /// used to coerce the ordering of the output data set.</param>
        /// <exception cref="System.ArgumentNullException">thrown
        /// when <paramref name="orderingKey"/> is null.</exception>
        void Add(IExpression orderingKey);
        /// <summary>
        /// Adds an ordering pair to the 
        /// <see cref="ILinqOrderingPairCollection"/>
        /// with the <paramref name="orderingKey"/> and
        /// <paramref name="direction"/> provided.
        /// </summary>
        /// <param name="orderingKey">The <see cref="IExpression"/>
        /// used to coerce the ordering of the output data set.</param>
        /// <param name="direction">
        /// The <see cref="LinqOrderByDirection"/> which denotes
        /// whether to consider largest values first
        /// (<see cref="LinqOrderByDirection.Descending"/>) or
        /// smallest values first
        /// (<see cref="LinqOrderByDirection.Ascending"/> (default)).</param>
        /// <exception cref="System.ArgumentNullException">thrown
        /// when <paramref name="orderingKey"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown
        /// when <paramref name="direction"/> is neither
        /// <see cref="LinqOrderByDirection.Ascending"/> or
        /// <see cref="LinqOrderByDirection.Descending"/>.</exception>
        void Add(IExpression orderingKey, LinqOrderByDirection direction);
    }
}
