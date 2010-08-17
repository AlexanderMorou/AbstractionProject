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
    /// The ordering direction used for a 
    /// <see cref="ILinqDirectedOrderByClause"/>
    /// to determine which direction the results are 
    /// ordered.
    /// </summary>
    public enum LinqOrderByDirection
    {
        /// <summary>
        /// The <see cref="ILinqDirectedOrderByClause"/> is ordered
        /// in ascending (normal) order.
        /// </summary>
        Ascending,
        /// <summary>
        /// The <see cref="ILinqDirectedOrderByClause"/> is ordered
        /// in descending (reverse) order.
        /// </summary>
        Descending,
    }
    /// <summary>
    /// Defines properties and methods for working with
    /// an order by clause in a language integrated
    /// query which is directed in either ascending 
    /// or descending order.
    /// </summary>
    public interface ILinqDirectedOrderByClause :
        ILinqOrderByClause
    {
        /// <summary>
        /// Returns/sets the <see cref="LinqOrderByDirection"/>
        /// for the <see cref="ILinqDirectedOrderByClause"/>.
        /// </summary>
        LinqOrderByDirection Direction { get; set; }
    }
}
