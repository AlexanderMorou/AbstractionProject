﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class MalleableStatementExpressionCollection :
        MalleableExpressionCollection<IStatementExpression>,
        IMalleableStatementExpressionCollection
    {
        public MalleableStatementExpressionCollection()
            : base()
        {
        }

        /// <summary>
        /// Creates a new <see cref="MalleableStatementExpressionCollection"/> with the <paramref name="expressions"/>
        /// provided.
        /// </summary>
        /// <param name="expressions">An array of <see cref="IStatementExpression"/> instances
        /// which are to be inserted into the <see cref="MalleableExpressionCollection"/>.</param>
        public MalleableStatementExpressionCollection(params IStatementExpression[] expressions)
            : base(expressions)
        {
        }

        public MalleableStatementExpressionCollection(IEnumerable<IStatementExpression> expressions)
            : base(expressions)
        {
        }
    }
}
