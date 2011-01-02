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
        void Visit(ILinqSelectBody expression);
        void Visit(ILinqGroupBody expression);
        void Visit(ILinqFusionSelectBody expression);
        void Visit(ILinqFusionGroupBody expression);
        void Visit(ILinqDirectedOrderByClause linqClause);
        void Visit(ILinqDirectedOrderByGroupClause linqClause);
        void Visit(ILinqFromClause linqClause);
        void Visit(ILinqJoinClause linqClause);
        void Visit(ILinqLetClause linqClause);
        void Visit(ILinqOrderByClause linqClause);
        void Visit(ILinqOrderByGroupClause linqClause);
        void Visit(ILinqTypedFromClause linqClause);
        void Visit(ILinqTypedJoinClause linqClause);
        void Visit(ILinqWhereClause linqClause);
    }
}
