using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    internal class LinqDirectedThenByBodyBuilder :
        LinqDirectedOrderByBodyBuilder
    {

        public LinqDirectedThenByBodyBuilder(ILinqBodyBuilderParent root, IExpression orderKey, LinqOrderByDirection direction) 
            : base(root, orderKey, direction)
        {

        }
    }
}
