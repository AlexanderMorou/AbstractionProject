using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Numerics;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override string ToString()
        {
            //Exclude the sizes if the count is wrong.
            if (this.Details.Any(k => k != null && k is ICreateArrayNestedDetailExpression))
            {
                var detailStrings = (from d in this.Details
                                     let strVariation = d.ToString()
                                     from curStr in strVariation.Split(MalleableCreateArrayNestedDetailExpression.splitRequirement, StringSplitOptions.None)
                                     group curStr by strVariation).ToArray();
                StringBuilder detailStringBuilder = new StringBuilder();
                detailStringBuilder.AppendLine("{");
                bool first = true;
                foreach (var grouping in detailStrings)
                {
                    if (first)
                        first = false;
                    else
                        detailStringBuilder.AppendLine(",");
                    bool firstInGroup = true;
                    foreach (var groupItem in grouping)
                    {
                        if (firstInGroup)
                            firstInGroup = false;
                        else
                            detailStringBuilder.AppendLine();
                        detailStringBuilder.Append("    ");
                        detailStringBuilder.Append(groupItem);
                    }
                }
                detailStringBuilder.AppendLine();
                detailStringBuilder.Append("}");
                if (this.Rank != this.Sizes.Count)
                    return string.Format("new {0}[{1}] \n{2}", this.ArrayType, ','.Repeat(this.Rank - 1), detailStringBuilder.ToString());
                else
                    return string.Format("new {0}[{1}] \n{2}", this.ArrayType, string.Join<IExpression>(",", this.Sizes), detailStringBuilder.ToString());
            }
            else
                if (this.Rank != this.Sizes.Count)
                    return string.Format("new {0}[{1}] {{ {2} }}", this.ArrayType, ','.Repeat(this.Rank - 1), string.Join<IExpression>(", ", this.Details));
                else
                    return string.Format("new {0}[{1}] {{ {2} }}", this.ArrayType, string.Join<IExpression>(",", this.Sizes), string.Join<IExpression>(", ", this.Details));
        }

        #region ICreateArrayNestedDetailExpression Members

        IExpressionCollection ICreateArrayNestedDetailExpression.Details
        {
            get { return this.Details; }
        }

        #endregion
    }
}
