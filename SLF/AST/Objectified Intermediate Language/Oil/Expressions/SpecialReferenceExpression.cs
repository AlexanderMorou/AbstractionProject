using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class SpecialReferenceExpression :
        MemberParentReferenceExpressionBase,
        IMalleableSpecialReferenceExpression
    {
        public SpecialReferenceExpression(SpecialReferenceKind referenceKind)
        {
            this.Kind = referenceKind;
        }


        #region ISpecialReferenceExpression Members

        public SpecialReferenceKind Kind { get; set; }

        #endregion

        #region IExpression Members

        public override ExpressionKinds Type
        {
            get
            {
                switch (Kind)
                {
                    case SpecialReferenceKind.Self:
                        return ExpressionKinds.SelfReference;
                    case SpecialReferenceKind.Base:
                        return ExpressionKinds.BaseReference;
                    case SpecialReferenceKind.This:
                        return ExpressionKinds.ThisReference;
                    default:
                        return ExpressionKinds.None;
                }
            }
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
