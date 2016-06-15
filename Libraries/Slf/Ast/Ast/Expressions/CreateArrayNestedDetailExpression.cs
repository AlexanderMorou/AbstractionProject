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
    public class MalleableCreateArrayNestedDetailExpression :
        IMalleableCreateArrayNestedDetailExpression
    {
        internal static readonly string[] splitRequirement = new[] { "\r\n" };
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

        public void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public TResult Accept<TResult, TContext>(ICommonExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        #endregion

        #region ISourceElement Members

        public Uri Location { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        #endregion

        public override string ToString()
        {
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
                return detailStringBuilder.ToString();
            }
            else
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
