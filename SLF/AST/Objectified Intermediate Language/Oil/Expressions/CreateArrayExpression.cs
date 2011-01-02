using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class CreateArrayExpression :
        MemberParentReferenceExpressionBase,
        ICreateArrayExpression
    {

        public CreateArrayExpression(IType arrayType, int rank, params IExpression[] sizes)
            : base()
        {
            this.ArrayType = arrayType;
            this.Rank = rank;
            this.Sizes = new MalleableExpressionCollection(sizes);
        }

        public CreateArrayExpression(IType arrayType, params IExpression[] sizes)
            : this(arrayType, 1, sizes)
        {
        }
        #region ICreateArrayExpression Members

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
        /// to denote the size of the <see cref="CreateArrayExpression"/>
        /// </summary>
        public IMalleableExpressionCollection Sizes { get; private set; }

        #endregion

        public override ExpressionKind Type
        {
            get { return ExpressionKinds.CreateArray; }
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
