﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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