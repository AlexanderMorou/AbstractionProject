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
    public class CreateArrayDetailExpression :
        CreateArrayExpression,
        ICreateArrayDetailExpression
    {
        public CreateArrayDetailExpression(IType arrayType, params IExpression[] details)
            : this(arrayType, 1, details)
        {
        }

        public CreateArrayDetailExpression(IType arrayType, int rank, params IExpression[] details)
            : base(arrayType, rank)
        {
            this.Details = new MalleableExpressionCollection(details);
        }

        #region ICreateArrayDetailExpression Members

        public IMalleableExpressionCollection Details { get; private set; }
        #endregion

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
