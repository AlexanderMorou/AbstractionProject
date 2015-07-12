using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
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

        public override void Visit(IStatementVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        public override TResult Visit<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            if (this.Target == null)
                return string.Empty;
            return string.Format("goto {0};", this.Target.Name);
        }
    }
}
