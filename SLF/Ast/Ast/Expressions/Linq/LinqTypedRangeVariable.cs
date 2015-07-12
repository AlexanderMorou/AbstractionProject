using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq
{
    public class LinqTypedRangeVariable :
        LinqRangeVariable,
        ILinqTypedRangeVariable 
    {
        /// <summary>
        /// Creates a new <see cref="LinqTypedRangeVariable"/> with the
        /// <paramref name="parent"/>, <paramref name="name"/> and <paramref name="rangeType"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <see cref="ILinqClause"/>
        /// which contains the <see cref="LinqTypedRangeVariable"/>.</param>
        /// <param name="name">The <see cref="String"/> value which denotes the unique
        /// identifier associated to the <see cref="LinqTypedRangeVariable"/>.</param>
        /// <param name="rangeType">The <see cref="IType"/>
        /// which denotes the range type associated to the <see cref="LinqTypedRangeVariable"/>.</param>
        public LinqTypedRangeVariable(ILinqClause parent, string name, IType rangeType)
            : base(parent, name)
        {

        }

        #region ILinqTypedRangeVariable Members
        /// <summary>
        /// Returns/sets the <see cref="IType"/> which identifies 
        /// the kind of <see cref="LinqTypedRangeVariable"/>.
        /// </summary>
        public IType RangeType { get; set; }

        #endregion
    }
}
