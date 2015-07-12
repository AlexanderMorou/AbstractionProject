using AllenCopeland.Abstraction.Slf.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    public class LinqExpressionRewriter :
        ILinqVisitor<bool, ICompilationContext>
    {
        public bool Visit(ILinqSelectBody expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public bool Visit(ILinqGroupBody expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public bool Visit(ILinqFusionSelectBody expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public bool Visit(ILinqFusionGroupBody expression, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public bool Visit(ILinqFromClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public bool Visit(ILinqJoinClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public bool Visit(ILinqLetClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public bool Visit(ILinqOrderByClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public bool Visit(ILinqTypedFromClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public bool Visit(ILinqTypedJoinClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }

        public bool Visit(ILinqWhereClause linqClause, ICompilationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
