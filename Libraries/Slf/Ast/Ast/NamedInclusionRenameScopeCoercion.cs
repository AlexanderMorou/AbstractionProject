﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class NamedInclusionRenameScopeCoercion :
        NamedInclusionScopeCoercion,
        INamedInclusionRenameScopeCoercion
    {
        #region IRenameScopeCoercion Members

        /// <summary>
        /// Returns/sets the new name of the entity included.
        /// </summary>
        public string NewName { get; set; }

        #endregion

        public override TResult Accept<TResult, TContext>(IScopeCoercionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override void Accept(IScopeCoercionVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}
