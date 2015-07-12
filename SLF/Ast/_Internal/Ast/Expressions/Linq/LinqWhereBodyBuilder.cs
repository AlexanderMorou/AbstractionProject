using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    /// <summary>
    /// Provides a class which represents a where clause builder
    /// which restricts the output data set by a given boolean
    /// condition.
    /// </summary>
    internal class LinqWhereBodyBuilder :
        LinqBodyBuilderBase
    {
        /// <summary>
        /// Creates a new <see cref="LinqWhereBodyBuilder"/> with the
        /// <paramref name="root"/> and 
        /// <paramref name="booleanCondition"/> provided.
        /// </summary>
        /// <param name="root">The <see cref="ILinqBodyBuilderParent"/>
        /// which contains the <see cref="LinqWhereBodyBuilder"/>.</param>
        /// <param name="booleanCondition">The <see cref="IExpression"/> 
        /// which restricts the output data set.</param>
        public LinqWhereBodyBuilder(ILinqBodyBuilderParent root, IExpression booleanCondition)
            : base(root)
        {
            this.BooleanCondition = booleanCondition;
        }
        /// <summary>
        /// Returns the <see cref="IExpression"/> which represents
        /// the condition that restricts the output data set.
        /// </summary>
        public IExpression BooleanCondition { get; private set; }
    }
}
