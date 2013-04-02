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

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class MalleableCreateArrayNestedDetailExpression :
        IMalleableCreateArrayNestedDetailExpression
    {
        public MalleableCreateArrayNestedDetailExpression()
        {
            this.Details = new MalleableExpressionCollection();
        }
        #region ICreateArrayNestedDetailExpression Members

        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/>
        /// used to instantiate the array.
        /// </summary>
        public IMalleableExpressionCollection Details { get; private set; }

        #endregion

        #region IExpression Members

        public ExpressionKind Type
        {
            get { return ExpressionKind.CreateArrayNestedDetail; }
        }

        public void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        #endregion

        #region ISourceElement Members

        public string FileName { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        #endregion

        public override string ToString()
        {
            return string.Format("{{ {0} }}", string.Join<IExpression>(", ", this.Details));
        }

        #region ICreateArrayNestedDetailExpression Members

        IExpressionCollection ICreateArrayNestedDetailExpression.Details
        {
            get { return this.Details; }
        }

        #endregion
    }
}
