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
    public class LabelStatement :
        StatementBase,
        ILabelStatement
    {
        private bool _isReferenced;
        public LabelStatement(IStatementParent parent, string name)
            : base(parent)
        {
            this.Name = name;
        }
        public override void Accept(IStatementVisitor visitor)
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
            if (!this._isReferenced)
                this._isReferenced = true;
            return new GoToStatement(gotoContainer, this);
        }

        #endregion

        public bool IsReferenced
        {
            get
            {
                return this._isReferenced;
            }
        }

        public override TResult Accept<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            return string.Format("{0}:", this.Name);
        }
    }
}
