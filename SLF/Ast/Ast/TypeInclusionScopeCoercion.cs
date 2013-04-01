using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class TypeInclusionScopeCoercion :
        ITypeInclusionScopeCoercion
    {
        #region ITypeInclusionScopeCoercion Members

        /// <summary>
        /// Returns the <see cref="IType"/> associated to the
        /// type inclusion to the active scope.
        /// </summary>
        public IType IncludedType { get; set; }

        #endregion
        #region ISourceElement Members

        /// <summary>
        /// Returns/sets the filename associated to the <see cref="TypeInclusionScopeCoercion"/>.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes
        /// the start point of the <see cref="TypeInclusionScopeCoercion"/>.
        /// </summary>
        public LineColumnPair? Start { get; set; }

        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes the
        /// end point of the <see cref="TypeInclusionScopeCoercion"/>.
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
