using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class MalleableCreateArrayExpression :
        MemberParentReferenceExpressionBase,
        IMalleableCreateArrayExpression
    {

        public MalleableCreateArrayExpression(IType arrayType, int rank, params IExpression[] sizes)
            : base()
        {
            if (rank <= 0)
                throw new ArgumentOutOfRangeException("rank");
            this.ArrayType = arrayType;
            this.Rank = rank;
            this.Sizes = new MalleableExpressionCollection(sizes);
        }

        public MalleableCreateArrayExpression(IType arrayType, params IExpression[] sizes)
            : this(arrayType, (sizes == null) ? 1 : sizes.Length, sizes)
        {
        }

        #region IMalleableCreateArrayExpression Members

        /// <summary>
        /// Returns the type of array to create.
        /// </summary>
        public IType ArrayType { get; set; }

        /// <summary>
        /// Returns the <see cref="Rank"/> of the array
        /// to create.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> used 
        /// to denote the size of the <see cref="MalleableCreateArrayExpression"/>
        /// </summary>
        public IMalleableExpressionCollection Sizes { get; private set; }

        #endregion

        public override ExpressionKind Type
        {
            get { return ExpressionKind.CreateArray; }
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        #region ICreateArrayExpression Members

        IExpressionCollection ICreateArrayExpression.Sizes
        {
            get { return this.Sizes; }
        }

        #endregion
    }
}
