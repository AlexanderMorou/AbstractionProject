using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

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

        public ExpressionKind Type
        {
            get { return ExpressionKind.CreateArrayNestedDetail; }
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

        public override string ToString()
        {
            return string.Format("{{ {0} }}", string.Join<IExpression>(", ", this.Details));
        }
    }
}
