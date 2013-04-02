using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Numerics;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class MalleableCreateArrayDetailExpression :
        MalleableCreateArrayExpression,
        IMalleableCreateArrayDetailExpression
    {
        public MalleableCreateArrayDetailExpression(IType arrayType, params IExpression[] details)
            : this(arrayType, 1, details)
        {
        }

        public MalleableCreateArrayDetailExpression(IType arrayType, int rank, params IExpression[] details)
            : base(arrayType, rank)
        {
            this.Details = new MalleableExpressionCollection(details);
        }

        #region ICreateArrayDetailExpression Members

        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/>
        /// used to instantiate the array.
        /// </summary>
        public IMalleableExpressionCollection Details { get; private set; }
        #endregion

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            //Exclude the sizes if the count is wrong.
            if (this.Rank != this.Sizes.Count)
                return string.Format("new {0}[{1}] {{ {2} }}", this.ArrayType, ','.Repeat(this.Rank - 1), string.Join<IExpression>(", \r\n", this.Details));
            else
                return string.Format("new {0}[{1}] {{ {2} }}", this.ArrayType, string.Join<IExpression>(",", this.Sizes), string.Join<IExpression>(", \r\n", this.Details));
        }

        #region ICreateArrayNestedDetailExpression Members

        IExpressionCollection ICreateArrayNestedDetailExpression.Details
        {
            get { return this.Details; }
        }

        #endregion
    }
}
