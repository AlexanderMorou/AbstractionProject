using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
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
