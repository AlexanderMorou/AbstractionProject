using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// The kind of clause the <see cref="ILinqClause"/> is.
    /// </summary>
    public enum ClauseType
    {
        /// <summary>
        /// The clause selects a data source.
        /// </summary>
        FromClause,
        /// <summary>
        /// The clause is assigns a temporary storage element for use within
        /// the query.
        /// </summary>
        LetClause,
        /// <summary>
        /// The clause filters the output by requiring a condition to be met.
        /// </summary>
        WhereClause,
        /// <summary>
        /// The clause creates a union with another data source only when a
        /// special condition is met.
        /// </summary>
        JoinClause,
        /// <summary>
        /// The clause orders the data set by a specific value from
        /// the elements of the range variables in scope.
        /// </summary>
        OrderByClause,
        /// <summary>
        /// The clause is a select or group by which continues into 
        /// another query.
        /// </summary>
        ContinuationClause,
    }

    /// <summary>
    /// Defines properties and methods for working with a language integrated
    /// query clause.
    /// </summary>
    public interface ILinqClause :
        IIntermediateMemberParent
    {
        /// <summary>
        /// Returns the kind of clause the <see cref="ILinqClause"/> is.
        /// </summary>
        ClauseType Type { get; }
        /// <summary>
        /// Visits the elements of the <see cref="ILinqClause"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/>
        /// to which the <see cref="ILinqClause"/> needs to repay the visit
        /// to.</param>
        void Visit(ILinqVisitor visitor);
    }
}
