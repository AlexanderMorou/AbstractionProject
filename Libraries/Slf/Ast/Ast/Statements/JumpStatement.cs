using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
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

        public override void Accept(IStatementVisitor visitor)
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

        public override TResult Accept<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

    }
}
