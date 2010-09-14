using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class GoToStatement :
        JumpStatement,
        IGoToStatement
    {
        public GoToStatement(IStatementParent parent, ILabelStatement target)
            : base(parent, target)
        {
        }

        #region IGoToStatement Members

        public new ILabelStatement Target
        {
            get
            {
                return (ILabelStatement)base.Target;
            }
            set
            {
                this.OnSetTarget(value);
            }
        }
        #endregion

        protected override void OnSetTarget(IJumpTarget value)
        {
            if (!(value is ILabelStatement))
                throw new ArgumentException("value must be a label statement", "value");
            base.OnSetTarget(value);
        }
    }
}
