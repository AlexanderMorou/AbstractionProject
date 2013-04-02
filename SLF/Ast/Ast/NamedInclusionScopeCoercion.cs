using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class NamedInclusionScopeCoercion :
        INamedInclusionScopeCoercion
    {
        #region INamedInclusionScopeCoercion Members

        /// <summary>
        /// Returns/sets the name included in the scope
        /// which coerces symbol table resolution.
        /// </summary>
        public string IncludedName { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("include {0};", IncludedName);
        }

        #region ISourceElement Members

        /// <summary>
        /// Returns/sets the filename associated to the <see cref="NamedInclusionScopeCoercion"/>.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes
        /// the start point of the <see cref="NamedInclusionScopeCoercion"/>.
        /// </summary>
        public LineColumnPair? Start { get; set; }

        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes the
        /// end point of the <see cref="NamedInclusionScopeCoercion"/>.
        /// </summary>
        public LineColumnPair? End { get; set; }

        #endregion

        #region IScopeCoercion Members

        public virtual void Visit(IIntermediateInclusionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public virtual TResult Visit<TResult, TContext>(IIntermediateInclusionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        #endregion
    }
}
