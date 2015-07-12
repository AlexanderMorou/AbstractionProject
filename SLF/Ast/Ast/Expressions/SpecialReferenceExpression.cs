using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class SpecialReferenceExpression :
        MemberParentReferenceExpressionBase,
        IMalleableSpecialReferenceExpression
    {
        /// <summary>
        /// Creates a new <see cref="SpecialReferenceExpression"/> with the
        /// <paramref name="referenceKind"/> provided.
        /// </summary>
        /// <param name="referenceKind">The <see cref="SpecialReferenceKind"/>
        /// which denotes whether the reference refers to the active instance
        /// type, its base, or the active scope disregarding the virtual nature
        /// of the targets.</param>
        public SpecialReferenceExpression(SpecialReferenceKind referenceKind)
        {
            this.Kind = referenceKind;
        }


        #region ISpecialReferenceExpression Members

        /// <summary>
        /// Returns the kind of special reference the reference is.
        /// </summary>
        public SpecialReferenceKind Kind { get; set; }

        #endregion

        #region IExpression Members

        public override ExpressionKind Type
        {
            get
            {
                switch (Kind)
                {
                    case SpecialReferenceKind.Self:
                        return ExpressionKind.SelfReference;
                    case SpecialReferenceKind.Base:
                        return ExpressionKind.BaseReference;
                    case SpecialReferenceKind.This:
                        return ExpressionKind.ThisReference;
                    default:
                        return ExpressionKind.None;
                }
            }
        }

        public override TResult Visit<TContext, TResult>(IExpressionVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");
            visitor.Visit(this);
        }

        #endregion

        public override string ToString()
        {
            switch (this.Kind)
            {
                case SpecialReferenceKind.Self:
                    return "self";
                case SpecialReferenceKind.Base:
                    return "base";
                case SpecialReferenceKind.This:
                    return "this";
                default:
                    return string.Empty;
            }
        }
    }
}
