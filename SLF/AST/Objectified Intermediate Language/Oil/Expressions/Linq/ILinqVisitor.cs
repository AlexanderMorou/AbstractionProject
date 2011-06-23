using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    public interface ILinqVisitor
    {
        /// <summary>
        /// Visits the <see cref="ILinqSelectBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqSelectBody"/>
        /// to visit.</param>
        void Visit(ILinqSelectBody expression);
        /// <summary>
        /// Visits the <see cref="ILinqGroupBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqGroupBody"/>
        /// to visit.</param>
        void Visit(ILinqGroupBody expression);
        /// <summary>
        /// Visits the <see cref="ILinqFusionSelectBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqFusionSelectBody"/>
        /// to visit.</param>
        void Visit(ILinqFusionSelectBody expression);
        /// <summary>
        /// Visits the <see cref="ILinqFusionGroupBody"/> 
        /// <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">The <see cref="ILinqFusionGroupBody"/>
        /// to visit.</param>
        void Visit(ILinqFusionGroupBody expression);
        /// <summary>
        /// Visits the <see cref="ILinqFromClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqFromClause"/>
        /// to visit.</param>
        void Visit(ILinqFromClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqJoinClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqJoinClause"/>
        /// to visit.</param>
        void Visit(ILinqJoinClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqLetClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqLetClause"/>
        /// to visit.</param>
        void Visit(ILinqLetClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqOrderByClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqOrderByClause"/>
        /// to visit.</param>
        void Visit(ILinqOrderByClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqTypedFromClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqTypedFromClause"/>
        /// to visit.</param>
        void Visit(ILinqTypedFromClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqTypedJoinClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqTypedJoinClause"/>
        /// to visit.</param>
        void Visit(ILinqTypedJoinClause linqClause);
        /// <summary>
        /// Visits the <see cref="ILinqWhereClause"/>.
        /// </summary>
        /// <param name="linqClause">The <see cref="ILinqWhereClause"/>
        /// to visit.</param>
        void Visit(ILinqWhereClause linqClause);
    }
}
