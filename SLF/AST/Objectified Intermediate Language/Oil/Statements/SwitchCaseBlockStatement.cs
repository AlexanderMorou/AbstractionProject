using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class SwitchCaseBlockStatement :
        BlockStatementBase,
        ISwitchCaseBlockStatement
    {
        private bool isDefault;

        public SwitchCaseBlockStatement(ISwitchStatement parent, params IExpression[] cases)
            : base(parent.Parent)
        {
            this.Parent = parent;
            this.Cases = new MalleableExpressionCollection(cases);
        }

        #region ISwitchCaseBlockStatement Members

        public IMalleableExpressionCollection Cases { get; private set; }

        public new ISwitchStatement Parent { get; private set; }

        public bool IsDefault
        {
            get
            {
                return this.isDefault;
            }
            set
            {
                if (value && this.Parent != null)
                {
                    var parentDefault = this.Parent.DefaultBlock;
                    if (parentDefault != null)
                        parentDefault.IsDefault = false;
                }
                this.isDefault = value;
            }
        }

        #endregion

        #region IBreakableBlockStatement Members

        public IBreakStatement Break()
        {
            return new BreakStatement(this, this.AssociatedJumpLabel);
        }

        public new IBreakableConditionBlockStatement If(IExpression condition)
        {
            return new BreakableConditionBlockStatement(this) { Condition = condition };
        }

        #endregion

        internal override IConditionBlockStatement OnIf(IExpression condition)
        {
            return this.If(condition);
        }

        #region IBreakableStatement Members

        public IBreakExit AssociatedJumpLabel
        {
            get { return this.Parent.BreakExit; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("cases {0}:", string.Join(", ", this.Cases));
        }
    }
}
