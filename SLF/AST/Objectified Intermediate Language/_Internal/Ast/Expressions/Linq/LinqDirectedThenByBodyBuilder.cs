﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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