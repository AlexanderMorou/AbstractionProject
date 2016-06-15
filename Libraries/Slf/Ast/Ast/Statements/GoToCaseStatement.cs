using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class GoToCaseStatement :
        JumpStatement,
        IGoToCaseStatement
    {
        public GoToCaseStatement(IStatementParent parent, ISwitchCaseBlockStatement target)
            : base(parent, target)
        {
        }
        public new ISwitchCaseBlockStatement Target
        {
            get
            {
                return (ISwitchCaseBlockStatement)base.Target;
            }
            set
            {
                this.OnSetTarget(value);
            }
        }

        protected override void OnSetTarget(IJumpTarget value)
        {
            if (!(value is ISwitchCaseBlockStatement))
                throw new ArgumentException("value must be a switch case block statement", "value");
            base.OnSetTarget(value);
        }

        public override void Accept(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        public override string ToString()
        {
            if (this.Target == null)
                return string.Empty;
            return string.Format("goto case {0};", this.Target.Cases.First().ToString());
        }
    }
}
