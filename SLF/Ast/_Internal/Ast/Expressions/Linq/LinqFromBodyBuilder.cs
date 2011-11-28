using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    /// <summary>
    /// Provides a language integrated query body builder relative
    /// to a from clause which specifies a data source to iterate.
    /// </summary>
    internal class LinqFromBodyBuilder :
        LinqBodyBuilderBase
    {
        /// <summary>
        /// Creates a new <see cref="LinqFromBodyBuilder"/>
        /// with the <paramref name="rangeVariableName"/>
        /// and <paramref name="rangeSource"/> provided.
        /// </summary>
        /// <param name="rangeVariableName">The <see cref="String"/> which represents
        /// the name of the range variable associated to the
        /// from clause which specifies a data source and a
        /// range variable used to iterate the elements of
        /// the <paramref name="rangeSource"/>.</param>
        /// <param name="rangeSource">The <see cref="IExpression"/> which selects
        /// the data source from which to derive the data set 
        /// elements associated to the <paramref name="rangeVariableName"/></param>
        internal LinqFromBodyBuilder(string rangeVariableName, IExpression rangeSource)
            : base()
        {
            this.RangeSource = rangeSource;
            this.RangeVariableName = rangeVariableName;
        }

        internal LinqFromBodyBuilder(ILinqBodyBuilderParent root, string rangeVariableName, IExpression rangeSource)
            : base(root)
        {
            this.RangeSource = rangeSource;
            this.RangeVariableName = rangeVariableName;
        }

        /// <summary>
        /// Returns the <see cref="String"/> which represents
        /// the name of the range variable associated to the
        /// from clause which specifies a data source and a
        /// range variable used to iterate the elements of
        /// the source.
        /// </summary>
        public string RangeVariableName { get; private set; }
        /// <summary>
        /// Returns the <see cref="IExpression"/> which selects
        /// the data source from which to derive the data set 
        /// elements associated to the <see cref="RangeVariableName"/>.
        /// </summary>
        public IExpression RangeSource { get; private set; }
    }
}
