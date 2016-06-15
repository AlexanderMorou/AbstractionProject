using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    /// <summary>
    /// A language integrated query ordering pair used to denote
    /// a single ordering in a group of orderings.
    /// </summary>
    public struct LinqOrderingPair
    {
        /// <summary>
        /// Creates a new <see cref="LinqOrderingPair"/> with the
        /// <paramref name="orderingKey"/> and <paramref name="direction"/>
        /// provided.
        /// </summary>
        /// <param name="orderingKey">The <see cref="IExpression"/>
        /// used to coerce the ordering of the output data set.</param>
        /// <param name="direction">
        /// The <see cref="LinqOrderByDirection"/> which denotes
        /// whether to consider largest values first
        /// (<see cref="LinqOrderByDirection.Descending"/>) or
        /// smallest values first
        /// (<see cref="LinqOrderByDirection.Ascending"/> (default)).
        /// </param>
        public LinqOrderingPair(IExpression orderingKey, LinqOrderByDirection direction)
            : this()
        {
            this.OrderingKey = orderingKey;
            this.Direction = direction;
        }

        /// <summary>
        /// Creates a new <see cref="LinqOrderingPair"/> 
        /// with the default <see cref="Direction"/>
        /// of <see cref="LinqOrderByDirection.Ascending"/> and the
        /// <paramref name="orderingKey"/> provided.
        /// </summary>
        /// <param name="orderingKey">The <see cref="IExpression"/>
        /// used to coerce the ordering of the output data set.</param>
        public LinqOrderingPair(IExpression orderingKey)
            : this(orderingKey, LinqOrderByDirection.Unspecified)
        {
        }
        /// <summary>
        /// Returns the <see cref="IExpression"/> which denotes the
        /// order to coerce the output data set.
        /// </summary>
        public IExpression OrderingKey { get; private set; }

        /// <summary>
        /// Returns the <see cref="LinqOrderByDirection"/> which
        /// denotes whether to consider largest values first
        /// (<see cref="LinqOrderByDirection.Descending"/>) or
        /// smallest values first
        /// (<see cref="LinqOrderByDirection.Ascending"/> (default)).
        /// </summary>
        public LinqOrderByDirection Direction { get; private set; }

        /// <summary>
        /// Returns the current <see cref="LinqOrderingPair"/> as
        /// a <see cref="String"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> value representing the
        /// <see cref="LinqOrderingPair"/>.</returns>
        public override string ToString()
        {
            string orderingKeyString = this.OrderingKey == null ? string.Empty : this.OrderingKey.ToString();
            switch (this.Direction)
            {
                case LinqOrderByDirection.Ascending:
                    return string.Format(CultureInfo.CurrentCulture, "{0} ascending", orderingKeyString);
                case LinqOrderByDirection.Descending:
                    return string.Format(CultureInfo.CurrentCulture, "{0} descending", orderingKeyString);
            }
            return orderingKeyString;
        }
    }
}
