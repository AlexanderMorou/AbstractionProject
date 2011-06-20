using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    internal class LinqThenByBodyBuilder :
        LinqOrderByBodyBuilder
    {
        public LinqThenByBodyBuilder(ILinqBodyBuilderParent root, IExpression orderKey)
            : base(root, orderKey)
        {
        }
    }
}
