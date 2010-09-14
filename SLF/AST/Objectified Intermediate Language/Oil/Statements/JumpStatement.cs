using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class JumpStatement :
        StatementBase,
        IJumpStatement
    {
        private IJumpTarget target;
        public JumpStatement(IStatementParent parent, IJumpTarget target)
            : base(parent)
        {
            this.target = target;
        }

        public override void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        #region IJumpStatement Members

        public IJumpTarget Target
        {
            get
            {
                return this.target;
            }
            set
            {
                this.OnSetTarget(value);
            }
        }

        protected virtual void OnSetTarget(IJumpTarget value)
        {
            this.target = value;
        }

        #endregion
    }
}
