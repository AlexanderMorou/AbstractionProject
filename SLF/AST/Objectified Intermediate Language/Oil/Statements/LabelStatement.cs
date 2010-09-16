using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class LabelStatement :
        StatementBase,
        ILabelStatement
    {
        public LabelStatement(IStatementParent parent, string name)
            : base(parent)
        {
            this.Name = name;
        }
        public override void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        #region ILabelStatement Members

        public string Name { get; set; }

        #endregion

        #region ILabelStatement Members

        public IGoToStatement GetGoTo(IStatementParent gotoContainer)
        {
            return new GoToStatement(gotoContainer, this);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}:", this.Name);
        }
    }
}
