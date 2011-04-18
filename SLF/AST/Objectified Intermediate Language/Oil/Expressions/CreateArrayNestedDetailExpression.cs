using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class CreateArrayNestedDetailExpression :
        ICreateArrayNestedDetailExpression
    {
        public CreateArrayNestedDetailExpression()
        {
            this.Details = new MalleableExpressionCollection();
        }
        #region ICreateArrayNestedDetailExpression Members

        public IMalleableExpressionCollection Details { get; private set; }

        #endregion

        #region IExpression Members

        public ExpressionKinds Type
        {
            get { return ExpressionKinds.CreateArrayNestedDetail; }
        }

        public void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion

        #region ISourceElement Members

        public string FileName { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        #endregion
    }
}
